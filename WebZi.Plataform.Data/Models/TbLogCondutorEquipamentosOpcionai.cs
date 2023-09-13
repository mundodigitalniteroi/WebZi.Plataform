using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogCondutorEquipamentosOpcionai
{
    public long Id { get; set; }

    public int? IdCondutorEquipamentoOpcional { get; set; }

    public int? IdGrv { get; set; }

    public decimal? IdEquipamentoOpcional { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAtualizacao { get; set; }

    public int? CodAvaria { get; set; }

    public string Avariado { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public string FlagPossuiEquipamento { get; set; }
}
