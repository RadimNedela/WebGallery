using Microsoft.AspNetCore.Mvc;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Application.Dtos;
using WebGalery.FileImport.Application.Dtos.Directories;
using WebGalery.SessionHandling.Applications;

namespace WebGalery.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class FileImportController : Controller
    {
        private readonly DirectoryContentApplication _directoryContentApplication;
        private readonly IDatabaseInfoInitializer _databaseInfoInitializer;
        public FileImportController(
            DirectoryContentApplication directoryContentApplication,
            IDatabaseInfoInitializer databaseInfoInitializer)
        {
            _directoryContentApplication = directoryContentApplication;
            _databaseInfoInitializer = databaseInfoInitializer;
        }

        [HttpGet("getRackInfo")]
        public RackInfoDto GetRackInfo(DirectoriesCallDto dto)
        {
            _databaseInfoInitializer.SetCurrentInfo(dto.DatabaseHash, dto.RackHash);
            return _directoryContentApplication.GetCurrentRackInfo();
        }

        //        [HttpGet("getDirectoryInfo")]
        //        public DirectoryInfoDto GetDirectoryInfo(DirectoriesCallDto dto)
        //        {
        //            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
        //            return _directoryContentApplication.GetSubDirectoryInfo(dto.SubDirectory);
        //        }

        //        [HttpGet("parseDirectoryContent")]
        //        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContent(DirectoriesCallDto dto)
        //        {
        //            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
        //            return await _directoryContentApplication.ParseDirectoryContentAsync(dto.SubDirectory);
        //        }

        //        [HttpGet("getThreadInfo")]
        //        public DirectoryContentThreadInfoDto GetThreadInfo(DirectoriesCallDto dto)
        //        {
        //            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
        //            return _directoryContentApplication.GetThreadInfo(dto.SubDirectory);
        //        }

    }
}
