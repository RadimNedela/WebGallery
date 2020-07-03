using System;
using Domain.Elements;
using Domain.Entities;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public interface IBuilder<out T>
    {
        T Build();
    }

    public class HashedElementBuilder : IBuilder<HashedElement>
    {
        private class Inner
        {
            public string path;
            public Hashed hashed;
            public string directory;
        }

        private readonly IHasher _hasher;
        private readonly IHashedEntitiesRepository _hashedRepository;
        private Inner _parameters;

        public HashedElementBuilder(IHasher hasher, IHashedEntitiesRepository hashedRepository)
        {
            _hasher = hasher;
            _hashedRepository = hashedRepository;
            Clean();
        }

        private void Clean()
        {
            _parameters = new Inner();
        }

        public HashedElementBuilder UsingFilePath(string path)
        {
            _parameters.path = path;
            return this;
        }

        public HashedElementBuilder UsingDirectory(string directory)
        {
            _parameters.directory = directory;
            return this;
        }

        public HashedElementBuilder UsingHashed(Hashed hashed)
        {
            _parameters.hashed = hashed;
            return this;
        }

        public HashedElement Build()
        {
            HashedElement retVal = null;
            
            if (!string.IsNullOrWhiteSpace(_parameters.path))
                retVal = BuildHashedFile(_parameters.path);
            else if (_parameters.hashed != null)
                retVal = BuildFromExisting(_parameters.hashed);
            else if (!string.IsNullOrWhiteSpace(_parameters.directory))
                retVal = BuildDirectory(_parameters.directory);

            Clean();

            if (retVal == null)
                throw new NotImplementedException();

            return retVal;
        }

        private HashedElement BuildFromExisting(Hashed hashed)
        {
            return new HashedElement(_hashedRepository).InitializeFrom(hashed);
        }

        private HashedElement BuildDirectory(string directoryPath)
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

        private HashedElement BuildHashedFile(string filePath)
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