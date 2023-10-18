using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Vistoria
{
    public class VistoriaModel
    {
        public int VistoriaId { get; set; }

        public int GrvId { get; set; }

        /// <summary>
        /// Faz referência à Tabela db_global.dbo.tb_glo_emp_empresas
        /// </summary>
        public int? EmpresaVistoriaId { get; set; }

        public byte? VistoriaStatusId { get; set; }

        public byte? VistoriaSituacaoChassiId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string MotivoNaoRealizacaoVistoria { get; set; }

        public string NumeroVistoria { get; set; }

        public string NomeVistoriador { get; set; }

        public string NumeroMotor { get; set; }

        public string ResumoVistoria { get; set; }

        public DateTime? DataVistoria { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// M: MANUAL;
        /// E: ELETRO HIDRÁULICA;
        /// H: HIDRÁULICA.
        /// </summary>
        public string TipoDirecao { get; set; }

        /// <summary>
        /// B: BOM;
        /// E: EXCELENTE;
        /// P: PÉSSIMO;
        /// R: RUIM
        /// </summary>
        public string EstadoGeralVeiculo { get; set; }

        public string FlagPossuiRestricoes { get; set; } = "N";

        public string FlagPossuiPlaca { get; set; } = "N";

        public string FlagPossuiVidroEletrico { get; set; } = "N";

        public string FlagPossuiTravaEletrica { get; set; } = "N";

        public virtual GrvModel Grv { get; set; }

        public virtual VistoriaSituacaoChassiModel VistoriaSituacaoChassi { get; set; }

        public virtual VistoriaStatusModel VistoriaStatus { get; set; }

        //public virtual UsuarioModel UsuarioCadastroId { get; set; }

        //public virtual UsuarioModel UsuarioAlteracaoId { get; set; }
    }
}