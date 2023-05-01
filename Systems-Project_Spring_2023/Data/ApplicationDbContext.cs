using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Models;

namespace Systems_Project_Spring_2023.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public virtual DbSet<Student> Students { get; set; } = null!;
		public virtual DbSet<Item> Items { get; set; } = null!;
		public virtual DbSet<Kit> Kits { get; set; } = null!;
		public virtual DbSet<Kit_Type> Kit_types { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<LabAssistant>? LabAssistant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        base.OnModelCreating(modelBuilder);
			// Adding seed data for Status codes
	        modelBuilder.Entity<Status>().HasData(
		        new Status { Status_code = "1", Status_desc = "Checked_In" },
		        new Status { Status_code = "2", Status_desc = "Checked_Out" },
		        new Status { Status_code = "3", Status_desc = "Dead" },
		        new Status { Status_code = "4", Status_desc = "Lost" },
		        new Status { Status_code = "5", Status_desc = "In_Transit" },
		        new Status { Status_code = "6", Status_desc = "Needs_Repair" },
		        new Status { Status_code = "7", Status_desc = "Pending" },
		        new Status { Status_code = "8", Status_desc = "Ready" },
		        new Status { Status_code = "9", Status_desc = "Unknown" }
	        );
			
        }
    }
}