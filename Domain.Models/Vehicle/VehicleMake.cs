using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vega.Domain.Models
{
    public class VehicleMake
    {
        public VehicleMake()
        {
            Models = new List<VehicleModel>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public List<VehicleModel> Models { get; set; }
    }
}