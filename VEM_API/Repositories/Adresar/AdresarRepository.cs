using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class AdresarRepository:IAdresarRepository
    {
        private readonly ILogProvider _logProvider;
        public AdresarRepository(ILogProvider logProvider)
        {
            _logProvider = logProvider;
        }
        #region "Adresar"
        public IEnumerable<AdresarDTO> GetAllAdresar()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<AdresarDTO> result = null;

            try
            {
                result = (from adr in db.Adresars

                          select new AdresarDTO()
                          {
                              id        = adr.id,
                              adresa    = adr.adresa,
                              opstina   = adr.opstina,
                              grad      = adr.grad,
                              ptt       = adr.ptt
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllAdresar()", ex.Message, true);
            }

            return result;
        }

        public IEnumerable<AdresarDTO> GetAllAdresarByAdresa(string Adresa)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<AdresarDTO> result = null;

            try
            {
                result = (from adr in db.Adresars

                          where adr.adresa.StartsWith(Adresa)

                          select new AdresarDTO()
                          {
                              id = adr.id,
                              adresa = adr.adresa,
                              opstina = adr.opstina,
                              grad = adr.grad,
                              ptt = adr.ptt
                          }).ToList();
            }
            catch (Exception ex)
            {
                _logProvider.AddToLog($"GetAllAdresarByAdresa(Adresa: {Adresa})", ex.Message, true);
            }

            return result;
        }
        #endregion "Adresar"
    }
}