using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTransacoesDetranBaStatus
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string CodigoDetran { get; set; }

    public int IdGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public byte? IdMotivoApreensao { get; set; }

    public int IdUsuario { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public string TermoRemocaoApreensao { get; set; }

    public int RegistrarApreensao { get; set; }

    public int RegistrarLiberacao { get; set; }

    public int RegistrarEntrega { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataEnvioLog { get; set; }
}
