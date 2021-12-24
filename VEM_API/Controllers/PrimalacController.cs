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
    public class PrimalacController : ApiController
    {
        private readonly IPrimalacRepository _primalacRepository;

        public PrimalacController(IPrimalacRepository primalacRepository)
        {
            _primalacRepository = primalacRepository;
        }

        // GET api/<controller>
        [Route("api/WMS/Primalac/GetAllPrimalac")]
        [BasicAuthentication]
        public IEnumerable<LPrimalac> Get()
        {
            return _primalacRepository.GetAllPrimalac();
        }

        [Route("api/WMS/Primalac/GetPrimalacById/{id}")]
        [BasicAuthentication]
        public IEnumerable<LPrimalac> Get(int id)
        {
            return _primalacRepository.GetPrimalacById(id);
        }

        // POST api/<controller>
        [Route("api/WMS/Primalac/InsertNewPrimalac")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]LPrimalac primalac)
        {
            if (_primalacRepository.InsertNewPrimalac(primalac))
            {
                return Request.CreateResponse(HttpStatusCode.Created, int.Parse(PrimalacRepository.OkResponseMessage));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, PrimalacRepository.ErrorResponseMessage);
            }
        }
    }
}