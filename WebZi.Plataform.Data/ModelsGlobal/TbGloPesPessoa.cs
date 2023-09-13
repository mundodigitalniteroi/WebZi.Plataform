using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesPessoa
{
    public long IdPessoa { get; set; }

    public long? IdPessoaPai { get; set; }

    public long? IdPessoaMae { get; set; }

    public byte? IdTipoEstadoCivil { get; set; }

    public short? IdTipoProfissao { get; set; }

    public string Nome { get; set; }

    public string NomeMeio { get; set; }

    public string Sobrenome { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string Sexo { get; set; }

    public string NomeUsuarioCadastro { get; set; }

    public string NomeUsuarioAlteracao { get; set; }

    public string CpfUsuarioCadastro { get; set; }

    public string CpfUsuarioAlteracao { get; set; }

    public string EmpresaUsuarioCadastro { get; set; }

    public string EmpresaUsuarioAlteracao { get; set; }

    public string IpUsuarioCadastro { get; set; }

    public string IpUsuarioAlteracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbGloPesTiposEstadoCivil IdTipoEstadoCivilNavigation { get; set; }

    public virtual TbGloPesTiposProfisso IdTipoProfissaoNavigation { get; set; }

    public virtual ICollection<TbGloPesPessoasDocumentosIdentificacao> TbGloPesPessoasDocumentosIdentificacaos { get; set; } = new List<TbGloPesPessoasDocumentosIdentificacao>();

    public virtual ICollection<TbGloPesPessoasFoto> TbGloPesPessoasFotos { get; set; } = new List<TbGloPesPessoasFoto>();

    public virtual ICollection<TbGloPesPessoasLogradouro> TbGloPesPessoasLogradouros { get; set; } = new List<TbGloPesPessoasLogradouro>();

    public virtual ICollection<TbGloPesPessoasTiposContato> TbGloPesPessoasTiposContatos { get; set; } = new List<TbGloPesPessoasTiposContato>();
}
