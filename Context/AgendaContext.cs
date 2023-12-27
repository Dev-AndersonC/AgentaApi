using AgentaApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgentaApi.Context
{
	public class AgendaContext : DbContext
	{
		public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) {
			
		}

		public DbSet<Contato> Contatos {get; set; }
		
	}
}
