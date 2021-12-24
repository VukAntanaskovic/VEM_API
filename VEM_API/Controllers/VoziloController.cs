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
    public class VoziloController : ApiController
    {

        private readonly IVoziloRepository _voziloRepository;

        public VoziloController(IVoziloRepository voziloRepository)
        {
            _voziloRepository = voziloRepository;
        }

        [Route("api/TMS/Vozilo/GetAllVozila")]
        [BasicAuthentication]
        public IEnumerable<LVozilo> Get()
        {
            return _voziloRepository.GetAllVozila();
        }

        // GET api/<controller>/5
        [Route("api/TMS/Vozilo/GetVoziloByParametar/{parametar}")]
        [BasicAuthentication]
        public IEnumerable<LVozilo> Get(string parametar)
        {
            return _voziloRepository.GetVoziloByParametar(parametar);
        }

        // POST api/<controller>
        [Route("api/TMS/Vozilo/CreateNewVozilo")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]LVozilo vozilo)
        {
            if (_voziloRepository.CreateNewVozilo(vozilo))
            {
                return Request.CreateResponse(HttpStatusCode.Created, VoziloRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, VoziloRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/TMS/Vozilo/UpdateVozilo/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]LVozilo vozilo)
        {
            if (_voziloRepository.UpdateVozilo(id, vozilo))
            {
                return Request.CreateResponse(HttpStatusCode.Created, VoziloRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, VoziloRepository.ErrorResponseMessage);
            }
        }
    }
}