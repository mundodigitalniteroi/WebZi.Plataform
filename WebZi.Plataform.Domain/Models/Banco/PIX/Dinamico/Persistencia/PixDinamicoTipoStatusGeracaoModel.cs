namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia
{
    public class PixDinamicoTipoStatusGeracaoModel
    {
        public byte PixDinamicoTipoStatusGeracaoId { get; set; }

        public string Descricao { get; set; }

        /// <summary>
        /// A: O PIX foi enviado com sucesso ao Banco e está sendo processado;
        /// C: O PIX foi transferido;
        /// R: O PIX não foi transferido.
        /// </summary>
        public string Status { get; set; }
    }
}