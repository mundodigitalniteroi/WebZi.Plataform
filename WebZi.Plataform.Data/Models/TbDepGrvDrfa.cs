using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDrfa
{
    public int IdGrvDrfa { get; set; }

    public int IdGrv { get; set; }

    public byte IdGrvDrfaTipoRegistro { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public byte IdAutoridadeDivisao { get; set; }

    public int IdUsuarioCadastro { get; set; }

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

    public virtual TbDepGrvDrfaTipoRegistro IdGrvDrfaTipoRegistroNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepGrvDrfaAgendamentoRetiradum> TbDepGrvDrfaAgendamentoRetirada { get; set; } = new List<TbDepGrvDrfaAgendamentoRetiradum>();

    public virtual ICollection<TbDepGrvDrfaArquivoRegistro> TbDepGrvDrfaArquivoRegistros { get; set; } = new List<TbDepGrvDrfaArquivoRegistro>();

    public virtual ICollection<TbDepGrvDrfaRegistroRecuperacao> TbDepGrvDrfaRegistroRecuperacaos { get; set; } = new List<TbDepGrvDrfaRegistroRecuperacao>();
}
