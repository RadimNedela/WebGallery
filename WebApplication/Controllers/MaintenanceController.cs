using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using WebGalery.Application.Maintenance;
using WebGalery.Core.Logging;

namespace WebGalery.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DatabaseInfoApplication _databaseInfoApplication;
        public MaintenanceController(DatabaseInfoApplication databaseInfoApplication)
        {
            _databaseInfoApplication = databaseInfoApplication;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<DatabaseInfoDto> Get()
        {
            return _databaseInfoApplication.GetAllDatabases();
        }

        [HttpGet("createNewDatabase")]
        public IEnumerable<DatabaseInfoDto> CreateNewDatabase(string databaseName)
        {
            Log.Info($"{nameof(CreateNewDatabase)} {databaseName}");
            _databaseInfoApplication.CreateNewDatabase(databaseName);
            return Get();
        }

        [HttpPost]
        public IEnumerable<DatabaseInfoDto> SaveDatabase(DatabaseInfoDto databaseDto)
        {
            Log.Info($"{nameof(SaveDatabase)} {databaseDto}");
            _databaseInfoApplication.UpdateDatabaseNames(databaseDto);
            return Get();
        }

        [HttpPost("addNewRack")]
        public IEnumerable<DatabaseInfoDto> AddNewRack(DatabaseInfoDto databaseDto)
        {
            Log.Info($"{nameof(AddNewRack)} {databaseDto}");
            _databaseInfoApplication.AddNewRack(databaseDto);
            return Get();
        }

        public class NewMountPointDto
        {
            public string DatabaseHash { get; set; }
            public string RackHash { get; set; }
        }

        [HttpPost("addNewMountPoint")]
        public IEnumerable<DatabaseInfoDto> AddNewMountPoint(NewMountPointDto dto)
        {
            Log.Info($"{nameof(AddNewMountPoint)} {dto.DatabaseHash} {dto.RackHash}");
            _databaseInfoApplication.AddNewMountPoint(dto.DatabaseHash, dto.RackHash);
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
