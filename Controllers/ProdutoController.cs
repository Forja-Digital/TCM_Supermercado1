using TCM_Supermercado1.Models;
using Microsoft.AspNetCore.Mvc;
using TCM_Supermercado1.Repositorio;

namespace TCM_Supermercado1.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly ProdutoRepositorio _produtoRepositorio;
        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }


    }
}
