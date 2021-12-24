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

        public IEnumerable<LPrimalac> GetAllPrimalac()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LPrimalac> primalacs = new List<LPrimalac>();
            foreach (var primalac in db.Krajnji_Primalac)
            {
                primalacs.Add(
                    new LPrimalac(
                        primalac.pri_sifra, 
                        primalac.pri_ime_prezime, 
                        primalac.pri_adresa, 
                        primalac.pri_adresa_broj, 
                        primalac.pri_grad, 
                        primalac.pri_ptt.ToString(), 
                        primalac.pri_telefon, 
                        primalac.pri_email));
            }
            return primalacs;
        }

        public IEnumerable<LPrimalac> GetPrimalacById(int id)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LPrimalac> primalacs = new List<LPrimalac>();
            foreach (var primalac in db.Krajnji_Primalac.Where(x => x.pri_sifra == id))
            {
                primalacs.Add(
                    new LPrimalac(
                        primalac.pri_sifra, 
                        primalac.pri_ime_prezime, 
                        primalac.pri_adresa, 
                        primalac.pri_adresa_broj, 
                        primalac.pri_grad, 
                        primalac.pri_ptt.ToString(), 
                        primalac.pri_telefon, 
                        primalac.pri_email));
            }
            return primalacs;
        }

        public bool InsertNewPrimalac(LPrimalac primalac)
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
                    krajnji_Primalac.pri_ptt = int.Parse(primalac.pri_ptt);
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