using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
    public class LabAssistant
    {
	    [Key]
	    public int La_id { get; set; }

	    [Display(Name = "First Name")]
	    [Required(ErrorMessage = "First name is required.")]
	    public string La_fname { get; set; } = null!;

	    [Display(Name = "Last Name")]
	    [Required(ErrorMessage = "Last name is required.")]
	    public string La_lname { get; set; } = null!;

	    [Display(Name = "Campus")]
	    [Required(ErrorMessage = "Campus is required.")]
	    public string La_camp { get; set; } = null!;

	    [Display(Name = "Work Schedule")]
	    [Required(ErrorMessage = "Work Schedule is required.")]
	    public string La_sch { get; set; } = null!;
    }
}
