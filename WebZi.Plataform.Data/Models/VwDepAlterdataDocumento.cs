using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAlterdataDocumento
{
    public int ClienteId { get; set; }

    public string Cliente { get; set; }

    public int DepositoId { get; set; }

    public string Deposito { get; set; }

    public int ClienteDepositoId { get; set; }

    public int EmpresaId { get; set; }

    public int MunicipioId { get; set; }

    public int AlterDataConfiguracaoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public bool Status { get; set; }

    public short IdentificadorNaturezaLancamentoId { get; set; }

    public string IdentificadorNaturezaLancamentoCodigo { get; set; }

    public string IdentificadorNaturezaLancamentoDescricao { get; set; }

    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public int AtendimentoId { get; set; }

    public string StatusCadastroCliente { get; set; }

    public string StatusCadastroDocumento { get; set; }

    public int UsuarioEntregaId { get; set; }

    public DateTime DataEntrega { get; set; }

    public int? AlterDataClienteId { get; set; }

    public string AlterDataIdentificadorCliente { get; set; }
}
