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
    public class ArtikalController : ApiController
    {
        private readonly IArtikalRepository _artikalRepository;

        public ArtikalController(IArtikalRepository artikalRepository)
        {
            _artikalRepository = artikalRepository;
        }

        // GET api/<controller>
        [Route("api/WMS/Artikal/GetAllArtikal")]
        [BasicAuthentication]
        public IEnumerable<ArtikalDTO> Get()
        {
            return _artikalRepository.GetAllArtikal();
        }

        // GET api/<controller>/5
        [Route("api/WMS/Artikal/GetArtikalByParameter/{parametar}")]
        [BasicAuthentication]
        public IEnumerable<ArtikalDTO> Get(string parametar)
        {
            return _artikalRepository.GetArtikalBySifra(parametar);
        }

        // POST api/<controller>
        [Route("api/WMS/Artikal/CreateNewArtikal")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]ArtikalDTO artikal)
        {
            if (_artikalRepository.CreateNewArtikal(artikal))
            {
                return Request.CreateResponse(HttpStatusCode.Created, ArtikalRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ArtikalRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/Artikal/UpdateArtikal/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]ArtikalDTO artikal)
        {
            if (_artikalRepository.UpdateArtikal(id, artikal.art_naziv, artikal.art_proizvodjac, artikal.art_ean, artikal.dobavljac.kom_sifra, artikal.art_tip, artikal.art_aktivan))
            {
                return Request.CreateResponse(HttpStatusCode.OK, ArtikalRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ArtikalRepository.ErrorResponseMessage);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/WMS/Artikal/ActivateDeactivateArtikal/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            if (_artikalRepository.ActivateDeactivateArtikal(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, ArtikalRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ArtikalRepository.ErrorResponseMessage);
            }
        }
    }
}