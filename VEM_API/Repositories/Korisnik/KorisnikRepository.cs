using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.DbModel;
using System.Web.Script.Serialization;

namespace VEM_API.Repositories
{
    public class KorisnikRepository:IKorisnikRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;
        public static int logon_kor_sifra;
        public static string logon_kor_ime_prezime;
        public static AppUser jsonResponse;
        public KorisnikRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        public bool AuthenticateUser(AppUser appUser, string app)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            JavaScriptSerializer json = new JavaScriptSerializer();
            string cryptPass = LoginSecurity.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", appUser.password);
            if (app == "VEM_GUI")
            {
                var user = (from n in db.Korisnik_Sistema
                            where n.kor_username.Equals(appUser.username) && n.kor_password.Equals(cryptPass)
                            && n.VEM_GUI == true
                            select n).FirstOrDefault();
                if (user != null)
                {
                    logon_kor_sifra = user.kor_sifra;
                    logon_kor_ime_prezime = user.kor_ime + " " + user.kor_prezime;
                    jsonResponse = new AppUser() { user_sifra = logon_kor_sifra, user_ime_prezime = logon_kor_ime_prezime };
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", "Ulogovan user: " + logon_kor_ime_prezime, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresni kredencijali";
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", ErrorResponseMessage, false);
                    return false;
                }
            }
            else if (app == "VEM_WEB")
            {
                var user = (from n in db.Korisnik_Sistema
                            where n.kor_username.Equals(appUser.username) && n.kor_password.Equals(cryptPass)
                            && n.VEM_WEB == true
                            select n).FirstOrDefault();
                if (user != null)
                {
                    logon_kor_sifra = user.kor_sifra;
                    logon_kor_ime_prezime = user.kor_ime + " " + user.kor_prezime;
                    jsonResponse = new AppUser() { user_sifra = logon_kor_sifra, user_ime_prezime = logon_kor_ime_prezime };
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", "Ulogovan user: " + logon_kor_ime_prezime, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresni kredencijali";
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", ErrorResponseMessage, false);
                    return false;
                }
            }
            else if (app == "VEM_MOBILE")
            {
                var user = (from n in db.Korisnik_Sistema
                            where n.kor_username.Equals(appUser.username) && n.kor_password.Equals(cryptPass)
                            && n.VEM_MOBILE == true
                            select n).FirstOrDefault();
                if (user != null)
                {
                    logon_kor_sifra = user.kor_sifra;
                    logon_kor_ime_prezime = user.kor_ime + " " + user.kor_prezime;
                    jsonResponse = new AppUser() { user_sifra = logon_kor_sifra, user_ime_prezime = logon_kor_ime_prezime };
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", "Ulogovan user: " + logon_kor_ime_prezime, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresni kredencijali";
                    _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", ErrorResponseMessage, false);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Zabranjen pristup aplikaciji";
                _logProvider.AddToLog("ApiHelper.AuthenticateUser(AppUser appUser, string app)", ErrorResponseMessage, false);
                return false;
            }
        }

        public IEnumerable<KorisnikDTO> GetAllKorisnik()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<KorisnikDTO> result = null;

            try
            {
                result = (from kor in db.Korisnik_Sistema
                          join atr in db.Autorizacija_Korisnika on kor.atr_autorizacija equals atr.atr_sifra

                           select new KorisnikDTO() 
                           {
                                kor_sifra = kor.kor_sifra,
                                kor_ime = kor.kor_ime,
                                kor_prezime = kor.kor_prezime,
                                kor_telefon = kor.kor_prezime,
                                kor_username = kor.kor_username,
                                atr_autorizacija = atr.atr_sifra,
                                atr_naziv = atr.atr_naziv,
                                psl_poslovnica_rada = kor.psl_poslovnica_rada,
                                WEB = kor.VEM_WEB,
                                GUI = kor.VEM_GUI,
                                MOBILE = kor.VEM_MOBILE
                           }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllKorisnik()", ex.Message, true);
            }

            return result;
        }


