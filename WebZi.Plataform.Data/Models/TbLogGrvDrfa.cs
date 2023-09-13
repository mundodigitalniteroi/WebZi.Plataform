using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvDrfa
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int IdGrvDrfa { get; set; }

    public int IdGrv { get; set; }

    public byte? IdGrvDrfaTipoRegistro { get; set; }

    public short? IdOrgaoEmissor { get; set; }

    public byte? IdAutoridadeDivisao { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string AutoridadeDivisaoComplemento { get; set; }

    public string NumeroRegistroRouboFurto { get; set; }

    public string RegistroRouboFurtoMatriculaAgente { get; set; }

    public string RegistroRouboFurtoNomeAgente { get; set; }

    public string LocalRemocaoEnderecoCompleto { get; set; }

    public string LocalRemocaoReferencia { get; set; }

    public string LocalRemocaoLatitude { get; set; }

    public string LocalRemocaoLongitude { get; set; }

    public string EstadoGeralVeiculo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagRegistroRecuperacao { get; set; }

    public string FlagRegistroAgendado { get; set; }
}
