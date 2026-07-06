using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Domain.DTO.DetranHub;

public class VeiculoDetranHubResponse
{
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public bool? ChassiRemarcado { get; set; }
    public string Renavam { get; set; }
    public string Uf { get; set; }
    public int? AnoFabricacao { get; set; }
    public int? AnoModelo { get; set; }
    public string CategoriaCodigo { get; set; }
    public string CategoriaDescricao { get; set; }
    public string Classificacao { get; set; }
    public string MarcaModelo { get; set; }
    public string TipoVeiculo { get; set; }
    public string CorPrimaria { get; set; }
    public int? CapacidadePassageiros { get; set; }
    public decimal? PesoBrutoTotal { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DebitosDetranHubResponse Debitos { get; set; }
    public RestricoesOperacionaisDetranHubResponse RestricoesOperacionais { get; set; }
    public List<RestricaoDetranHubResponse> Restricoes { get; set; }
}
