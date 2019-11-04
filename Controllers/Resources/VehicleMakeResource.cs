using System.Collections.Generic;

namespace vega.Controllers.Resources
{
    public class VehicleMakeResource
    {
         public VehicleMakeResource()
        {
            Models = new List<VehicleModelResource>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VehicleModelResource> Models { get; set; }
    }
}