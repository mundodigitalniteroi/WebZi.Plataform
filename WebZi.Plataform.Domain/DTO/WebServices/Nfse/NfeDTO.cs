using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class NfeDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();
        public int GrvId { get; set; }

        public int? FaturamentoServicoTipoVeiculoId { get; set; }

        public string IdentificadorNota { get; set; }

        public int? NfeComplementarId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public string Cnpj { get; set; }

        public string Numero { get; set; }

        public string CodigoVerificacao { get; set; }

        public int? CodigoRetorno { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// C: Cadastro;
        /// A: Aguardando Processamento (envio da solicitação com sucesso, para a Prefeitura);
        /// P: Processado (download da Nfe e atualização da Nfe no Sistema concluídos com sucesso);
        /// E: Erro (quando a Prefeitura indicou algum problema);
        /// R: Reprocessar (marcação manual para o envio de uma nova solicitação de Nfe para o mesmo GRV, esta opção gera um novo registro de Nfe);
        /// S: ReproceSsado (conclusão do reprocessamento);
        /// I: Inválido;
        /// N: CaNcelado;
        /// M: Cadastro Manual.
        /// </summary>
        public string Status { get; set; } = "C";

        public string StatusNfe { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string NumeroRps { get; set; }

        public string NumeroNotaFiscal { get; set; }

        public string CaminhoXmlNotaFiscal { get; set; }

        public string Referencia { get; set; }

        public string SerieRps { get; set; }
    }
}
