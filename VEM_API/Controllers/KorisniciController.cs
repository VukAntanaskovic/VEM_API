using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VEM_API.DbModel;
using VEM_API.Models;
using VEM_API.Repositories;

namespace VEM_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: Settings.appUrl, headers: "*", methods: "*")]
    public class KorisniciController : ApiController
    {
        private readonly IKorisnikRepository _korisnikRepository;

        public KorisniciController(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        // GET api/<controller>
        [Route("api/WMS/Korisnici/GetAllKorisnici")]
        [BasicAuthentication]
        public IEnumerable<KorisnikDTO> Get()
        {
            return _korisnikRepository.GetAllKorisnik();
        }

        // GET api/<controller>/5
        [Route("api/WMS/Korisnici/GetKorisnikByParametar/{parametar}")]
        [BasicAuthentication]
        public IEnumerable<KorisnikDTO> Get(string parametar)
        {
            return _korisnikRepository.GetKorisnikByParametar(parametar);
        }

        // POST api/<controller>
        [Route("api/WMS/Korisnici/CreateNewKorisnik")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]KorisnikDTO korisnik)
        {
            if (_korisnikRepository.CreateNewKorisnik(korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, KorisnikRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KorisnikRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/WMS/Korisnici/ChangePassword/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]KorisnikDTO korisnik)
        {
            if (_korisnikRepository.UpdatePasswordKorisnik(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, KorisnikRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KorisnikRepository.ErrorResponseMessage);
            }
        }

        [HttpPut]
        [Route("api/WMS/Korisnici/ChangePoslovnica/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage ChangePoslovnica(int id, [FromBody]KorisnikDTO korisnik)
        {
            if (_korisnikRepository.ChangePoslovnica(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, KorisnikRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KorisnikRepository.ErrorResponseMessage);
            }
        }

        [HttpPut]
        [Route("api/WMS/Korisnici/UpdateKorisnik/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage UpdateKorisnik(int id, [FromBody]KorisnikDTO korisnik)
        {
            if (_korisnikRepository.UpdateKorisnik(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, KorisnikRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, KorisnikRepository.ErrorResponseMessage);
            }
        }

        [HttpGet]
        [Route("api/WMS/Korisnik/GetAllRola")]
        [BasicAuthentication]
        public IEnumerable<AutorizacijaKorisnikaDTO> GetAllRola()
        {
            return _korisnikRepository.GetAllRola();
        }
    }
}