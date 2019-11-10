using System;
using System.Collections.Generic;
using vega.Domain.Models.Vehicle;

namespace vega.Controllers.Resources
{
    public class SaveVehicleResource : BaseResource
    {
        public int VehicleModelId { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<int> Features { get; set; }
        public ContactResource Contact { get; set; }
        public SaveVehicleResource()
        {
            Features = new List<int>();
        }
    }
}