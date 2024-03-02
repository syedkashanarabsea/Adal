using Adal.DbContexts;
using Core.CoreClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adal.Controllers
{
    public class LoginController : Controller
    {

        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users Model)
        {
            
            var users = await _context.Users.FirstOrDefaultAsync(c=>c.Email == Model.Email);

            if (users != null)
            {
                if (users.Password == Model.Password)
                {
                    return RedirectToAction("Index","Home");
                }
            }

            return View(Model);
        }


    }
}
