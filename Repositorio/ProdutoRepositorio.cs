using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TCM_Supermercado1.Models;

namespace TCM_Supermercado1.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {

        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public IEnumerable<ProdutoLista> TodosProdutos()
        {
            List<ProdutoLista> Produtolist = new List<ProdutoLista>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.cod_produto, t2.nome_categoria, t3.nome_fornecedor, t1.nome_produto, t1.preco_produto, t1.descricao_produto, t1.quantidade_produto FROM tb_produto t1 INNER JOIN tb_categoriaProd t2 ON t1.cod_categoria = t2.cod_categoria INNER JOIN tb_fornecedor t3 ON t1.cnpj = t3.cnpj;", conexao);
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


    }
}
