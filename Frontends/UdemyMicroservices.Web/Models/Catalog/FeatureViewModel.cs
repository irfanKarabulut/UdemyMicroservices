
using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservices.Web.Models.Catalog
{
    public class FeatureViewModel
    {      
        [Display(Name ="Kurs süresi")]
        public int Duration { get; set; }
    }
}
