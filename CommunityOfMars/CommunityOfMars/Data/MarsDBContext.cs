using CommunityOfMars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityOfMars.Data
{
	public class MarsDBContext : IdentityDbContext
	{
		public MarsDBContext(DbContextOptions<MarsDBContext> options) : base(options)
		{ }

		public DbSet<Message> Messages { get; set; }
	}
}
