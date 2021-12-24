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
    public class KomitentiController : ApiController
    {
        private readonly IKomitentRepository _komitentRepository;

        public KomitentiController(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }
        // GET api/<controller>
        [Route("api/WMS/Komitenti/GetAllKomitenti")]
        [BasicAuthentication]
        public IEnumerable<KomitentDTO> Get()
        {
            return _komitentRepository.GetAllKomitent();
        }

        // GET api/<controller>/5
        [Route("api/WMS/Komitenti/GetKomitentByParameter/{parametar}")]
        [BasicAuthentication]
        public IEnumerable<KomitentDTO> Get(string parametar)
        {
            return _komitentRepository.GetKomitentByParameter(parametar);
        }

        // POST api/<controller>
        [Route("api/WMS/Komitenti/CreateNewKomitent")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] KomitentDTO komitent)
        {
            if (_komitentRepository.CreateNewKomitent(komitent))
            {
                return Request.CreateResponse(HttpStatusCode.Created, KomitentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KomitentRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/Komitenti/UpdateKomitent/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody] KomitentDTO komitent)
        {
            if (_komitentRepository.UpdateKomitent(id, komitent))
            {
                return Request.CreateResponse(HttpStatusCode.OK, KomitentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KomitentRepository.ErrorResponseMessage);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/WMS/Komitenti/ActivateDeactivateKomitent/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            if (_komitentRepository.ActivateDeactivateKomitent(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, KomitentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KomitentRepository.ErrorResponseMessage);
            }
        }

        [Route("api/WMS/Komitenti/GetAllDobavljac")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<KomitentDTO> GetAllDobavljac()
        {
            return _komitentRepository.GetAllDobavljac();
        }

        [Route("api/WMS/Komitenti/GetDobaljacByParametar/{parametar}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<KomitentDTO> GetDobavljacByParametar(string parametar)
        {
            return _komitentRepository.GetDobavljacByParametar(parametar);
        }
    }
}