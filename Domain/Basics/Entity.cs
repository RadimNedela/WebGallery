namespace WebGalery.Domain.Basics
{
    public class Entity : IEntity
    {
        public virtual string Hash { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(string hash)
        {
            Hash = ParamAssert.NotEmtpy(hash, nameof(hash));
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity entityObj)
                return Hash == entityObj.Hash;
            return false;
        }
    }
}
