using Newtonsoft.Json;

namespace Archspace2
{
    public abstract class Entity
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object aOther)
        {
            return Equals((Entity)aOther);
        }

        protected virtual bool Equals(Entity aOther)
        {
            return Id == aOther.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