        public IEnumerable<KorisnikDTO> GetKorisnikByParametar(string parametar)
        {
            int sifra;
            int.TryParse(parametar, out sifra);
            VEMTESTEntities db = new VEMTESTEntities();
            List<KorisnikDTO> result = null;

            try
            {
                result = (from kor in db.Korisnik_Sistema
                          join atr in db.Autorizacija_Korisnika on kor.atr_autorizacija equals atr.atr_sifra
                         
                          where  kor.kor_sifra == sifra                 || 
                                (kor.kor_ime.StartsWith(parametar)      || 
                                 kor.kor_prezime.StartsWith(parametar)  || 
                                 kor.kor_username.Equals(parametar))
                          
                          select new KorisnikDTO()
                          {
                              kor_sifra = kor.kor_sifra,
                              kor_ime = kor.kor_ime,
                              kor_prezime = kor.kor_prezime,
                              kor_telefon = kor.kor_prezime,
                              kor_username = kor.kor_username,
                              atr_autorizacija = atr.atr_sifra,
                              atr_naziv = atr.atr_naziv,
                              psl_poslovnica_rada = kor.psl_poslovnica_rada,
                              WEB = kor.VEM_WEB,
                              GUI = kor.VEM_GUI,
                              MOBILE = kor.VEM_MOBILE
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetKorisnikByParametar(parametar: {parametar})", ex.Message, true);
            }

            return result;
        }

        public bool CreateNewKorisnik(KorisnikDTO korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Korisnik_Sistema k = new Korisnik_Sistema()
                {
                    kor_ime = korisnik.kor_ime,
                    kor_prezime = korisnik.kor_prezime,
                    kor_telefon = korisnik.kor_telefon,
                    kor_username = korisnik.kor_ime.ToLower().Trim() + "." + korisnik.kor_prezime.ToLower().Trim(),
                    kor_password = LoginSecurity.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", korisnik.kor_ime.ToLower() + "123"),
                    VEM_WEB = korisnik.WEB,
                    VEM_GUI = korisnik.GUI,
                    VEM_MOBILE = korisnik.MOBILE,
                    atr_autorizacija = korisnik.atr_autorizacija,
                    psl_poslovnica_rada = korisnik.psl_poslovnica_rada
                };
                db.Korisnik_Sistema.Add(k);
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste dodali korisnika: " + k.kor_sifra;
                _logProvider.AddToLog("ApiHelper.CreateNewKorisnik(LKorisnik korisnik)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom dodavanja korisnika: " + e.Message;
                _logProvider.AddToLog("ApiHelper.CreateNewKorisnik(LKorisnik korisnik)", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool UpdatePasswordKorisnik(int id, KorisnikDTO korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Korisnik_Sistema kor = db.Korisnik_Sistema.SingleOrDefault(x => x.kor_sifra == id);
                kor.kor_password = LoginSecurity.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", korisnik.kor_password);
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili password za korisnika: " + kor.kor_sifra;
                _logProvider.AddToLog("ApiHelper.UpdatePasswordKorisnik(int id, LKorisnik korisnik)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene passworda: " + e.Message;
                _logProvider.AddToLog("ApiHelper.UpdatePasswordKorisnik(int id, LKorisnik korisnik)", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool ChangePoslovnica(int id, KorisnikDTO korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Korisnik_Sistema kor = db.Korisnik_Sistema.SingleOrDefault(x => x.kor_sifra == id);
                kor.psl_poslovnica_rada = korisnik.psl_poslovnica_rada;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili poslovnicu za korisnika: " + kor.kor_sifra;
                _logProvider.AddToLog("ApiHelper.ChangePoslovnica(int id, LKorisnik korisnik)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene poslovnice: " + e.Message;
                _logProvider.AddToLog("ApiHelper.ChangePoslovnica(int id, LKorisnik korisnik)", ErrorResponseMessage, true);
                return false;
            }
        }

        public bool UpdateKorisnik(int id, KorisnikDTO korisnik)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Korisnik_Sistema kor = db.Korisnik_Sistema.SingleOrDefault(x => x.kor_sifra == id);
                kor.VEM_WEB = korisnik.WEB;
                kor.VEM_GUI = korisnik.GUI;
                kor.VEM_MOBILE = korisnik.MOBILE;
                kor.atr_autorizacija = korisnik.atr_autorizacija;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenili prava za korisnika: " + kor.kor_sifra;
                _logProvider.AddToLog("ApiHelper.UpdateKorisnik(int id, LKorisnik korisnik)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene prava: " + e.Message;
                _logProvider.AddToLog("ApiHelper.UpdateKorisnik(int id, LKorisnik korisnik)", ErrorResponseMessage, true);
                return false;
            }
        }

        public IEnumerable<AutorizacijaKorisnikaDTO> GetAllRola()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<AutorizacijaKorisnikaDTO> result = null;

            try
            {
                result = (from atr in db.Autorizacija_Korisnika

                          select new AutorizacijaKorisnikaDTO()
                          {
                              atr_sifra = atr.atr_sifra,
                              atr_naziv = atr.atr_naziv
                          }).ToList();
            }
            catch(Exception ex)
            {
                _logProvider.AddToLog("GetAllRola()", ex.Message, true);
            }

            return result;
        }
    }
}