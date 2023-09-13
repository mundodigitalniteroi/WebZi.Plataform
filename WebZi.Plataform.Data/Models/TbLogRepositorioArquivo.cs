using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogRepositorioArquivo
{
    public int RepositorioArquivoLogId { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DataHoraLog { get; set; }

    public int? RepositorioArquivoId { get; set; }

    public short? NomeTabelaOrigemId { get; set; }

    public int? TabelaOrigemId { get; set; }

    public string NomeArquivo { get; set; }

    public int? TamanhoBytes { get; set; }

    public string Url { get; set; }

    public string TipoArquivo { get; set; }

    public string PermissaoAcesso { get; set; }

    public DateTime? DataHoraCadastro { get; set; }
}
