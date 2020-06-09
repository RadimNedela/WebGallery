using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Directories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class DirectoriesController : Controller
    {
        private DirectoryContentApplication _directoryContentApplication;
        private PhysicalFileApplication _physicalFileApplication;

        public DirectoriesController(
            DirectoryContentApplication directoryContentApplication,
            PhysicalFileApplication physicalFileApplication)
        {
            _directoryContentApplication = directoryContentApplication;
            _physicalFileApplication = physicalFileApplication;
        }

        [HttpGet("getImage/{hash}")]
        public FileStreamResult GetImage(string hash)
        {
            var fileName = _physicalFileApplication.GetFileName(hash);
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string mimeType);

            var stream = System.IO.File.OpenRead(fileName);

            return new FileStreamResult(stream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(mimeType));
        }

        [HttpGet("getFileStream")]
        public FileStreamResult DownloadAsync()
        {
            var fileName = "/home/radim/Source/WebGalery/TestPictures/Duha/2017-08-20-Duha0367.JPG";
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
            return _directoryContentApplication.GetDirectoryContent("").Select(dto => dto.FileName);
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
