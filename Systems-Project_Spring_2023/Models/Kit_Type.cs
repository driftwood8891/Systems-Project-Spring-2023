using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Systems_Project_Spring_2023.Models
{
	public class Kit_Type
	{
        public Kit_Type()
        {
            // Generate a unique Kit ID
            Kt_id = Guid.NewGuid().ToString().Substring(0, 8);
        }

        [Key]
		[Display(Name = "Kit Type")]
		[StringLength(8)]
		[Required(ErrorMessage = "Kit type is required.")]
		public string Kt_id { get; set; } = null!;

		[Display(Name = "Kit/Item Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Kit/Item name is required.")]
		public string Kt_item_name { get; set; } = null!;

		/*[Display(Name = "Kit/Item Quantity")]
		[Required(ErrorMessage = "Kit/Item quantity is required.")]
		public int Kt_item_qty { get; set; }*/

		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(8,2)")]
		[Display(Name = "Kit/Item Cost")]
		[Required(ErrorMessage = "Kit/Item cost is required.")]
		public decimal Kt_item_cost { get; set; }

		[Display(Name = "Kit/Item Date")]
		[Required(ErrorMessage = "Kit/Item date is required.")]
		public DateTime Kt_date { get; set; } = DateTime.Now;

		/*[Display(Name = "Item Id")]
		[StringLength(36)]
		[Required(ErrorMessage = "Item Id is required.")]
		public string Item_id { get; set; } = null!;

		public virtual Item? Item { get; set; }*/
	}
}
