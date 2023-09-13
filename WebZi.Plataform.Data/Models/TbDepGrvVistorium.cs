using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvVistorium
{
    public int IdGrvVistoria { get; set; }

    public int IdGrv { get; set; }

    /// <summary>
    /// Faz referência à Tabela db_global.dbo.tb_glo_emp_empresas
    /// </summary>
    public int? IdEmpresaVistoria { get; set; }

    public byte? IdGrvVistoriaStatus { get; set; }

    public byte? IdGrvVistoriaSituacaoChassi { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string MotivoNaoRealizacaoVistoria { get; set; }

    public string NumeroVistoria { get; set; }

    public string NomeVistoriador { get; set; }

    public string NumeroMotor { get; set; }

    public DateTime? DataVistoria { get; set; }

    public string ResumoVistoria { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    /// <summary>
    /// M: MANUAL;
    /// E: ELETRO HIDRÁULICA;
    /// H: HIDRÁULICA.
    /// </summary>
    public string TipoDirecao { get; set; }

    /// <summary>
    /// B: BOM;
    /// E: EXCELENTE;
    /// P: PÉSSIMO;
    /// R: RUIM
    /// </summary>
    public string EstadoGeralVeiculo { get; set; }

    public string FlagPossuiRestricoes { get; set; }

    public string FlagPossuiPlaca { get; set; }

    public string FlagPossuiVidroEletrico { get; set; }

    public string FlagPossuiTravaEletrica { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepGrvVistoriaSituacaoChassi IdGrvVistoriaSituacaoChassiNavigation { get; set; }

    public virtual TbDepGrvVistoriaStatus IdGrvVistoriaStatusNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepGrvVistoriaArquivoLaudo> TbDepGrvVistoriaArquivoLaudos { get; set; } = new List<TbDepGrvVistoriaArquivoLaudo>();
}
