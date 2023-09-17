using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class ImportacaoLote
{
    public double? IdLote { get; set; }

    public double? IdLeilao { get; set; }

    public string Leilao { get; set; }

    public string Lote { get; set; }

    public string Processo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string Renavan { get; set; }

    public string Uf { get; set; }

    public string Cor { get; set; }

    public string Ano { get; set; }

    public string Ano1 { get; set; }

    public string F14 { get; set; }

    public string Combustivel { get; set; }

    public string NumeroMotor { get; set; }

    public string StatusLote { get; set; }

    public string Observacoes { get; set; }

    public string Apreensao { get; set; }

    public string TipoVeiculo { get; set; }

    public string Classificacao { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public string Setor { get; set; }

    public string Vaga { get; set; }

    public string Chave { get; set; }

    public string Código { get; set; }

    public decimal? Fipe { get; set; }
}
