using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Maintenance.Applications.InternalServices
{
    internal class DatabaseInfoDtoConverter
    {
        internal DatabaseInfoDto ToDto(DatabaseInfoEntity entity)
        {
            DatabaseInfoDto dto = new()
            {
                Hash = entity.Hash,
                Name = entity.Name,
                Racks = entity.Racks.Select(rackEntity => ToDto(rackEntity)).ToList()
            };
            return dto;
        }

        private RackDto ToDto(RackEntity entity)
        {
            RackDto dto = new()
            {
                Hash = entity.Hash,
                Name = entity.Name,
                MountPoints = entity.MountPoints.Select(mpEntity => mpEntity.Path).ToList()
            };
            return dto;
        }
    }
}
