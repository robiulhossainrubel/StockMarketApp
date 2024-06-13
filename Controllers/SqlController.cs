using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using StockMarketApp.Models;

namespace StockMarketApp.Controllers
{
    public class SqlController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new StockMarket());
            }
            else
            {
                StockMarket stockMarket = _unitOfWork.stockRepositoty.Get(u => u.Id == id);
                return View(stockMarket);
            }

        }
        [HttpPost]
        public IActionResult Upsert(StockMarket updatestock)
        {
            if (ModelState.IsValid)
            {


                if (updatestock.Id == 0)
                {
                    _unitOfWork.stockRepositoty.Add(updatestock);
                }
                else
                {
                    _unitOfWork.stockRepositoty.Update(updatestock);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfuly";
                return RedirectToAction("Index");
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
            List<StockMarket> objCompanyList = _unitOfWork.stockRepositoty.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var StockToBeDeleted = _unitOfWork.stockRepositoty.Get(u => u.Id == id);
            if (StockToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.stockRepositoty.Remove(StockToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
