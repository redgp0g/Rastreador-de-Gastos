using Microsoft.EntityFrameworkCore;

namespace Rastreador_de_Gastos.Models
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options) 
		{ }

		public DbSet<Transaction> Transaction { get; set; }
		public DbSet<Category> Category { get; set; }
	}
}
