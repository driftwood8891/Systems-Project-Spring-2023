using System.ComponentModel.DataAnnotations;

namespace Systems_Project_Spring_2023.Models
{
	public class LabAssistant
	{
		[Key]
		public int La_id { get; set; }

		[Display(Name = "First Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "First name is required.")]
		public string La_fname { get; set; } = null!;

		[Display(Name = "Last Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Last name is required.")]
		public string La_lname { get; set; } = null!;

		[Display(Name = "Campus")]
		[StringLength(3)]
		[Required(ErrorMessage = "Campus is required. Only 3 letter campus code(COL, MOB).")]
		public string La_camp { get; set; } = null!;

		[Display(Name = "Work Schedule")]
		[StringLength(255)]
		[Required(ErrorMessage = "Work Schedule is required.")]
		public string La_sch { get; set; } = null!;
	}
}
