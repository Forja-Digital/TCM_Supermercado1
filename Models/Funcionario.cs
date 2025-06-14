using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace TCM_Supermercado1.Models
{
    public class Funcionario
    {
        public int cod_funcionario { get; set; }
        public String? nome_funcionario { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public String? email_funcionario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")] 
        public String? senha_funcionario { get; set; }

    }
}
