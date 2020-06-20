using System;
using Domain.Elements;
using Domain.Entities;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class HashedElementBuilder
    {
        private readonly IHasher _hasher;
        private readonly IHashedEntitiesRepository _hashedRepository;

        public HashedElementBuilder(IHasher hasher, IHashedEntitiesRepository hashedRepository)
        {
            _hasher = hasher;
            _hashedRepository = hashedRepository;
        }

        public class Inner
        {
            HashedElementBuilder _outer;
            internal Inner(HashedElementBuilder outer)
            {
                _outer = outer;
            }

            private string _path;
            private Hashed _hashed;
            private string _directory;

            public Inner UsingFilePath(string path)
            {
                _path = path;
                return this;
            }

            public Inner UsingDirectory(string directory)
            {
                _directory = directory;
                return this;
            }

            public HashedElement Build()
            {
                if (!string.IsNullOrWhiteSpace(_path))
                    return _outer.BuildHashedFile(_path);
                else if (_hashed != null)
                    return _outer.BuildFromExisting(_hashed);
                else if (!string.IsNullOrWhiteSpace(_directory))
                    return _outer.BuildDirectory(_directory);
                throw new NotImplementedException();
            }

            internal Inner UsingHashed(Hashed hashed)
            {
                _hashed = hashed;
                return this;
            }
        }

        public Inner UsingDirectory(string directoryName) => new Inner(this).UsingDirectory(directoryName);

        public Inner UsingFilePath(string path) => new Inner(this).UsingFilePath(path);

        public Inner UsingHashed(Hashed hashed) => new Inner(this).UsingHashed(hashed);

        public HashedElement BuildFromExisting(Hashed hashed)
        {
            return new HashedElement(_hashedRepository).InitializeFrom(hashed);
        }

        public HashedElement BuildDirectory(string directoryPath)
        {
            string hash = _hasher.ComputeDirectoryHash(directoryPath);
            var hashed = _hashedRepository.Get(hash);

            HashedElement retVal;
            if (hashed != null)
                retVal = UsingHashed(hashed).Build();
            else
                retVal = new HashedElement(_hashedRepository).InitializeUsing(hash).IsDirectory();

            retVal.AddPath(directoryPath);

            return retVal;
        }

        public HashedElement BuildHashedFile(string filePath)
        {
            string hash = _hasher.ComputeFileContentHash(filePath);
            var hashed = _hashedRepository.Get(hash);

            HashedElement retVal;
            if (hashed != null)
                retVal = UsingHashed(hashed).Build();
            else
                retVal = new HashedElement(_hashedRepository).InitializeUsing(hash).IsFile();

            retVal.AddPath(filePath);

            return retVal;
        }
    }
}