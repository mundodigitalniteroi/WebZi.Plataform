using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Usuario.CadastrarSenha
{
    public class CadastrarSenhaParameters
    {
        [Required(ErrorMessage = "O Login é obrigatória")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare(nameof(Senha), ErrorMessage = "A confirmação de senha deve ser igual à Senha")]
        public string ConfirmarSenha { get; set; }
    }
}