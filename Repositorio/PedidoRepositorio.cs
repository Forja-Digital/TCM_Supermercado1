using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TCM_Supermercado1.Models;
using Microsoft.Extensions.Configuration;

namespace TCM_Supermercado1.Repositorio
{
    public class PedidoRepositorio
    {
        private readonly string _conexaoMySQL;

        public PedidoRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        }

        public List<PedidoLista> ListarTodosPedidos()
        {
            var pedidos = new List<PedidoLista>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                using (var cmd = new MySqlCommand(
                    "SELECT p.cod_pedido, f.nome_funcionario, c.nome_cliente, pay.tipo_pagamento, " +
                    "p.data_pedido, p.total, e.UF AS estado_uf, p.endereco_pedido, p.complemento_pedido " +
                    "FROM tb_pedido p " +
                    "INNER JOIN tb_funcionario f ON p.cod_funcionario = f.cod_funcionario " +
                    "INNER JOIN tb_cliente c ON p.cod_cliente = c.cod_cliente " +
                    "INNER JOIN tb_pagamento pay ON p.cod_pagamento = pay.cod_pagamento " +
                    "INNER JOIN tb_estado e ON p.cod_estado = e.cod_estado " +
                    "ORDER BY p.cod_pedido DESC;", conexao))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new PedidoLista
                        {
                            cod_pedido = Convert.ToInt32(reader["cod_pedido"]),
                            nome_funcionario = reader["nome_funcionario"].ToString(),
                            nome_cliente = reader["nome_cliente"].ToString(),
                            tipo_pagamento = reader["tipo_pagamento"].ToString(),
                            data_pedido = Convert.ToDateTime(reader["data_pedido"]),
                            total = Convert.ToDouble(reader["total"]),
                            estado_uf = reader["estado_uf"].ToString(),
                            endereco_pedido = reader["endereco_pedido"].ToString(),
                            complemento_pedido = reader["complemento_pedido"].ToString()
                        };

                        pedidos.Add(pedido);
                    }
                }
            }

            return pedidos;
        }


        public void SalvarPedidoComItens(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                using (var cmdPedido = new MySqlCommand(
                    "INSERT INTO tb_pedido (cod_funcionario, cod_pagamento, cod_cliente, data_pedido, total, cod_estado, endereco_pedido, complemento_pedido) " +
                    "VALUES (@funcionario, @pagamento, @cliente, @data, @total, @estado, @endereco, @complemento); " +
                    "SELECT LAST_INSERT_ID();", conexao))
                {
                    cmdPedido.Parameters.AddWithValue("@funcionario", pedido.cod_funcionario);
                    cmdPedido.Parameters.AddWithValue("@pagamento", pedido.cod_pagamento);
                    cmdPedido.Parameters.AddWithValue("@cliente", pedido.cod_cliente);
                    cmdPedido.Parameters.AddWithValue("@data", pedido.data_pedido);
                    cmdPedido.Parameters.AddWithValue("@total", pedido.total);
                    cmdPedido.Parameters.AddWithValue("@estado", pedido.cod_estado);
                    cmdPedido.Parameters.AddWithValue("@endereco", pedido.endereco_pedido);
                    cmdPedido.Parameters.AddWithValue("@complemento", pedido.complemento_pedido);

                    pedido.cod_pedido = Convert.ToInt32(cmdPedido.ExecuteScalar());
                }

                foreach (var item in pedido.itens)
                {
                    using (var cmdItem = new MySqlCommand(
                        "INSERT INTO tb_item_pedido (cod_pedido, cod_produto, quantidade_itempedido, preco_unitario, total_item) " +
                        "VALUES (@pedido, @produto, @quantidade, @preco, @total);", conexao))
                    {
                        cmdItem.Parameters.AddWithValue("@pedido", pedido.cod_pedido);
                        cmdItem.Parameters.AddWithValue("@produto", item.cod_produto);
                        cmdItem.Parameters.AddWithValue("@quantidade", item.quantidade_itempedido);
                        cmdItem.Parameters.AddWithValue("@preco", item.preco_unitario);
                        cmdItem.Parameters.AddWithValue("@total", item.total_item);

                        cmdItem.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}