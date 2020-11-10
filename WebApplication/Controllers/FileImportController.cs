using Application.Directories;
using Domain.Dtos;
using Domain.Dtos.Directories;
using Domain.Logging;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("parseDirectoryContent")]
        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContent(DirectoriesCallDto dto)
        {
            databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return await directoryContentApplication.ParseDirectoryContentAsync(dto.SubDirectory);
        }

        [HttpGet("getThreadInfo")]
        public DirectoryContentThreadInfoDto GetThreadInfo(DirectoriesCallDto dto)
        {
            databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return directoryContentApplication.GetThreadInfo(dto.SubDirectory);
        }

    }
}
