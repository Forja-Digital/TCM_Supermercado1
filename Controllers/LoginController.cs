using TCM_Supermercado1.Repositorio;
using Microsoft.AspNetCore.Mvc;
using TCM_Supermercado1.Models;

namespace TCM_Supermercado1.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginRepositorio _loginRepositorio;

        public LoginController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var funcionario = _loginRepositorio.ObterFuncionario(email);

            if (funcionario != null && funcionario.senha_funcionario == senha)
            {
                // Autenticação bem-sucedida
                return RedirectToAction("RecuperarSenha", "Home");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");

            //retorna view Login 
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha(String email)
        {
            Console.WriteLine($"Email recebido: {email}");

            var funcionario = _loginRepositorio.ObterFuncionario(email);
            if (funcionario == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Login));
        }

        //[HttpPost]
        //public IActionResult EditarSenha(String email, [Bind("email_funcionario, senha_funcionario")] Funcionario funcionario)
        //{
        //    if (email != funcionario.email_funcionario)
        //    {
        //        return BadRequest();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (_loginRepositorio.Editar(funcionario))
        //            {
        //                return RedirectToAction(nameof(Login));
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            ModelState.AddModelError("", "Ocorreu um erro ao editar.");
        //            return View();
        //        }
        //    }
        //    return View();
        //}

    }
}
