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
            // Retrieve the TimeZoneInfo object for Central Standard Time
            var cstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var timeZoneOffset = cstTimeZone.BaseUtcOffset;

            // Check if daylight saving time is in effect
            if (cstTimeZone.IsDaylightSavingTime(DateTime.UtcNow))
            {
                timeZoneOffset += TimeSpan.FromHours(1);
            }

            // Adjust the UTC time by the time zone offset
            Kt_date = DateTime.UtcNow.Add(timeZoneOffset);

            // Generate a unique Kit ID
            if (string.IsNullOrEmpty(Kt_id))
            {
                Kt_id = Guid.NewGuid().ToString().Substring(0, 10);
            }
        }

        [Key]
		[Display(Name = "Kit Type ID")]
		[StringLength(10)]
		[Required(ErrorMessage = "Kit Type ID is required.")]
		public string Kt_id { get; set; } = null!;

		[Display(Name = "Kit Type Name")]
		[StringLength(20)]
		[Required(ErrorMessage = "Kit Type name is required.")]
		public string Kt_name { get; set; } = null!;

        [Display(Name = "Kit Desc")]
        [StringLength(75)]
        [Required(ErrorMessage = "Kit Desc is required.")]
        public string Kt_desc { get; set; } = null!;

        /*[Display(Name = "Kit/Item Quantity")]
		[Required(ErrorMessage = "Kit/Item quantity is required.")]
		public int Kt_item_qty { get; set; }*/

        [DataType(DataType.Currency)]
		[Column(TypeName = "decimal(8,2)")]
		[Display(Name = "Kit Type Cost")]
		[Required(ErrorMessage = "Kit Type cost is required.")]
		public decimal Kt_cost { get; set; }

		//[Display(Name = "Kit Type Date")]
		//[Required(ErrorMessage = "Kit Type date is required.")]
		public DateTime Kt_date { get; set; } = DateTime.Now;

		/*[Display(Name = "Item Id")]
		[StringLength(36)]
		[Required(ErrorMessage = "Item Id is required.")]
		public string Item_id { get; set; } = null!;

		public virtual Item? Item { get; set; }*/
	}
}
