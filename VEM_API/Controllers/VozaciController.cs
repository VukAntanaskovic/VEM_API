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
    public class VozaciController : ApiController
    {

        private readonly IVozacRepository _vozacRepository;

        public VozaciController(IVozacRepository vozacRepository)
        {
            _vozacRepository = vozacRepository;
        }

        // GET api/<controller>
        [Route("api/TMS/Vozaci/GetAllVozace")]
        [BasicAuthentication]
        public IEnumerable<LVozaci> Get()
        {
            return _vozacRepository.GetAllVozace();
        }

        // GET api/<controller>/5
        [Route("api/TMS/Vozaci/GetVozaceBySifra/{id}")]
        [BasicAuthentication]
        public IEnumerable<LVozaci> Get(int id)
        {
            return _vozacRepository.GetVozaceBySifra(id);
        }

        // POST api/<controller>
        [Route("api/TMS/Vozaci/CreateNewVozac")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]LVozaci vozaci)
        {
            if (_vozacRepository.CreateNewVozac(vozaci))
            {
                return Request.CreateResponse(HttpStatusCode.Created, VozacRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, VozacRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/TMS/Vozaci/UpdateVozac/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]LVozaci vozac)
        {
            if (_vozacRepository.UpdateVozac(id, vozac))
            {
                return Request.CreateResponse(HttpStatusCode.OK, VozacRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, VozacRepository.ErrorResponseMessage);
            }
        }
    }
}