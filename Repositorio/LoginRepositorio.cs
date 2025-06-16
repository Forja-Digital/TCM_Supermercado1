using MySql.Data.MySqlClient;
using TCM_Supermercado1.Models;
using System.Data;

namespace TCM_Supermercado1.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public Funcionario ObterFuncionario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM tb_funcionario WHERE email_funcionario = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Funcionario funcionario = null;
                    if (dr.Read())
                    {

                        funcionario = new Funcionario
                        {
                            cod_funcionario = Convert.ToInt32(dr["cod_funcionario"]),
                            nome_funcionario = dr["nome_funcionario"].ToString(),
                            email_funcionario = dr["email_funcionario"].ToString(),
                            senha_funcionario = dr["senha_funcionario"].ToString()
                        };
                    }

                    return funcionario;
                }
            }
        }


        public bool Editar(Funcionario funcionario) 
        {

            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new(" SET SQL_SAFE_UPDATES = 0; update tb_funcionario set senha_funcionario = @senha where email_funcionario= @email; SET SQL_SAFE_UPDATES = 1;", conexao);
                    cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = funcionario.senha_funcionario;
                    cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = funcionario.email_funcionario;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {

                Console.WriteLine($"Erro ao atualizar a senha: {ex.Message}");
                return false;

            }


        }




    }
}
