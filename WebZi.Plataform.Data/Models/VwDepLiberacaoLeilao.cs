using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepLiberacaoLeilao
{
    public int IdGrv { get; set; }

    public string Nome { get; set; }

    public string EnderecoCompleto { get; set; }

    public DateTime DataAtual { get; set; }

    public string Registro { get; set; }

    public string Mensagem1 { get; set; }

    public string Processo { get; set; }

    public string MarcaModelo { get; set; }

    public string Placa { get; set; }

    public string CodigoLote { get; set; }

    public string Renavam { get; set; }

    public string Chassi { get; set; }

    public string Cor { get; set; }

    public string Ano { get; set; }

    public string Mensagem2 { get; set; }

    public string Mensagem3 { get; set; }

    public string Mensagem4 { get; set; }

    public string Mensagem5 { get; set; }

    public string Mensagem6 { get; set; }

    public string ArrematanteNomeArrematante { get; set; }

    public string ArrematanteCpfCnpj { get; set; }

    public string GrvEstacionamentoSetor { get; set; }

    public string GrvEstacionamentoNumeroVaga { get; set; }

    public string GrvNumeroChave { get; set; }
}
