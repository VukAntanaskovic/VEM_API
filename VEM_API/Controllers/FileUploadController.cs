using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VEM_API.LogProvider;
using VEM_API.Models;
using VEM_API.Repositories;

namespace VEM_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: Settings.appUrl, headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        private readonly ILogProvider _logProvider;

        public FileUploadController(ILogProvider logProvider)
        {
            _logProvider = logProvider;
        }

        [Route("api/FileUpload")]
        [BasicAuthentication]
        [HttpPost]
        public async Task<string> UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/img");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    File.Move(localFileName, filePath);
                    _logProvider.AddToLog("UploadFile()", "Uspesan upload fajla: " + filePath, false);
                }
                return "File uplodovan";
            }
            catch (Exception e)
            {
                _logProvider.AddToLog("FileUpload()", "Greska prilikom uploda fajla: " + e.Message, true);
                return $"Error: " + e.Message;
            }
        }
    }
}