using Microsoft.EntityFrameworkCore;
using TectProject.API.Models;

namespace TectProject.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<TrBpkb> TrBpkbs { get; set; }
		public DbSet<MsStorageLocation> MsStorageLocations { get; set; }
		public DbSet<MsUser> MsUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TrBpkb>()
				.HasOne(t => t.StorageLocation)
				.WithMany(m => m.TrBpkbs)
				.HasForeignKey(t => t.LocationId);
		}
	}
}