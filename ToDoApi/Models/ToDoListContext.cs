using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class TodoListContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }
    }
}
