
using StockMarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IStockRepositoty : IRepository<StockMarket>
    {
        void Update(StockMarket obj);
        
    }
}
