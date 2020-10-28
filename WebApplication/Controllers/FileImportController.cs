using Application.Directories;
using Domain.Dtos;
using Domain.Logging;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class FileImportController : Controller
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentApplication directoryContentApplication;
        public FileImportController(DirectoryContentApplication directoryContentApplication)
        {
            this.directoryContentApplication = directoryContentApplication;
        }

        [HttpGet("getRackInfo")]
        public RackInfoDto GetRackInfo(string rackHash)
        {
            return directoryContentApplication.GetRackInfo(rackHash);
        }
    }
}
