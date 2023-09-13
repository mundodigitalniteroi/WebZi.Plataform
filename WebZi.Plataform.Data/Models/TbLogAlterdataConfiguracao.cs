using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAlterdataConfiguracao
{
    public long LogId { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int AlterDataConfiguracaoId { get; set; }

    public int ClienteDepositoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public short IdentificadorNaturezaLancamentoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public bool Status { get; set; }

    public bool? EnviarLoteBaixa { get; set; }
}
