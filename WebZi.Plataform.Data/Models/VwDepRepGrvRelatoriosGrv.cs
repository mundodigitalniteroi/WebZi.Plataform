using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepRepGrvRelatoriosGrv
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string GrvDataRemocao { get; set; }

    public string GrvHoraRemocao { get; set; }

    public string GrvDataGuarda { get; set; }

    public string GrvHoraGuarda { get; set; }

    public string LogradouroApreensao { get; set; }

    public string EstacionamentoSetor { get; set; }

    public string EstacionamentoNumeroVaga { get; set; }

    public string AgenteApreensorMatricula { get; set; }

    public string AgenteApreensorNome { get; set; }

    public string AgenteApreensorTipo { get; set; }

    public string GrvPlaca { get; set; }

    public string GrvChassi { get; set; }

    public string GrvRenavam { get; set; }

    public short? VeiculoAnoFabricacao { get; set; }

    public short? VeiculoAnoModelo { get; set; }

    public string VeiculoUf { get; set; }

    public string MarcaModelo { get; set; }

    public string Cor { get; set; }

    public string ReboquePlaca { get; set; }

    public string ReboquistaNome { get; set; }

    public string CondutorNome { get; set; }

    public string CondutorDocumento { get; set; }

    public string CondutorIdentidade { get; set; }

    public string CondutorOrgaoExpedidor { get; set; }

    public string ChaveVeiculo { get; set; }

    public string NumeroChaveVeiculo { get; set; }

    public string EnquadramentoInfracaoCodigo { get; set; }

    public string EnquadramentoInfracaoDescricao { get; set; }

    public string TipoVeiculo { get; set; }

    public int IdTarifaTipoVeiculo { get; set; }

    public string TarifasDescricao { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteCnpj { get; set; }

    public string ClienteTipoLogradouro { get; set; }

    public string ClienteLogradouro { get; set; }

    public string ClienteNumeroLogradouro { get; set; }

    public string ClienteComplementoLogradouro { get; set; }

    public string ClienteBairro { get; set; }

    public string ClienteMunicipio { get; set; }

    public string ClienteEstado { get; set; }

    public string ClienteUf { get; set; }

    public string ClienteCep { get; set; }

    public string BancoCodigoFebraban { get; set; }

    public string BancoNome { get; set; }

    public string AgenciaCodigo { get; set; }

    public string AgenciaContaCorrente { get; set; }

    public string AgenciaDigitoVerificador { get; set; }

    public string AgenciaCodigoCedente { get; set; }

    public string DepositoNome { get; set; }

    public string DepositoTipoLogradouro { get; set; }

    public string DepositoLogradouro { get; set; }

    public string DepositoNumeroLogradouro { get; set; }

    public string DepositoComplementoLogradouro { get; set; }

    public string DepositoBairro { get; set; }

    public string DepositoMunicipio { get; set; }

    public string DepositoEstado { get; set; }

    public string DepositoUf { get; set; }

    public string DepositoCep { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }
}
