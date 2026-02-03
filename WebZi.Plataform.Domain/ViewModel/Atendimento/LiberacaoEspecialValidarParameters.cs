
using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class LiberacaoEspecialValidarParameters
    {
        [Required(ErrorMessage = "É nessario um Login")]public string Login { get; set; }

        [Required(ErrorMessage = "É nessario uma Senha")] public string Password { get; set; }
    }
}