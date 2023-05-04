using System.ComponentModel.DataAnnotations;

namespace Systems_Project_Spring_2023.Models
{
	public class Status
	{
		[Key] 
        [Display(Name = "Status Code")]
		[StringLength(2)] 
		[Required(ErrorMessage = "Status code is required")] 
		public string Status_code { get; set; } = null!;

        [Display(Name = "Status Description")]
		[StringLength(20)] 
		[Required(ErrorMessage = "Status description is required.")]
		public string Status_desc { get; set; } = null!;
	}
}
