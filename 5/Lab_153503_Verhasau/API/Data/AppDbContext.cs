using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Souvenir> Souvenir { get; set; }
		public DbSet<Category> Category { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Souvenir>().HasKey(cl => cl.Id);
			modelBuilder.Entity<Souvenir>().Property(cl => cl.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Souvenir>().HasOne(cl => cl.Category).WithMany(ct => ct.SouvenirList).HasForeignKey("CategoryId").IsRequired();

			modelBuilder.Entity<Category>().HasKey(cc => cc.Id);
			modelBuilder.Entity<Category>().Property(cc => cc.Id).ValueGeneratedOnAdd();
		}
	}
}
