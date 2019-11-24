using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace vega.Domain.Models.Vehicle
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedExtensions { get; set; }
        public bool IsValid(IFormFile file) 
        {
            return AcceptedExtensions.Any(i => i == Path.GetExtension(file.FileName).ToLower()) && file.Length < MaxBytes;
        }
    }
}