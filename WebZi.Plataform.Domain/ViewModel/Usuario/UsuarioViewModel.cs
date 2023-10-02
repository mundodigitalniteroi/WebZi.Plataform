namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime? DataUltimoAcesso { get; set; }

        public string FlagPermissaoDesconto { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public string FlagReceberEmailErro { get; set; }

        public string FlagAtivo { get; set; }

        public string Matricula { get; set; }
    }
}