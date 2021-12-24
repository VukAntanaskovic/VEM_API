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
    public class DokumentRepository:IDokumentRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        private readonly IIsporukaRepository _isporukaRepository;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public DokumentRepository(ILogProvider logProvider, IEntityRepository entity, IIsporukaRepository isporukaRepository)
        {
            _logProvider = logProvider;
            _entity = entity;
            _isporukaRepository = isporukaRepository;
        }

        #region "Tip dokumenta"
        public IEnumerable<TipDokumentaDTO> GetAllTipDokumenta()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<TipDokumentaDTO> result = null;

            try
            {
                result = (from tip in db.Tip_Dokumenta

                          select new TipDokumentaDTO()
                          {
                              tipdok_sifra = tip.tipdok_sifra,
                              tipdok_naziv = tip.tipdok_naziv,
                              tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllTipDokumenta()", ex.Message, true);
            }

            return result;
        }
        #endregion "Tip dokumenta"

        #region "Dokumenti"
        public IEnumerable<DokumentDTO> GetAllDokument()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<DokumentDTO> result = null;

            try
            {

                result =  (from dok in db.Dokuments
                           join tip in db.Tip_Dokumenta         on dok.tip_dokumenta        equals tip.tipdok_sifra
                           join kor in db.Korisnik_Sistema      on dok.dok_korisnik         equals kor.kor_sifra
                           join kom in db.Komitents             on dok.kom_komitent         equals kom.kom_sifra
                           join pri in db.Krajnji_Primalac      on dok.pri_primalac         equals pri.pri_sifra
                           join pos in db.Poslovnicas           on dok.psl_sifra_poslovnice equals pos.psl_sifra

                           select new DokumentDTO() 
                           {
                               dok_sifra_dokumenta = dok.dok_sifra_dokumenta,
                               dok_broj_dokumenta = dok.dok_broj_dokumenta,
                               dok_datum_isporuke = dok.dok_datum_isporuke,
                               dok_datum_Kreiranja = dok.dok_datum_kreiranja,
                               dok_napomena = dok.dok_napomena,
                               dok_otvoren = dok.dok_otvoren,
                               dok_veza = dok.dok_veza,
                               komitent = new KomitentDTO()
                               {
                                   kom_sifra = kom.kom_sifra,
                                   kom_adresa = kom.kom_adresa,
                                   kom_dobavljac = kom.kom_dobavljac,
                                   kom_grad = kom.kom_grad,
                                   kom_aktivan = kom.kom_aktivan,
                                   kom_MBR = kom.kom_MBR,
                                   kom_naziv = kom.kom_naziv,
                                   kom_PIB = kom.kom_PIB,
                                   kom_ptt = kom.kom_ptt
                               },
                               korisnik = new KorisnikDTO()
                               {
                                   kor_sifra = kor.kor_sifra,
                                   kor_ime = kor.kor_ime,
                                   kor_prezime = kor.kor_prezime,
                                   kor_username = kor.kor_username
                               },
                               pri_primalac = new PrimalacDTO()
                               {
                                   pri_sifra = pri.pri_sifra,
                                   pri_ime_pezime = pri.pri_ime_prezime,
                                   pri_adresa = pri.pri_adresa,
                                   pri_adresa_broj = pri.pri_adresa_broj,
                                   pri_grad = pri.pri_grad,
                                   pri_ptt = pri.pri_ptt,
                                   pri_email = pri.pri_email,
                                   pri_telefon = pri.pri_telefon
                               },
                               tip_dokumenta = new TipDokumentaDTO()
                               {
                                   tipdok_sifra = tip.tipdok_sifra,
                                   tipdok_naziv = tip.tipdok_naziv,
                                   tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                               },
                               psl_sifra_poslovnica = new PoslovnicaDTO()
                               {
                                   psl_sifra = pos.psl_sifra,
                                   psl_naziv = pos.psl_naziv,
                                   psl_aktivna = pos.psl_aktivna
                               }
                           }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllDokument()", ex.Message, true);
            }

            return result;

        }

        public IEnumerable<DokumentDTO> GetDokumentById(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<DokumentDTO> result = null;
            int sifra;
            int.TryParse(parametar, out sifra);

            try
            {

                result = (from dok in db.Dokuments
                          join tip in db.Tip_Dokumenta          on dok.tip_dokumenta            equals tip.tipdok_sifra
                          join kor in db.Korisnik_Sistema       on dok.dok_korisnik             equals kor.kor_sifra
                          join kom in db.Komitents              on dok.kom_komitent             equals kom.kom_sifra
                          join pri in db.Krajnji_Primalac       on dok.pri_primalac             equals pri.pri_sifra
                          join pos in db.Poslovnicas            on dok.psl_sifra_poslovnice     equals pos.psl_sifra

                          where dok.dok_sifra_dokumenta == sifra || 
                                dok.dok_broj_dokumenta == parametar

                          select new DokumentDTO()
                          {
                              dok_sifra_dokumenta = dok.dok_sifra_dokumenta,
                              dok_broj_dokumenta = dok.dok_broj_dokumenta,
                              dok_datum_isporuke = dok.dok_datum_isporuke,
                              dok_datum_Kreiranja = dok.dok_datum_kreiranja,
                              dok_napomena = dok.dok_napomena,
                              dok_otvoren = dok.dok_otvoren,
                              dok_veza = dok.dok_veza,
                              komitent = new KomitentDTO()
                              {
                                  kom_sifra = kom.kom_sifra,
                                  kom_adresa = kom.kom_adresa,
                                  kom_dobavljac = kom.kom_dobavljac,
                                  kom_grad = kom.kom_grad,
                                  kom_aktivan = kom.kom_aktivan,
                                  kom_MBR = kom.kom_MBR,
                                  kom_naziv = kom.kom_naziv,
                                  kom_PIB = kom.kom_PIB,
                                  kom_ptt = kom.kom_ptt
                              },
                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },
                              pri_primalac = new PrimalacDTO()
                              {
                                  pri_sifra = pri.pri_sifra,
                                  pri_ime_pezime = pri.pri_ime_prezime,
                                  pri_adresa = pri.pri_adresa,
                                  pri_adresa_broj = pri.pri_adresa_broj,
                                  pri_grad = pri.pri_grad,
                                  pri_ptt = pri.pri_ptt,
                                  pri_email = pri.pri_email,
                                  pri_telefon = pri.pri_telefon
                              },
                              tip_dokumenta = new TipDokumentaDTO()
                              {
                                  tipdok_sifra = tip.tipdok_sifra,
                                  tipdok_naziv = tip.tipdok_naziv,
                                  tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                              },
                              psl_sifra_poslovnica = new PoslovnicaDTO()
                              {
                                  psl_sifra = pos.psl_sifra,
                                  psl_naziv = pos.psl_naziv,
                                  psl_aktivna = pos.psl_aktivna
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetDokumentById(parametar: {parametar})", ex.Message, true);
            }

            return result;
        }
        
        public IEnumerable<DokumentDTO> GetAllPotvrdjenDokument()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<DokumentDTO> result = null;

            try
            {

                result = (from dok in db.Dokuments
                          join tip in db.Tip_Dokumenta              on dok.tip_dokumenta            equals tip.tipdok_sifra
                          join kor in db.Korisnik_Sistema           on dok.dok_korisnik             equals kor.kor_sifra
                          join kom in db.Komitents                  on dok.kom_komitent             equals kom.kom_sifra
                          join pri in db.Krajnji_Primalac           on dok.pri_primalac             equals pri.pri_sifra
                          join pos in db.Poslovnicas                on dok.psl_sifra_poslovnice     equals pos.psl_sifra

                          where dok.tip_dokumenta == 12 && 
                                dok.dok_otvoren == true

                          select new DokumentDTO()
                          {
                              dok_sifra_dokumenta = dok.dok_sifra_dokumenta,
                              dok_broj_dokumenta = dok.dok_broj_dokumenta,
                              dok_datum_isporuke = dok.dok_datum_isporuke,
                              dok_datum_Kreiranja = dok.dok_datum_kreiranja,
                              dok_napomena = dok.dok_napomena,
                              dok_otvoren = dok.dok_otvoren,
                              dok_veza = dok.dok_veza,
                              komitent = new KomitentDTO()
                              {
                                  kom_sifra = kom.kom_sifra,
                                  kom_adresa = kom.kom_adresa,
                                  kom_dobavljac = kom.kom_dobavljac,
                                  kom_grad = kom.kom_grad,
                                  kom_aktivan = kom.kom_aktivan,
                                  kom_MBR = kom.kom_MBR,
                                  kom_naziv = kom.kom_naziv,
                                  kom_PIB = kom.kom_PIB,
                                  kom_ptt = kom.kom_ptt
                              },
                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },
                              pri_primalac = new PrimalacDTO()
                              {
                                  pri_sifra = pri.pri_sifra,
                                  pri_ime_pezime = pri.pri_ime_prezime,
                                  pri_adresa = pri.pri_adresa,
                                  pri_adresa_broj = pri.pri_adresa_broj,
                                  pri_grad = pri.pri_grad,
                                  pri_ptt = pri.pri_ptt,
                                  pri_email = pri.pri_email,
                                  pri_telefon = pri.pri_telefon
                              },
                              tip_dokumenta = new TipDokumentaDTO()
                              {
                                  tipdok_sifra = tip.tipdok_sifra,
                                  tipdok_naziv = tip.tipdok_naziv,
                                  tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                              },
                              psl_sifra_poslovnica = new PoslovnicaDTO()
                              {
                                  psl_sifra = pos.psl_sifra,
                                  psl_naziv = pos.psl_naziv,
                                  psl_aktivna = pos.psl_aktivna
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllPotvrdjenDokument()", ex.Message, true);
            }

            return result;
        }
        
        public IEnumerable<DokumentDTO> GetPotvrdjeniDokumentById(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<DokumentDTO> result = null;
            int sifra;
            int.TryParse(parametar, out sifra);

            try
            {

                result = (from dok in db.Dokuments
                          join tip in db.Tip_Dokumenta          on dok.tip_dokumenta            equals tip.tipdok_sifra
                          join kor in db.Korisnik_Sistema       on dok.dok_korisnik             equals kor.kor_sifra
                          join kom in db.Komitents              on dok.kom_komitent             equals kom.kom_sifra
                          join pri in db.Krajnji_Primalac       on dok.pri_primalac             equals pri.pri_sifra
                          join pos in db.Poslovnicas            on dok.psl_sifra_poslovnice     equals pos.psl_sifra

                          where (dok.dok_sifra_dokumenta == sifra       || 
                                 dok.dok_broj_dokumenta == parametar)   && 
                                 dok.tip_dokumenta == 12                && 
                                 dok.dok_otvoren == true

                          select new DokumentDTO()
                          {
                              dok_sifra_dokumenta = dok.dok_sifra_dokumenta,
                              dok_broj_dokumenta = dok.dok_broj_dokumenta,
                              dok_datum_isporuke = dok.dok_datum_isporuke,
                              dok_datum_Kreiranja = dok.dok_datum_kreiranja,
                              dok_napomena = dok.dok_napomena,
                              dok_otvoren = dok.dok_otvoren,
                              dok_veza = dok.dok_veza,
                              komitent = new KomitentDTO()
                              {
                                  kom_sifra = kom.kom_sifra,
                                  kom_adresa = kom.kom_adresa,
                                  kom_dobavljac = kom.kom_dobavljac,
                                  kom_grad = kom.kom_grad,
                                  kom_aktivan = kom.kom_aktivan,
                                  kom_MBR = kom.kom_MBR,
                                  kom_naziv = kom.kom_naziv,
                                  kom_PIB = kom.kom_PIB,
                                  kom_ptt = kom.kom_ptt
                              },
                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },
                              pri_primalac = new PrimalacDTO()
                              {
                                  pri_sifra = pri.pri_sifra,
                                  pri_ime_pezime = pri.pri_ime_prezime,
                                  pri_adresa = pri.pri_adresa,
                                  pri_adresa_broj = pri.pri_adresa_broj,
                                  pri_grad = pri.pri_grad,
                                  pri_ptt = pri.pri_ptt,
                                  pri_email = pri.pri_email,
                                  pri_telefon = pri.pri_telefon
                              },
                              tip_dokumenta = new TipDokumentaDTO()
                              {
                                  tipdok_sifra = tip.tipdok_sifra,
                                  tipdok_naziv = tip.tipdok_naziv,
                                  tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                              },
                              psl_sifra_poslovnica = new PoslovnicaDTO()
                              {
                                  psl_sifra = pos.psl_sifra,
                                  psl_naziv = pos.psl_naziv,
                                  psl_aktivna = pos.psl_aktivna
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetPotvrdjeniDokumentById(parametar: {parametar})", ex.Message, true);
            }

            return result;
        }
        public bool CreateRequestDocument(DokumentDTO dokument, int? na_poslovnicu)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (dokument != null)
            {
                try
                {
                    Dokument d = new Dokument();
                    d.tip_dokumenta = dokument.tip_dokumenta.tipdok_sifra;
                    d.dok_datum_kreiranja = DateTime.Now;
                    d.dok_korisnik = dokument.korisnik.kor_sifra;
                    d.kom_komitent = dokument.komitent.kom_sifra;
                    d.dok_veza = dokument.dok_veza;
                    d.pri_primalac = dokument.pri_primalac.pri_sifra;
                    d.psl_sifra_poslovnice = dokument.psl_sifra_poslovnica.psl_sifra;
                    d.dok_datum_isporuke = dokument.dok_datum_isporuke;
                    d.dok_napomena = dokument.dok_napomena;
                    d.dok_broj_dokumenta = dokument.dok_broj_dokumenta;
                    d.dok_otvoren = true;
                    db.Dokuments.Add(d);
                    db.SaveChanges();
                    OkResponseMessage = d.dok_sifra_dokumenta.ToString();
                   _logProvider.AddToLog("EDIHelper.CreateRequestDocument(LDokument dokument)", "Korisnik: " + d.dok_korisnik + " | " + " Uspesno kreiran dokument: " + OkResponseMessage, false);
                    switch (d.tip_dokumenta)
                    {
                        case 3:
                            CreateResponseDocument(4, Convert.ToInt32(d.kom_komitent), Convert.ToInt32(d.dok_veza), Convert.ToInt32(d.pri_primalac), Convert.ToInt32(d.psl_sifra_poslovnice), DateTime.Now, d.dok_napomena, 2);
                            CreatePrijemRobe(d.dok_veza.Value);
                            CreateResponseDocument(13, Convert.ToInt32(d.kom_komitent), Convert.ToInt32(d.dok_veza), Convert.ToInt32(d.pri_primalac), Convert.ToInt32(d.psl_sifra_poslovnice), DateTime.Now, d.dok_napomena, 2);
                            break;
                        case 7:
                            CreateNewPrenos(d.dok_sifra_dokumenta, Convert.ToInt32(d.psl_sifra_poslovnice), Convert.ToInt32(na_poslovnicu));
                            break;
                    }
                    _entity.Refresh();
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom kreiranja dokumenta " + e.Message;
                   _logProvider.AddToLog("EDIHelper.CreateRequestDocument(LDokument dokument)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
               _logProvider.AddToLog("EDIHelper.CreateRequestDocument(LDokument dokument)", ErrorResponseMessage, true);
                return false;
            }
        }

        public void CreateResponseDocument(int resp_tip_dokumenta, int resp_komitent, int resp_dok_veza, int resp_primalac, int resp_poslovnica, DateTime resp_datum_isporuke, string resp_napomena, int korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Dokument d = new Dokument();
                d.tip_dokumenta = resp_tip_dokumenta;
                d.dok_datum_kreiranja = DateTime.Now;
                d.dok_korisnik = korisnik;
                d.kom_komitent = resp_komitent;
                d.dok_veza = resp_dok_veza;
                d.pri_primalac = resp_primalac;
                d.psl_sifra_poslovnice = resp_poslovnica;
                d.dok_datum_isporuke = resp_datum_isporuke;
                d.dok_napomena = resp_napomena;
                d.dok_otvoren = true;
                db.Dokuments.Add(d);
                db.SaveChanges();
                OkResponseMessage = "Sistem je kreirao dokument: " + d.dok_sifra_dokumenta;
               _logProvider.AddToLog("EDIHelper.CreateResponseDocument(int resp_tip_dokumenta, int resp_komitent, int resp_dok_veza, int resp_primalac, int resp_poslovnica, DateTime resp_datum_isporuke, string resp_napomena)",
                    "Korisnik: " + d.dok_korisnik + " | " + OkResponseMessage, false);
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom kreiranja dokumenta " + e.Message;
               _logProvider.AddToLog("EDIHelper.CreateResponseDocument(int resp_tip_dokumenta, int resp_komitent, int resp_dok_veza, int resp_primalac, int resp_poslovnica, DateTime resp_datum_isporuke, string resp_napomena)",
                    ErrorResponseMessage, true);
            }
        }

        public bool ConfirmDocument(int dok_sifra_dokumenta, int korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var dok = (from d in db.Dokuments
                       where d.dok_sifra_dokumenta == dok_sifra_dokumenta
                       select d).FirstOrDefault();
            if (dok != null)
            {
                try
                {

                    switch (dok.tip_dokumenta)
                    {
                        case 5:
                            CreateResponseDocument(6, Convert.ToInt32(dok.kom_komitent), dok.dok_sifra_dokumenta, Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                            break;
                        case 2:
                            CreateResponseDocument(12, Convert.ToInt32(dok.kom_komitent), dok.dok_sifra_dokumenta, Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                            break;
                        case 7:
                            CreateResponseDocument(8, Convert.ToInt32(dok.kom_komitent), dok.dok_sifra_dokumenta, Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                            break;
                        case 12:
                            CreateResponseDocument(10, Convert.ToInt32(dok.kom_komitent), Convert.ToInt32(dok.dok_veza), Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                            CreateResponseDocument(9, Convert.ToInt32(dok.kom_komitent), Convert.ToInt32(dok.dok_veza), Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                            _isporukaRepository.CreateNewIsporuka(dok.pri_primalac, dok.dok_datum_isporuke, dok.dok_veza);
                            break;
                    }
                    OkResponseMessage = "Uspesno potvrdjen dokument: " + dok_sifra_dokumenta;
                   _logProvider.AddToLog("EDIHelper.ConfirmDocument(int dok_sifra_dokumenta)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom potvrdjivanja dokumenata: " + e.Message;
                   _logProvider.AddToLog("EDIHelper.ConfirmDocument(int dok_sifra_dokumenta)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Greska prilikom potvrdjivanja dokumenata, nedostaje broj dokumenta";
               _logProvider.AddToLog("EDIHelper.ConfirmDocument(int dok_sifra_dokumenta)", ErrorResponseMessage, true);
                return false;
            }

        }

        public bool CreateLicnoPreuzimanje(int dok_sifra_dokumenta, int korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var dok = (from d in db.Dokuments
                       where d.dok_sifra_dokumenta == dok_sifra_dokumenta
                       select d).FirstOrDefault();
            try
            {
                CreateResponseDocument(11, Convert.ToInt32(dok.kom_komitent), Convert.ToInt32(dok.dok_veza), Convert.ToInt32(dok.pri_primalac), Convert.ToInt32(dok.psl_sifra_poslovnice), Convert.ToDateTime(dok.dok_datum_isporuke), dok.dok_napomena, korisnik);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<DokumentDTO> GetAllVezniDokumenti(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<DokumentDTO> result = null;

            try
            {

                result = (from dok in db.Dokuments
                          join tip in db.Tip_Dokumenta          on dok.tip_dokumenta        equals tip.tipdok_sifra
                          join kor in db.Korisnik_Sistema       on dok.dok_korisnik         equals kor.kor_sifra
                          join kom in db.Komitents              on dok.kom_komitent         equals kom.kom_sifra
                          join pri in db.Krajnji_Primalac       on dok.pri_primalac         equals pri.pri_sifra
                          join pos in db.Poslovnicas            on dok.psl_sifra_poslovnice equals pos.psl_sifra

                          where dok.dok_veza == id

                          select new DokumentDTO()
                          {
                              dok_sifra_dokumenta = dok.dok_sifra_dokumenta,
                              dok_broj_dokumenta = dok.dok_broj_dokumenta,
                              dok_datum_isporuke = dok.dok_datum_isporuke,
                              dok_datum_Kreiranja = dok.dok_datum_kreiranja,
                              dok_napomena = dok.dok_napomena,
                              dok_otvoren = dok.dok_otvoren,
                              dok_veza = dok.dok_veza,
                              komitent = new KomitentDTO()
                              {
                                  kom_sifra = kom.kom_sifra,
                                  kom_adresa = kom.kom_adresa,
                                  kom_dobavljac = kom.kom_dobavljac,
                                  kom_grad = kom.kom_grad,
                                  kom_aktivan = kom.kom_aktivan,
                                  kom_MBR = kom.kom_MBR,
                                  kom_naziv = kom.kom_naziv,
                                  kom_PIB = kom.kom_PIB,
                                  kom_ptt = kom.kom_ptt
                              },
                              korisnik = new KorisnikDTO()
                              {
                                  kor_sifra = kor.kor_sifra,
                                  kor_ime = kor.kor_ime,
                                  kor_prezime = kor.kor_prezime,
                                  kor_username = kor.kor_username
                              },
                              pri_primalac = new PrimalacDTO()
                              {
                                  pri_sifra = pri.pri_sifra,
                                  pri_ime_pezime = pri.pri_ime_prezime,
                                  pri_adresa = pri.pri_adresa,
                                  pri_adresa_broj = pri.pri_adresa_broj,
                                  pri_grad = pri.pri_grad,
                                  pri_ptt = pri.pri_ptt,
                                  pri_email = pri.pri_email,
                                  pri_telefon = pri.pri_telefon
                              },
                              tip_dokumenta = new TipDokumentaDTO()
                              {
                                  tipdok_sifra = tip.tipdok_sifra,
                                  tipdok_naziv = tip.tipdok_naziv,
                                  tipdok_kratki_naziv = tip.tipdok_kratki_naziv
                              },
                              psl_sifra_poslovnica = new PoslovnicaDTO()
                              {
                                  psl_sifra = pos.psl_sifra,
                                  psl_naziv = pos.psl_naziv,
                                  psl_aktivna = pos.psl_aktivna
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetDokumentById(id: {id})", ex.Message, true);
            }

            return result;
        }

        public bool CloseDocument(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var dok = from d in db.Dokuments
                      where d.dok_veza == id
                      select d;
            if (dok.Count() == 1)
            {
                List<LStavkeDokumenta> stavke = new List<LStavkeDokumenta>();
                var d = dok.FirstOrDefault();
                d.dok_otvoren = false;
                foreach (var stv in db.Stavke_Dokumenta.Where(x => x.dok_broj_dokumenta == id))
                {
                    UpdateDostupnaKolicina(Convert.ToInt32(stv.stv_sifra_artikla), Convert.ToInt32(stv.stv_kolicina), Convert.ToInt32(stv.dok_broj_dokumenta));
                }
                db.SaveChanges();
                OkResponseMessage = "Uspesno zatvoren dokument " + d.dok_sifra_dokumenta;
               _logProvider.AddToLog("EDIHelper.CloseDocument(int id)", OkResponseMessage, false);
                return true;
            }
            else
            {
                ErrorResponseMessage = "Ne mozete zatvarati dokumente koji imaju sistemski kreirane veze";
               _logProvider.AddToLog("EDIHelper.CloseDocument(int id)", ErrorResponseMessage, true);
                return false;
            }
        }
        #endregion "Dokumenti"

        #region "Stavke dokumenata"
        public IEnumerable<StavkeDokumentaDTO> GetAllStavkeDokument(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int rbr = 1;
            List<StavkeDokumentaDTO> result = null;
            try
            {
                var upit = from stv in db.Stavke_Dokumenta
                           join art in db.Artikals on stv.stv_sifra_artikla equals art.art_sifra
                           join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                           join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                           where stv.dok_broj_dokumenta == id
                           select new { stv, art, jed, dob };
                foreach (var st in upit)
                {
                    result.Add(new StavkeDokumentaDTO()
                    {
                        stv_id = st.stv.stv_id,
                        stv_broj_dokumenta = st.stv.dok_broj_dokumenta,
                        stv_kolicina = st.stv.stv_kolicina,
                        rbr = rbr,
                        artikal = new ArtikalDTO()
                        {
                            art_sifra = st.art.art_sifra,
                            art_tip = st.art.art_tip,
                            art_naziv = st.art.art_naziv,
                            art_proizvodjac = st.art.art_proizvodjac,
                            art_ean = st.art.art_ean,
                            jedinica_mere = new JedinicaMereDTO()
                            {
                                jed_sifra = st.jed.jed_sifra,
                                jed_naziv = st.jed.jed_naziv,
                                jed_kratki_naziv = st.jed.jed_kratki_naziv
                            }
                        }

                    });
                    rbr++;
                }
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllStavkeDokument(id: {id})", ex.Message, true);
            }

            return result;
        }

        public bool CreateNewStavkaDokumenta(LStavkeDokumenta stavka)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int tipdok = (from dok in db.Dokuments
                          where dok.dok_sifra_dokumenta == stavka.stv_broj_dokumenta
                          select dok.tip_dokumenta.Value).FirstOrDefault();
            if (tipdok == 2 || tipdok == 5 || tipdok == 7)
            {

                if (stavka != null && CheckDostupnaKolicina(stavka.artikal.art_sifra, stavka.stv_kolicina, stavka.stv_broj_dokumenta))
                {
                    try
                    {
                        Stavke_Dokumenta s = new Stavke_Dokumenta();
                        s.dok_broj_dokumenta = stavka.stv_broj_dokumenta;
                        s.stv_sifra_artikla = stavka.artikal.art_sifra;
                        s.stv_kolicina = stavka.stv_kolicina;
                        db.Stavke_Dokumenta.Add(s);
                        db.SaveChanges();
                        UpdateRezervisanaKolicina(s.stv_sifra_artikla.Value, s.stv_kolicina.Value, s.dok_broj_dokumenta.Value);
                        OkResponseMessage = "Uspesno uneta stavka " + s.stv_sifra_artikla;
                        _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", OkResponseMessage, false);
                        return true;
                    }
                    catch (Exception e)
                    {
                        ErrorResponseMessage = "Greska prilikom unosa stavke " + e.Message;
                        _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", ErrorResponseMessage, true);
                        return false;
                    }
                }
                else
                {
                    ErrorResponseMessage = "Artikal " + stavka.artikal.art_sifra + " nema trazenu kolicinu na stanju";
                    _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                if (stavka != null)
                {
                    try
                    {
                        Stavke_Dokumenta s = new Stavke_Dokumenta();
                        s.dok_broj_dokumenta = stavka.stv_broj_dokumenta;
                        s.stv_sifra_artikla = stavka.artikal.art_sifra;
                        s.stv_kolicina = stavka.stv_kolicina;
                        db.Stavke_Dokumenta.Add(s);
                        db.SaveChanges();
                        OkResponseMessage = "Uspesno uneta stavka " + s.stv_sifra_artikla;
                        _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", OkResponseMessage, false);
                        return true;
                    }
                    catch (Exception e)
                    {
                        ErrorResponseMessage = "Greska prilikom unosa stavke " + e.Message;
                        _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", ErrorResponseMessage, true);
                        return false;
                    }
                }
                else
                {
                    ErrorResponseMessage = "Pogresno prosledjeni podaci";
                    _logProvider.AddToLog("EDIHelper.CreateNewStavkaDokumenta(LStavkeDokumenta stavka)", ErrorResponseMessage, true);
                    return false;
                }
            }

        }

        public bool DeleteStavka(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                var stavka = (from s in db.Stavke_Dokumenta
                              where s.stv_id == id
                              select s).FirstOrDefault();
                int tipdok = (from s in db.Stavke_Dokumenta
                              join d in db.Dokuments on s.dok_broj_dokumenta equals d.dok_sifra_dokumenta
                              where s.stv_id == id
                              select d.tip_dokumenta.Value).FirstOrDefault();
                if (tipdok == 2 || tipdok == 5 || tipdok == 7)
                {
                    UpdateDostupnaKolicina(stavka.stv_sifra_artikla.Value, stavka.stv_kolicina.Value, stavka.dok_broj_dokumenta.Value);
                }
                db.Stavke_Dokumenta.Remove(stavka);
                db.SaveChanges();
                OkResponseMessage = "Uspesno obrisana stavka " + stavka.stv_id;
                _logProvider.AddToLog("EDIHelper.DeleteStavka(int id)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom brisanja stavke: " + e.Message;
                _logProvider.AddToLog("EDIHelper.DeleteStavka(int id)", ErrorResponseMessage, true);
                return false;
            }
        }

        public void UpdateRezervisanaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int poslovnica = (from dok in db.Dokuments
                              where dok.dok_sifra_dokumenta == broj_dokumenta
                              select dok.psl_sifra_poslovnice.Value).FirstOrDefault();

            var zaliha = (from zal in db.Zaliha_artikla
                          where zal.art_sifra == sifra_artikla && zal.psl_poslovnica == poslovnica
                          select zal).FirstOrDefault();
            try
            {
                zaliha.art_rezervisana_kolicina = zaliha.art_rezervisana_kolicina.Value + kolicina;
                zaliha.art_dostupna_kolicina = zaliha.art_dostupna_kolicina.Value - kolicina;
                db.SaveChanges();
                OkResponseMessage = "Uspesno azurirana kolicina za artikal: " + zaliha.art_sifra + " na poslovnici: " + zaliha.psl_poslovnica;
                _logProvider.AddToLog("EDIHelper.UpdateRezervisanaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", OkResponseMessage, false);
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom azuriranja kolicine " + e.Message;
                _logProvider.AddToLog("EDIHelper.UpdateRezervisanaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", ErrorResponseMessage, true);
            }
        }

        public void UpdateDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int poslovnica = (from dok in db.Dokuments
                              where dok.dok_sifra_dokumenta == broj_dokumenta
                              select dok.psl_sifra_poslovnice.Value).FirstOrDefault();

            var zaliha = (from zal in db.Zaliha_artikla
                          where zal.art_sifra == sifra_artikla && zal.psl_poslovnica == poslovnica
                          select zal).FirstOrDefault();

            var tipDokumenta = (from zal in db.Dokuments
                                where zal.dok_sifra_dokumenta == broj_dokumenta
                                select zal.tip_dokumenta).FirstOrDefault();
            try
            {
                if (tipDokumenta != 1)
                {
                    zaliha.art_rezervisana_kolicina = zaliha.art_rezervisana_kolicina.Value - kolicina;
                    zaliha.art_dostupna_kolicina = zaliha.art_dostupna_kolicina.Value + kolicina;
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno azurirana kolicina za artikal: " + zaliha.art_sifra + " na poslovnici: " + zaliha.psl_poslovnica;
                    _logProvider.AddToLog("EDIHelper.UpdateDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", OkResponseMessage, false);
                }
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom azuriranja kolicine " + e.Message;
                _logProvider.AddToLog("EDIHelper.UpdateDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", ErrorResponseMessage, true);
            }
        }

        public bool CheckDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int poslovnica = (from dok in db.Dokuments
                              where dok.dok_sifra_dokumenta == broj_dokumenta
                              select dok.psl_sifra_poslovnice.Value).FirstOrDefault();

            var zaliha = (from zal in db.Zaliha_artikla
                          where zal.art_sifra == sifra_artikla && zal.psl_poslovnica == poslovnica
                          select zal).FirstOrDefault();
            if (zaliha.art_dostupna_kolicina.Value >= kolicina)
            {
                OkResponseMessage = "Artikal " + zaliha.art_sifra + " uspesno prosao proveru dostupne kolicine";
                _logProvider.AddToLog("EDIHelper.CheckDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", OkResponseMessage, false);
                return true;
            }
            else
            {
                ErrorResponseMessage = "Kolicina artikla " + zaliha.art_sifra + " prevazilazi dozvoljenu kolicinu na dokumentu";
                _logProvider.AddToLog("EDIHelper.CheckDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta)", ErrorResponseMessage, true);
                return false;
            }
        }
        #endregion "Stavke dokumenata"

        #region "Manipulacija zalihom"
        public void CreatePrijemRobe(int br_narudzbe)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                var stavke = from st in db.Stavke_Dokumenta
                             join dok in db.Dokuments on st.dok_broj_dokumenta equals dok.dok_sifra_dokumenta
                             where st.dok_broj_dokumenta == br_narudzbe
                             select new { st, dok };
                if (stavke != null)
                {
                    foreach (var sta in stavke)
                    {
                        var artikal = (from art in db.Zaliha_artikla
                                       where art.art_sifra == sta.st.stv_sifra_artikla.Value && art.psl_poslovnica == sta.dok.psl_sifra_poslovnice.Value
                                       select art).FirstOrDefault();
                        artikal.art_dostupna_kolicina = artikal.art_dostupna_kolicina.Value + sta.st.stv_kolicina.Value;
                        OkResponseMessage = "Uspesno azurirana kolicina za artikal " + artikal.art_sifra.Value;
                        _logProvider.AddToLog("EDIHelper.CreatePrijemRobe(int br_narudzbe)", OkResponseMessage, false);
                    }
                    db.SaveChanges();
                }
                else
                {
                    ErrorResponseMessage = "Dokument nema stavke";
                    _logProvider.AddToLog("EDIHelper.CreatePrijemRobe(int br_narudzbe)", ErrorResponseMessage, true);
                }
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom azuriranja kolicine " + e.Message;
                _logProvider.AddToLog("EDIHelper.CreatePrijemRobe(int br_narudzbe)", ErrorResponseMessage, true);
            }
        }

        public bool CheckZalihaForDocument(int sifra, int kolicina, int poslovnica)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var upit = (from zl in db.Zaliha_artikla
                        where zl.art_sifra == sifra && zl.psl_poslovnica == poslovnica
                        select zl).FirstOrDefault();
            if (upit.art_dostupna_kolicina >= kolicina)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion "Manipulacija zalihom"

        #region "Prenosi"
        public IEnumerable<LInterniPrenos> GetAllOtvoreniPrenos()
        {
            List<LInterniPrenos> interniPrenosi = null;
            VEMTESTEntities db = new VEMTESTEntities();

            try
            {

                interniPrenosi = (from ip in db.Interni_Prenos
                                  join dok in db.Dokuments
                                  on ip.dok_sifra equals dok.dok_sifra_dokumenta

                                  where dok.dok_otvoren == true

                                  select new LInterniPrenos()
                                  {
                                      Id = ip.Id,
                                      SaPoslovnice = ip.sa_poslovnice,
                                      NaPoslovnicu = ip.na_poslovnicu,
                                      datum_kreiranja = ip.datum_kreiranja,
                                      dokument = ip.dok_sifra
                                  }).ToList();
            }
            catch(Exception ex)
            {
                _logProvider.AddToLog($"GetAllOtvoreniPrenos()", ex.Message, true);
            }

            return interniPrenosi;
        }

        public bool CreateNewPrenos(int dokument, int sa_poslovnice, int na_poslovnicu)
        {
            VEMTESTEntities db = new VEMTESTEntities();

            try
            {
                Interni_Prenos interni_Prenos = new Interni_Prenos()
                {
                    dok_sifra = dokument,
                    sa_poslovnice = sa_poslovnice,
                    na_poslovnicu = na_poslovnicu
                };

                db.Interni_Prenos.Add(interni_Prenos);
                db.SaveChanges();
                OkResponseMessage = "Uspesno kreiran interni prenos";
                _logProvider.AddToLog($"CreateNewPrenos(dokument: {dokument.ToString()}, sa_poslovnice: {sa_poslovnice.ToString()}, na_poslovnicu: {na_poslovnicu.ToString()})", OkResponseMessage, false);
                return true;
            }
            catch(Exception ex)
            {
                ErrorResponseMessage = $"Greska prilikom kreiranja internog prenosa: {ex.Message}";
                _logProvider.AddToLog($"CreateNewPrenos(dokument: {dokument.ToString()}, sa_poslovnice: {sa_poslovnice.ToString()}, na_poslovnicu: {na_poslovnicu.ToString()})", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool CreateNewInterniPrijem(int? prenos)
        {
            VEMTESTEntities db = new VEMTESTEntities();

            Interni_Prenos interni_Prenos = null;
            Dokument dokument = null;
            List<Stavke_Dokumenta> stavke_Dokumenta = null;
            Zaliha_artikla zaliha = null;

            try
            {
                //pretraga prosledjenog internog prenosa
                interni_Prenos = (from ip in db.Interni_Prenos

                                 where ip.Id.Equals(prenos.Value)

                                 select ip).FirstOrDefault();

                if(interni_Prenos != null)
                {
                    //pretraga dokumenta vezanog za interni prenos
                    dokument = (from dok in db.Dokuments

                                 where dok.dok_sifra_dokumenta.Equals(interni_Prenos.dok_sifra)

                                 select dok).FirstOrDefault();

                    if(dokument != null)
                    {
                        //pravljenje kolekcije svih stavki nadjenog dokumenta
                        stavke_Dokumenta = (from stv in db.Stavke_Dokumenta

                                            where stv.dok_broj_dokumenta == dokument.dok_sifra_dokumenta

                                            select stv).ToList();

                        if(stavke_Dokumenta != null)
                        {
                            foreach(var stv in stavke_Dokumenta)
                            {
                                //azuriranje kolicine u plus sa poslovnicu na koju ide artikal
                                zaliha = ( from art in db.Zaliha_artikla

                                           where art.art_sifra == stv.stv_sifra_artikla && art.psl_poslovnica == interni_Prenos.na_poslovnicu

                                           select art).FirstOrDefault();
                                zaliha.art_dostupna_kolicina += stv.stv_kolicina;

                                //azuriranje kolicine u minus za poslovnicu sa koje ide artikal
                                zaliha = (from art in db.Zaliha_artikla

                                          where art.art_sifra == stv.stv_sifra_artikla && art.psl_poslovnica == interni_Prenos.sa_poslovnice

                                          select art).FirstOrDefault();
                                zaliha.art_dostupna_kolicina -= stv.stv_kolicina;

                                db.SaveChanges();

                                ConfirmDocument(dokument.dok_sifra_dokumenta, 2);
                                CloseDocument(dokument.dok_sifra_dokumenta); //istraziti, moguce da se podvukla greska // verovatno treba napraviti zamenu vremena pozivanja 1.CloseDocument 2.ConfirmDocument

                                OkResponseMessage = $"Uspesno obradjen interni prenos {prenos}";
                                _logProvider.AddToLog($"CreateNewInterniPrijem(prenos: {prenos})", OkResponseMessage, false);
                            }
                            return true;
                        }
                        else
                        {
                            OkResponseMessage = $"Stavke dokumenta su prazne: {dokument.dok_sifra_dokumenta}";
                            _logProvider.AddToLog($"CreateNewInterniPrijem(prenos: {prenos})", OkResponseMessage, false);
                            return false;
                        }
                    }
                    else
                    {
                        OkResponseMessage = $"Trazeni dokument ne postoji: {interni_Prenos.dok_sifra}";
                        _logProvider.AddToLog($"CreateNewInterniPrijem(prenos: {prenos})", OkResponseMessage, false);
                        return false;
                    }
                }
                else
                {
                    OkResponseMessage = $"Interni prenos nije pronadjen: {prenos}";
                    _logProvider.AddToLog($"CreateNewInterniPrijem(prenos: {prenos})", OkResponseMessage, false);
                    return false;
                }
            }
            catch(Exception ex)
            {
                ErrorResponseMessage = $"Greska prilikom obrade internog prenosa {prenos}: {ex.Message}";
                _logProvider.AddToLog($"CreateNewInterniPrijem(prenos: {prenos})", ErrorResponseMessage, true);
                return false;
            }
        }
        #endregion "Prenosi"
    }
}