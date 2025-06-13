using TCM_Supermercado1.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace TCM_Supermercado1.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginRepositorio _loginRepositorio;

        public IActionResult Login()
        {
            return View();
        }
    }
}
