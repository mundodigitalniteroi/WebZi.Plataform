using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboqueDrfa
{
    public int IdSolicitacaoReboqueDrfa { get; set; }

    public int IdSolicitacaoReboque { get; set; }

    public byte IdGrvDrfaTipoRegistro { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public byte IdAutoridadeDivisao { get; set; }

    public string AutoridadeDivisaoComplemento { get; set; }

    public string NumeroRegistroRouboFurto { get; set; }

    public string RegistroRouboFurtoMatriculaAgente { get; set; }

    public string RegistroRouboFurtoNomeAgente { get; set; }

    public string EstadoGeralVeiculo { get; set; }

    public virtual TbDepGrvDrfaTipoRegistro IdGrvDrfaTipoRegistroNavigation { get; set; }

    public virtual TbDepSolicitacaoReboque IdSolicitacaoReboqueNavigation { get; set; }
}
