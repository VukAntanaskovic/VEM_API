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
    public class DokumentiController : ApiController
    {
        private readonly IDokumentRepository _dokumentRepository;

        #region "Dokument"
        public DokumentiController(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        // GET api/<controller>
        [Route("api/EDI/Dokumenti/GetAllDokumenti")]
        [BasicAuthentication]
        public IEnumerable<DokumentDTO> Get()
        {
            return _dokumentRepository.GetAllDokument();
        }

        [Route("api/EDI/Dokumenti/GetAllPotvrdjenDokument")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<DokumentDTO> GetAllPotvrdjenDokument()
        {
            return _dokumentRepository.GetAllPotvrdjenDokument();
        }

        // GET api/<controller>/5
        [Route("api/EDI/Dokumenti/GetDokumentById/{id}")]
        [BasicAuthentication]
        public IEnumerable<DokumentDTO> Get(string id)
        {
            return _dokumentRepository.GetDokumentById(id);
        }

        [Route("api/EDI/Dokumenti/GetAllPotvrdjenDokumentById/{id}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<DokumentDTO> GetAllPotvrdjenDokumentById(string id)
        {
            return _dokumentRepository.GetPotvrdjeniDokumentById(id);
        }

        // POST api/<controller>
        [Route("api/EDI/Dokumenti/CreateRequestDocument")]
        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] DokumentDTO dokument)
        {
            if (_dokumentRepository.CreateRequestDocument(dokument, null))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        // PUT api/<controller>/5
        [Route("api/EDI/Dokumenti/ConfirmDocument/{id}/{korisnik}")]
        [BasicAuthentication]
        public HttpResponseMessage Put(int id, int korisnik)
        {
            if (_dokumentRepository.ConfirmDocument(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        [Route("api/EDI/Dokumenti/CreateLicnoPreuzimanje/{id}/{korisnik}")]
        [BasicAuthentication]
        [HttpPut]
        public HttpResponseMessage CreateLicnoPreuzimanje(int id, int korisnik)
        {
            if (_dokumentRepository.CreateLicnoPreuzimanje(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/EDI/Dokumenti/CloseDocument/{id}")]
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            if (_dokumentRepository.CloseDocument(id))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        #endregion "Dokument"

        #region "Stavke dokumenta"
        [Route("api/EDI/StavkeDokumenta/GetAllStavkeDokument/{id}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LStavkeDokumenta> GetAllStavkeDokument(int id)
        {
            return _dokumentRepository.GetAllStavkeDokument(id);
        }

        [Route("api/EDI/StavkeDokumenta/CreateNewStavkaDokumenta")]
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage CreateNewStavkaDokumenta([FromBody] LStavkeDokumenta stavke)
        {
            if (_dokumentRepository.CreateNewStavkaDokumenta(stavke))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        [Route("api/EDI/StavkeDokumenta/DeleteStavka/{id}")]
        [BasicAuthentication]
        [HttpDelete]
        public HttpResponseMessage DeleteStavka(int id)
        {
            if (_dokumentRepository.DeleteStavka(id))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }
        #endregion "Stavke dokumenta"

        #region "Tip dokumenta"
        [Route("api/EDI/TipDokumenta/GetAllTipDokumenta")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LTipDokumenta> GetAllTipDokumenta()
        {
            return _dokumentRepository.GetAllTipDokumenta();
        }
        #endregion "Tip dokumenta"

        #region "Vezni dokumenti"
        [Route("api/EDI/VezniDokumenti/GetAllVezniDokumenti/{id}")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<DokumentDTO> GetAllVezniDokumenti(int id)
        {
            return _dokumentRepository.GetAllVezniDokumenti(id);
        }

        // POST api/<controller>
        [Route("api/EDI/VezniDokumenti/ConfirmDocument/{id}")]
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage ConfirmDocument(int id, int korisnik)
        {
            if (_dokumentRepository.ConfirmDocument(id, korisnik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }
        #endregion "Vezni dokumenti"

        #region "Interni prenos"
        [Route("api/EDI/Dokumenti/CreateNewPrenos/{na_poslovnicu}")]
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage CreateNewPrenos([FromBody] DokumentDTO dokument, int na_poslovnicu)
        {
            if (_dokumentRepository.CreateRequestDocument(dokument, na_poslovnicu))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        [Route("api/EDI/Dokumenti/CreateNewInterniPrijem/{Id}")]
        [BasicAuthentication]
        [HttpGet]
        public HttpResponseMessage CreateNewInterniPrijem(int id)
        {
            if (_dokumentRepository.CreateNewInterniPrijem(id))
            {
                return Request.CreateResponse(HttpStatusCode.Created, DokumentRepository.OkResponseMessage);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, DokumentRepository.ErrorResponseMessage);
            }
        }

        [Route("api/EDI/Dokumenti/GetAllOtvoreniPrenos")]
        [BasicAuthentication]
        [HttpGet]
        public IEnumerable<LInterniPrenos> GetAllOtvoreniPrenos()
        {
            return _dokumentRepository.GetAllOtvoreniPrenos();
        }

        #endregion "Interni prenos"
    }
}