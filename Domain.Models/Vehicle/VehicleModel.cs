using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Domain.Models
{
    [Table("Models")]
    public class VehicleModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public VehicleMake Make { get; set; }
        public int MakeId { get; set; }
    }
}