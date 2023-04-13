using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Systems_Project_Spring_2023.Models
{
    public class Kit
    {
        [Key]
        [Display(Name = "Kit ID")]
        [StringLength(10)]
        [Required(ErrorMessage = "Kit ID is required.")]
        public string Kit_id { get; set; } = null!;

        [Display(Name = "Kit Barcode")]
        [StringLength(15)]
        [Required(ErrorMessage = "Kit Barcode is required.")]
        public string? Kit_barcd { get; set; }

        [Display(Name = "Kit Name")]
        [StringLength(20)]
        [Required(ErrorMessage = "Kit Name is required.")]
        public string Kit_name { get; set; } = null!;

        [Display(Name = "Kit Quantity")]
        [Range(1, 2)]
        [Required(ErrorMessage = "Kit quantity is required.")]
        public int Kit_qty { get; set; }

        [Display(Name = "Kit Description")]
        [StringLength(120)]
        public string? Kit_desc { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name = "Kit Cost")]
        [Required(ErrorMessage = "Kit cost is required.")]
        public decimal Kit_cost { get; set; }

        [Display(Name = "Kit Date")]
        [Required(ErrorMessage = "Kit date is required.")]
        public DateTime Kit_date { get; set; }

        [Display(Name = "Notes")]
        [StringLength(60)]
        public string? Kit_note { get; set; }

        [Display(Name = "Kit Type")]
        [StringLength(8)]
        [Required(ErrorMessage = "Kit type is required.")]
        public string Kt_id { get; set; } = null!;

        [Display(Name = "Status Code")]
        [StringLength(2)]
        [Required(ErrorMessage = "Status code is required.")]
        public string Status_code { get; set; } = null!;

        [Display(Name = "MACC ID/Room number")]
        [StringLength(10)]
        [Required(ErrorMessage = "MACC ID or Room number is required.")]
        public string Student_macid { get; set; } = null!;

        public virtual Student? Student { get; set; }

        public virtual Status? Status { get; set; }

        public virtual Kit_Type? Kit_type { get; set; }

        public class MyModelConfiguration : IEntityTypeConfiguration<Kit>
        {
            public void Configure(EntityTypeBuilder<Kit> builder)
            {
                builder.HasIndex(x => x.Kit_id).IsUnique();
            }
        }
    }
}

