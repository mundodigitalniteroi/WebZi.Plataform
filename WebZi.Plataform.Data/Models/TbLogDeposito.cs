using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogDeposito
{
    public long Id { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public int? IdBairro { get; set; }

    public int? IdSistemaExterno { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string EmailNfe { get; set; }

    public byte? GrvMinimoFotosExigidas { get; set; }

    public byte? GrvLimiteMinimoDatahoraGuarda { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public string EnderecoMob { get; set; }

    public string TelefoneMob { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagEnderecoCadastroManual { get; set; }

    public string FlagAtivo { get; set; }

    public string FlagVirtual { get; set; }

    public DateTime? DatahoraLog { get; set; }
}
