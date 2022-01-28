using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Infrastructure.Data.InMemory
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Models.Entities.CustomerCardEntity> CustumerAccount{ get; set; }
    }
}
