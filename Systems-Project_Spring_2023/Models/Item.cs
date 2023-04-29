using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Systems_Project_Spring_2023.Models
{
	public class Item
	{
        public Item()
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
            Item_date = DateTime.UtcNow.Add(timeZoneOffset);

            // auto generate the Item ID
            Item_id = Guid.NewGuid().ToString().Substring(0, 10);

            // auto generate the Item quantity
            //Item_qty = 1;
        }


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

        [Display(Name = "Item Type")]
        [StringLength(50)]
        [Required(ErrorMessage = "Item Type is required.")]
        public string Item_type { get; set; } = null!;

        /*[Display(Name = "Item Quantity")]
		[Range(1, 3)]
		[Required(ErrorMessage = "Item quantity is required.")]
		public int Item_qty { get; set; }*/

		[Display(Name = "Item Cost")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(8,2)")]
		[Required(ErrorMessage = "Item cost is required.")]
		public decimal Item_cost { get; set; }

		//[Display(Name = "Creation Date")]
		//[Required(ErrorMessage = "Creation date is required.")]
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

		public class MyModelConfiguration : IEntityTypeConfiguration<Item>
		{
			public void Configure(EntityTypeBuilder<Item> builder)
			{
				builder.HasIndex(x => x.Item_id).IsUnique();
			}
		}
	}
}
