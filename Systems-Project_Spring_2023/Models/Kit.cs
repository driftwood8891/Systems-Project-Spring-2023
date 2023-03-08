using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
    public class Kit
    {
        [Key]
        [Display(Name = "Kit ID")]
        [Required(ErrorMessage = "Kit ID is required.")]
        public int Kit_id { get; set; }

        [Display(Name = "Kit Barcode")]
        public string Kit_barcd { get; set; }

        [Display(Name = "Kit Name")]
        [Required(ErrorMessage = "Kit Name is required.")]
        public string Kit_name { get; set; }

        [Display(Name = "Kit Description")]
        public string Kit_desc { get; set; }

        [Display(Name = "Kit Cost")]
        [Required(ErrorMessage = "Kit Cost is required.")]
        public decimal Kit_cost { get; set; }

        [Display(Name = "Kit Date")]
        public DateTime Kit_date { get; set; }

        [Display(Name = "Kit Status")]
        [Required(ErrorMessage = "Kit Status is required.")]
        public string Kit_stat { get; set; }

        [Display(Name = "Kit Note")]
        public string Kit_note { get; set; }

        [Display(Name = "Student ID")]
        public virtual int Student_id { get; set; }

        [ForeignKey("Student_id")]
        public virtual Student Student { get; set; }
    }
}

