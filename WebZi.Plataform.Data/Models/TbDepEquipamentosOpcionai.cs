using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepEquipamentosOpcionai
{
    public decimal IdEquipamentoOpcional { get; set; }

    public byte? IdEquipamentoOpcionalLocalizacao { get; set; }

    public int IdUsuario { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public int? OrdemVistoria { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string ItemObrigatorio { get; set; }

    public string Status { get; set; }

    public string ItemOcorrenciaDetranBa { get; set; }

    public virtual TbDepEquipamentosOpcionaisLocalizacao IdEquipamentoOpcionalLocalizacaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }

    public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionais { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

    public virtual ICollection<TbDepTipoVeiculosEquipamentosAssociacao> TbDepTipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TbDepTipoVeiculosEquipamentosAssociacao>();
}
