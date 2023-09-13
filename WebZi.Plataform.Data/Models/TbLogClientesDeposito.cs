using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogClientesDeposito
{
    public long Id { get; set; }

    public int? UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime? DataHoraLog { get; set; }

    public int? IdClienteDeposito { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public short? IdOrgaoEmissor { get; set; }

    public int? IdEmpresa { get; set; }

    public string IdSistemaExterno { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string CodigoDetran { get; set; }

    public string CodigoSap { get; set; }

    public string CodigoSapOrdemVendas { get; set; }

    public string FlagUtilizaSistemaMobileGgv { get; set; }

    public string FlagCadastrarGrvBloqueado { get; set; }

    public string FlagValorIssIgualProdutoBaseCalculoAliquota { get; set; }

    public string FlagAtivo { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public decimal? AliquotaIss { get; set; }
}
