using System.ComponentModel.DataAnnotations;

namespace Systems_Project_Spring_2023.Models
{
    public class Location
    {
        [Key]
        public int loc_id { get; set; }

        [Display(Name = "Location Name")]
        [StringLength(35)]
        [Required(ErrorMessage = "Location name is required.")]
        public string loc_name { get; set; }
    }
}
