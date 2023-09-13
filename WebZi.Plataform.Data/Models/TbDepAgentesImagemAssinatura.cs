using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAgentesImagemAssinatura
{
    public int AgenteImagemAssinaturaId { get; set; }

    public int AgenteId { get; set; }

    public byte[] Assinatura { get; set; }

    public virtual TbDepAgente Agente { get; set; }
}
