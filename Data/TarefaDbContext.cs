using Microsoft.EntityFrameworkCore;

namespace Tarefas.Data;
public class TarefaDbContext : DbContext
{
     public TarefaDbContext(DbContextOptions<TarefaDbContext> options)
        : base(options) { }

     public DbSet<Tarefa> Tarefas => Set<Tarefa>();
}
