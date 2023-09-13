using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvCobrancasLegai
{
    public int IdCobrancaLegal { get; set; }

    public int IdGrv { get; set; }

    public byte IdTipoCobrancaLegal { get; set; }

    public int? IdMunicipio { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string NumeroAutoInfracao { get; set; }

    public decimal? Exercicio { get; set; }

    public decimal Valor { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepTiposCobrancasLegai IdTipoCobrancaLegalNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
