using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwEstoqueVeiculo
{
    public string NumeroFormularioGrv { get; set; }

    public string Renavam { get; set; }

    public string Placa { get; set; }

    public string PlacaOstentada { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string Cor { get; set; }

    public string CorOstentada { get; set; }

    public string AutoridadeOrgao { get; set; }

    public string AutoridadeSigla { get; set; }

    public string AutoridadeDivisao { get; set; }

    public string FlagComboio { get; set; }

    public string Reboque { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public string Status { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public int IdGrv { get; set; }

    public int? IdTarifaTipoVeiculo { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int? IdReboquista { get; set; }

    public int? IdReboque { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }

    public string NomeAutoridadeResponsavel { get; set; }

    public int? IdCor { get; set; }

    public int? IdDetranMarcaModelo { get; set; }

    public decimal? ValorFaturado { get; set; }

    public decimal? Expr1 { get; set; }

    public string NumeroNotaFiscal { get; set; }

    public DateTime? DataPagamento { get; set; }

    public string TipoComposicao { get; set; }

    public decimal? Diarias { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Logradouro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public string MatriculaAutoridadeResponsavel { get; set; }

    public string EstacionamentoSetor { get; set; }

    public string EstacionamentoNumeroVaga { get; set; }

    public string Frota { get; set; }

    public string NomeReboquista { get; set; }

    public string FlagChaveDeposito { get; set; }

    public string NumeroChave { get; set; }

    public string Classificação { get; set; }

    public int Quilometragem { get; set; }

    public string EmpresaReboque { get; set; }

    public decimal? TarifaReboqueTerceirizado { get; set; }
}
