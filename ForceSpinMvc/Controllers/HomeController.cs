using System.Diagnostics;
using ForceSpinMvc.Data;
using ForceSpinMvc.Models;
using ForceSpinMvc.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForceSpinMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index()
        {
            ICollection<Message> messages = _context.Messages.OrderByDescending(m => m.Id).ToList();

            return View(messages);
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
        [HttpPost]
        public IActionResult SendMessage(MessageVM messageVM)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message
                {
                    Title = messageVM.Title,
                    Body = messageVM.Body
                };

                // Mesajý veritabanýna ekle
                _context.Messages.Add(message);
                _context.SaveChanges(); // Deðiþiklikleri kaydet

                // Baþarýlý bir þekilde mesaj gönderildiðini belirtmek için TempData'ya mesaj ekle
                TempData["MessageSent"] = "Your message has been sent successfully!";

                // Baþarýyla gönderilen mesajla birlikte formu tekrar göster
                return RedirectToAction("Index");
            }

            // Eðer ModelState geçersizse, kullanýcýya formu tekrar göster
            return View(messageVM);
        }
    }
}
