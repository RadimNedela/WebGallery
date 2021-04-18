using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Application.Maintenance
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

        public void Merge(DatabaseInfoEntity dbEntity, DatabaseInfoDto dto)
        {
            dbEntity.Name = dto.Name;
            foreach (var rack in dbEntity.Racks)
            {
                var newRack = dto.Racks.First(nr => nr.Hash == rack.Hash);
                rack.Name = newRack.Name;
                for (var i=0; i<rack.MountPoints.Count; i++)
                {
                    rack.MountPoints[i].Path = newRack.MountPoints[i];
                }
            }
        }
    }
}
