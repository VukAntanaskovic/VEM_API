using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.DbModel;



namespace VEM_API.Repositories
{
    public class RafRepository:IRafRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public RafRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        public bool AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            Zaliha_artikla zal = (from n in db.Zaliha_artikla
                                  where n.art_sifra == art_sifra && n.psl_poslovnica == psl_sifra
                                  select n).FirstOrDefault();
            try
            {
                zal.raf_sifra = rad_sifra;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste zalihu za artikal " + zal.art_sifra + " na raf: " + zal.raf_sifra;
                _logProvider.AddToLog("AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom dodavanja zalihe na raf: " + e.Message;
                _logProvider.AddToLog("AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool CreateNewRaf(RafDTO raf)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (raf != null)
            {
                try
                {
                    Raf r = new Raf();
                    r.raf_lokacija = raf.raf_lokacija;
                    r.psl_poslovnica = raf.psl_poslovnica;
                    db.Rafs.Add(r);
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste kreirali raf " + r.raf_sifra;
                    _logProvider.AddToLog("CreateNewRaf(LRaf raf)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom kreiranja rafa: " + e.Message;
                    _logProvider.AddToLog("CreateNewRaf(LRaf raf)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
                _logProvider.AddToLog("CreateNewRaf(LRaf raf)", ErrorResponseMessage, true);
                return false;
            }

        }

        public IEnumerable<ZalihaArtiklaDTO> GetAllArtikalInRaf(int raf_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<ZalihaArtiklaDTO> result = null;

            try
            {
                result = (from zal in db.Zaliha_artikla
                          join art in db.Artikals on zal.art_sifra equals art.art_sifra
                          join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                          join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                          join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                          join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra

                          where zal.raf_sifra == raf_sifra

                          select new ZalihaArtiklaDTO() 
                          { 
                             zal_sifra = zal.zal_sifra,
                             art_dostupna_kolicina = zal.art_dostupna_kolicina,
                             art_rezervisana_kolicina = zal.art_rezervisana_kolicina,

                             artikal = new ArtikalDTO()
                             {
                                 art_sifra = art.art_sifra,
                                 art_naziv = art.art_naziv,
                                 art_proizvodjac = art.art_proizvodjac,
                                 art_tip = art.art_tip,
                                 art_ean = art.art_ean,

                                 jedinica_mere = new JedinicaMereDTO()
                                 {
                                     jed_sifra = jed.jed_sifra,
                                     jed_naziv = jed.jed_naziv,
                                     jed_kratki_naziv = jed.jed_kratki_naziv
                                 },

                                 dobavljac = new KomitentDTO()
                                 {
                                     kom_sifra = dob.kom_sifra,
                                     kom_naziv = dob.kom_naziv
                                 }
                             },

                             poslovnica = new PoslovnicaDTO()
                             {
                                 psl_sifra = psl.psl_sifra,
                                 psl_naziv = psl.psl_naziv
                             },

                             raf = new RafDTO()
                             {
                                 raf_sifra = raf.raf_sifra,
                                 raf_lokacija = raf.raf_lokacija,
                                 psl_poslovnica = raf.psl_poslovnica
                             }
                          }).ToList();
            }
            catch(Exception ex)
            {
                _logProvider.AddToLog($"GetAllArtikalInRaf(raf_sifra: {raf_sifra})", ex.Message, true);
            }

            return result;
        }

        public IEnumerable<RafDTO> GetAllRaf()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<RafDTO> rafovi = new List<RafDTO>();
            var raf = from n in db.Rafs
                      orderby n.psl_poslovnica
                      select n;
            foreach (var r in raf)
            {
                rafovi.Add(new RafDTO(r.raf_sifra, r.raf_lokacija, Convert.ToInt32(r.psl_poslovnica)));
            }
            return rafovi;
        }

        public bool UpdateRaf(int id, RafDTO lokacija)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Raf raf = (from rf in db.Rafs
                           where rf.raf_sifra == id
                           select rf).FirstOrDefault();
                if (raf != null)
                {
                    raf.raf_lokacija = lokacija.raf_lokacija;
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste izmenili raf " + raf.raf_sifra;
                    _logProvider.AddToLog("UpdateRaf(int id, LRaf lokacija)", OkResponseMessage, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresno proslejdeni podaci";
                    _logProvider.AddToLog("UpdateRaf(int id, LRaf lokacija)", ErrorResponseMessage, true);
                    return false;
                }
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene rafa: " + e.Message;
                _logProvider.AddToLog("UpdateRaf(int id, LRaf lokacija)", ErrorResponseMessage, true);
                return false;
            }

        }

        public IEnumerable<RafDTO> GetAllRafInPoslovnica(int id, string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<RafDTO> rafovi = new List<RafDTO>();
            if (string.IsNullOrEmpty(parametar))
            {

                foreach (var r in db.Rafs.Where(x => x.psl_poslovnica == id || x.psl_poslovnica == null))
                {
                    rafovi.Add(new RafDTO(r.raf_sifra, r.raf_lokacija, Convert.ToInt32(r.psl_poslovnica)));
                }

                return rafovi;
            }
            else
            {
                int sifraPsl = 0;
                int.TryParse(parametar, out sifraPsl);
                foreach (var r in db.Rafs.Where(x => (x.psl_poslovnica == id || x.psl_poslovnica == null) && (x.raf_sifra == sifraPsl || x.raf_lokacija.StartsWith(parametar))))
                {
                    rafovi.Add(new RafDTO(r.raf_sifra, r.raf_lokacija, Convert.ToInt32(r.psl_poslovnica)));
                }

                return rafovi;

            }
        }
    }
}