using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TCM_Supermercado1.Models;

namespace TCM_Supermercado1.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {

        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public Produto ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tb_produto where cod_produto=@codigo ", conexao);
                cmd.Parameters.AddWithValue("@codigo", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    produto.cod_produto = Convert.ToInt32(dr["cod_produto"]);
                    produto.cod_categoria = Convert.ToInt32(dr["cod_categoria"]);
                    produto.cnpj = (string)(dr["cnpj"]);
                    produto.nome_produto = ((string)dr["nome_produto"]);
                    produto.preco_produto = Convert.ToDouble(dr["preco_produto"]);
                    produto.descricao_produto = ((string)dr["descricao_produto"]);
                    produto.quantidade_produto = Convert.ToInt32(dr["quantidade_produto"]);
                }
                return produto;
            }
        }

        public IEnumerable<ProdutoLista> TodosProdutos()
        {
            List<ProdutoLista> Produtolist = new List<ProdutoLista>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.cod_produto, t2.nome_categoria, t3.nome_fornecedor, t1.nome_produto, t1.preco_produto, t1.descricao_produto, t1.quantidade_produto FROM tb_produto t1 INNER JOIN tb_categoriaProd t2 ON t1.cod_categoria = t2.cod_categoria INNER JOIN tb_fornecedor t3 ON t1.cnpj = t3.cnpj where t1.ativo_produto=true;", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new ProdutoLista
                                {
                                    cod_produto = Convert.ToInt32(dr["cod_produto"]),
                                    nome_categoria = ((string)dr["nome_categoria"]),
                                    nome_fornecedor = ((string)dr["nome_fornecedor"]),
                                    nome_produto = ((string)dr["nome_produto"]),
                                    preco_produto = Convert.ToDouble(dr["preco_produto"]),
                                    descricao_produto = ((string)dr["descricao_produto"]),
                                    quantidade_produto = Convert.ToInt32(dr["quantidade_produto"]),
                                });
                }
                return Produtolist;
            }
        }

        public IEnumerable<Fornecedor> TodosFornecedores()
        {
            List<Fornecedor> fornecedorList = new List<Fornecedor>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cnpj, nome_fornecedor FROM tb_fornecedor", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fornecedorList.Add(new Fornecedor
                    {
                        cnpj = reader["cnpj"].ToString(),
                        nome_fornecedor = reader["nome_fornecedor"].ToString()
                    });
                }

                conexao.Close();
                return fornecedorList;
            }
        }
        public IEnumerable<TipoPagamento> TodosTipoPagamento()
        {
            List<TipoPagamento> pagamentoList = new List<TipoPagamento>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cod_pagamento, tipo_pagamento FROM tb_pagamento", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    pagamentoList.Add(new TipoPagamento
                    {
                        cod_pagamento = Convert.ToInt32(reader["cod_pagamento"]),
                        tipo_pagamento = reader["tipo_pagamento"].ToString()
                    });
                }

                conexao.Close();
                return pagamentoList;
            }
        }

        public IEnumerable<Estado> TodosEstado()
        {
            List<Estado> estadoList = new List<Estado>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cod_estado, UF FROM tb_estado", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    estadoList.Add(new Estado
                    {
                        cod_estado = Convert.ToInt32(reader["cod_estado"]),
                        UF = reader["UF"].ToString()
                    });
                }

                conexao.Close();
                return estadoList;
            }
        }

        public IEnumerable<Categoria> TodasCategorias()
        {
            List<Categoria> categoriaList = new List<Categoria>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cod_categoria, nome_categoria FROM tb_categoriaProd", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categoriaList.Add(new Categoria
                    {
                        cod_categoria = Convert.ToInt32(reader["cod_categoria"]),
                        nome_categoria = reader["nome_categoria"].ToString()
                    });
                }

                conexao.Close();
                return categoriaList;
            }
        }

        public IEnumerable<Cliente> TodosClientes()
        {
            List<Cliente> clienteList = new List<Cliente>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cod_cliente, nome_cliente FROM tb_cliente", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    clienteList.Add(new Cliente
                    {
                        cod_cliente = Convert.ToInt32(reader["cod_cliente"]),
                        nome_cliente = reader["nome_cliente"].ToString()
                    });
                }

                conexao.Close();
                return clienteList;
            }
        }

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tb_produto (cod_categoria, cnpj, nome_produto, preco_produto, descricao_produto, quantidade_produto) values (@categoria, @cnpj, @nome, @preco, @descricao, @quantidade)", conexao);
                cmd.Parameters.Add("@categoria", MySqlDbType.Int32).Value = produto.cod_categoria;
                cmd.Parameters.Add("@cnpj", MySqlDbType.VarChar).Value = produto.cnpj;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome_produto;
                cmd.Parameters.Add("@preco", MySqlDbType.Double).Value = produto.preco_produto;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao_produto;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade_produto;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public bool EditarProduto(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update tb_produto set cod_categoria=@categoria, cnpj=@cnpj, nome_produto=@nome, preco_produto=@preco, descricao_produto=@descricao, quantidade_produto=@quantidade where cod_produto=@codigo;", conexao);
                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = produto.cod_produto;
                    cmd.Parameters.Add("@categoria", MySqlDbType.Int32).Value = produto.cod_categoria;
                    cmd.Parameters.Add("@cnpj", MySqlDbType.VarChar).Value = produto.cnpj;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome_produto;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao_produto;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco_produto;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade_produto;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {

                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;

            }
        }
        
        public IEnumerable<Funcionario> TodosFuncionarios()
        {
          List<Funcionario> funcionarioList = new List<Funcionario>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT cod_funcionario, nome_funcionario FROM tb_funcionario", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    funcionarioList.Add(new Funcionario
                    {
                        cod_funcionario = Convert.ToInt32(reader["cod_funcionario"]),
                        nome_funcionario = reader["nome_funcionario"].ToString()
                    });
                }

                conexao.Close();
                return funcionarioList;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update tb_produto set ativo_produto = FALSE where cod_produto = @codigo;", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

    }
}
