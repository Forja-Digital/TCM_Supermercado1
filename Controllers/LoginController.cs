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
                return RedirectToAction("Index", "Produto");
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

            var funcionario = _loginRepositorio.ObterFuncionario(email);

            if (funcionario == null)
            {
                return NotFound();
            }

            TempData["Email"] = email;

            return RedirectToAction(nameof(NovaSenha));

        }


        [HttpGet]
        public IActionResult NovaSenha(string email)
        {
            TempData.Keep("Email");

            return View();
        }

        [HttpPost]
        public IActionResult NovaSenha([Bind("senha_funcionario")] Funcionario funcionario)
        {
            var email = TempData["Email"] as string;
            funcionario.email_funcionario = email;
           
                try
                {
                    if (_loginRepositorio.Editar(funcionario))
                    {
                        return RedirectToAction(nameof(Login));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao editar.");
                    return View();
                }
            

            TempData["Mensagem"] = "Senha alterada com sucesso!";
            return RedirectToAction("Login", "Login"); ;
        }

    }
}
