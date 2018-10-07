using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Armada : JsonSerializable<Armada>, ICollection<Deployment>
    {
        [JsonProperty("Deployments")]
        public List<Deployment> Deployments { get; set; }

        public int Count => ((ICollection<Deployment>)Deployments).Count;

        bool ICollection<Deployment>.IsReadOnly => ((ICollection<Deployment>)Deployments).IsReadOnly;

        public Armada()
        {
            Deployments = new List<Deployment>();
        }

        IEnumerator<Deployment> IEnumerable<Deployment>.GetEnumerator()
        {
            return ((IEnumerable<Deployment>)Deployments).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Deployment>)Deployments).GetEnumerator();
        }

        public void Add(Deployment item)
        {
            ((ICollection<Deployment>)Deployments).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Deployment>)Deployments).Clear();
        }

        public bool Contains(Deployment item)
        {
            return ((ICollection<Deployment>)Deployments).Contains(item);
        }

        void ICollection<Deployment>.CopyTo(Deployment[] array, int arrayIndex)
        {
            ((ICollection<Deployment>)Deployments).CopyTo(array, arrayIndex);
        }

        public bool Remove(Deployment item)
        {
            return ((ICollection<Deployment>)Deployments).Remove(item);
        }
    }
}
