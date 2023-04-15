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
		//public virtual DbSet<LabAssistant> LabAssistants { get; set; } = null!;
		public virtual DbSet<Status> Statuses { get; set; } = null!;
		//public virtual DbSet<LabAssistant> LabAssistants { get; set; } = null!;
		public DbSet<Systems_Project_Spring_2023.Models.LabAssistant>? LabAssistant { get; set; }
			

	}
}