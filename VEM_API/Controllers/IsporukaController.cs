using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VEM_API.DbModel;
using VEM_API.EdiRepositories;
using VEM_API.Models;

namespace VEM_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: Settings.appUrl, headers: "*", methods: "*")]
    public class IsporukaController : ApiController
    {
        private readonly IIsporukaRepository _isporukaRepository;

        public IsporukaController(IIsporukaRepository isporukaRepository)
        {
            _isporukaRepository = isporukaRepository;
        }

        // GET api/<controller>
        [Route("api/TMS/Isporuke/GetAllUnetaIsporuka/{status}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LIsporuka> GetAllIsporukaByStatus(int status)
        {
            return _isporukaRepository.GetAllIsporukaByStatus(status);
        }


        [Route("api/TMS/Isporuke/CheckStatusIsporuke")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LIsporuka> CheckStatusIsporuke(int? isporuka, DateTime? datum)
        {
            return _isporukaRepository.CheckStatusIsporuke(isporuka, datum);
        }

        [Route("api/TMS/Isporuke/GetAllTovarniList")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LTovarniList> GetAllTL()
        {
            return _isporukaRepository.GetAllTovarniList();
        }

        // POST api/<controller>
        [Route("api/TMS/Isporuke/CreateNewTL")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody]Tovarni_List tovanriList)
        {
            if (_isporukaRepository.CreateNewTL(tovanriList))
            {
                return Request.CreateResponse(HttpStatusCode.Created, IsporukaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, IsporukaRepository.ErrorResponseMessage);
            }
        }

        [Route("api/TMS/Isporuke/GetStavkeTl/{tl}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LStavkeTovarnogLista> GetStavkeTl(int tl)
        {
            return _isporukaRepository.GetAllStavkeTL(tl);
        }



        [Route("api/TMS/Isporuke/AddIsporukaToTL/{isporuka}/{tl}")]
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage AddIsporukaToTL(int isporuka, int tl)
        {
            if (_isporukaRepository.AddIsporukaToTL(isporuka, tl))
            {
                return Request.CreateResponse(HttpStatusCode.Created, IsporukaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, IsporukaRepository.ErrorResponseMessage);
            }
        }

        [Route("api/TMS/Isporuke/ResetIsporuka/{isporuka}/{tl}")]
        [BasicAuthentication]
        [HttpDelete]
        public HttpResponseMessage ResetIsporuka(int isporuka, int tl)
        {
            if (_isporukaRepository.ResetIsporuka(isporuka, tl))
            {
                return Request.CreateResponse(HttpStatusCode.OK, IsporukaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, IsporukaRepository.ErrorResponseMessage);
            }
        }

        [Route("api/TMS/Isporuke/StatusRequest/{isporuka}/{status}")]
        [BasicAuthentication]
        [HttpPut]
        public HttpResponseMessage SetStatus(int isporuka, string status)
        {
            if (_isporukaRepository.StatusRequest(isporuka, status))
            {
                return Request.CreateResponse(HttpStatusCode.OK, IsporukaRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, IsporukaRepository.ErrorResponseMessage);
            }
        }
    }
}