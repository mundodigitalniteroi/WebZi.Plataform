using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAgente
{
    public long Id { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DataHoraLog { get; set; }

    public int AgenteId { get; set; }

    public int AutoridadeResponsavelId { get; set; }

    public int TipoProfissaoId { get; set; }

    public short OrgaoEmissorId { get; set; }

    public byte AutoridadeDivisaoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public string Matricula { get; set; }

    public string Login { get; set; }

    public string Senha { get; set; }

    public string Nome { get; set; }

    public DateTime? DataUltimoLogin { get; set; }

    public DateTime? DataUltimaAlteracaoSenha { get; set; }

    public DateTime? DataDesativacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
