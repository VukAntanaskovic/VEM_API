using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.DbModel;

namespace VEM_API.Repositories
{
    public class KomitentRepository:IKomitentRepository
    {

        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public KomitentRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        public IEnumerable<KomitentDTO> GetAllKomitent()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<KomitentDTO> result = null;

            try
            {
                result = (from kom in db.Komitents

                          select new KomitentDTO()
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
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllKomitent()", ex.Message, true);
            }

            return result;
        }

        public IEnumerable<KomitentDTO> GetKomitentByParameter(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<KomitentDTO> result = null;
            int sifra;
            int.TryParse(parametar, out sifra);

            try
            {
                result = (from kom in db.Komitents

                          where kom.kom_sifra == sifra ||
                                kom.kom_naziv.Contains(parametar)

                          select new KomitentDTO()
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
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllKomitent()", ex.Message, true);
            }

            return result;
        }

        public IEnumerable<KomitentDTO> GetDobavljacByParametar(string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<KomitentDTO> result = null;
            int sifra;
            int.TryParse(parametar, out sifra);

            try
            {
                result = (from kom in db.Komitents

                          where (kom.kom_sifra == sifra || kom.kom_naziv.Contains(parametar)) &&
                                 kom.kom_dobavljac == true &&
                                 kom.kom_dobavljac == true

                          select new KomitentDTO()
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
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllKomitent()", ex.Message, true);
            }

            return result;
        }
        public bool CreateNewKomitent(KomitentDTO komitent)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (komitent != null)
            {
                try
                {
                    Komitent k = new Komitent();
                    k.kom_naziv = komitent.kom_naziv;
                    k.kom_adresa = komitent.kom_adresa;
                    k.kom_grad = komitent.kom_grad;
                    k.kom_ptt = komitent.kom_ptt;
                    k.kom_dobavljac = komitent.kom_dobavljac;
                    k.kom_aktivan = true;
                    k.kom_PIB = komitent.kom_PIB;
                    k.kom_MBR = komitent.kom_MBR;
                    db.Komitents.Add(k);
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste kreirali novog komitenta: " + k.kom_sifra;
                    _logProvider.AddToLog("CreateNewKomitent(LKomitent komitent)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom kreiranja komitenta: " + e.Message;
                    _logProvider.AddToLog("CreateNewKomitent(LKomitent komitent)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
                _logProvider.AddToLog("CreateNewKomitent(LKomitent komitent)", ErrorResponseMessage, true);
                return false;
            }

        }

        public bool UpdateKomitent(int id, KomitentDTO komitent)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Komitent kom = (from km in db.Komitents
                                where km.kom_sifra == id
                                select km).FirstOrDefault();
                if (kom != null)
                {
                    if (komitent.kom_naziv != null)
                        kom.kom_naziv = komitent.kom_naziv;
                    if (komitent.kom_adresa != null)
                        kom.kom_adresa = komitent.kom_adresa;
                    if (komitent.kom_grad != null)
                        kom.kom_grad = komitent.kom_grad;
                    if (komitent.kom_ptt != null)
                        kom.kom_ptt = komitent.kom_ptt;
                    kom.kom_dobavljac = komitent.kom_dobavljac;
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste izmenili komitenta " + kom.kom_sifra;
                    _logProvider.AddToLog("UpdateKomitent(int id, LKomitent komitent)", OkResponseMessage, false);
                    return true;
                }
                else
                {
                    ErrorResponseMessage = "Pogresno proslejdeni podaci";
                    _logProvider.AddToLog("UpdateKomitent(int id, LKomitent komitent)", ErrorResponseMessage, true);
                    return false;
                }
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene komitenta: " + e.Message;
                _logProvider.AddToLog("UpdateKomitent(int id, LKomitent komitent)", ErrorResponseMessage, true);
                return false;
            }

        }

        public bool ActivateDeactivateKomitent(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Komitent kom = (from km in db.Komitents
                                where km.kom_sifra == id
                                select km).FirstOrDefault();
                if (kom.kom_aktivan == true)
                {
                    kom.kom_aktivan = false;
                }
                else
                {
                    kom.kom_aktivan = true;
                }
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izvrsili zahtev aktivacije/deaktivacije komitenta " + kom.kom_sifra;
                _logProvider.AddToLog("ActivateDeactivateKomitent(int id)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom aktivacije/deaktivacije komitenta: " + e.Message;
                _logProvider.AddToLog("ActivateDeactivateKomitent(int id)", ErrorResponseMessage, true);
                return false;
            }
        }

        public IEnumerable<KomitentDTO> GetAllDobavljac()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<KomitentDTO> result = null;

            try
            {
                result = (from kom in db.Komitents

                          where  kom.kom_dobavljac == true &&
                                 kom.kom_dobavljac == true

                          select new KomitentDTO()
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
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllKomitent()", ex.Message, true);
            }

            return result;
        }
    }
}