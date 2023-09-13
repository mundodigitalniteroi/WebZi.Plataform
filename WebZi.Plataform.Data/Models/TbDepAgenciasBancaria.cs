using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAgenciasBancaria
{
    public short IdAgenciaBancaria { get; set; }

    public short IdBanco { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string CodigoAgencia { get; set; }

    public string ContaCorrente { get; set; }

    public string DigitoVerificador { get; set; }

    public string CodigoCedente { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public string SacadoCarteira { get; set; }

    public string SapCodigoBanco { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepCliente> TbDepClientes { get; set; } = new List<TbDepCliente>();

    public virtual ICollection<TbDepContasTemporaria> TbDepContasTemporaria { get; set; } = new List<TbDepContasTemporaria>();
}
