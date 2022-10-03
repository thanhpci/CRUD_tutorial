using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GrpcServiceCRUD.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
