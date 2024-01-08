using CommunityOfMars.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityOfMars.Data
{
	public class MarsDBContext : DbContext
	{
		public MarsDBContext(DbContextOptions<MarsDBContext> options) : base(options)
		{ }

		public DbSet<Message> Messages { get; set; }
		//TODO: Remove users when we add Identity
		public DbSet<AppUser> Users { get; set; }
	}
}
