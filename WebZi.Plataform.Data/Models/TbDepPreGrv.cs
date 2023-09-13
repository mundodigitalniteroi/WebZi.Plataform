using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPreGrv
{
    public int IdPreGrv { get; set; }

    public int NumeroGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string GpsLatitude { get; set; }

    public string GpsLongitude { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataRecolhimento { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Tipo { get; set; }

    public DateTime? DataPagamento { get; set; }

    public int? QtdDiasDep { get; set; }

    public decimal? IdAgente { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepPreGrvFoto> TbDepPreGrvFotos { get; set; } = new List<TbDepPreGrvFoto>();
}
