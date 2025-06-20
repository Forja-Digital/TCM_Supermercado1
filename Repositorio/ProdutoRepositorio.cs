using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TCM_Supermercado1.Models;

namespace TCM_Supermercado1.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {

        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> Produtolist = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tb_produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new Produto
                                {
                                    cod_produto = Convert.ToInt32(dr["cod_produto"]),
                                    cod_categoria = Convert.ToInt32(dr["cod_categoria"]),
                                    cnpj = ((string)dr["cnpj"]),
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
