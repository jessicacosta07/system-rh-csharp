using Microsoft.EntityFrameworkCore;
using system_rh_csharp.Models;

namespace system_rh_csharp.Context
{
    public class RHContext : DbContext
    {
        public RHContext(DbContextOptions<RHContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}