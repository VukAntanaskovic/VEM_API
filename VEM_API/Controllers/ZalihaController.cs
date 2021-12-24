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
    public class ZalihaController : ApiController
    {
        private readonly IZalihaRepository _zalihaRepository;

        public ZalihaController(IZalihaRepository zalihaRepository)
        {
            _zalihaRepository = zalihaRepository;
        }

        // GET api/<controller>/5
        [Route("api/WMS/ZalihaArtikla/GetAllZaliheArtikla/{psl_sifra}")]
        [BasicAuthentication]
        public IEnumerable<LZalihaArtikla> Get(int psl_sifra, string parametar)
        {
            if (parametar == null)
            {
                return _zalihaRepository.GetAllZaliheArtikla(psl_sifra).ToList();
            }
            else
            {
                return _zalihaRepository.FindZalihaArtikla(psl_sifra, parametar).ToList();
            }
        }

        // POST api/<controller>
        [Route("api/WMS/ZalihaArtikla/AddToZalihaArtikla")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] LZalihaArtikla zaliha)
        {
            if (_zalihaRepository.AddToZalihaArtikla(zaliha))
            {
                return Request.CreateResponse(HttpStatusCode.Created, ZalihaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ZalihaRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/ZalihaArtikla/AddToRaf/{artikal}/{poslovnica}/{raf}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int artikal, int poslovnica, int raf)
        {
            if (_zalihaRepository.AddToRaf(artikal, poslovnica, raf))
            {
                return Request.CreateResponse(HttpStatusCode.Created, ZalihaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ZalihaRepository.ErrorResponseMessage);
            }
        }

        // DELETE api/<controller>/5
    }
}