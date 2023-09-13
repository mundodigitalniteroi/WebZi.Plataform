using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAlterdataOperacao
{
    public long LogId { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? AlterDataOperacaoId { get; set; }

    public int? AlterDataConfiguracaoId { get; set; }

    public short? OperacaoId { get; set; }

    public int? ParametroMunicipioId { get; set; }

    public short? CfopId { get; set; }

    public short? IdentificadorProdutoId { get; set; }

    public int? UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }
}
