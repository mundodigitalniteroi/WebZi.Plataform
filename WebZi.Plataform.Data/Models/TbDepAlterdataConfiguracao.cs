using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataConfiguracao
{
    public int AlterDataConfiguracaoId { get; set; }

    public int ClienteDepositoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public short IdentificadorNaturezaLancamentoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public bool? Status { get; set; }

    public bool EnviarLoteBaixa { get; set; }

    public virtual TbDepClientesDeposito ClienteDeposito { get; set; }

    public virtual TbDepAlterdataConfiguracaoIdentificadorNaturezaLancamento IdentificadorNaturezaLancamento { get; set; }

    public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaos { get; set; } = new List<TbDepAlterdataOperacao>();

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
