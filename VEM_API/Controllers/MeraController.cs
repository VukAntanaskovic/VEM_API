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
    public class MeraController : ApiController
    {
        private readonly IMernaJedinicaRepository _meraRepository;

        public MeraController(IMernaJedinicaRepository meraRepository)
        {
            _meraRepository = meraRepository;
        }
        [Route("api/WMS/JedinicaMere/GetAllMera")]
        [BasicAuthentication]
        public IEnumerable<JedinicaMereDTO> Get()
        {
            return _meraRepository.GetAllJedinicaMere();
        }

        // GET api/<controller>/5
        [Route("api/WMS/JedinicaMere/GetJedMereByID/{id}")]
        [BasicAuthentication]
        public IEnumerable<JedinicaMereDTO> Get(int id)
        {
            return _meraRepository.GetAllJedinicaMere().Where(x => x.jed_sifra == id);
        }

        // POST api/<controller>
        [Route("api/WMS/JedinicaMere/CreateNewJedinicaMere")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]JedinicaMereDTO jedinicaMere)
        {
            if (_meraRepository.CreateNewJedinicaMere(jedinicaMere))
            {
                return Request.CreateResponse(HttpStatusCode.Created, MernaJedinicaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, MernaJedinicaRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/JedinicaMere/UpdateJedinicaMere/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]JedinicaMereDTO jedinicaMere)
        {
            if (_meraRepository.UpdateJedinicaMere(id, jedinicaMere))
            {
                return Request.CreateResponse(HttpStatusCode.Created, MernaJedinicaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, MernaJedinicaRepository.ErrorResponseMessage);
            }
        }
    }
}