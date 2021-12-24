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

        public IEnumerable<LVozaci> GetAllVozace()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LVozaci> vozaci = new List<LVozaci>();
            var upit = from vzc in db.Vozacs
                       join kor in db.Korisnik_Sistema on vzc.kor_sifra equals kor.kor_sifra
                       join vzl in db.Voziloes on vzc.vzl_sifra equals vzl.vzl_sifra
                       select new { vzc, kor, vzl };

            foreach (var v in upit)
            {
                vozaci.Add(new LVozaci()
                {
                    vzc_sifra = v.vzc.vzc_sifra,
                    korisnik = new LKorisnik(v.kor.kor_sifra, v.kor.kor_ime, v.kor.kor_prezime, v.kor.kor_telefon, v.kor.kor_username, null, v.kor.atr_autorizacija, v.kor.psl_poslovnica_rada.ToString()),
                    vozilo = new LVozilo()
                    {
                        vzl_sifra = v.vzl.vzl_sifra,
                        vzl_marka = v.vzl.vzl_marka,
                        vzl_model = v.vzl.vzl_model,
                        vzl_registracija = v.vzl.vzl_registracija,
                        vzl_aktivno = v.vzl.vzl_aktivno,
                        vzl_slika = v.vzl.vzl_slika
                    }
                });
            }

            return vozaci;
        }
        public IEnumerable<LVozaci> GetVozaceBySifra(int sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LVozaci> vozaci = new List<LVozaci>();
            var upit = from vzc in db.Vozacs
                       join kor in db.Korisnik_Sistema on vzc.kor_sifra equals kor.kor_sifra
                       join vzl in db.Voziloes on vzc.vzl_sifra equals vzl.vzl_sifra
                       where vzc.vzc_sifra == sifra
                       select new { vzc, kor, vzl };

            foreach (var v in upit)
            {
                vozaci.Add(new LVozaci()
                {
                    vzc_sifra = v.vzc.vzc_sifra,
                    korisnik = new LKorisnik(v.kor.kor_sifra, v.kor.kor_ime, v.kor.kor_prezime, v.kor.kor_telefon, v.kor.kor_username, null, v.kor.atr_autorizacija, v.kor.psl_poslovnica_rada.ToString()),
                    vozilo = new LVozilo()
                    {
                        vzl_sifra = v.vzl.vzl_sifra,
                        vzl_marka = v.vzl.vzl_marka,
                        vzl_model = v.vzl.vzl_model,
                        vzl_registracija = v.vzl.vzl_registracija,
                        vzl_aktivno = v.vzl.vzl_aktivno,
                        vzl_slika = v.vzl.vzl_slika
                    }
                });
            }

            return vozaci;
        }

        public bool CreateNewVozac(LVozaci vozac)
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

        public bool UpdateVozac(int id, LVozaci vozac)
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