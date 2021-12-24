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
    public class MernaJedinicaRepository:IMernaJedinicaRepository
    {
        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static string OkResponseMessage;
        public static string ErrorResponseMessage;

        public MernaJedinicaRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }

        #region "Merna jedinica"
        public IEnumerable<JedinicaMereDTO> GetAllJedinicaMere()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<JedinicaMereDTO> result = null;

            try
            {
                result = (from jed in db.Jedinica_Mere

                          select new JedinicaMereDTO()
                          {
                              jed_sifra = jed.jed_sifra,
                              jed_naziv = jed.jed_naziv,
                              jed_kratki_naziv = jed.jed_kratki_naziv
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllOtvoreniPrenos()", ex.Message, true);
            }

            return result;
        }

        public bool CreateNewJedinicaMere(JedinicaMereDTO jedinicaMere)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            try
            {
                Jedinica_Mere j = new Jedinica_Mere()
                {
                    jed_kratki_naziv = jedinicaMere.jed_kratki_naziv,
                    jed_naziv = jedinicaMere.jed_naziv
                };
                db.Jedinica_Mere.Add(j);
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste kreirali novu jedinicu mere";
                _logProvider.AddToLog("CreateNewJedinicaMere(LJedinicaMere jedinicaMere)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom dodavanja merne jedinice: " + e.Message;
                _logProvider.AddToLog("CreateNewJedinicaMere(LJedinicaMere jedinicaMere)", ErrorResponseMessage, true);
                return true;
            }
        }

        public bool UpdateJedinicaMere(int sifra, JedinicaMereDTO jedinicaMere)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            Jedinica_Mere jedinica = (from jed in db.Jedinica_Mere
                                      where jed.jed_sifra == sifra
                                      select jed).FirstOrDefault();
            try
            {
                jedinica.jed_kratki_naziv = jedinicaMere.jed_kratki_naziv;
                jedinica.jed_naziv = jedinicaMere.jed_naziv;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste izmenilijedinicu mere";
                _logProvider.AddToLog("UpdateJedinicaMere(int sifra ,LJedinicaMere jedinicaMere)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom izmene merne jedinice: " + e.Message;
                _logProvider.AddToLog("UpdateJedinicaMere(int sifra ,LJedinicaMere jedinicaMere)", ErrorResponseMessage, true);
                return true;
            }
        }
        #endregion "Merna jedinica"
    }
}