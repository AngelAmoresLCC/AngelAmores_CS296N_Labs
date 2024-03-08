using CommunityOfMars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CommunityOfMars.Data
{
	public class MarsDBContext : IdentityDbContext
	{
		public MarsDBContext(DbContextOptions<MarsDBContext> options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Message>()
				.HasOne(m => m.ParentMessage)
				.WithMany(p => p.Replies)
				.HasForeignKey(r => r.Parent)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Message> Messages { get; set; }
	}
}
