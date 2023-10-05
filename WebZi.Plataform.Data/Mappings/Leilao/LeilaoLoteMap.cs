using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.LeilaoLote
{
    public class LeilaoLoteMap : IEntityTypeConfiguration<LeilaoLoteModel>
    {
        public void Configure(EntityTypeBuilder<LeilaoLoteModel> builder)
        {
            builder
                .ToTable("tb_leilao_lotes", "dbo", tb =>
                {
                    tb.HasTrigger("tb_leilao_lotes_delete");
                    tb.HasTrigger("tb_leilao_lotes_insert");
                    tb.HasTrigger("tb_leilao_lotes_update");
                    tb.HasTrigger("tr_log_upd_leilao_grv");
                    tb.HasTrigger("tr_log_upd_leilao_lotes");
                    tb.HasTrigger("tr_log_upd_leilao_processo_placa_chassi");
                })
                .HasKey(e => e.LeilaoLoteId);

            builder.Property(e => e.LeilaoLoteId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.AnoFabricacao)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            
            builder.Property(e => e.AnoModelo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            
            builder.Property(e => e.ArCondicionado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ar_condicionado");
            
            builder.Property(e => e.Cambio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cambio");
            
            builder.Property(e => e.Chassi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("chassi");
            
            builder.Property(e => e.Chave)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .HasColumnName("chave");
            
            builder.Property(e => e.ConferidoPatio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("conferido_patio");
            
            builder.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            
            builder.Property(e => e.CorOstentada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor_ostentada");
            
            builder.Property(e => e.DataHoraAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_alteracao");
            
            builder.Property(e => e.DataHoraApreensao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_apreensao");
            
            builder.Property(e => e.DataHoraEntrada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_entrada");
            
            builder.Property(e => e.DataHoraInsercao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_insercao");
            
            builder.Property(e => e.DataHoraLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_liberacao");
            
            builder.Property(e => e.Direcao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direcao");
            
            builder.Property(e => e.FlagAgendado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_agendado");
            
            builder.Property(e => e.FlagAnaliseSobra)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_analise_sobra");
            
            builder.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_normalizado");
            
            builder.Property(e => e.FlagTransacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength()
                .HasColumnName("flag_transacao");
            
            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");
            
            builder.Property(e => e.LeilaoId)
                .HasColumnName("id_leilao");
            
            builder.Property(e => e.LeilaoLoteStatusId)
                .HasColumnName("id_status_lote");
            
            builder.Property(e => e.StatusOperacaoId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id_status_operacao");
            
            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");
            
            builder.Property(e => e.UsuarioInclusaoId)
                .HasColumnName("id_usuario_inclusao");
            
            builder.Property(e => e.InformacaoRoubo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            
            builder.Property(e => e.LanceMinimo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("lance_minimo");
            
            builder.Property(e => e.Localizacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localizacao");
            
            builder.Property(e => e.LogRecolhimento)
                .HasColumnName("log_recolhimento");
            
            builder.Property(e => e.MarcaModelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca_modelo");
            
            builder.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            
            builder.Property(e => e.NumeroFormularioGrv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");
            
            builder.Property(e => e.NumeroItemLote)
                .HasDefaultValueSql("((0))")
                .HasColumnName("numero_item_lote");
            
            builder.Property(e => e.NumeroLote)
                .HasColumnName("numero_lote");
            
            builder.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            
            builder.Property(e => e.ObsTransacao)
                .IsUnicode(false)
                .HasColumnName("obs_transacao");
            
            builder.Property(e => e.Observacoes)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            
            builder.Property(e => e.Patio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patio");
            
            builder.Property(e => e.Periciado)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periciado");
            
            builder.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa");
            
            builder.Property(e => e.PlacaOstentada)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa_ostentada");
            
            builder.Property(e => e.ProcedenciaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("procedencia_veiculo");
            
            builder.Property(e => e.Quilometragem)
                .HasColumnName("quilometragem");
            
            builder.Property(e => e.Reboque)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reboque");
            
            builder.Property(e => e.Renavan)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("renavan");
            
            builder.Property(e => e.ResponsavelRemocao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_remocao");
            
            builder.Property(e => e.RestricaoEstelionato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao_estelionato");
            
            builder.Property(e => e.SituacaoChassi)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_chassi");
            
            builder.Property(e => e.SituacaoGnv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_gnv");
            
            builder.Property(e => e.SituacaoLote)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_lote");
            
            builder.Property(e => e.SituacaoPlaca)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_placa");
            
            builder.Property(e => e.SituacaoVeiculo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_veiculo");
            
            builder.Property(e => e.SituacaoVeiculoPericia)
                .HasColumnName("situacao_veiculo_pericia");
            
            builder.Property(e => e.StatusPericia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_pericia");
            
            builder.Property(e => e.TipoCombustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_combustivel");
            
            builder.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculo");
            
            builder.Property(e => e.TravaEletrica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trava_eletrica");
            
            builder.Property(e => e.UF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf");
            
            builder.Property(e => e.ValorAvaliacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("valor_avaliacao");
            
            builder.Property(e => e.VidroEletrico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vidro_eletrico");
        }
    }
}