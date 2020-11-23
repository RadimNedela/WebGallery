﻿using System.Threading.Tasks;
using Application.Directories;
using Domain.Dtos;
using Domain.Dtos.Directories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
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
        public RackInfoDto GetRackInfo(string rackHash)
        {
            _databaseInfoInitializer.SetCurrentInfo(rackHash);
            return _directoryContentApplication.GetCurrentRackInfo();
        }

        [HttpGet("getDirectoryInfo")]
        public DirectoryInfoDto GetDirectoryInfo(DirectoriesCallDto dto)
        {
            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return _directoryContentApplication.GetSubDirectoryInfo(dto.SubDirectory);
        }

        [HttpGet("parseDirectoryContent")]
        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContent(DirectoriesCallDto dto)
        {
            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return await _directoryContentApplication.ParseDirectoryContentAsync(dto.SubDirectory);
        }

        [HttpGet("getThreadInfo")]
        public DirectoryContentThreadInfoDto GetThreadInfo(DirectoriesCallDto dto)
        {
            _databaseInfoInitializer.SetCurrentInfo(dto.RackHash);
            return _directoryContentApplication.GetThreadInfo(dto.SubDirectory);
        }

    }
}
