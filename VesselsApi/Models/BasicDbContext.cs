using Microsoft.EntityFrameworkCore;

namespace VesselsApi.Models {
	public class BasicDbContext : DbContext {
		public DbSet<Basic> basic { get; set; }

		public BasicDbContext(DbContextOptions<BasicDbContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			builder.Entity<Basic>().ToTable("Basic");
		}
	}
}
