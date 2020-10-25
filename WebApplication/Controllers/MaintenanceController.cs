using System.Collections.Generic;
using Domain.Dtos.Maintenance;
using Domain.Logging;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DatabaseInfoApplication databaseInfoApplication;
        public MaintenanceController(DatabaseInfoApplication databaseInfoApplication)
        {
            this.databaseInfoApplication = databaseInfoApplication;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<DatabaseInfoDto> Get()
        {
            return databaseInfoApplication.GetAllDatabases();
        }

        [HttpGet("createNewDatabase")]
        public IEnumerable<DatabaseInfoDto> CreateNewDatabase(string databaseName)
        {
            Log.Info($"{nameof(CreateNewDatabase)} {databaseName}");
            var notUsed = databaseInfoApplication.CreateNewDatabase(databaseName);
            return Get();
        }

        [HttpPost]
        public IEnumerable<DatabaseInfoDto> SaveDatabase(DatabaseInfoDto databaseDto)
        {
            Log.Info($"{nameof(SaveDatabase)} {databaseDto}");
            var notUsed = databaseInfoApplication.PersistDatabase(databaseDto);
            return Get();
        }

        [HttpPost("addNewRack")]
        public IEnumerable<DatabaseInfoDto> AddNewRack(DatabaseInfoDto databaseDto)
        {
            Log.Info($"{nameof(AddNewRack)} {databaseDto}");
            var notUsed = databaseInfoApplication.AddNewRack(databaseDto);
            return Get();
        }

        public class NewMountPointDto
        {
            public string databaseHash { get; set; }
            public string rackHash { get; set; }
        }

        [HttpPost("addNewMountPoint")]
        public IEnumerable<DatabaseInfoDto> AddNewMountPoint(NewMountPointDto dto)
        {
            Log.Info($"{nameof(AddNewMountPoint)} {dto.databaseHash} {dto.rackHash}");
            var notUsed = databaseInfoApplication.AddNewMountPoint(dto.databaseHash, dto.rackHash);
            return Get();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
