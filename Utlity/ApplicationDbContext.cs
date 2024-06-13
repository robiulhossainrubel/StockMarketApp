using Microsoft.EntityFrameworkCore;
using StockMarketApp.Models;

namespace StockMarketApp.Utlity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<StockMarket> stockMarkets { get; set; }
    }
}
