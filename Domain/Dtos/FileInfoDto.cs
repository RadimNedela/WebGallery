namespace Domain.Dtos
{
    public class FileInfoDto : DirectoryElementDto
    {
        public bool IsDisplayableAsImage { get; set; } = false;
        public string Checksum { get; set; }
    }
}