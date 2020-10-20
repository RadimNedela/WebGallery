using Domain.Dtos.Maintenance;
using System.Linq;

namespace Domain.Elements.Maintenance
{
    public static class DatabaseInfoElementExtensions
    {
        public static RackDto ToDto(this RackElement element)
        {
            var dto = new RackDto().Initialize(
                element.Hash,
                element.Name,
                element.MountPoints.Select(mp => mp).ToList()
                );

            return dto;
        }

        public static DatabaseInfoDto ToDto(this DatabaseInfoElement element)
        {
            var dto = new DatabaseInfoDto().Initialize(
                element.Hash,
                element.Name,
                element.Racks.Select(re => re.ToDto()).ToList());

            return dto;
        }
    }
}
