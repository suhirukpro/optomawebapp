using Microsoft.AspNetCore.Mvc;
using SimpleContact.Models;
using SimpleContact.Services;
using System.Diagnostics;

namespace SimpleContact.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger,IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;  
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /// <summary>
        /// Sends email to optoma 
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _emailService.SendContactEmail(contactViewModel.Name, contactViewModel.Email, contactViewModel.Message);
                ModelState.Clear();
                ViewBag.IsSuccess = true;
                ViewBag.SuccessMessage = "Thank you! We have recieved your message and will respond soon";
            }
            return View("index");
        }
    }
}