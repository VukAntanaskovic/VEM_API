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
    public class AnalitikaController : ApiController
    {
        private readonly IAnalitikaRepository _analitikaRepository;

        public AnalitikaController(IAnalitikaRepository analitikaRepository)
        {
            _analitikaRepository = analitikaRepository;
        }
        // GET api/<controller>
        [Route("api/WMS/Analitika/GetAnalitika")]
        [BasicAuthentication]
        public IEnumerable<AnalitikaDTO> Get()
        {
            return _analitikaRepository.GetAnalitika();
        }
    }
}