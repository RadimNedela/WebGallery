using Application.Directories;
using Domain.Dtos;
using Domain.Dtos.Directories;
using Domain.Logging;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class FileImportController : Controller
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentApplication directoryContentApplication;
        private readonly IDatabaseInfoInitializer databaseInfoInitializer;
        public FileImportController(
            DirectoryContentApplication directoryContentApplication,
            IDatabaseInfoInitializer databaseInfoInitializer)
        {
            this.directoryContentApplication = directoryContentApplication;
            this.databaseInfoInitializer = databaseInfoInitializer;
        }

        [HttpGet("getRackInfo")]
        public RackInfoDto GetRackInfo(string rackHash)
        {
            databaseInfoInitializer.SetCurrentInfo(rackHash);
            return directoryContentApplication.GetCurrentRackInfo();
        }

        [HttpGet("getDirectoryInfo")]
        public DirectoryInfoDto GetDirectoryInfo(DirectoriesCallDto dto)
        {
            databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return directoryContentApplication.GetSubDirectoryInfo(dto.SubDirectory);
        }
    }
}
