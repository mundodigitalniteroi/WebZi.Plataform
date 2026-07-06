namespace WebZi.Plataform.Domain.DTO.DetranHub;

public class RestricoesOperacionaisDetranHubResponse
{
    public bool? ComunicacaoVenda { get; set; }
    public bool? BloqueioAdministrativo { get; set; }
    public bool? Renajud { get; set; }
    public bool? RouboFurto { get; set; }
}
