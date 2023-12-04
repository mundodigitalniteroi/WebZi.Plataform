using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Report;

namespace WebZi.Plataform.Data.Mappings.Report
{
    internal class GuiaPagamentoReboqueEstadiaMap : IEntityTypeConfiguration<ViewGuiaPagamentoReboqueEstadiaModel>
    {
        public void Configure(EntityTypeBuilder<ViewGuiaPagamentoReboqueEstadiaModel> builder)
        {
            builder
                .ToView("vw_dep_rep_guia_pagamento_estadia_reboque", "dbo")
                .HasNoKey();

            builder.Property(e => e.AtendimentoFormaLiberacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("atendimento_forma_liberacao");
            
            builder.Property(e => e.AtendimentoFormaLiberacaoCnh)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("atendimento_forma_liberacao_cnh");
            
            builder.Property(e => e.AtendimentoFormaLiberacaoCpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("atendimento_forma_liberacao_cpf");
            
            builder.Property(e => e.AtendimentoFormaLiberacaoNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("atendimento_forma_liberacao_nome");
            
            builder.Property(e => e.AtendimentoFormaLiberacaoPlaca)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("atendimento_forma_liberacao_placa");
            
            builder.Property(e => e.AtendimentoResponsavelDocumento)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("atendimento_responsavel_documento");
            
            builder.Property(e => e.AtendimentoResponsavelNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("atendimento_responsavel_nome");
            
            builder.Property(e => e.ClienteBairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_bairro");
            
            builder.Property(e => e.ClienteBanco)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_banco");
            
            builder.Property(e => e.ClienteCep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_cep");
            
            builder.Property(e => e.ClienteCnpj)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("cliente_cnpj");
            
            builder.Property(e => e.ClienteComplementoLogradouro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente_complemento_logradouro");
            
            builder.Property(e => e.ClienteEstado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_estado");
            
            builder.Property(e => e.ClienteLogradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_logradouro");
            
            builder.Property(e => e.ClienteMunicipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_municipio");
            
            builder.Property(e => e.ClienteNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente_nome");
            
            builder.Property(e => e.ClienteNumeroLogradouro)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cliente_numero_logradouro");
            
            builder.Property(e => e.ClienteTipoLogradouro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_tipo_logradouro");
            
            builder.Property(e => e.ClienteUf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cliente_uf");
            
            builder.Property(e => e.CodigoAgencia)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("codigo_agencia");
            
            builder.Property(e => e.ContaCorrente)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("conta_corrente");
            
            builder.Property(e => e.Cor)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cor");
            
            builder.Property(e => e.DataHoraAtual)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("data_hora_atual");
            
            builder.Property(e => e.DepositoBairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_bairro");
            
            builder.Property(e => e.DepositoCep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_cep");
            
            builder.Property(e => e.DepositoComplementoLogradouro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deposito_complemento_logradouro");
            
            builder.Property(e => e.DepositoDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deposito_descricao");
            
            builder.Property(e => e.DepositoEstado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_estado");
            
            builder.Property(e => e.DepositoLogradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_logradouro");
            
            builder.Property(e => e.DepositoMunicipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_municipio");
            
            builder.Property(e => e.DepositoNumeroLogradouro)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("deposito_numero_logradouro");
            
            builder.Property(e => e.DepositoTipoLogradouro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_tipo_logradouro");
            
            builder.Property(e => e.DepositoUf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("deposito_uf");
            
            builder.Property(e => e.DigitoVerificador)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("digito_verificador");
            
            builder.Property(e => e.EmpresaAssociadaCnpj)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("empresa_associada_cnpj");
            
            builder.Property(e => e.EmpresaAssociadaNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("empresa_associada_nome");
            
            builder.Property(e => e.FlagClienteRealizaFaturamentoArrecadacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_cliente_realiza_faturamento_arrecadacao");
            
            builder.Property(e => e.GrvChassi)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("grv_chassi");
            
            builder.Property(e => e.GrvDataHoraGuarda)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("grv_data_hora_guarda");
            
            builder.Property(e => e.GrvDataHoraRemocao)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("grv_data_hora_remocao");
            
            builder.Property(e => e.GrvEstacionamentoNumeroVaga)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("grv_estacionamento_numero_vaga");
            
            builder.Property(e => e.GrvEstacionamentoSetor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("grv_estacionamento_setor");
            
            builder.Property(e => e.GrvNumeroChave)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("grv_numero_chave");
            
            builder.Property(e => e.GrvNumeroFormulario)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("grv_numero_formulario");
            
            builder.Property(e => e.GrvPlaca)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("grv_placa");
            
            builder.Property(e => e.GrvRenavam)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("grv_renavam");
            
            builder.Property(e => e.IdAtendimento).HasColumnName("id_atendimento");
            
            builder.Property(e => e.IdGrv).HasColumnName("id_grv");
            
            builder.Property(e => e.MarcaModelo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("marca_modelo");
            
            builder.Property(e => e.QualificacaoResponsavelDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("qualificacao_responsavel_descricao");
            
            builder.Property(e => e.ReboquePlaca)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reboque_placa");
            
            builder.Property(e => e.ReboquistaNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("reboquista_nome");
            
            builder.Property(e => e.TipoVeiculo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculo");
        }
    }
}