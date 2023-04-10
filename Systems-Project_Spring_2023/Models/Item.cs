using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
	public class Item
	{
		[Key]
		[Display(Name = "Item ID")]
		[StringLength(10)]
		[Required(ErrorMessage = "Item ID is required.")]
		public string Item_id { get; set; } = null!;

		[Display(Name = "Item Barcode")]
		[StringLength(15)]
		[Required(ErrorMessage = "Item Barcode is required.")]
		public string Item_barcode { get; set; } = null!;

		[Display(Name = "Item Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Item Name is required.")]
		public string Item_name { get; set; } = null!;

		[Display(Name = "Item Quantity")]
		[Range(1, 9)]
		[Required(ErrorMessage = "Item quantity is required.")]
		public int Item_qty { get; set; }

		[Display(Name = "Item Cost")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(8,2)")]
		[Required(ErrorMessage = "Item cost is required.")]
		public decimal Item_cost { get; set; }

		[Display(Name = "Creation Date")]
		[Required(ErrorMessage = "Creation date is required.")]
		public DateTime Item_date { get; set; }

		[Display(Name = "Item Notes")]
		[StringLength(60)]
		[Required(ErrorMessage = "Item Notes is required.")]
		public string? Item_note { get; set; }

		[Display(Name = "Status Code")]
		[StringLength(2)]
		[Required(ErrorMessage = "Status Code is required.")]
		public string Status_code { get; set; } = null!;

		[Display(Name = "MACC ID/Room Number")]
		[StringLength(10)]
		[Required(ErrorMessage = "MACC ID or room number is required.")]
		public string Student_macid { get; set; } = null!;

		public virtual Student? Student { get; set; }

		public virtual Status? Status { get; set; }
	}
}
