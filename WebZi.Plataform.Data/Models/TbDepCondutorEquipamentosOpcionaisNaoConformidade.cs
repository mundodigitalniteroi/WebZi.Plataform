using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepCondutorEquipamentosOpcionaisNaoConformidade
{
    public int IdCondutorEquipamentoOpcionalNaoConformidade { get; set; }

    public int IdCondutorEquipamentoOpcional { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string Explicacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepCondutorEquipamentosOpcionai IdCondutorEquipamentoOpcionalNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
