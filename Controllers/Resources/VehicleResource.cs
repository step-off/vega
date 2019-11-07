using System;
using System.Collections.Generic;
using vega.Domain.Models.Vehicle;

namespace vega.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        public string Name { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<int> Features { get; set; }
        public ContactResource Contact { get; set; }
        public VehicleResource()
        {
            Features = new List<int>();
        }
    }
}