using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class PrimalacRepository:IPrimalacRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;
        public PrimalacRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        public IEnumerable<PrimalacDTO> GetAllPrimalac()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<PrimalacDTO> result = null;

            try
            {
                result = (from p in db.Krajnji_Primalac

                          orderby p.pri_ime_prezime ascending

                          select new PrimalacDTO()
                          {
                              pri_sifra = p.pri_sifra,
                              pri_ime_pezime = p.pri_ime_prezime,
                              pri_adresa = p.pri_adresa,
                              pri_adresa_broj = p.pri_adresa_broj,
                              pri_email = p.pri_email,
                              pri_grad = p.pri_grad,
                              pri_ptt = p.pri_ptt,
                              pri_telefon = p.pri_telefon
                          }).ToList();
            }
            catch(Exception ex)
            {
                _logProvider.AddToLog("GetAllPrimalac()", ex.Message, true);
            }

            return result;
        }

        public IEnumerable<PrimalacDTO> GetPrimalacById(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<PrimalacDTO> result = null;

            try
            {
                result = (from p in db.Krajnji_Primalac

                          where p.pri_sifra == id

                          orderby p.pri_ime_prezime ascending

                          select new PrimalacDTO()
                          {
                              pri_sifra = p.pri_sifra,
                              pri_ime_pezime = p.pri_ime_prezime,
                              pri_adresa = p.pri_adresa,
                              pri_adresa_broj = p.pri_adresa_broj,
                              pri_email = p.pri_email,
                              pri_grad = p.pri_grad,
                              pri_ptt = p.pri_ptt,
                              pri_telefon = p.pri_telefon
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetPrimalacById(id: {id})", ex.Message, true);
            }

            return result;
        }

        public bool InsertNewPrimalac(PrimalacDTO primalac)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            if (primalac != null)
            {
                try
                {
                    Krajnji_Primalac krajnji_Primalac = new Krajnji_Primalac();
                    krajnji_Primalac.pri_ime_prezime = primalac.pri_ime_pezime;
                    krajnji_Primalac.pri_adresa = primalac.pri_adresa;
                    krajnji_Primalac.pri_adresa_broj = primalac.pri_adresa_broj;
                    krajnji_Primalac.pri_grad = primalac.pri_grad;
                    krajnji_Primalac.pri_ptt = primalac.pri_ptt;
                    krajnji_Primalac.pri_telefon = primalac.pri_telefon;
                    krajnji_Primalac.pri_email = primalac.pri_email;
                    db.Krajnji_Primalac.Add(krajnji_Primalac);
                    db.SaveChanges();
                    OkResponseMessage = krajnji_Primalac.pri_sifra.ToString();
                    _logProvider.AddToLog("InsertNewPrimalac(LPrimalac primalac)", OkResponseMessage, false);
                    return true;

                }
                catch (Exception ex)
                {
                    ErrorResponseMessage = "Greska prilikom unosa primaoca: " + ex.Message;
                    _logProvider.AddToLog("InsertNewPrimalac(LPrimalac primalac)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci prilikom unosa primaoca";
                _logProvider.AddToLog("InsertNewPrimalac(LPrimalac primalac)", ErrorResponseMessage, true);
                return false;
            }
        }
    }
}