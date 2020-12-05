namespace Domain.Services
{
    public class DirectoryContentThreadInfoDto
    {
        public int Files { get; set; }
        public long Bytes { get; set; }
        public int FilesDone { get; set; }
        public long BytesDone { get; set; }
    }
}
