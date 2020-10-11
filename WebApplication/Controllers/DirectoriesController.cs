using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Directories;
using Domain.Dtos;
using Domain.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class DirectoriesController : Controller
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentApplication _directoryContentApplication;
        private readonly PhysicalFileApplication _physicalFileApplication;

        public DirectoriesController(
            DirectoryContentApplication directoryContentApplication,
            PhysicalFileApplication physicalFileApplication)
        {
            _directoryContentApplication = directoryContentApplication;
            _physicalFileApplication = physicalFileApplication;
        }

        [HttpGet("getDirectoryContent")]
        public DisplayableInfoDto GetDirectoryContent(string directoryName)
        {
            Log.Info($"GetDirectoryContent {directoryName}");
            var retVal = _directoryContentApplication.GetDirectoryContent(directoryName);
            Log.Info(retVal);
            return retVal;
        }


        [HttpGet("getImage/{hash}")]
        public string GetImage(string hash)
        {
            var content = _physicalFileApplication.GetContent(hash);
            byte[] b = System.IO.File.ReadAllBytes(content.FilePath);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }


        //[HttpGet("getImage/{hash}")]
        //public FileStreamResult GetImage(string hash)
        //{
        //    var content = _physicalFileApplication.GetContent(hash);

        //    new FileExtensionContentTypeProvider().TryGetContentType(content.FilePath, out string mimeType);
        //    var stream = System.IO.File.OpenRead(content.FilePath);

        //    return new FileStreamResult(stream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(mimeType));
        //}

        [HttpGet("getFileStream")]
        public FileStreamResult DownloadAsync()
        {
            //var fileName = "/home/radim/Source/WebGalery/TestPictures/Duha/2017-08-20-Duha0367.JPG";
            var fileName = "d:\\Sources\\WebGallery\\TestPictures\\Duha\\2017-08-20-Duha0367.JPG";
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string mimeType);

            var stream = System.IO.File.OpenRead(fileName);

            return new FileStreamResult(stream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(mimeType))
            {
                //FileDownloadName = fileName
            };
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _directoryContentApplication.GetDirectoryContent("").ContentInfos.Select(dc => dc.Label);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
