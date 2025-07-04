﻿using TCM_Supermercado1.Models;
using Microsoft.AspNetCore.Mvc;
using TCM_Supermercado1.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult CadastrarProduto()
        {
            ViewBag.Categorias = new SelectList(_produtoRepositorio.TodasCategorias(), "cod_categoria", "nome_categoria");
            ViewBag.Fornecedores = new SelectList(_produtoRepositorio.TodosFornecedores(), "cnpj", "nome_fornecedor");
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.Cadastrar(produto); 
                return RedirectToAction("Index"); 
            }

            ViewBag.Categorias = new SelectList(_produtoRepositorio.TodasCategorias(), "cod_categoria", "nome_categoria");
            ViewBag.Fornecedores = new SelectList(_produtoRepositorio.TodosFornecedores(), "cnpj", "nome_fornecedor");
            return View(produto);
        }

        [HttpGet]
        public IActionResult EditarProduto(int id)
        {
            ViewBag.Categorias = new SelectList(_produtoRepositorio.TodasCategorias(), "cod_categoria", "nome_categoria");
            ViewBag.Fornecedores = new SelectList(_produtoRepositorio.TodosFornecedores(), "cnpj", "nome_fornecedor");
            var produto = _produtoRepositorio.ObterProduto(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }
        [HttpPost]
        public IActionResult EditarProduto(int id, [Bind("cod_produto, cod_categoria, cnpj, nome_produto, preco_produto, descricao_produto, quantidade_produto")] Produto produto)
        {
            if (id != produto.cod_produto)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.EditarProduto(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ViewBag.Categorias = new SelectList(_produtoRepositorio.TodasCategorias(), "cod_categoria", "nome_categoria");
                    ViewBag.Fornecedores = new SelectList(_produtoRepositorio.TodosFornecedores(), "cnpj", "nome_fornecedor");
                    ModelState.AddModelError("", "Ocorreu um erro ao editar.");
                    return View(produto);
                }
            }


            return View(produto);
        }

        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.Excluir(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
