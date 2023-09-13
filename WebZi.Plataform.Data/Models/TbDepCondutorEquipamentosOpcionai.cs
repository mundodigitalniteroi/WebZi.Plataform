using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepCondutorEquipamentosOpcionai
{
    public int IdCondutorEquipamentoOpcional { get; set; }

    public int IdGrv { get; set; }

    public decimal IdEquipamentoOpcional { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAtualizacao { get; set; }

    public int? CodAvaria { get; set; }

    public string Avariado { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public string FlagPossuiEquipamento { get; set; }

    public virtual TbDepEquipamentosOpcionai IdEquipamentoOpcionalNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAtualizacaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepCondutorEquipamentosOpcionaisNaoConformidade> TbDepCondutorEquipamentosOpcionaisNaoConformidades { get; set; } = new List<TbDepCondutorEquipamentosOpcionaisNaoConformidade>();
}
