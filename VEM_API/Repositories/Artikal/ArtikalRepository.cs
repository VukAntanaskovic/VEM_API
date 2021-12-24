using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Repositories;
using VEM_API.Models;
namespace VEM_API.Repositories
{
    public class ArtikalRepository:IArtikalRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public ArtikalRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }
        #region "Artikal"
        //Artikal
        public IEnumerable<ArtikalDTO> GetAllArtikal()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            _entity.Refresh();
            List<ArtikalDTO> result = null;

            try
            {
                result = (from art in db.Artikals
                          join dob in db.Komitents      on art.kom_dobavljac        equals dob.kom_sifra
                          join jed in db.Jedinica_Mere  on art.art_jedinica_mere    equals jed.jed_sifra

                          select new ArtikalDTO()
                          {
                              art_sifra = art.art_sifra,
                              art_tip = art.art_tip,
                              art_naziv = art.art_naziv,
                              art_ean = art.art_naziv,
                              art_proizvodjac = art.art_proizvodjac,
                              art_aktivan = art.art_aktivan,
                              dobavljac = new KomitentDTO()
                              {
                                  kom_sifra = dob.kom_sifra,
                                  kom_adresa = dob.kom_adresa,
                                  kom_dobavljac = dob.kom_dobavljac,
                                  kom_grad = dob.kom_grad,
                                  kom_aktivan = dob.kom_aktivan,
                                  kom_MBR = dob.kom_MBR,
                                  kom_naziv = dob.kom_naziv,
                                  kom_PIB = dob.kom_PIB,
                                  kom_ptt = dob.kom_ptt
                              },
                              jedinica_mere = new JedinicaMereDTO()
                              {
                                  jed_sifra = jed.jed_sifra,
                                  jed_naziv = jed.jed_naziv,
                                  jed_kratki_naziv = jed.jed_kratki_naziv
                              }

                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllArtikal()", ex.Message, true);
            }

            return result;
        }//uzimanje svih artikala iz baze

        public IEnumerable<ArtikalDTO> GetArtikalBySifra(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            _entity.Refresh();
            List<ArtikalDTO> result = null;
            int sifra;
            int.TryParse(parametar, out sifra);

            try
            {
                result = (from art in db.Artikals
                          join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                          join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra

                          where
                           art.art_sifra == sifra ||
                           art.art_proizvodjac.Contains(parametar) ||
                           art.art_naziv.Contains(parametar) ||
                           art.art_ean.Equals(parametar)

                          select new ArtikalDTO()
                          {
                              art_sifra = art.art_sifra,
                              art_tip = art.art_tip,
                              art_naziv = art.art_naziv,
                              art_ean = art.art_naziv,
                              art_proizvodjac = art.art_proizvodjac,
                              art_aktivan = art.art_aktivan,
                              dobavljac = new KomitentDTO()
                              {
                                  kom_sifra = dob.kom_sifra,
                                  kom_adresa = dob.kom_adresa,
                                  kom_dobavljac = dob.kom_dobavljac,
                                  kom_grad = dob.kom_grad,
                                  kom_aktivan = dob.kom_aktivan,
                                  kom_MBR = dob.kom_MBR,
                                  kom_naziv = dob.kom_naziv,
                                  kom_PIB = dob.kom_PIB,
                                  kom_ptt = dob.kom_ptt
                              },
                              jedinica_mere = new JedinicaMereDTO()
                              {
                                  jed_sifra = jed.jed_sifra,
                                  jed_naziv = jed.jed_naziv,
                                  jed_kratki_naziv = jed.jed_kratki_naziv
                              }

                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllArtikal()", ex.Message, true);
            }

            return result;

        }//pretraga artikla prema parametru

        public bool CreateNewArtikal(ArtikalDTO artikal)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (artikal != null)
            {
                try
                {
                    Artikal a = new Artikal();
                    a.art_naziv = artikal.art_naziv;
                    a.art_ean = artikal.art_ean;
                    a.art_naziv = artikal.art_naziv;
                    a.art_proizvodjac = artikal.art_proizvodjac;
                    a.art_tip = artikal.art_tip;
                    a.art_jedinica_mere = artikal.jedinica_mere.jed_sifra;
                    a.kom_dobavljac = artikal.dobavljac.kom_sifra;
                    a.art_aktivan = true;
                    db.Artikals.Add(a);
                    //AddToZaliha(a.art_sifra, 1000);
                    db.SaveChanges();
                    _entity.Refresh();
                    OkResponseMessage = "Uspesno ste kreirali novi artikal";
                    _logProvider.AddToLog("ArtikalRepository.CreateNewArtikal(LArtikal artikal)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom kreiranja artikla: " + e.Message;
                    _logProvider.AddToLog("ArtikalRepository.CreateNewArtikal(LArtikal artikal)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
                _logProvider.AddToLog("ArtikalRepository.CreateNewArtikal(LArtikal artikal)", ErrorResponseMessage, true);
                return false;
            }
        }//unos novog artikla i dodavanje na zalihu sa sifrom default poslovnice

        public void AddToZaliha(int art_sifra, int psl_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Zaliha_artikla zl = new Zaliha_artikla();
                zl.art_sifra = art_sifra;
                zl.art_dostupna_kolicina = 0;
                zl.art_rezervisana_kolicina = 0;
                zl.psl_poslovnica = psl_sifra;
                db.Zaliha_artikla.Add(zl);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }//dodavanje artikla na zalihu na poslovnicu

        public bool UpdateArtikal(int art_sifra, string art_naziv, string art_proizvodjac, string art_ean, int kom_dobavljac, string art_tip, bool? art_aktivan)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                DbModel.Artikal artikal = db.Artikals.SingleOrDefault(x => x.art_sifra == art_sifra);
                if (art_naziv != null)
                    artikal.art_naziv = art_naziv;
                if (art_proizvodjac != null)
                    artikal.art_proizvodjac = art_proizvodjac;
                if (art_ean != null)
                    artikal.art_ean = art_ean;
                if (kom_dobavljac.ToString() != null)
                    artikal.kom_dobavljac = kom_dobavljac;
                if (art_tip != null)
                    artikal.art_tip = art_tip;
                artikal.art_aktivan = art_aktivan;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili artikal " + artikal.art_sifra;
                _logProvider.AddToLog("ApiHelper.UpdateArtikal(int art_sifra, string art_naziv, string art_proizvodjac, string art_ean, int kom_dobavljac, string art_tip, bool art_aktivan)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene artikla: " + e.Message;
                _logProvider.AddToLog("ApiHelper.UpdateArtikal(int art_sifra, string art_naziv, string art_proizvodjac, string art_ean, int kom_dobavljac, string art_tip, bool art_aktivan)", ErrorResponseMessage, true);
                return false;
            }
        }//azuriranje podataka o artiklu

        public bool ActivateDeactivateArtikal(int art_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                DbModel.Artikal artikal = (
                    from ar in db.Artikals
                    join zl in db.Zaliha_artikla on ar.art_sifra equals zl.art_sifra

                    where 
                    ar.art_sifra == art_sifra       && 
                    zl.art_dostupna_kolicina == 0   && 
                    zl.art_rezervisana_kolicina == 0

                    select ar).SingleOrDefault();
                if (artikal != null)
                {
                    if (artikal.art_aktivan == true)
                    {
                        artikal.art_aktivan = false;
                    }
                    else
                    {
                        artikal.art_aktivan = true;
                    }

                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste realizovali zahtev aktivacije/deaktivacije artikla";
                    _logProvider.AddToLog("ArtikalRepository.ActivateDeactivateArtikal(int art_sifra)", OkResponseMessage, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresno prosledjeni podaci";
                    _logProvider.AddToLog("ArtikalRepository.ActivateDeactivateArtikal(int art_sifra)", ErrorResponseMessage, true); 
                    return false;
                }
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom realizovanja zahteva aktivacija/deaktivacija artikla: " + e.Message;
                _logProvider.AddToLog("ArtikalRepository.ActivateDeactivateArtikal(int art_sifra)", ErrorResponseMessage, true); 
                return false;
            }
        }//aktiviranje i deaktiviranje artikla samo ukoliko je kolicina artikla na svim poslovnicama 0

        //--Artikal
        #endregion "Artikal"
    }
}