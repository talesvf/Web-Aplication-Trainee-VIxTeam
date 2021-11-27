using Web_Aplication_Trainee_VIxTeam.Data;
using Web_Aplication_Trainee_VIxTeam.Models;

namespace Web_Aplication_Trainee_VIxTeam.Business
{
    public static class PessoaBusiness
    {
        public static bool VerificaSituacaoPessoaDeletar(PessoaModel pessoaModel)
        {
            return pessoaModel.Situacao;
        }
        
        //esta função verifica o estado antes e depois da tentativa de edição para permitir que ela trate casos diferentes.
        public static int VerificaSituacaoPessoaEditar(PessoaModel pessoaModel, PessoaModel pessoaBD)
        {
            //erro 1: a pessoa está 'inativa' e permanece 'inativa'.
            if (!pessoaModel.Situacao && !pessoaBD.Situacao)
            {
                return 1;
            }
            //erro 2: a pessoa está 'inativa' e tenta ser 'ativada' enquanto muda outro atributo.(Se nenhum atributo além da situacao for alterado, a edição será salva, 'ativando' a pessoa.
            if (pessoaModel.Situacao && !pessoaBD.Situacao)
            {
                if (pessoaModel.NomePessoa != pessoaBD.NomePessoa || pessoaModel.Email != pessoaBD.Email || pessoaModel.DataNascimento != pessoaBD.DataNascimento || pessoaModel.QtdFilhos != pessoaBD.QtdFilhos || pessoaModel.Salario != pessoaBD.Salario)
                {
                    return 2;
                }
            }
            
            return 0;
        }

        //como esta classe nao tem acesso ao _context, esta função existe apenas para organização das regras de negócios.
        public static bool VerificaEmailAoCriar(bool temEmailIgual)
        {
            return !temEmailIgual;
        }
        //esta função verifica se o email editado é válido.
        public static bool VerificaEmailAoEditar(PessoaModel pessoaModel, PessoaModel pessoaBD, bool temEmailIgual)
        {
            if(pessoaModel.Email == pessoaBD.Email)
            {
                return true;
            }
            if (temEmailIgual)
            {
                return false;
            }
            return true;
        }
        //esta função verifica se a data de nascimento é superior a 01/01/1990.
        public static bool VerificaDataNacimento(PessoaModel pessoaModel)
        {
            if (pessoaModel.DataNascimento >= new DateTime(1990, 1, 1))
            {
                return true;
            }
            return false;
        }
        //verifica se a quantidade de filhos é maior ou igual a zero.
        public static bool VerificaQuantidadeDeFilhos(PessoaModel pessoaModel)
        {
            if (pessoaModel.QtdFilhos < 0)
            {
                return false;
            }
            return true;
        }
        //verifica se o Salário é inferior a 1200 ou superior a 13000.
        public static int VerificaSalario(PessoaModel pessoaModel)
        {
            if(pessoaModel.Salario < 1200)
            {
                return -1;
            }
            if(pessoaModel.Salario > 13000)
            {
                return 1;
            }
            return 0;
        }
    }
}
