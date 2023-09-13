using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvVistorium
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdGrvVistoria { get; set; }

    public int? IdGrv { get; set; }

    public int? IdEmpresaVistoria { get; set; }

    public byte? IdGrvVistoriaStatus { get; set; }

    public byte? IdGrvVistoriaSituacaoChassi { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string MotivoNaoRealizacaoVistoria { get; set; }

    public string NumeroVistoria { get; set; }

    public string NomeVistoriador { get; set; }

    public string NumeroMotor { get; set; }

    public DateTime? DataVistoria { get; set; }

    public string ResumoVistoria { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string TipoDirecao { get; set; }

    public string EstadoGeralVeiculo { get; set; }

    public string FlagPossuiRestricoes { get; set; }

    public string FlagPossuiPlaca { get; set; }

    public string FlagPossuiVidroEletrico { get; set; }

    public string FlagPossuiTravaEletrica { get; set; }
}
