using System;
using Domain.Elements;
using Domain.Entities;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class FileInfoBuilder
    {
        private readonly IHasher _hasher;
        private readonly IHashedEntitiesRepository _hashedRepository;

        public FileInfoBuilder(IHasher hasher, IHashedEntitiesRepository hashedRepository)
        {
            _hasher = hasher;
            _hashedRepository = hashedRepository;
        }

        public class Inner
        {
            FileInfoBuilder _outer;
            internal Inner(FileInfoBuilder outer)
            {
                _outer = outer;
            }

            private string _path;
            private Hashed _hashed;

            public Inner UsingPath(string path)
            {
                _path = path;
                return this;
            }

            public HashedElement Build()
            {
                if (!string.IsNullOrWhiteSpace(_path))
                    return _outer.Build(_path);
                //else if (_hashed != null)
                return _outer.Build(_hashed);
            }

            internal Inner UsingHashed(Hashed hashed)
            {
                _hashed = hashed;
                return this;
            }
        }

        public Inner UsingPath(string path) => new Inner(this).UsingPath(path);

        public Inner UsingHashed(Hashed hashed) => new Inner(this).UsingHashed(hashed);

        public HashedElement Build(Hashed hashed)
        {
            var retVal = new HashedElement()
            {
                ID = hashed.Id,
                Hash = hashed.Hash
                //Locations
            };
            return retVal;
        }

        public HashedElement Build(string _path)
        {
            string hash = null;
            if (_hasher.CanHandlePath(_path))
                hash = _hasher.GetImageHash(_path);

            HashedElement retVal = null;
            if (!string.IsNullOrWhiteSpace(hash))
            {
                var hashed = _hashedRepository.GetHashedEntity(hash);
                if (hashed != null) retVal = UsingHashed(hashed).Build();
            }

            if (retVal == null)
                retVal = new HashedElement() { Hash = hash };

            retVal.AddPath(_path);

            return retVal;
        }
    }
}