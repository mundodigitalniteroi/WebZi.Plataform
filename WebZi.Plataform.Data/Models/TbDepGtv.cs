using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGtv
{
    public int IdGtv { get; set; }

    public int IdClienteEnvio { get; set; }

    public int IdClienteRecebimento { get; set; }

    public int IdDepositoEnvio { get; set; }

    public int? IdDepositoRecebimento { get; set; }

    public int IdReboquista { get; set; }

    public int IdReboque { get; set; }

    public int IdUsuarioSeparacaoVeiculos { get; set; }

    public int? IdUsuarioRecebimento { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public DateTime DataHoraPrevisaoSaida { get; set; }

    public DateTime? DataHoraRecebimento { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    /// <summary>
    /// N = Novo (em cadastramento no Depósito de origem);
    /// E = Enviado para ser recebido pelo Depósito de destino;
    /// C = Cancelado pelo Depósito de origem. Se cancelar depois de Enviado o Sistema deverá obrigar a digitação de um Motivo;
    /// R = Recebido pelo Depósito de destino.
    /// </summary>
    public string Status { get; set; }

    public virtual TbDepCliente IdClienteEnvioNavigation { get; set; }

    public virtual TbDepCliente IdClienteRecebimentoNavigation { get; set; }

    public virtual TbDepDeposito IdDepositoEnvioNavigation { get; set; }

    public virtual TbDepDeposito IdDepositoRecebimentoNavigation { get; set; }

    public virtual TbDepReboque IdReboqueNavigation { get; set; }

    public virtual TbDepReboquista IdReboquistaNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioSeparacaoVeiculosNavigation { get; set; }

    public virtual ICollection<TbDepGtvGrv> TbDepGtvGrvs { get; set; } = new List<TbDepGtvGrv>();

    public virtual TbDepGtvMotivosCancelamento TbDepGtvMotivosCancelamento { get; set; }
}
