using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepCliente
{
    public int IdCliente { get; set; }

    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public string HoraDiaria { get; set; }

    public short MaximoDiariasParaCobranca { get; set; }

    public short MaximoDiasVencimento { get; set; }

    public string CodigoSap { get; set; }

    public string FlagUsarHoraDiaria { get; set; }

    public string FlagEmissaoNotaFiscalSap { get; set; }

    public string FlagCadastrarQuilometragem { get; set; }

    public string FlagCobrarDiariasDiasCorridos { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string FlagAtivo { get; set; }

    public string TipoLogradouro { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Estado { get; set; }

    public string Uf { get; set; }

    public string Cep { get; set; }

    public string EnderecoCompleto { get; set; }
}
