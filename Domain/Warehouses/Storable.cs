using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class Storable : Entity
    {
        /// <summary>
        /// Hash of the real entity that wants to be stored here in the warehouse.
        /// It is the key to totaly different composition root - the entity we are talking about...
        /// </summary>
        public string EntityHash { get; protected set; }

        public string Name { get; protected set; }

        protected Storable() { }

        public Storable(string hash, string name, string entityHash)
            : base(hash)
        {
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            EntityHash = ParamAssert.NotEmtpy(entityHash, nameof(entityHash));
        }
    }
}