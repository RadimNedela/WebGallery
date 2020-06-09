using Domain.Dtos;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class FileInfoBuilder
    {
        private readonly IHasher _hasher;

        public FileInfoBuilder(IHasher hasher)
        {
            _hasher = hasher;
        }

        public class Inner
        {
            FileInfoBuilder _outer;
            internal Inner(FileInfoBuilder outer)
            {
                _outer = outer;
            }

            private string _path;
            public Inner UsingPath(string path)
            {
                _path = path;
                return this;
            }

            public FileInfoDto Build()
            {
                var retVal = new FileInfoDto { FileName = _path };
                if (_path.ToUpper().EndsWith(".JPG"))
                {
                    retVal.IsDisplayableAsImage = true;
                    retVal.Checksum = _outer._hasher.GetImageHash(_path);
                }
                return retVal;
            }
        }

        public Inner UsingPath(string path)
        {
            return new Inner(this).UsingPath(path);
        }
    }
}