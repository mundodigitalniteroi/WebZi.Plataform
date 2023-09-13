using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPixDinamicoTipoStatusGeracao
{
    public byte PixDinamicoTipoStatusGeracaoId { get; set; }

    public string Descricao { get; set; }

    /// <summary>
    /// A: O PIX foi enviado com sucesso ao Banco e está sendo processado;
    /// C: O PIX foi transferido;
    /// R: O PIX não foi transferido.
    /// </summary>
    public string Status { get; set; }

    public virtual ICollection<TbDepPixDinamicoConsultum> TbDepPixDinamicoConsulta { get; set; } = new List<TbDepPixDinamicoConsultum>();

    public virtual ICollection<TbDepPixDinamico> TbDepPixDinamicos { get; set; } = new List<TbDepPixDinamico>();
}
