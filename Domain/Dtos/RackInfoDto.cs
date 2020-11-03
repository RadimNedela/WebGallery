namespace Domain.Dtos
{
    public class RackInfoDto
    {
        public string ActiveDatabaseName { get; set; }
        public string ActiveDatabaseHash { get; set; }
        public string ActiveRackName { get; set; }
        public string ActiveRackHash { get; set; }
        public string ActiveDirectory { get; set; }
        public DirectoryInfoDto DirectoryInfo { get; set; }
    }
}
