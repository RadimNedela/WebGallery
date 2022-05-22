using Microsoft.AspNetCore.Mvc;
using WebGalery.Application.FileImport;
using WebGalery.Core;
using WebGalery.Core.FileImport;
using WebGalery.Core.FileImport.Directories;

namespace WebGalery.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class FileImportController : Controller
    {
        private readonly FileImportApplication _directoryContentApplication;
        private readonly IGalerySessionInitializer _databaseInfoInitializer;
        public FileImportController(
            FileImportApplication directoryContentApplication,
            IGalerySessionInitializer databaseInfoInitializer)
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
