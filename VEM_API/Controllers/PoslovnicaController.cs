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
    public class PoslovnicaController : ApiController
    {
        private readonly IPoslovnicaRepository _poslovnicaRepository;

        public PoslovnicaController(IPoslovnicaRepository poslovnicaRepository)
        {
            _poslovnicaRepository = poslovnicaRepository;
        }
        // GET api/<controller>
        [Route("api/WMS/Poslovnica/GetAllPoslovnica")]
        [BasicAuthentication]
        public IEnumerable<LPoslovnica> Get()
        {
            return _poslovnicaRepository.GetAllPoslovnica();
        }

        [Route("api/WMS/Poslovnica/GetAllAktivnePoslovnicePoslovnica")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LPoslovnica> GetAktivnaPoslovnica()
        {
            return _poslovnicaRepository.GetAllPoslovnica().Where(x => x.psl_aktivna == true);
        }

        [Route("api/WMS/Poslovnica/GetPoslovnicaByParametar/{parametar}")]
        [BasicAuthentication]
        public IEnumerable<LPoslovnica> Get(string parametar)
        {
            return _poslovnicaRepository.GetPoslovnicaByParametar(parametar);
        }

        // POST api/<controller>
        [Route("api/WMS/Poslovnica/AddNewPoslovnica")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] LPoslovnica poslovnica)
        {
            if (_poslovnicaRepository.AddNewPoslovnica(poslovnica))
            {
                return Request.CreateResponse(HttpStatusCode.Created, PoslovnicaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, PoslovnicaRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/Poslovnica/UpdatePoslovnica/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody] LPoslovnica poslovnica)
        {
            if (_poslovnicaRepository.UpdatePoslovnica(id, poslovnica))
            {
                return Request.CreateResponse(HttpStatusCode.OK, PoslovnicaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, PoslovnicaRepository.ErrorResponseMessage);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/WMS/Poslovnica/ActivateDeactivatePoslovnica/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            if (_poslovnicaRepository.ActivateDeactivatePoslovnica(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, PoslovnicaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, PoslovnicaRepository.ErrorResponseMessage);
            }
        }
    }
}