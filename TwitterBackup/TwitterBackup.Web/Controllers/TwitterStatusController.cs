using Microsoft.AspNetCore.Mvc;

namespace TwitterBackup.Web.Controllers
{
    public class TwitterStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}