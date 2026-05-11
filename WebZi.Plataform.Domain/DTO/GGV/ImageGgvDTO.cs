namespace WebZi.Plataform.Domain.DTO.GGV;

public class ImageGgvDTO
{
    public int Identificador { get; set; }
    public int? IdentificadorTipoCadastro { get; set; }
    public string? Sigla { get; set; }
    
    public string? TipoCadastro { get; set; }
    public byte[] Imagem { get; set; }
}