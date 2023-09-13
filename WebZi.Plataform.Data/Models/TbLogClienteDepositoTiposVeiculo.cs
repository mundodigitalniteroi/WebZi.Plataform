﻿using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogClienteDepositoTiposVeiculo
{
    public long Id { get; set; }

    public short IdClienteDepositoTipoVeiculo { get; set; }

    public int IdUsuario { get; set; }

    public string Coluna { get; set; }

    public string ValorAnterior { get; set; }

    public string ValorNovo { get; set; }

    public DateTime DatahoraLog { get; set; }

    public string Crud { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}
