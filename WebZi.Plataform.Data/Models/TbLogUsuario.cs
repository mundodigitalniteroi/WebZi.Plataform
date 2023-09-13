using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogUsuario
{
    public long Id { get; set; }

    public int? UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime? DataHoraLog { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Login { get; set; }

    public string Senha1 { get; set; }

    public string Senha2 { get; set; }

    public string Senha3 { get; set; }

    public string Senha4 { get; set; }

    public string Senha5 { get; set; }

    public string SenhaAndroid { get; set; }

    public string Email { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public DateTime? DataCadastroSenha { get; set; }

    public DateTime? DataUltimoAcesso { get; set; }

    public string FlagPermissaoDesconto { get; set; }

    public string FlagPermissaoDataRetroativaFaturamento { get; set; }

    public string FlagReceberEmailErro { get; set; }

    public string FlagAtivo { get; set; }

    public long? PessoaId { get; set; }

    public string Matricula { get; set; }

    public byte? IdTipoOperador { get; set; }

    public int? IdFuncionario { get; set; }

    public string Dummy { get; set; }
}
