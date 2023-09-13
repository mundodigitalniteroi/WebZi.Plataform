using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepRepositorioArquivo
{
    public int RepositorioArquivoId { get; set; }

    public short NomeTabelaOrigemId { get; set; }

    public int TabelaOrigemId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public string NomeArquivo { get; set; }

    public int TamanhoBytes { get; set; }

    public string Url { get; set; }

    public string TipoArquivo { get; set; }

    public string PermissaoAcesso { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public string TipoCadastro { get; set; }

    public virtual TbDepConfiguracoesNomeTabelaOrigem NomeTabelaOrigem { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
