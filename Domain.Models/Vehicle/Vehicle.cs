using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Domain.Models.Vehicle
{   
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public string Name { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<VehicleFeature> Features { get; set; }
        public Vehicle()
        {
            Features = new List<VehicleFeature>();
        }
    }
}