using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataOperacao
{
    public int AlterDataOperacaoId { get; set; }

    public int AlterDataConfiguracaoId { get; set; }

    public short OperacaoId { get; set; }

    public int ParametroMunicipioId { get; set; }

    public short CfopId { get; set; }

    public short IdentificadorProdutoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepAlterdataConfiguracao AlterDataConfiguracao { get; set; }

    public virtual TbDepAlterdataConfiguracaoCfop Cfop { get; set; }

    public virtual TbDepAlterdataConfiguracaoIdentificadorProduto IdentificadorProduto { get; set; }

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
