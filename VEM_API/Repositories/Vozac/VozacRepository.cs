using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class VozacRepository:IVozacRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public VozacRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        public IEnumerable<VozaciDTO> GetAllVozace()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<VozaciDTO> result = null;

            try
            {
                result = (from vzc in db.Vozacs
                          join kor in db.Korisnik_Sistema on vzc.kor_sifra equals kor.kor_sifra
                          join vzl in db.Voziloes on vzc.vzl_sifra equals vzl.vzl_sifra

                          select new VozaciDTO()
                          {
                              vzc_sifra = vzc.vzc_sifra,

                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },

                              vozilo = new VoziloDTO()
                              {
                                  vzl_sifra = vzl.vzl_sifra,
                                  vzl_model = vzl.vzl_model,
                                  vzl_marka = vzl.vzl_marka,
                                  vzl_slika = vzl.vzl_slika,
                                  vzl_registracija = vzl.vzl_registracija
                              }
                          }).ToList();
            }
            catch(Exception ex)
            {
                _logProvider.AddToLog("GetAllVozace()", ex.Message, true);
            }

            return result;
        }
        public IEnumerable<VozaciDTO> GetVozaceBySifra(int sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<VozaciDTO> result = null;

            try
            {
                result = (from vzc in db.Vozacs
                          join kor in db.Korisnik_Sistema on vzc.kor_sifra equals kor.kor_sifra
                          join vzl in db.Voziloes on vzc.vzl_sifra equals vzl.vzl_sifra

                          where vzc.vzc_sifra == sifra

                          select new VozaciDTO()
                          {
                              vzc_sifra = vzc.vzc_sifra,

                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },

                              vozilo = new VoziloDTO()
                              {
                                  vzl_sifra = vzl.vzl_sifra,
                                  vzl_model = vzl.vzl_model,
                                  vzl_marka = vzl.vzl_marka,
                                  vzl_slika = vzl.vzl_slika,
                                  vzl_registracija = vzl.vzl_registracija
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetVozaceBySifra(sifra: {sifra})", ex.Message, true);
            }

            return result;
        }

        public bool CreateNewVozac(VozaciDTO vozac)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Vozac vzc = new Vozac()
                {
                    kor_sifra = vozac.korisnik.kor_sifra,
                    vzl_sifra = vozac.vozilo.vzl_sifra
                };
                db.Vozacs.Add(vzc);
                db.SaveChanges();
                OkResponseMessage = "Uspesno unet vozac: " + vzc.vzc_sifra;
                _logProvider.AddToLog("ApiHelper.CreateNewVozac(LVozaci vozac)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom unosa vozaca: " + e.Message;
                _logProvider.AddToLog("ApiHelper.CreateNewVozac(LVozaci vozac)", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool UpdateVozac(int id, VozaciDTO vozac)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Vozac vzc = db.Vozacs.SingleOrDefault(x => x.vzc_sifra == id);
                vzc.vzl_sifra = vozac.vozilo.vzl_sifra;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili vozilo za vozaca: " + vzc.vzc_sifra;
                _logProvider.AddToLog("ApiHelper.UpdateVozac(int id, LVozaci vozac)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene vozila: " + e.Message;
                _logProvider.AddToLog("ApiHelper.UpdateVozac(int id, LVozaci vozac)", ErrorResponseMessage, true);
                return false;
            }
        }
    }
}