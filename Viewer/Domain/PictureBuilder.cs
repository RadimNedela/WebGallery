using Domain.Elements;
using Infrastructure.Databases;
using System;
using System.Linq;
using WebGalery.DatabaseEntities;

namespace WebGalery.PictureViewer.Domain
{
    public class PictureBuilder
    {
        private readonly IUserInfo userInfo;
        private readonly IGaleryReadDatabase database;

        public PictureBuilder(IUserInfo userInfo, IGaleryReadDatabase database)
        {
            this.userInfo = userInfo;
            this.database = database;
        }

        public Picture Get(string hash)
        {
            string fullPath = GetFullPath(hash);
            if (string.IsNullOrWhiteSpace(fullPath))
                throw new Exception($"Entity with hash {hash} not found in the database");
            //var pathBinders = contentEntity.AttributedBinders.Select(ab => ab.)
            return new Picture(hash, fullPath);
        }

        private string GetFullPath(string hash)
        {
            var content = database.Contents.FirstOrDefault(c => c.Hash == hash);
            if (content == null) throw new Exception($"Content with hash {hash} not found in the database");

            var aBindersList = from attr in content.AttributedBinders
                       where (attr.Binder.Type == BinderTypeEnum.DirectoryType.ToString()
                       && userInfo.IsRackActive(attr.Binder.RackHash))
                       select attr;

            
            string fullPath = "";

            return fullPath;
        }
    }
}
