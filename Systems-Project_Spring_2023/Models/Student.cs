using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
	public class Student
	{
		[Key]
		[Display(Name = "MACC ID")]
		[StringLength(10)]
		[Required(ErrorMessage = "MACC ID is required.")]
		public string Student_macid { get; set; } = null!;

		[Display(Name = "First Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "First Name is required.")]
		public string Student_fname { get; set; } = null!;

		[Display(Name = "Last Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Last Name is required.")]
		public string Student_lname { get; set;} = null!;

		[Display(Name = "Student Email")]
		[EmailAddress]
		[StringLength(40)]
		[Required(ErrorMessage = "Student email is required.")]
		public string Student_cmail { get; set;} = null!;

		[Display(Name = "Personal Email")]
		[EmailAddress]
		[StringLength(50)]
		[Required(ErrorMessage = "Personal email is required.")]
		public string Student_pmail { get; set;} = null!;

		[Display(Name = "Phone Number")]
		[Phone]
		[StringLength(20)]
		[Required(ErrorMessage = "Phone number is required.")]
		public string Student_phone { get;set;} = null!;

		[Display(Name = "Emergency Phone Number")]
		[Phone]
		[StringLength(20)]
		[Required(ErrorMessage = "Emergency phone number is required.")]
		public string Student_ephone { get; set;} = null!;

		[Display(Name = "Address")]
		[StringLength(80)]
		[Required(ErrorMessage = "Address is required.")]
		public string Student_addr { get; set;} = null!;

		[Display(Name = "Course")]
		[StringLength(25)]
		[Required(ErrorMessage = "Course name is required.")]
		public string Student_cour { get; set;} = null!;

		[Display(Name = "Campus")]
		[StringLength(3)]
		[Required(ErrorMessage = "Campus abbreviation is required.")]
		public string Student_camp { get; set;} = null!;

		[Display(Name = "Instructor Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Name of instructor is required.")]
		public string Student_instr { get; set;} = null!;
	}
}
