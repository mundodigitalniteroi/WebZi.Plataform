using WebZi.Plataform.Domain.DTO.Empresa;
using WebZi.Plataform.Domain.DTO.Faturamento.Servico;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.Vistoria;

namespace WebZi.Plataform.Domain.DTO.GGV
{
    public class DadosMestresDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public CorListDTO ListagemCorOstentada { get; set; }

        public EmpresaListDTO ListagemEmpresa { get; set; }

        public TabelaGenericaListDTO ListagemEstadoGeralVeiculo { get; set; }

        public VistoriaSituacaoChassiListDTO ListagemSituacaoChassi { get; set; }

        public VistoriaStatusListDTO ListagemStatusVistoria { get; set; }

        public TipoAvariaListDTO ListagemTipoAvaria { get; set; }

        public TabelaGenericaListDTO ListagemTipoCadastroFotoGGV { get; set; }

        public TabelaGenericaListDTO ListagemTipoDirecao { get; set; }

        public ServicoAssociadoTipoVeiculoListDTO ListagemServicoAssociadoVeiculo { get; set; }
    }
}