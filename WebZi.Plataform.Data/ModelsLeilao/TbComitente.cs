using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbComitente
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public string Contrato { get; set; }

    public string Documento { get; set; }

    public string Status { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdCliente { get; set; }

    public int? Iss { get; set; }

    public int TipoImportacao { get; set; }

    public string Cor { get; set; }

    public bool? MonitorTransacao { get; set; }

    public virtual ICollection<TbComitentesRegra> TbComitentesRegras { get; set; } = new List<TbComitentesRegra>();

    public virtual ICollection<TbComitentesTaxa> TbComitentesTaxas { get; set; } = new List<TbComitentesTaxa>();

    public virtual ICollection<TbLeilao> TbLeilaos { get; set; } = new List<TbLeilao>();

    public virtual TbComitentesTipoImportacao TipoImportacaoNavigation { get; set; }
}
