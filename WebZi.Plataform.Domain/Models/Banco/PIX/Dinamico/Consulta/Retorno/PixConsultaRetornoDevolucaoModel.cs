namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Retorno
{
    public class PixConsultaRetornoDevolucaoModel
    {
        public string Id { get; set; }

        public string RtrId { get; set; }

        public string Valor { get; set; }

        public PixConsultaRetornoHorarioModel Horario { get; set; }

        public string Status { get; set; }
    }
}