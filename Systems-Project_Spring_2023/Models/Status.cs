using System.ComponentModel.DataAnnotations;

namespace Systems_Project_Spring_2023.Models
{
	public class Status
	{
		[Key] 
		[StringLength(2)] 
		[Required] 
		public string Status_code { get; set; } = null!;

		[StringLength(20)] 
		public string Status_desc { get; set; } = null!;
	}
}
