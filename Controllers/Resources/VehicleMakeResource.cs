using System.Collections.Generic;

namespace vega.Controllers.Resources
{
    public class VehicleMakeResource : BaseResource
    {
         public VehicleMakeResource()
        {
            Models = new List<VehicleModelResource>();
        }
        public List<VehicleModelResource> Models { get; set; }
    }
}