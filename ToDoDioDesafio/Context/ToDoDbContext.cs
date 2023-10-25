using Microsoft.EntityFrameworkCore;

namespace ToDoDioDesafio.Context
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
           : base (options){}

        DbSet<Todo> Todos { get; set; }

    }
}
 