using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrv
{
    public long Id { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdGrv { get; set; }

    public int? IdTarifaTipoVeiculo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public byte? IdTipoVeiculo { get; set; }

    public int? IdReboquista { get; set; }

    public int? IdReboque { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }

    public int? IdCor { get; set; }

    public int? IdCorOstentada { get; set; }

    public int? IdDetranMarcaModelo { get; set; }

    public int? IdCep { get; set; }

    public byte? IdMotivoApreensao { get; set; }

    public string IdStatusOperacao { get; set; }

    public int? IdLiberacao { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public int? IdUsuarioEdicao { get; set; }

    public int? IdUsuarioCadastroGgv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string FaturamentoProdutoCodigo { get; set; }

    public string MatriculaAutoridadeResponsavel { get; set; }

    public string NomeAutoridadeResponsavel { get; set; }

    public string Placa { get; set; }

    public string PlacaOstentada { get; set; }

    public string Chassi { get; set; }

    public string Renavam { get; set; }

    public string Rfid { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string Referencia { get; set; }

    public string PontoReferencia { get; set; }

    public string NumeroChave { get; set; }

    public string EstacionamentoSetor { get; set; }

    public string EstacionamentoNumeroVaga { get; set; }

    public string Divergencia1 { get; set; }

    public string Divergencia2 { get; set; }

    public string Divergencia3 { get; set; }

    public string Divergencia4 { get; set; }

    public string Divergencia5 { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public string NumeroOficio { get; set; }

    public string MatriculaComandante { get; set; }

    public DateTime? DataOficio { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagComboio { get; set; }

    public string FlagVeiculoNaoIdentificado { get; set; }

    public string FlagVeiculoSemRegistro { get; set; }

    public string FlagVeiculoRoubadoFurtado { get; set; }

    public string FlagChaveDeposito { get; set; }

    public string FlagEstadoLacre { get; set; }

    public string FlagVeiculoMesmasCondicoes { get; set; }

    public string FlagGgv { get; set; }

    public string FlagVistoria { get; set; }

    public string TermoDetran { get; set; }

    public string FlagVeiculoNaoOstentaPlaca { get; set; }

    public string FlagTransbordo { get; set; }

    public DateTime? DataTransbordo { get; set; }

    public int? AgenteId { get; set; }

    public string LongitudeAcautelamento { get; set; }

    public string LatitudeAcautelamento { get; set; }

    public decimal? DistanciaAteAcautelamento { get; set; }

    public string VeiculoUf { get; set; }
}
