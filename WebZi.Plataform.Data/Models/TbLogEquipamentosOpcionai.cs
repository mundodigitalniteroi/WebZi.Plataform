using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogEquipamentosOpcionai
{
    public long Id { get; set; }

    public decimal? IdEquipamentoOpcional { get; set; }

    public byte? IdEquipamentoOpcionalLocalizacao { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public int? OrdemVistoria { get; set; }

    public string Descricao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string ItemObrigatorio { get; set; }

    public string Status { get; set; }

    public string ItemOcorrenciaDetranBa { get; set; }

    public DateTime DatahoraLog { get; set; }
}
