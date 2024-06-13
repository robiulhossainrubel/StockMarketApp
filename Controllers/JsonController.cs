using Microsoft.AspNetCore.Mvc;
using StockMarketApp.Models;
using StockMarketApp.Utlity;
using System;

namespace StockMarketApp.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StockMarket newstock)
        {
            var stock = JsonHelper.ReadFromJsonFile<StockMarket>();
            var cnt = JsonHelper.ReadCountJsonFile();
            cnt.Count = cnt.Count + 1;
            newstock.Id = cnt.Count;    
            stock.Add(newstock);
            JsonHelper.WriteToJsonFile(stock);
            JsonHelper.WriteToCountJsonFile(cnt);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            var stocks = JsonHelper.ReadFromJsonFile<StockMarket>();
            var stock = stocks.FirstOrDefault(u => u.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            else
            {
                return View(stock);
            }
        }
        [HttpPost]
        public IActionResult Update(StockMarket updatestock)
        {
            var stocks = JsonHelper.ReadFromJsonFile<StockMarket>();
            var stock = stocks.FirstOrDefault(u => u.Id == updatestock.Id);

            if (ModelState.IsValid && stock != null)
            {
                stock.Id = updatestock.Id;
                stock.Date = updatestock.Date;
                stock.Trade_Code = updatestock.Trade_Code;
                stock.High = updatestock.High;
                stock.Low = updatestock.Low;
                stock.Open = updatestock.Open;
                stock.Close = updatestock.Close;
                stock.Volume = updatestock.Volume;

                JsonHelper.WriteToJsonFile(stocks);
                TempData["success"] = "Stock Created Successfuly";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id ==0)
            {
                return View(new StockMarket());
            }
            else
            {
                var stocks = JsonHelper.ReadFromJsonFile<StockMarket>();
                var stock = stocks.FirstOrDefault(u => u.Id == id);
                if (stock == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(stock);
                }
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(StockMarket updatestock)
        {


            if (ModelState.IsValid)
            {
                if (updatestock.Id == 0)
                {
                    var stock = JsonHelper.ReadFromJsonFile<StockMarket>();
                    var cnt = JsonHelper.ReadCountJsonFile();
                    cnt.Count = cnt.Count + 1;
                    updatestock.Id = cnt.Count;
                    stock.Add(updatestock);
                    JsonHelper.WriteToJsonFile(stock);
                    JsonHelper.WriteToCountJsonFile(cnt);
                    TempData["success"] = "Stock Created Successfuly";
                    return RedirectToAction("Index");
                }
                else
                {
                    var stocks = JsonHelper.ReadFromJsonFile<StockMarket>();
                    var stock = stocks.FirstOrDefault(u => u.Id == updatestock.Id);
                    stock.Id = updatestock.Id;
                    stock.Date = updatestock.Date;
                    stock.Trade_Code = updatestock.Trade_Code;
                    stock.High = updatestock.High;
                    stock.Low = updatestock.Low;
                    stock.Open = updatestock.Open;
                    stock.Close = updatestock.Close;
                    stock.Volume = updatestock.Volume;

                    JsonHelper.WriteToJsonFile(stocks);
                    TempData["success"] = "Stock Update Successfuly";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(updatestock);
            }
        }
        public IActionResult Chart()
        {
            return View();
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<StockMarket> ObjList = JsonHelper.ReadFromJsonFile<StockMarket>();

            return Json(new { data = ObjList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var stocks = JsonHelper.ReadFromJsonFile<StockMarket>();
            var stock = stocks.FirstOrDefault(u => u.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            stocks.Remove(stock);
            JsonHelper.WriteToJsonFile<StockMarket>(stocks);
            return NoContent();
        }
        #endregion
    }
}
