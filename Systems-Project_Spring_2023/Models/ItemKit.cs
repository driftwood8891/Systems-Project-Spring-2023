using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Systems_Project_Spring_2023.Models
{
    public class ItemKit
    {
        public ItemKit()
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
            Item_Kit_Date = DateTime.UtcNow.Add(timeZoneOffset);
        }

        [Key]
        [Display(Name = "Item/Kit Barcode")]
        [StringLength(15)]
        [Required(ErrorMessage = "Item/Kit Barcode is required.")]
        public string Item_Kit_Barcode { get; set; } = null!;

        [Display(Name = "Item/Kit Name")]
        [StringLength(20)]
        [Required(ErrorMessage = "Item/Kit Name is required.")]
        public string Item_Kit_Name { get; set; } = null!;

        [Display(Name = "Item/Kit Cost")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Required(ErrorMessage = "Item/Kit cost is required.")]
        public decimal Item_Kit_Cost { get; set; }

        //[Display(Name = "Creation Date")]
        //[Required(ErrorMessage = "Creation date is required.")]
        public DateTime Item_Kit_Date { get; set; }

        [Display(Name = "Item/Kit Notes")]
        [StringLength(60)]
        [Required(ErrorMessage = "Item/Kit Notes is required.")]
        public string? Item_Kit_Note { get; set; }

        [Display(Name = "Status Code")]
        [StringLength(2)]
        [Required(ErrorMessage = "Status Code is required.")]
        public string Status_code { get; set; } = null!;

        [Display(Name = "Item/Kit ID")]
        [StringLength(10)]
        [Required(ErrorMessage = "Item/Kit ID is required.")]
        public string Item_Kit_ID { get; set; } = null!;

        [Display(Name = "Item/Kit Type")]
        [StringLength(20)]
        [Required(ErrorMessage = "Item/Kit Type is required.")]
        public string Item_Kit_Type { get; set; } = null!;

        [Display(Name = "MACC ID/Room Number")]
        [StringLength(10)]
        [Required(ErrorMessage = "MACC ID or room number is required.")]
        public string Student_macid { get; set; } = null!;

        public virtual Student? Student { get; set; }

        public virtual Status? Status { get; set; }

        public class MyModelConfiguration : IEntityTypeConfiguration<ItemKit>
        {
            public void Configure(EntityTypeBuilder<ItemKit> builder)
            {
                builder.HasIndex(x => x.Item_Kit_Barcode).IsUnique();
            }
        }
    }
}
