using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class ListaContato
{
    public int Id { get; set; }

    public int IdGrupoListaContatos { get; set; }

    public string Email { get; set; }

    public string PrimeiroNome { get; set; }

    public string UltimoNome { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string Cidade { get; set; }

    public string Uf { get; set; }

    public string Cep { get; set; }

    public string Fone1 { get; set; }

    public string Fone2 { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public string FlagAtivo { get; set; }
}
