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
    public class LoginController : ApiController
    {
        private readonly IKorisnikRepository _korisnikRepository;

        public LoginController(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }
        // GET api/<controller>/5
        [Route("api/User/AuthenticateUser")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]AppUser appUser, string app)
        {
            if (_korisnikRepository.AuthenticateUser(appUser, app))
            {
                return Request.CreateResponse(HttpStatusCode.OK, KorisnikRepository.jsonResponse);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, KorisnikRepository.ErrorResponseMessage);
            }
        }
    }
}