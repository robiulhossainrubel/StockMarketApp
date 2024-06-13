
using Bulky.DataAccess.Repository.IRepository;

using StockMarketApp.Models;
using StockMarketApp.Utlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class StockRepositoty : Repository<StockMarket>, IStockRepositoty
    {
        private ApplicationDbContext _db;
        public StockRepositoty(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StockMarket obj)
        {
            _db.stockMarkets.Update(obj);
        }
    }
}
