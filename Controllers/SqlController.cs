using Microsoft.AspNetCore.Mvc;

namespace StockMarketApp.Controllers
{
    public class SqlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
