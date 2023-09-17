﻿using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilao
{
    public int Id { get; set; }

    public int IdLeiloeiro { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdComitente { get; set; }

    public int IdExpositor { get; set; }

    public int IdStatus { get; set; }

    public int? IdEmpresa { get; set; }

    public int IdRegraPrestacaoContas { get; set; }

    public string Descricao { get; set; }

    public string NomeLocal { get; set; }

    public string NumeroDiarioOficial { get; set; }

    public string OrdemInternaMatriz { get; set; }

    public string OrdemInternaLeilao { get; set; }

    public string DataLeilao { get; set; }

    public string HoraLeilao { get; set; }

    public DateTime? DataHoraCadastro { get; set; }

    public string DataAgendamento { get; set; }

    public string DataInicioRetirada { get; set; }

    public string DataFimRetirada { get; set; }

    public string DataHoraPublicacaoDiarioOficial { get; set; }

    public string Cep { get; set; }

    public string Endereco { get; set; }

    public string EndNumero { get; set; }

    public string EndComplemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string IdentificacaoLeilaoOrgao { get; set; }

    public DateTime? DataNotificacao { get; set; }

    public DateTime? DataEncerramento { get; set; }

    public DateTime? DataEditalLiberacao { get; set; }

    public string EmailNotificacao { get; set; }

    public string LeilaoDsin { get; set; }

    public virtual TbComitente IdComitenteNavigation { get; set; }

    public virtual TbLeiloeiro IdLeiloeiroNavigation { get; set; }

    public virtual TbLeilaoRegrasPrestacaoConta IdRegraPrestacaoContasNavigation { get; set; }

    public virtual TbLeilaoStatus IdStatusNavigation { get; set; }

    public virtual ICollection<TbArrematantesFatura> TbArrematantesFaturas { get; set; } = new List<TbArrematantesFatura>();

    public virtual ICollection<TbLeilaoEditai> TbLeilaoEditais { get; set; } = new List<TbLeilaoEditai>();

    public virtual ICollection<TbLeilaoLote> TbLeilaoLotes { get; set; } = new List<TbLeilaoLote>();
}
