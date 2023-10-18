namespace WebZi.Plataform.Domain.Models.Veiculo
{
    public class MarcaModeloModel
    {
        public int MarcaModeloId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string MarcaModelo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagOrigemDetran { get; set; } = "S";
    }
}