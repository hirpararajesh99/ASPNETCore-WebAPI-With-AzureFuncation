using Microsoft.EntityFrameworkCore;

namespace Stockexchange.Model.StockExchangeDB
{
    public partial class StockExchangeContext : DbContext
    {
        public StockExchangeContext()
        {
        }

        public StockExchangeContext(DbContextOptions<StockExchangeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StockDetail> StockDetails { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
