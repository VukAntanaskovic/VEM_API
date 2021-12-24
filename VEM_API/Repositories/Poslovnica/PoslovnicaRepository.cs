using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.Repositories;

namespace VEM_API.Repositories
{
    public class PoslovnicaRepository:IPoslovnicaRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public PoslovnicaRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }
        #region "Poslovnica"
        //Poslovnica

        public  IEnumerable<LPoslovnica> GetAllPoslovnica()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LPoslovnica> poslovnicas = new List<LPoslovnica>();
            foreach (DbModel.Poslovnica p in db.Poslovnicas)
            {
                poslovnicas.Add(
                    new LPoslovnica(
                        p.psl_sifra, 
                        p.psl_naziv, 
                        Convert.ToBoolean(p.psl_aktivna)));
            }

            return poslovnicas;
        }// uzimanje svih poslovnica

        public IEnumerable<LPoslovnica> GetPoslovnicaByParametar(string parametar)
        {
            int sifra;
            int.TryParse(parametar, out sifra);
            VEMTESTEntities db = new VEMTESTEntities();
            List<LPoslovnica> poslovnicas = new List<LPoslovnica>();
            var upit = from n in db.Poslovnicas
                       where n.psl_sifra == sifra || n.psl_naziv.StartsWith(parametar)
                       select n;
            foreach (DbModel.Poslovnica p in upit)
            {
                poslovnicas.Add(new LPoslovnica(p.psl_sifra, p.psl_naziv, Convert.ToBoolean(p.psl_aktivna)));
            }
            return poslovnicas;
        }//pretraga poslovnica

        public bool AddNewPoslovnica(LPoslovnica poslovnica)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (poslovnica != null)
            {
                try
                {
                    DbModel.Poslovnica pos = new DbModel.Poslovnica();
                    pos.psl_naziv = poslovnica.psl_naziv;
                    pos.psl_aktivna = true;
                    db.Poslovnicas.Add(pos);
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste kreirali poslovnicu";
                    _logProvider.AddToLog("PoslovnicaRepository.AddNewPoslovnica(LPoslovnica poslovnica)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom kreiranja poslovnice: " + e.Message;
                    _logProvider.AddToLog("PoslovnicaRepository.AddNewPoslovnica(LPoslovnica poslovnica)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
                _logProvider.AddToLog("PoslovnicaRepository.AddNewPoslovnica(LPoslovnica poslovnica)", ErrorResponseMessage, true);
                return false;
            }
        } // dodavanje nove poslovnice

        public  bool UpdatePoslovnica(int id, LPoslovnica psl)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                DbModel.Poslovnica poslovnica = db.Poslovnicas.SingleOrDefault(x => x.psl_sifra == id);
                poslovnica.psl_naziv = psl.psl_naziv;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili poslovnicu";
                _logProvider.AddToLog("PoslovnicaRepository.UpdatePoslovnica(int id, LPoslovnica psl)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene poslovnice: " + e.Message;
                _logProvider.AddToLog("PoslovnicaRepository.UpdatePoslovnica(int id, LPoslovnica psl)", ErrorResponseMessage, true);
                return false;
            }
        }// azuriranje naziva poslovnice

        public  bool ActivateDeactivatePoslovnica(int psl_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                DbModel.Poslovnica poslovnica = (from psl in db.Poslovnicas
                                         where psl.psl_sifra == psl_sifra
                                         select psl).FirstOrDefault();
                int zaliha = (from psl in db.Poslovnicas
                              join zl in db.Zaliha_artikla on psl.psl_sifra equals zl.psl_poslovnica
                              where psl.psl_sifra == psl_sifra && zl.art_dostupna_kolicina != 0 && zl.art_rezervisana_kolicina != 0
                              select psl).Count();
                if (zaliha == 0)
                {
                    if (poslovnica.psl_aktivna == true)
                    {
                        poslovnica.psl_aktivna = false;
                    }
                    else
                    {
                        poslovnica.psl_aktivna = true;
                    }

                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste izvrsili aktivaciju/deaktivaciju poslovnice " + poslovnica.psl_sifra;
                    _logProvider.AddToLog("PoslovnicaRepository.ActivateDeactivatePoslovnica(int psl_sifra)", OkResponseMessage, false);
                    return true;
                }//deaktiviranje i aktiviranje poslovnice koja se nalazi u zaliham artikala ali sadrzi kolicine svih artikala 0
                else
                {
                    /*Poslovnica poslovnica1 = (from n in db.Poslovnicas where n.psl_sifra == psl_sifra select n).SingleOrDefault();
                    if (poslovnica1.psl_aktivna == true)
                    {
                        poslovnica1.psl_aktivna = false;
                    }
                    else
                    {
                        poslovnica1.psl_aktivna = true;
                    }
                    db.SaveChanges();*/
                    ErrorResponseMessage = "Poslovnica se ne moze deaktivirati zbog aktivnih zaliha";
                    _logProvider.AddToLog("PoslovnicaRepository.ActivateDeactivatePoslovnica(int psl_sifra)", ErrorResponseMessage, true);
                    return false;
                }// deaktiviranje i aktiviranje poslovnica koje se ne nalaze na zalihama artikala
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom aktivacije/deaktivacije poslovnice: " + e.Message;
                _logProvider.AddToLog("PoslovnicaRepository.ActivateDeactivatePoslovnica(int psl_sifra)", ErrorResponseMessage, true);
                return false;
            }
        }//aktiviranje i deaktiviranje poslovnice
        //--Poslovnica
        #endregion "Poslovnica"
    }
}