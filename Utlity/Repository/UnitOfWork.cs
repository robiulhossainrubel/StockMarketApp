
using Bulky.DataAccess.Repository.IRepository;
using StockMarketApp.Utlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IStockRepositoty stockRepositoty { get; private set; }

        

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            stockRepositoty = new StockRepositoty(_db);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
