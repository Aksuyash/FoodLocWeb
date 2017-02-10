using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLoc.Models
{
    public class PostViewModel
    {
        public string UpoadedBy { get; set; }
        [Display(Name = "Food Picture")]

        public string ImageName { get; set; }
        [Display(Name = "Caption")]

        public string PhotoTitle { get; set; }

        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public DateTime UploadedDate { get; set; }

        public double Distance { get; set; }
    }
}
