using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
	public class Student
	{
		[Key]
		[Display(Name = "Student ID")]                        // Display field name
		[Required(ErrorMessage = "Student ID is required.")]  // Requires you to enter data into the field or will output error message
		public int Student_id { get; set; }

		[Display(Name = "First Name")]
		[Required(ErrorMessage = "First name is required.")]
		public string Student_fname { get; set; }

		[Display(Name = "Last Name")]
		[Required(ErrorMessage = "Last name is required.")]
		public string Student_lname { get; set; }

		[EmailAddress]
		[Display(Name = "College email")]
		[Required(ErrorMessage = "College email is required.")]
		public string Student_cmail { get; set; }

		[EmailAddress]
		[Display(Name = "Personal email")]
		[Required(ErrorMessage = "Personal email is required.")]
		public string Student_pmail { get; set; }

		[Phone]
		[Display(Name = "Phone number")]
		[Required(ErrorMessage = "Phone number is required.")]
		public string Student_phone { get; set; }

		[Phone]
		[Display(Name = "Emergency Contact")]
		public string Student_ephone { get; set; }

		[Display(Name = "Address")]
		[Required(ErrorMessage = "Address name is required.")]
		public string Student_addr { get; set; }

		[Display(Name = "Course name")]
		[Required(ErrorMessage = "Course name is required.")]
		public string Student_cour { get; set; }

		[Display(Name = "Campus")]
		[Required(ErrorMessage = "Campus name is required.")]
		public string Student_camp { get; set; }
		
		[Display(Name = "Instructor Name")]
		[Required(ErrorMessage = "Instructor name is required.")]
		public string Student_instr { get; set; }
	}
}
