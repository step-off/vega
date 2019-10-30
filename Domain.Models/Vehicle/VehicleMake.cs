using System.Collections.Generic;

namespace vega.Domain.Models
{
    public class VehicleMake
    {
        public VehicleMake()
        {
            Models = new List<VehicleModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VehicleModel> Models { get; set; }
    }
}