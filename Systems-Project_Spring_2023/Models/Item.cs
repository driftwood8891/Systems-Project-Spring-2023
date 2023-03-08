using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
	public class Item
	{
		[Key]
		[Display(Name = "Item ID")]
		[Required(ErrorMessage = "Item ID is required.")]
		public int Item_id { get; set; }

		[Display(Name = "Item Barcode")]
		public string Item_barcd { get; set; }

		[Display(Name = "Item Name")]
		[Required(ErrorMessage = "Item Name is required.")]
		public string Item_name { get; set; }

		[Display(Name = "Item Cost")]
		[Required(ErrorMessage = "Item Cost is required.")]
		public decimal Item_cost { get; set; }

		[Display(Name = "Item Date")]
		public DateTime Item_date { get; set; }

		[Display(Name = "Item Status")]
		[Required(ErrorMessage = "Item Status is required.")]
		public string Item_stat { get; set; }

		[Display(Name = "Item Note")]
		public string Item_note { get; set; }

		[Display(Name = "Student ID")]
		public virtual int Student_id { get; set; }

		[ForeignKey("Student_id")]
		public virtual Student Student { get; set; }
	}
}
