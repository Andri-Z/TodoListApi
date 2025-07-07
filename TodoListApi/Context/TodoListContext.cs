using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;
using TodoListApi.Models.Jwt;

namespace TodoListApi.Context
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}
