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
    public class RafController : ApiController
    {
        private readonly IRafRepository _rafRepository;

        public RafController(IRafRepository rafRepository)
        {
            _rafRepository = rafRepository;
        }

        // GET api/<controller>
        [Route("api/WMS/Raf/GetAllRaf")]
        [BasicAuthentication]
        public IEnumerable<LRaf> Get()
        {
            return _rafRepository.GetAllRaf().ToList();
        }

        // GET api/<controller>/5
        [Route("api/WMS/Raf/GetAllRafInPoslovnica/{id}")]
        [BasicAuthentication]
        public IEnumerable<LRaf> Get(int id, string parametar)
        {
            return _rafRepository.GetAllRafInPoslovnica(id, parametar);
        }

        // POST api/<controller>
        [Route("api/WMS/Raf/CreateNewRaf")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]LRaf raf)
        {
            if (_rafRepository.CreateNewRaf(raf))
            {
                return Request.CreateResponse(HttpStatusCode.Created, RafRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, RafRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/Raf/UpdateRaf/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody] LRaf lokacija)
        {
            if (_rafRepository.UpdateRaf(id, lokacija))
            {
                return Request.CreateResponse(HttpStatusCode.Created, RafRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, RafRepository.ErrorResponseMessage);
            }
        }
    }
}