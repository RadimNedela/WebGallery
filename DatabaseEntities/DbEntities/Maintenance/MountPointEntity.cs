﻿namespace WebGalery.Core.DbEntities.Maintenance
{
    public class MountPointEntity : IDatabaseEntity
    {
        public string Path { get; set; }
        public RackEntity Rack { get; set; }
        public string RackHash { get; set; }
    }
}
