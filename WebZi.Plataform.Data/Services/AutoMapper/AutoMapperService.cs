using AutoMapper;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.Deposito;
using WebZi.Plataform.Domain.ViewModel.Empresa;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.ViewModel.Pessoa;
using WebZi.Plataform.Domain.ViewModel.Servico;
using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;
using WebZi.Plataform.Domain.Views.Localizacao;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services.AutoMapper
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            // CreateMap<Model, ViewModel>();
            // CreateMap<ViewModel, Model>();

            // Model to ViewModel
            CreateMap<AtendimentoModel, AtendimentoViewModel>()
                .ForMember(dest => dest.IdentificadorAtendimento, opt => opt.MapFrom(src => src.AtendimentoId))
                .ForMember(dest => dest.IdentificadorGrv, opt => opt.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, opt => opt.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<AgenciaBancariaModel, AgenciaBancariaViewModel>()
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, opt => opt.MapFrom(src => src.AgenciaBancariaId));

            CreateMap<AutoridadeResponsavelModel, AutoridadeResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, opt => opt.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, opt => opt.MapFrom(src => src.OrgaoEmissorId))
                .ForMember(dest => dest.IdentificadorSistemaExterno, opt => opt.MapFrom(src => src.SistemaExternoId));

            CreateMap<BancoModel, BancoViewModel>()
                .ForMember(dest => dest.IdentificadorBanco, opt => opt.MapFrom(src => src.BancoId));

            CreateMap<ClienteModel, ClienteViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, opt => opt.MapFrom(src => src.AgenciaBancariaId))
                .ForMember(dest => dest.IdentificadorCEP, opt => opt.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, opt => opt.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, opt => opt.MapFrom(src => src.TipoMeioCobrancaId))
                .ForMember(dest => dest.IdentificadorEmpresa, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorOrgaoExecutivoTransito, opt => opt.MapFrom(src => src.OrgaoExecutivoTransitoId))
                .ForMember(dest => dest.IdentificadorTipoChavePIX, opt => opt.MapFrom(src => src.PixTipoChaveId));

            CreateMap<ClienteViewModel, ClienteSimplificadoViewModel>();

            CreateMap<CorModel, CorViewModel>()
                .ForMember(dest => dest.IdentificadorCor, opt => opt.MapFrom(src => src.CorId));

            CreateMap<DepositoModel, DepositoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorEmpresa, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorCEP, opt => opt.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, opt => opt.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorSistemaExterno, opt => opt.MapFrom(src => src.SistemaExternoId));

            CreateMap<EmpresaModel, EmpresaViewModel>()
                .ForMember(dest => dest.IdentificadorEmpresa, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorEmpresaMatriz, opt => opt.MapFrom(src => src.EmpresaMatrizId))
                .ForMember(dest => dest.IdentificadorEmpresaClassificacao, opt => opt.MapFrom(src => src.EmpresaClassificacaoId))
                .ForMember(dest => dest.IdentificadorCEP, opt => opt.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorCNAE, opt => opt.MapFrom(src => src.CnaeId))
                .ForMember(dest => dest.IdentificadorCNAEListaServico, opt => opt.MapFrom(src => src.CnaeListaServicoId));

            CreateMap<FaturamentoProdutoModel, FaturamentoProdutoViewModel>()
                .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<GrvModel, GrvViewModel>()
                .ForMember(dest => dest.IdentificadorGrv, opt => opt.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorTipoVeiculo, opt => opt.MapFrom(src => src.TipoVeiculoId))
                .ForMember(dest => dest.IdentificadorReboquista, opt => opt.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorReboque, opt => opt.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, opt => opt.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorCor, opt => opt.MapFrom(src => src.CorId))
                .ForMember(dest => dest.IdentificadorMarcaModelo, opt => opt.MapFrom(src => src.MarcaModeloId))
                .ForMember(dest => dest.IdentificadorMotivoApreensao, opt => opt.MapFrom(src => src.MotivoApreensaoId))
                .ForMember(dest => dest.IdentificadorStatusOperacao, opt => opt.MapFrom(src => src.StatusOperacaoId))
                .ForMember(dest => dest.IdentificadorLiberacao, opt => opt.MapFrom(src => src.LiberacaoId))
                .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<LacreModel, LacreViewModel>()
                .ForMember(dest => dest.IdentificadorLacre, opt => opt.MapFrom(src => src.LacreId));

            CreateMap<MarcaModeloModel, MarcaModeloViewModel>()
                .ForMember(dest => dest.IdentificadorMarcaModelo, opt => opt.MapFrom(src => src.MarcaModeloId));

            CreateMap<MotivoApreensaoModel, MotivoApreensaoViewModel>()
                .ForMember(dest => dest.IdentificadorMotivoApreensao, opt => opt.MapFrom(src => src.MotivoApreensaoId));

            CreateMap<QualificacaoResponsavelModel, QualificacaoResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, opt => opt.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<ReboqueModel, ReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorReboque, opt => opt.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId));

            CreateMap<ReboquistaModel, ReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorReboquista, opt => opt.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId));

            CreateMap<TabelaGenericaModel, TabelaGenericaViewModel>()
                .ForMember(dest => dest.Identificador, opt => opt.MapFrom(src => src.TabelaGenericaId));

            CreateMap<TipoAvariaModel, TipoAvariaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoAvaria, opt => opt.MapFrom(src => src.TipoAvariaId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, opt => opt.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, opt => opt.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoMeioCobrancaModel, TipoMeioCobrancaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, opt => opt.MapFrom(src => src.TipoMeioCobrancaId));

            CreateMap<TipoVeiculoModel, TipoVeiculoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoVeiculo, opt => opt.MapFrom(src => src.TipoVeiculoId));

            CreateMap<UsuarioModel, UsuarioViewModel>()
                .ForMember(dest => dest.IdentificadorUsuario, opt => opt.MapFrom(src => src.UsuarioId));

            CreateMap<VistoriaSituacaoChassiModel, VistoriaSituacaoChassiViewModel>()
                .ForMember(dest => dest.IdentificadorSituacaoChassi, opt => opt.MapFrom(src => src.VistoriaSituacaoChassiId));

            CreateMap<ViewEnderecoCompletoModel, EnderecoViewModel>()
                .ForMember(dest => dest.IdentificadorCEP, opt => opt.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorMunicipio, opt => opt.MapFrom(src => src.MunicipioId))
                .ForMember(dest => dest.IdentificadorBairro, opt => opt.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouroId));

            CreateMap<ViewUsuarioClienteDepositoReboqueModel, UsuarioClienteDepositoReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboque, opt => opt.MapFrom(src => src.ReboqueId));

            CreateMap<ViewUsuarioClienteDepositoReboquistaModel, UsuarioClienteDepositoReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboquista, opt => opt.MapFrom(src => src.ReboquistaId));

            CreateMap<ViewUsuarioClienteDepositoModel, ClienteDepositoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, opt => opt.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorCliente, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.DepositoNome))
                .ForMember(dest => dest.FlagAtivo, opt => opt.MapFrom(src => src.DepositoFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboqueViewModel, ReboqueSimplificadoViewModel>()
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.ReboquePlaca))
                .ForMember(dest => dest.FlagAtivo, opt => opt.MapFrom(src => src.ReboqueFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboquistaViewModel, ReboquistaSimplificadoViewModel>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ReboquistaNome))
                .ForMember(dest => dest.FlagAtivo, opt => opt.MapFrom(src => src.ReboquistaFlagAtivo));

            // ViewModel to Model
            CreateMap<CadastroCondutorViewModel, CondutorModel>();

            CreateMap<UsuarioClienteDepositoReboqueViewModel, UsuarioClienteDepositoReboqueViewModel>();

            CreateMap<CadastroEnquadramentoInfracaoGrvViewModel, EnquadramentoInfracaoGrvModel>()
                .ForMember(dest => dest.EnquadramentoInfracaoId, opt => opt.MapFrom(src => src.IdentificadorEnquadramentoInfracao));
        }
    }
}