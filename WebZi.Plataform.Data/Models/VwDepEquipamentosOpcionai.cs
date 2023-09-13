using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepEquipamentosOpcionai
{
    public decimal IdEquipamentoOpcional { get; set; }

    public byte? IdEquipamentoOpcionalLocalizacao { get; set; }

    public int IdUsuario { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public int? OrdemVistoria { get; set; }

    public string Descricao { get; set; }

    public string Localizacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string ItemObrigatorio { get; set; }

    public string Status { get; set; }
}
