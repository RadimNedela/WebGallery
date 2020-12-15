using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Maintenance;
using Domain.Elements.Maintenance;
using Domain.Services;
using Domain.Services.InfrastructureInterfaces;
using WebGalery.Maintenance.Services;

namespace Application.Maintenance
{
    public class DatabaseInfoApplication
    {
        private readonly IDatabaseInfoEntityRepository _repository;
        private readonly DatabaseInfoDtoConverter _converter;
        private readonly IDirectoryMethods _directoryMethods;

        public DatabaseInfoApplication(
            IDatabaseInfoEntityRepository repository,
            DatabaseInfoDtoConverter converter,
            IDirectoryMethods directoryMethods)
        {
            _repository = repository;
            _converter = converter;
            _directoryMethods = directoryMethods;
        }

        public IEnumerable<DatabaseInfoDto> GetAllDatabases()
        {
            var allDatabases = _repository.GetAll();
            var allDtos = allDatabases.Select(entity => _converter.ToDto(entity));

            return allDtos;
        }

        //public DatabaseInfoDto CreateNewDatabase(string databaseName)
        //{
        //    if (string.IsNullOrEmpty(databaseName))
        //        throw new Exception("Please give correct name to the new created database");
        //    var element = _infoBuilder.BuildNewDatabase(databaseName);
        //    _repository.Add(element.Entity);
        //    _repository.Save();

        //    return element.ToDto();
        //}

        //public DatabaseInfoDto PersistDatabase(DatabaseInfoDto dto)
        //{
        //    var element = _infoBuilder.Create(dto);

        //    _repository.Save();

        //    return element.ToDto();
        //}

        //public DatabaseInfoDto AddNewRack(DatabaseInfoDto dto)
        //{
        //    return AddNewRack(dto.Hash, "Default", _directoryMethods.GetCurrentDirectoryName());
        //}

        //public DatabaseInfoDto AddNewRack(string databaseHash, string name, string initialMountPointPath)
        //{
        //    var element = _infoBuilder.GetDatabase(databaseHash);
        //    element.AddNewRack(name, initialMountPointPath);
        //    _repository.Save();

        //    return element.ToDto();
        //}

        //public object AddNewMountPoint(string databaseHash, string rackHash)
        //{
        //    var database = _infoBuilder.GetDatabase(databaseHash);
        //    var rack = database.Racks.First(r => r.Hash == rackHash);
        //    rack.AddMountPoint(_directoryMethods.GetCurrentDirectoryName());

        //    _repository.Save();

        //    return database.ToDto();
        //}
    }
}
