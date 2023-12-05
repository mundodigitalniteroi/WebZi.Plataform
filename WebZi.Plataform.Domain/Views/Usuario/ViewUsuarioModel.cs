namespace WebZi.Plataform.Domain.Views.Usuario
{
    public class ViewUsuarioModel
    {
        public int UsuarioId { get; set; }

        public long? PessoaId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Login { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }

        public string NomeMeio { get; set; }

        public string Sobrenome { get; set; }

        public string NomeCompleto { get; set; }

        public string Cpf { get; set; }

        public string CpfFormatado { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime DataCadastroSenha { get; set; }

        public DateTime? DataUltimoAcesso { get; set; }

        public string FlagPermissaoDesconto { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public string FlagReceberEmailErro { get; set; }

        public string FlagAtivo { get; set; }
    }
}