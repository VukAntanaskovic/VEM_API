using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VEM_API.Models;
using VEM_API.Repositories;

namespace VEM_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: Settings.appUrl, headers: "*", methods: "*")]
    public class AdresarController : ApiController
    {
        private readonly IAdresarRepository _adresarRepository;

        public AdresarController(IAdresarRepository adresarRepository)
        {
            _adresarRepository = adresarRepository;
        }
        // GET api/<controller>
        [Route("api/WMS/Adresar/GetAllAdresar")]
        [BasicAuthentication]
        public IEnumerable<AdresarDTO> Get()
        {
            return _adresarRepository.GetAllAdresar();
        }

        // GET api/<controller>/5
        [Route("api/WMS/Adresar/GetAllAdresarByAdresa/{Adresa}")]
        [BasicAuthentication]
        public IEnumerable<AdresarDTO> Get(string Adresa)
        {
            return _adresarRepository.GetAllAdresarByAdresa(Adresa);
        }
    }
}