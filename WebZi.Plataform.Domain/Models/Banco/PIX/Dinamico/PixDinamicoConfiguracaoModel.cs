using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico
{
    public class PixDinamicoConfiguracaoModel
    {
        public byte PixDinamicoConfiguracaoId { get; set; }

        public int? ClienteId { get; set; }

        public string BaseUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Certificate { get; set; }

        public string SenhaCertificado { get; set; }

        public int? BancoPixId { get; set; }

        public byte PixTipoChaveId { get; set; }

        public string PixChave { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        //public virtual TbDepUsuario UsuarioAlteracao { get; set; }

        //public virtual TbDepUsuario UsuarioCadastro { get; set; }
    }
}