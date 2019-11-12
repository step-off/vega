using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Domain.Models.Features
{
    [Table("Feature")]
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}