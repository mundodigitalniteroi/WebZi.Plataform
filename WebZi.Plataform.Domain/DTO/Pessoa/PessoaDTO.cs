namespace WebZi.Plataform.Domain.DTO.Pessoa;

public class PessoaDTO
{
    public long IdentificadorPessoa { get; set; }
    public string Nome { get; set; }
    public string NomeDoMeio { get; set; }
    public string Sobrenome { get; set; }
    public PessoaDocumentoDTO Documento { get; set; }
}