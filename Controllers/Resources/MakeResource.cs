using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaAPI.Controllers.Resource
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }

    }
    
}