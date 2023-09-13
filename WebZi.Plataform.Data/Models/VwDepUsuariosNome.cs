using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepUsuariosNome
{
    public int IdUsuario { get; set; }

    public long? PessoaId { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Login { get; set; }

    public string Matricula { get; set; }

    public string Senha1 { get; set; }

    public string Senha2 { get; set; }

    public string Senha3 { get; set; }

    public string Senha4 { get; set; }

    public string Senha5 { get; set; }

    public string SenhaAndroid { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public DateTime DataCadastroSenha { get; set; }

    public DateTime? DataUltimoAcesso { get; set; }

    public string FlagPermissaoDesconto { get; set; }

    public string FlagPermissaoDataRetroativaFaturamento { get; set; }

    public string FlagReceberEmailErro { get; set; }

    public string FlagAtivo { get; set; }

    public string Nome { get; set; }

    public string NomeMeio { get; set; }

    public string Sobrenome { get; set; }

    public string NomeCompleto { get; set; }

    public string Cpf { get; set; }

    public string CpfFormatado { get; set; }
}
