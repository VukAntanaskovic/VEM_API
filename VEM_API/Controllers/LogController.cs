using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: Settings.appUrl, headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        private readonly ILogProvider _logProvider;

        public LogController(ILogProvider logProvider)
        {
            _logProvider = logProvider;
        }
        // GET api/<controller>
        [Route("api/Log/GetLog")]
        [BasicAuthentication]
        public IEnumerable<log_VEM> Get()
        {
            return _logProvider.GetLog();
        }
    }
}