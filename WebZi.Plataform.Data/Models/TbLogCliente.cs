using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogCliente
{
    public DateTime? DatahoraLog { get; set; }

    public long Id { get; set; }

    public int? IdCliente { get; set; }

    public short? IdAgenciaBancaria { get; set; }

    public int? IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public int? IdBairro { get; set; }

    public byte? IdTipoMeioCobranca { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public decimal? GpsLatitude { get; set; }

    public decimal? GpsLongitude { get; set; }

    public decimal? MetragemTotal { get; set; }

    public decimal? MetragemGuarda { get; set; }

    public string HoraDiaria { get; set; }

    public short? MaximoDiariasParaCobranca { get; set; }

    public short? MaximoDiasVencimento { get; set; }

    public string CodigoSap { get; set; }

    public string LabelClienteCodigoIdentificacao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagUsarHoraDiaria { get; set; }

    public string FlagEmissaoNotaFiscalSap { get; set; }

    public string FlagCadastrarQuilometragem { get; set; }

    public string FlagCobrarDiariasDiasCorridos { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string FlagEnderecoCadastroManual { get; set; }

    public string FlagPermiteAlteracaoTipoVeiculo { get; set; }

    public string FlagLancarIpvaMultas { get; set; }

    public string FlagPossuiClienteCodigoIdentificacao { get; set; }

    public string FlagAtivo { get; set; }

    public int? IdOrgaoExecutivoTransito { get; set; }

    public string CodigoOrgao { get; set; }

    public string FlagPossuiPixEstatico { get; set; }

    public byte? PixTipoChaveId { get; set; }

    public string PixChave { get; set; }

    public string FlagPossuiPixDinamico { get; set; }

    public string TipoPix { get; set; }

    public string FlagPossuiPix { get; set; }
}
