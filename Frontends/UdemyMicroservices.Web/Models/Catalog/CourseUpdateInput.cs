using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UdemyMicroservices.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; }
        public string? PictureUrl { get; set; }
        public FeatureViewModel Feature { get; set; }
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resmi")]
        public IFormFile? PhotoFormFile { get; set; }
    }
}
