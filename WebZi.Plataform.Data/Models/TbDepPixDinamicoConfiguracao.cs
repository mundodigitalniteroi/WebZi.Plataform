using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPixDinamicoConfiguracao
{
    public byte PixDinamicoConfiguracaoId { get; set; }

    public string BaseUrl { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string Certificate { get; set; }

    public string SenhaCertificado { get; set; }

    public int? ClienteId { get; set; }

    public int? BancoPixId { get; set; }

    public int? UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public byte PixTipoChaveId { get; set; }

    public string PixChave { get; set; }

    public virtual TbDepCliente Cliente { get; set; }

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
