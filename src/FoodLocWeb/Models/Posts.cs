using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLoc.Models
{

    public class Posts
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Your Name")]
        public string UpoadedBy { get; set; }
        [Display(Name = "Food Picture")]
        [Required(ErrorMessage = "Please Upload the image of food")]
        public string ImageName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(100)]
        [Display(Name = "Restaurant Name")]
        public string RestaurantName { get; set; }
        [Display(Name = "Caption")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(100)]
        public string PhotoTitle { get; set; }
        [Required(ErrorMessage = "Please Enable the Location")]
        public double Longitude { get; set; }
        [Required(ErrorMessage = "Please Enable the Location")]
        public double Latitude { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
