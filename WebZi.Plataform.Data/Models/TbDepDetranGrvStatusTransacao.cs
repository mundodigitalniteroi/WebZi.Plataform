using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranGrvStatusTransacao
{
    public int IdDetranGrvTransacao { get; set; }

    public int IdGrv { get; set; }

    public short IdTransacaoClienteDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    /// <summary>
    /// PE = Pendente de Envio. Quando é realizado o cadastro na Tabela antes de executar o WS DETRAN;
    /// EN = Enviado;
    /// RS = Recebido com Sucesso;
    /// RE = Recebido com Erro. Quando o DETRAN informa algum problema com o Veículo;
    /// EF = Erro Fatal. Quando ocorre erro na execução do WS DETRAN, nestes casos o WS deve ser reexecutado
    /// </summary>
    public string Status { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepDetranAssociacaoTransacaoClienteDeposito IdTransacaoClienteDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepDetranTransacaoConsultarPendenciasLiberacaoVeiculo> TbDepDetranTransacaoConsultarPendenciasLiberacaoVeiculos { get; set; } = new List<TbDepDetranTransacaoConsultarPendenciasLiberacaoVeiculo>();

    public virtual ICollection<TbDepDetranTransacaoConsultarVeiculo> TbDepDetranTransacaoConsultarVeiculos { get; set; } = new List<TbDepDetranTransacaoConsultarVeiculo>();

    public virtual ICollection<TbDepDetranTransacaoIncluirVeiculoPatio> TbDepDetranTransacaoIncluirVeiculoPatios { get; set; } = new List<TbDepDetranTransacaoIncluirVeiculoPatio>();

    public virtual ICollection<TbDepDetranTransacaoLiberarVeiculoPatio> TbDepDetranTransacaoLiberarVeiculoPatios { get; set; } = new List<TbDepDetranTransacaoLiberarVeiculoPatio>();

    public virtual ICollection<TbDepDetranTransacaoRecolherVeiculo> TbDepDetranTransacaoRecolherVeiculos { get; set; } = new List<TbDepDetranTransacaoRecolherVeiculo>();
}
