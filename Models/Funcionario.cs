using Microsoft.AspNetCore.Http.HttpResults;

namespace TCM_Supermercado1.Models
{
    public class Funcionario
    {
        public int cod_funcionario { get; set; }
        public String? nome_funcionario { get; set; }
        public String? email_funcionario { get; set; }
        public String? senha_funcionario { get; set; }

    }
}
