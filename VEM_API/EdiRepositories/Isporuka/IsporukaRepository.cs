using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.Repositories;

namespace VEM_API.EdiRepositories
{
    public class IsporukaRepository : IIsporukaRepository
    {

        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public IsporukaRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        #region "Isporuka"
        public void CreateNewIsporuka(int? primalac, DateTime? datum, int? veza)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Isporuka isporuka = new Isporuka()
                {
                    pri_primalac = primalac,
                    isp_datum = datum,
                    dok_veza = veza,
                    sts_status = 5
                };
                db.Isporukas.Add(isporuka);

                List<Stavke_Dokumenta> stavke = null;
                stavke = (from stv in db.Stavke_Dokumenta
                          where stv.dok_broj_dokumenta == veza
                          select stv).ToList();

                foreach (Stavke_Dokumenta s in stavke)
                {
                    Stavke_Isporuke stavke_isporuke = new Stavke_Isporuke()
                    {
                        isp_broj = isporuka.isp_broj,
                        art_sifra = s.stv_sifra_artikla,
                        art_kolicina = s.stv_kolicina
                    };
                    db.Stavke_Isporuke.Add(stavke_isporuke);
                }

                db.SaveChanges();
                _logProvider.AddToLog("CreateNewIsporuka(int primalac, DateTime datum, int veza)", "Uspesno uneta isporuka " + isporuka.isp_broj, false);

            }
            catch (Exception ex)
            {
                _logProvider.AddToLog("CreateNewIsporuka(int primalac, DateTime datum, int veza)", "Greska prilikom unosa isporuke: " + ex.Message, true);
            }
        }
        public IEnumerable<LIsporuka> GetAllIsporukaByStatus(int status)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LIsporuka> isporuke = new List<LIsporuka>();

            if (status == 0)
            {
                var query =
                        from isp in db.Isporukas
                        join pri in db.Krajnji_Primalac on isp.pri_primalac equals pri.pri_sifra
                        join sts in db.Status_Isporuke on isp.sts_status equals sts.sts_sifra

                        orderby pri.pri_ptt ascending

                        select new
                        {
                            isp,
                            pri,
                            sts
                        };

                foreach (var isp in query)
                {
                    isporuke.Add(
                        new LIsporuka
                        {
                            isp_broj = isp.isp.isp_broj,
                            dok_veza = isp.isp.dok_veza,
                            isp_datum = isp.isp.isp_datum,

                            pri_primalac = new LPrimalac
                            (
                                isp.pri.pri_sifra,
                                isp.pri.pri_ime_prezime,
                                isp.pri.pri_adresa,
                                isp.pri.pri_adresa_broj,
                                isp.pri.pri_grad,
                                isp.pri.pri_ptt.ToString(),
                                isp.pri.pri_telefon,
                                isp.pri.pri_email
                                ),

                            sts_status = new LStatusIsporuke
                            {
                                sts_sifra = isp.sts.sts_sifra,
                                sts_naziv = isp.sts.sts_naziv
                            }
                        });
                }

                return isporuke;
            }
            else
            {

                var sveIsporuke =
                            from isp in db.Isporukas
                            join pri in db.Krajnji_Primalac on isp.pri_primalac equals pri.pri_sifra
                            join sts in db.Status_Isporuke on isp.sts_status equals sts.sts_sifra

                            where isp.sts_status == status
                            orderby pri.pri_ptt ascending

                            select new
                            {
                                isp,
                                pri,
                                sts
                            };

                foreach (var isp in sveIsporuke)
                {
                    isporuke.Add(
                        new LIsporuka
                        {
                            isp_broj = isp.isp.isp_broj,
                            dok_veza = isp.isp.dok_veza,
                            isp_datum = isp.isp.isp_datum,

                            pri_primalac = new LPrimalac
                            (
                                isp.pri.pri_sifra,
                                isp.pri.pri_ime_prezime,
                                isp.pri.pri_adresa,
                                isp.pri.pri_adresa_broj,
                                isp.pri.pri_grad,
                                isp.pri.pri_ptt.ToString(),
                                isp.pri.pri_telefon,
                                isp.pri.pri_email
                                ),

                            sts_status = new LStatusIsporuke
                            {
                                sts_sifra = isp.sts.sts_sifra,
                                sts_naziv = isp.sts.sts_naziv
                            }
                        });
                }

                return isporuke;
            }
        }
        public IEnumerable<LIsporuka> CheckStatusIsporuke(int? isporuka, DateTime? datum)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LIsporuka> isporuke = new List<LIsporuka>();

            var sveIsporuke =
                        from isp in db.Isporukas
                        join pri in db.Krajnji_Primalac on isp.pri_primalac equals pri.pri_sifra
                        join sts in db.Status_Isporuke on isp.sts_status equals sts.sts_sifra

                        where isp.isp_broj == isporuka || isp.isp_datum == datum
                        orderby isp.isp_datum descending

                        select new
                        {
                            isp,
                            pri,
                            sts
                        };

            foreach (var isp in sveIsporuke)
            {
                isporuke.Add(
                    new LIsporuka
                    {
                        isp_broj = isp.isp.isp_broj,
                        dok_veza = isp.isp.dok_veza,
                        isp_datum = isp.isp.isp_datum,

                        pri_primalac = new LPrimalac
                        (
                            isp.pri.pri_sifra,
                            isp.pri.pri_ime_prezime,
                            isp.pri.pri_adresa,
                            isp.pri.pri_adresa_broj,
                            isp.pri.pri_grad,
                            isp.pri.pri_ptt.ToString(),
                            isp.pri.pri_telefon,
                            isp.pri.pri_email
                            ),

                        sts_status = new LStatusIsporuke
                        {
                            sts_sifra = isp.sts.sts_sifra,
                            sts_naziv = isp.sts.sts_naziv
                        }
                    });
            }

            return isporuke;
        }
        public void ChangeStatusIsporuke(int isporuka, int status)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Isporuka isp =
                    (
                    from i in db.Isporukas
                    where i.isp_broj == isporuka
                    select i
                    ).FirstOrDefault();

                isp.sts_status = status;
                db.SaveChanges();
                _logProvider.AddToLog("ChangeStatusIsporuke(int isporuka, int status)", "Uspesno promenjen status isporuke: " + isp.isp_broj, false);
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog("ChangeStatusIsporuke(int isporuka, int status)", "Greska prilikom promene statusa isporuke: " + ex.Message, false);
            }

        }
        public bool ResetIsporuka(int isporuka, int tl)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {

                Stavke_Tovarnog_Lista stavke =
                    (
                    from stv in db.Stavke_Tovarnog_Lista
                    where stv.isp_broj == isporuka && stv.tl_broj == tl
                    select stv
                    ).FirstOrDefault();
                db.Stavke_Tovarnog_Lista.Remove(stavke);
                db.SaveChanges();
                ChangeStatusIsporuke(isporuka, 5);
                OkResponseMessage = "Uspesno resetovana isporuka: " + isporuka;
                _logProvider.AddToLog("ResetIsporuka(int isporuka, int tl)", OkResponseMessage, false);
                return true;
            }
            catch (Exception ex)
            {
                ErrorResponseMessage = "Greska prilikom resetovanja isporuke: " + ex.Message;
                _logProvider.AddToLog("ResetIsporuka(int isporuka, int tl)", ErrorResponseMessage, true);
                return false;
            }
        }
        public bool StatusRequest(int isporuka, string status)
        {
            if (status == "isporuceno")
            {
                ChangeStatusIsporuke(isporuka, 2);
                OkResponseMessage = "Uspesno promenjen status isporuke";
                return true;
            }
            else if (status == "odlozeno")
            {
                ChangeStatusIsporuke(isporuka, 3);
                OkResponseMessage = "Uspesno promenjen status isporuke";
                return true;
            }
            else if (status == "otkazano")
            {
                ChangeStatusIsporuke(isporuka, 4);
                OkResponseMessage = "Uspesno promenjen status isporuke";
                return true;
            }
            else
            {
                ErrorResponseMessage = "Greska prilikom promene statusa";
                return false;
            }
        }
        #endregion "Isporuka"

        #region "Tovarni list"
        public bool CreateNewTL(Tovarni_List tovarni_List)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Tovarni_List tl = new Tovarni_List()
                {
                    tl_datum_kreiranja = DateTime.Now,
                    vzc_vozac = tovarni_List.vzc_vozac,
                    tl_zatvoren = false
                };
                db.Tovarni_List.Add(tl);
                db.SaveChanges();
                OkResponseMessage = "Uspesno kreiran tovarni list: " + tl.tl_broj;
                _logProvider.AddToLog("CreateNewTL(Tovarni_List tovarni_List)", OkResponseMessage, false);
                return true;
            }
            catch (Exception ex)
            {
                ErrorResponseMessage = "Greska prilikom kreiranja TL dokumenta: " + ex.Message;
                _logProvider.AddToLog("CreateNewTL(Tovarni_List tovarni_List)", ErrorResponseMessage, true);
                return false;
            }
        }
        public IEnumerable<LTovarniList> GetAllTovarniList()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LTovarniList> tovarniListovi = new List<LTovarniList>();

            var tvrl =
                 from tl in db.Tovarni_List
                 join vzc in db.Vozacs on tl.vzc_vozac equals vzc.vzc_sifra
                 join kor in db.Korisnik_Sistema on vzc.kor_sifra equals kor.kor_sifra
                 join vzl in db.Voziloes on vzc.vzl_sifra equals vzl.vzl_sifra

                 where tl.tl_zatvoren == false

                 select new
                 {
                     tl,
                     vzc,
                     kor,
                     vzl
                 };

            foreach (var t in tvrl)
            {
                tovarniListovi.Add(
                    new LTovarniList
                    {
                        tl_broj = t.tl.tl_broj,
                        tl_datum = t.tl.tl_datum_kreiranja,
                        vzc_vozac = new LVozaci
                        {
                            vzc_sifra = t.vzc.vzc_sifra,
                            korisnik = new LKorisnik(
                                t.kor.kor_sifra,
                                t.kor.kor_ime,
                                t.kor.kor_prezime,
                                t.kor.kor_telefon,
                                t.kor.kor_username,
                                null,
                                t.kor.atr_autorizacija,
                                null
                                ),
                            vozilo = new LVozilo
                            {
                                vzl_sifra = t.vzl.vzl_sifra,
                                vzl_marka = t.vzl.vzl_marka,
                                vzl_model = t.vzl.vzl_model,
                                vzl_registracija = t.vzl.vzl_registracija,
                                vzl_slika = t.vzl.vzl_slika,
                                vzl_aktivno = t.vzl.vzl_aktivno
                            }

                        }
                    }

                    );
            }

            return tovarniListovi;
        }
        public bool AddIsporukaToTL(int isporuka, int tl)
        {
            try
            {
                VEMTESTEntities db = new VEMTESTEntities();
                Stavke_Tovarnog_Lista stavke = new Stavke_Tovarnog_Lista()
                {
                    isp_broj = isporuka,
                    tl_broj = tl
                };

                db.Stavke_Tovarnog_Lista.Add(stavke);
                db.SaveChanges();
                ChangeStatusIsporuke(isporuka, 1);
                OkResponseMessage = "Uspesno kreirana stavka tovarnog lista";
                _logProvider.AddToLog("AddIsporukaToTL(int isporuka, int tl)", OkResponseMessage, false);
                return true;
            }
            catch (Exception ex)
            {
                ErrorResponseMessage = "Greska prilikom kreiranja stavke TL: " + ex.Message;
                _logProvider.AddToLog("AddIsporukaToTL(int isporuka, int tl)", ErrorResponseMessage, true);
                return false;
            }
        }
        #endregion "Tovarni list"

        #region "Stavke tovarnog lista"
        public IEnumerable<LStavkeTovarnogLista> GetAllStavkeTL(int tl)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LStavkeTovarnogLista> stavke = new List<LStavkeTovarnogLista>();

            var stv =
                from st in db.Stavke_Tovarnog_Lista
                join isp in db.Isporukas on st.isp_broj equals isp.isp_broj
                join sts in db.Status_Isporuke on isp.sts_status equals sts.sts_sifra

                where st.tl_broj == tl

                select new
                {
                    st,
                    isp,
                    sts
                };

            foreach (var s in stv)
            {
                stavke.Add(
                    new LStavkeTovarnogLista
                    {
                        tl_broj = Convert.ToInt32(s.st.tl_broj),
                        isporuka = new LIsporuka
                        {
                            isp_broj = s.isp.isp_broj,
                            isp_datum = s.isp.isp_datum,
                            sts_status = new LStatusIsporuke
                            {
                                sts_sifra = s.sts.sts_sifra,
                                sts_naziv = s.sts.sts_naziv
                            }
                        }
                    });
            }

            return stavke;

        }
        #endregion "Stavke tovarnog lista"
    }
}