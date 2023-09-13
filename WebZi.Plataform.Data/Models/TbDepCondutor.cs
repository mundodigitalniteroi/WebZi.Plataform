using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepCondutor
{
    public int IdCondutor { get; set; }

    public int IdGrv { get; set; }

    public long? IdPessoa { get; set; }

    public decimal? IdEnquadramentoInfracao { get; set; }

    public string Documento { get; set; }

    public string Identidade { get; set; }

    public string OrgaoExpedidor { get; set; }

    public string Nome { get; set; }

    public string Telefone { get; set; }

    public string TelefoneDdd { get; set; }

    public string Email { get; set; }

    public string NumeroChaveVeiculo { get; set; }

    public string NumeroInfracao { get; set; }

    public string InformacoesAdicionais { get; set; }

    public string OutrosEquipamentos1 { get; set; }

    public string OutrosEquipamentos2 { get; set; }

    public string OutrosEquipamentos3 { get; set; }

    public string OutrosEquipamentos4 { get; set; }

    public string OutrosEquipamentos5 { get; set; }

    /// <summary>
    /// 1 = ASSINOU;
    /// 2 = AUSENTE;
    /// 3 = EVADIU-SE;
    /// 4 = RECUSOU-SE.
    /// </summary>
    public string StatusAssinaturaCondutor { get; set; }

    public string FlagChaveVeiculo { get; set; }

    public string FlagDocumentacaoVeiculo { get; set; }

    public string Celular { get; set; }

    public string CelularDdd { get; set; }

    public virtual TbDepEnquadramentoInfraco IdEnquadramentoInfracaoNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }
}
