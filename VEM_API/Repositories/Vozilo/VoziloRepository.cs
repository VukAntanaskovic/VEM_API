using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class VoziloRepository:IVoziloRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public VoziloRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }
        public IEnumerable<VoziloDTO> GetAllVozila()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<VoziloDTO> vozilo = new List<VoziloDTO>();

            foreach (var v in db.Voziloes)
            {
                vozilo.Add(new VoziloDTO
                {
                    vzl_sifra = v.vzl_sifra,
                    vzl_marka = v.vzl_marka,
                    vzl_model = v.vzl_model,
                    vzl_registracija = v.vzl_registracija,
                    vzl_slika = v.vzl_slika,
                    vzl_aktivno = v.vzl_aktivno
                });
            }
            return vozilo;
        }

        public IEnumerable<VoziloDTO> GetVoziloByParametar(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<VoziloDTO> vozilo = new List<VoziloDTO>();
            int sifra;
            int.TryParse(parametar, out sifra);

            foreach (var v in db.Voziloes.Where(x => (x.vzl_sifra == sifra) || (x.vzl_registracija.Equals(parametar) || x.vzl_model.StartsWith(parametar) || x.vzl_marka.StartsWith(parametar))))
            {
                vozilo.Add(new VoziloDTO
                {
                    vzl_sifra = v.vzl_sifra,
                    vzl_marka = v.vzl_marka,
                    vzl_model = v.vzl_model,
                    vzl_registracija = v.vzl_registracija,
                    vzl_slika = v.vzl_slika,
                    vzl_aktivno = v.vzl_aktivno
                });
            }
            return vozilo;
        }

        public bool CreateNewVozilo(VoziloDTO vozilo)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (vozilo != null)
            {
                try
                {
                    Vozilo v = new Vozilo();
                    v.vzl_marka = vozilo.vzl_marka;
                    v.vzl_model = vozilo.vzl_model;
                    v.vzl_registracija = vozilo.vzl_registracija;
                    v.vzl_slika = vozilo.vzl_slika;
                    v.vzl_aktivno = true;
                    db.Voziloes.Add(v);
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno uneto vozilo: " + v.vzl_sifra;
                    _logProvider.AddToLog("ApiHelper.CreateNewVozilo(LVozilo vozilo)", OkResponseMessage, false);
                    return true;

                }
                catch (Exception ex)
                {
                    ErrorResponseMessage = "Greska prilikom unosa vozila: " + ex.Message;
                    _logProvider.AddToLog("ApiHelper.CreateNewVozilo(LVozilo vozilo)", OkResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci prilikom unosa vozila";
                _logProvider.AddToLog("ApiHelper.CreateNewVozilo(LVozilo vozilo)", OkResponseMessage, true);
                return false;
            }
        }

        public bool UpdateVozilo(int id, VoziloDTO vozilo)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Vozilo vzl = db.Voziloes.SingleOrDefault(x => x.vzl_sifra == id);
                vzl.vzl_aktivno = vozilo.vzl_aktivno;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili aktivnost vozila: " + vzl.vzl_sifra;
                _logProvider.AddToLog("ApiHelper.UpdateVozilo(int id, LVozilo vozilo)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene vozila: " + e.Message;
                _logProvider.AddToLog("ApiHelper.UpdateVozilo(int id, LVozilo vozilo)", ErrorResponseMessage, true);
                return false;
            }
        }
    }
}