using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class GrvMap : IEntityTypeConfiguration<GrvModel>
    {
        public void Configure(EntityTypeBuilder<GrvModel> builder)
        {
            builder
                .ToTable("tb_dep_grv", "dbo", tb =>
                {
                    tb.HasTrigger("tr_log_del_grv");
                    tb.HasTrigger("tr_log_del_grv_new");
                    tb.HasTrigger("tr_log_upd_grv");
                    tb.HasTrigger("tr_log_upd_grv_leilao");
                    tb.HasTrigger("tr_log_upd_grv_new");
                })
                .HasKey(e => e.GrvId);

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EnderecoLocalizacaoVeiculoBairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");

            builder.Property(e => e.Chassi)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("chassi");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataHoraGuarda)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_guarda");

            builder.Property(e => e.DataHoraRemocao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_remocao");

            builder.Property(e => e.DataOficio)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_oficio");

            builder.Property(e => e.DataTransbordo)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_transbordo");

            builder.Property(e => e.DistanciaAteAcautelamento)
                .HasColumnType("numeric(18, 0)");

            builder.Property(e => e.Divergencia1)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("divergencia1");

            builder.Property(e => e.Divergencia2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("divergencia2");

            builder.Property(e => e.Divergencia3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("divergencia3");

            builder.Property(e => e.Divergencia4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("divergencia4");

            builder.Property(e => e.Divergencia5)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("divergencia5");

            builder.Property(e => e.EstacionamentoNumeroVaga)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estacionamento_numero_vaga");

            builder.Property(e => e.EstacionamentoSetor)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("estacionamento_setor");

            builder.Property(e => e.FaturamentoProdutoId)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("('DEP')")
                .IsFixedLength()
                .HasColumnName("faturamento_produto_codigo");

            builder.Property(e => e.FlagChaveDeposito)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_chave_deposito");

            builder.Property(e => e.FlagComboio)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_comboio");

            builder.Property(e => e.FlagEstadoLacre)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_estado_lacre");

            builder.Property(e => e.FlagGgv)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasComment("Flag que identifica se o GGV já foi cadastrado")
                .HasColumnName("flag_ggv");

            builder.Property(e => e.FlagTransbordo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_transbordo");

            builder.Property(e => e.FlagVeiculoMesmasCondicoes)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_veiculo_mesmas_condicoes");

            builder.Property(e => e.FlagVeiculoNaoIdentificado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_veiculo_nao_identificado");

            builder.Property(e => e.FlagVeiculoNaoOstentaPlaca)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_veiculo_nao_ostenta_placa");

            builder.Property(e => e.FlagVeiculoRoubadoFurtado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_veiculo_roubado_furtado");

            builder.Property(e => e.FlagVeiculoSemRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_veiculo_sem_registro");

            builder.Property(e => e.FlagVistoria)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_vistoria");

            builder.Property(e => e.AutoridadeResponsavelId)
                .HasColumnName("id_autoridade_responsavel");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoCEPId)
                .HasColumnName("id_cep");

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.CorId)
                .HasColumnName("id_cor");

            builder.Property(e => e.CorOstentadaId)
                .HasColumnName("id_cor_ostentada");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.MarcaModeloId)
                .HasColumnName("id_detran_marca_modelo");

            builder.Property(e => e.LiberacaoId)
                .HasColumnName("id_liberacao");

            builder.Property(e => e.MotivoApreensaoId)
                .HasColumnName("id_motivo_apreensao");

            builder.Property(e => e.ReboqueId)
                .HasColumnName("id_reboque");

            builder.Property(e => e.ReboquistaId)
                .HasColumnName("id_reboquista");

            builder.Property(e => e.StatusOperacaoId)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('P')")
                .IsFixedLength()
                .HasColumnName("id_status_operacao");

            builder.Property(e => e.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioCadastroGgvId)
                .HasComment("ID do Usuário que realizou o cadastro das informações de GGV")
                .HasColumnName("id_usuario_cadastro_ggv");

            builder.Property(e => e.UsuarioEdicaoId)
                .HasColumnName("id_usuario_edicao");

            builder.Property(e => e.Latitude)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("latitude");

            builder.Property(e => e.LatitudeAcautelamento)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.EnderecoLocalizacaoVeiculoLogradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            builder.Property(e => e.Longitude)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("longitude");

            builder.Property(e => e.LongitudeAcautelamento)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.MatriculaAutoridadeResponsavel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("matricula_autoridade_responsavel");

            builder.Property(e => e.MatriculaComandante)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("matricula_comandante");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoMunicipio)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");

            builder.Property(e => e.NomeAutoridadeResponsavel)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_autoridade_responsavel");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoNumero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");

            builder.Property(e => e.NumeroChave)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_chave");

            builder.Property(e => e.NumeroFormularioGrv)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");

            builder.Property(e => e.NumeroOficio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_oficio");

            builder.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("placa");

            builder.Property(e => e.PlacaOstentada)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa_ostentada");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoPontoReferencia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ponto_referencia");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoReferencia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("referencia");

            builder.Property(e => e.Renavam)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("renavam");

            builder.Property(e => e.Rfid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rfid");

            builder.Property(e => e.TermoDetran)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("termo_detran");

            builder.Property(e => e.EnderecoLocalizacaoVeiculoUF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            builder.Property(e => e.VeiculoUF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            //builder.HasOne(d => d.MarcaModelo).WithMany(p => p.Grvs)
            //    .HasForeignKey(d => d.MarcaModeloId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.UsuarioCadastroGrvs)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioAlteracao).WithMany(p => p.UsuarioAlteracaoGrvs)
                .HasForeignKey(d => d.UsuarioAlteracaoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioEdicao).WithMany(p => p.UsuarioEdicaoGrvs)
                .HasForeignKey(d => d.UsuarioEdicaoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioCadastroGgv).WithMany(p => p.UsuarioCadastroGgvs)
                .HasForeignKey(d => d.UsuarioCadastroGgvId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.FaturamentoProduto).WithMany(p => p.Grvs)
                .HasForeignKey(d => d.FaturamentoProdutoId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioClienteDepositoGrvModel).WithOne(p => p.Grv)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}