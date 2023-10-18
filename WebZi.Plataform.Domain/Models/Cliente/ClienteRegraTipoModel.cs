namespace WebZi.Plataform.Domain.Models.Cliente
{
    public class ClienteRegraTipoModel
    {
        public short ClienteRegraTipoId { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string PossuiValor { get; set; } = "S";

        public string Ativo { get; set; } = "S";
    }
}