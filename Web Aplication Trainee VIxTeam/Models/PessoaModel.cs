using System.ComponentModel.DataAnnotations;

namespace Web_Aplication_Trainee_VIxTeam.Models
{
    public class PessoaModel
    {
        [Key]
        public int CodigoPessoa { get; set; }
        [Display(Name = "Nome")]
        public string NomePessoa { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Quantidade de Filhos")]
        public int QtdFilhos { get; set; }
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }
    }
}
