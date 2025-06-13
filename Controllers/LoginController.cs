using TCM_Supermercado1.Repositorio;
using Microsoft.AspNetCore.Mvc;

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
                // Redireciona o usuário para a action "Index" do Controller "Cliente".
                return RedirectToAction("RecuperarSenha", "Home");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");

            //retorna view Login 
            return View();
        }

    }
}
