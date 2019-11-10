using System;
using System.Collections.Generic;

namespace vega.Controllers.Resources
{
    public class VehicleResource : BaseResource
    {
        public VehicleModelResource VehicleModel { get; set; }
        public BaseResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<FeatureResource> Features { get; set; }
        public VehicleResource()
        {
            Features = new List<FeatureResource>();
        }
    }
}