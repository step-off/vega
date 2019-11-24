using System.ComponentModel.DataAnnotations;

namespace vega.Domain.Models.Vehicle
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}