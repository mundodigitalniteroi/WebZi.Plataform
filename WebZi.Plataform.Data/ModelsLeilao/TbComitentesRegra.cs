using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbComitentesRegra
{
    public int Id { get; set; }

    public int IdComitente { get; set; }

    public int IdRegra { get; set; }

    public string Valor { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public virtual TbComitente IdComitenteNavigation { get; set; }

    public virtual TbRegra IdRegraNavigation { get; set; }
}
