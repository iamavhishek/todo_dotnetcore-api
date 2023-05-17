using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers.Models;

namespace WebAPI.Data
{
    public class TodosAPIDbContext : DbContext
    {
        public TodosAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
