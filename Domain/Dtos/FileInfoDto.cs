namespace Domain.Dtos
{
    public class FileInfoDto : DirectoryElementDto
    {
        public bool IsDisplayableAsImage { get; set; } = false;
    }
}