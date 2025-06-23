using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TCM_Supermercado1.Models;
using TCM_Supermercado1.Repositorio;

namespace TCM_Supermercado1.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly PedidoRepositorio _pedidoRepositorio;

        public PedidoController(ProdutoRepositorio produtoRepositorio, PedidoRepositorio pedidoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_pedidoRepositorio.ListarTodosPedidos());
        }

        [HttpGet]
        public IActionResult CadastrarPedido()
        {
            ViewBag.Funcionarios = new SelectList(_produtoRepositorio.TodosFuncionarios(), "cod_funcionario", "nome_funcionario");
            ViewBag.Categorias = new SelectList(_produtoRepositorio.TodasCategorias(), "cod_categoria", "nome_categoria");
            ViewBag.Fornecedores = new SelectList(_produtoRepositorio.TodosFornecedores(), "cnpj", "nome_fornecedor");
            ViewBag.Clientes = new SelectList(_produtoRepositorio.TodosClientes(), "cod_cliente", "nome_cliente");
            ViewBag.Pagamentos = new SelectList(_produtoRepositorio.TodosTipoPagamento(), "cod_pagamento", "tipo_pagamento");
            ViewBag.Estados = new SelectList(_produtoRepositorio.TodosEstado(), "cod_estado", "UF");
            return View(_produtoRepositorio.TodosProdutos());
        }
        [HttpPost]
        public IActionResult CadastrarPedido(IFormCollection form)
        {
            int cod_funcionario = int.Parse(form["cod_funcionario"]);
            int cod_cliente = int.Parse(form["cod_cliente"]);
            int cod_pagamento = int.Parse(form["cod_pagamento"]);
            int cod_estado = int.Parse(form["cod_estado"]);
            string endereco = form["endereco_pedido"];
            string complemento = form["complemento_pedido"];

            Pedido pedido = new Pedido
            {
                cod_funcionario = cod_funcionario,
                cod_cliente = cod_cliente,
                cod_pagamento = cod_pagamento,
                cod_estado = cod_estado,
                endereco_pedido = endereco,
                complemento_pedido = complemento,
                data_pedido = DateTime.Now,
                itens = new List<ItemPedido>()
            };

            var produtosSelecionados = form["produtosSelecionados"].ToString().Split(',');

            foreach (var codProdStr in produtosSelecionados)
            {
                if (int.TryParse(codProdStr, out int codProd))
                {
                    int quantidade = int.Parse(form[$"quantidade_{codProd}"]);

                    var produto = _produtoRepositorio.ObterProduto(codProd);

                    if (produto != null)
                    {
                        pedido.itens.Add(new ItemPedido
                        {
                            cod_produto = codProd,
                            quantidade_itempedido = quantidade,
                            preco_unitario = (double)produto.preco_produto,
                            total_item = quantidade * (double)produto.preco_produto
                        });
                    }
                }
            }

            pedido.total = pedido.itens.Sum(i => i.total_item);

            try
            {
                _pedidoRepositorio.SalvarPedidoComItens(pedido);
                TempData["MensagemSucesso"] = "Pedido cadastrado com sucesso!";
                return RedirectToAction("Index", "Pedido");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao cadastrar pedido: " + ex.Message;

                // Recarregar os dados necessários para a view
                ViewBag.Funcionarios = new SelectList(_produtoRepositorio.TodosFuncionarios(), "cod_funcionario", "nome_funcionario");
                ViewBag.Clientes = new SelectList(_produtoRepositorio.TodosClientes(), "cod_cliente", "nome_cliente");
                ViewBag.Pagamentos = new SelectList(_produtoRepositorio.TodosTipoPagamento(), "cod_pagamento", "tipo_pagamento");
                ViewBag.Estados = new SelectList(_produtoRepositorio.TodosEstado(), "cod_estado", "UF");

                return View(_produtoRepositorio.TodosProdutos());
            }
        }

    }
}
