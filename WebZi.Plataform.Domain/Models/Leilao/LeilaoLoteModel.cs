using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Leilao
{
    public class LeilaoLoteModel
    {
        public int LeilaoLoteId { get; set; }

        public int LeilaoId { get; set; }

        public int? GrvId { get; set; }

        public int LeilaoLoteStatusId { get; set; }

        public string StatusOperacaoId { get; set; }

        public int? NumeroLote { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public string Renavan { get; set; }

        public string AnoFabricacao { get; set; }

        public string AnoModelo { get; set; }

        public string MarcaModelo { get; set; }

        public string Municipio { get; set; }

        public string UF { get; set; }

        public string Cor { get; set; }

        public string CorOstentada { get; set; }

        public string TipoVeiculo { get; set; }

        public string Localizacao { get; set; }

        public string ResponsavelRemocao { get; set; }

        public string Patio { get; set; }

        public string Observacoes { get; set; }

        public string Reboque { get; set; }

        public DateTime? DataHoraApreensao { get; set; }

        public string SituacaoLote { get; set; }

        public string SituacaoPlaca { get; set; }

        public string SituacaoChassi { get; set; }

        public string SituacaoGnv { get; set; }

        public string SituacaoVeiculo { get; set; }

        public string TipoCombustivel { get; set; }

        public string ProcedenciaVeiculo { get; set; }

        public string Chave { get; set; }

        public string ValorAvaliacao { get; set; }

        public string NumeroMotor { get; set; }

        public string LanceMinimo { get; set; }

        public int? Quilometragem { get; set; }

        public string Cambio { get; set; }

        public string ArCondicionado { get; set; }

        public string Direcao { get; set; }

        public string VidroEletrico { get; set; }

        public string TravaEletrica { get; set; }

        public string StatusPericia { get; set; }

        public int? SituacaoVeiculoPericia { get; set; }

        public string ConferidoPatio { get; set; } = "N";

        public string FlagTransacao { get; set; } = "C";

        public string FlagAgendado { get; set; } = "S";

        public string FlagNormalizado { get; set; } = "N";

        public int? LogRecolhimento { get; set; }

        public string InformacaoRoubo { get; set; }

        public string RestricaoEstelionato { get; set; }

        public string ObsTransacao { get; set; }

        public DateTime DataHoraInsercao { get; set; }

        public string PlacaOstentada { get; set; }

        public DateTime? DataHoraAlteracao { get; set; }

        public int UsuarioInclusaoId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string FlagAnaliseSobra { get; set; } = "N";

        public DateTime? DataHoraEntrada { get; set; }

        public DateTime? DataHoraLiberacao { get; set; }

        public int? NumeroItemLote { get; set; }

        public bool? Periciado { get; set; }

        public virtual LeilaoModel Leilao { get; set; }

        public virtual GrvModel Grv { get; set; }

        public virtual LeilaoLoteStatusModel LeilaoLoteStatus { get; set; }

        //public virtual ICollection<TbLeilaoLotesProprietario> TbLeilaoLotesProprietarios { get; set; }

        //public virtual ICollection<TbLeilaoLotesRestrico> TbLeilaoLotesRestricos { get; set; }

        //public virtual ICollection<TbLeilaoLotesTransaco> TbLeilaoLotesTransacos { get; set; }
    }
}