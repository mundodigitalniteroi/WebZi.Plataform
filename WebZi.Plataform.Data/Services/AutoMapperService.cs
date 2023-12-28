using AutoMapper;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.Deposito;
using WebZi.Plataform.Domain.ViewModel.Documento;
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
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;
using WebZi.Plataform.Domain.Views.Localizacao;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            // CreateMap<Model, ViewModel>();
            // CreateMap<ViewModel, Model>();

            // Model to ViewModel
            CreateMap<AtendimentoModel, AtendimentoViewModel>()
                .ForMember(dest => dest.IdentificadorAtendimento, from => from.MapFrom(src => src.AtendimentoId))
                .ForMember(dest => dest.IdentificadorGrv, from => from.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<AgenciaBancariaModel, AgenciaBancariaViewModel>()
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(src => src.AgenciaBancariaId));

            CreateMap<AutoridadeResponsavelModel, AutoridadeResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(src => src.OrgaoEmissorId));

            CreateMap<BancoModel, BancoViewModel>()
                .ForMember(dest => dest.IdentificadorBanco, from => from.MapFrom(src => src.BancoId));

            CreateMap<ClienteModel, ClienteViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(src => src.AgenciaBancariaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorOrgaoExecutivoTransito, from => from.MapFrom(src => src.OrgaoExecutivoTransitoId))
                .ForMember(dest => dest.IdentificadorTipoChavePIX, from => from.MapFrom(src => src.PixTipoChaveId));

            CreateMap<ClienteViewModel, ClienteSimplificadoViewModel>();

            CreateMap<CorModel, CorViewModel>()
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(src => src.CorId));

            CreateMap<DepositoModel, DepositoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorSistemaExterno, from => from.MapFrom(src => src.SistemaExternoId));

            CreateMap<DetranRioVeiculoModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.IdentificadorVeiculo, from => from.MapFrom(src => src.DetranVeiculoId))
                .ForMember(dest => dest.Classificacao, from => from.MapFrom(src => src.Classificacao.ToNullIfEmpty()))
                .ForMember(dest => dest.CodigoCategoria, from => from.MapFrom(src => src.CodigoCategoria.ToNullIfEmpty()))
                .ForMember(dest => dest.DescricaoCategoria, from => from.MapFrom(src => src.DescricaoCategoria.ToNullIfEmpty()))
                .ForMember(dest => dest.InformacaoRoubo, from => from.MapFrom(src => src.InformacaoRoubo.ToNullIfEmpty()))
                .ForMember(dest => dest.RestricaoEstelionato, from => from.MapFrom(src => src.RestricaoEstelionato.ToNullIfEmpty()))
                .ForMember(dest => dest.Placa, from => from.MapFrom(src => src.Placa.ToNullIfEmpty()))
                .ForMember(dest => dest.Chassi, from => from.MapFrom(src => src.Chassi.ToNullIfEmpty()))
                .ForMember(dest => dest.Uf, from => from.MapFrom(src => src.Uf.ToNullIfEmpty()));

            CreateMap<DetranRioVeiculoModel, DetranRioVeiculoModel>()
                .ForMember(dest => dest.DetranVeiculoId, option => option.Ignore())
                .ForMember(dest => dest.DataCadastro, option => option.Ignore());

            CreateMap<CorModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.Cor, from => from.MapFrom(x => x));

            CreateMap<MarcaModeloModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.MarcaModelo, from => from.MapFrom(x => x));

            CreateMap<EmpresaModel, EmpresaViewModel>()
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorEmpresaMatriz, from => from.MapFrom(src => src.EmpresaMatrizId))
                .ForMember(dest => dest.IdentificadorEmpresaClassificacao, from => from.MapFrom(src => src.EmpresaClassificacaoId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorCNAE, from => from.MapFrom(src => src.CnaeId))
                .ForMember(dest => dest.IdentificadorCNAEListaServico, from => from.MapFrom(src => src.CnaeListaServicoId));

            CreateMap<EnquadramentoInfracaoModel, EnquadramentoInfracaoViewModel>()
                .ForMember(dest => dest.IdentificadorEnquadramentoInfracao, from => from.MapFrom(src => src.EnquadramentoInfracaoId))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.Status));

            CreateMap<FaturamentoProdutoModel, FaturamentoProdutoViewModel>()
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<GrvModel, GrvViewModel>()
                .ForMember(dest => dest.IdentificadorGrv, from => from.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(src => src.TipoVeiculoId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(src => src.CorId))
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(src => src.MarcaModeloId))
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(src => src.MotivoApreensaoId))
                .ForMember(dest => dest.IdentificadorStatusOperacao, from => from.MapFrom(src => src.StatusOperacaoId))
                .ForMember(dest => dest.IdentificadorLiberacao, from => from.MapFrom(src => src.LiberacaoId))
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<LacreModel, LacreViewModel>()
                .ForMember(dest => dest.IdentificadorLacre, from => from.MapFrom(src => src.LacreId));

            CreateMap<MarcaModeloModel, MarcaModeloViewModel>()
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(src => src.MarcaModeloId));

            CreateMap<MotivoApreensaoModel, MotivoApreensaoViewModel>()
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(src => src.MotivoApreensaoId));

            CreateMap<OrgaoEmissorModel, OrgaoEmissorViewModel>()
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(src => src.OrgaoEmissorId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.Descricao));

            CreateMap<QualificacaoResponsavelModel, QualificacaoResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<ReboqueModel, ReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId));

            CreateMap<ReboquistaModel, ReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId));

            CreateMap<TabelaGenericaModel, TabelaGenericaViewModel>()
                .ForMember(dest => dest.Identificador, from => from.MapFrom(src => src.TabelaGenericaId));

            CreateMap<TipoAvariaModel, TipoAvariaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoAvaria, from => from.MapFrom(src => src.TipoAvariaId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoMeioCobrancaModel, TipoMeioCobrancaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId));

            CreateMap<TipoVeiculoModel, TipoVeiculoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(src => src.TipoVeiculoId));

            CreateMap<UsuarioModel, UsuarioViewModel>()
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId));

            CreateMap<VistoriaSituacaoChassiModel, VistoriaSituacaoChassiViewModel>()
                .ForMember(dest => dest.IdentificadorSituacaoChassi, from => from.MapFrom(src => src.VistoriaSituacaoChassiId));

            CreateMap<ViewEnderecoCompletoModel, EnderecoViewModel>()
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorMunicipio, from => from.MapFrom(src => src.MunicipioId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId));

            CreateMap<ViewUsuarioClienteDepositoReboqueModel, UsuarioClienteDepositoReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId));

            CreateMap<ViewUsuarioClienteDepositoReboquistaModel, UsuarioClienteDepositoReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId));

            CreateMap<ViewUsuarioClienteDepositoModel, ClienteDepositoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.DepositoNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.DepositoFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboqueViewModel, ReboqueSimplificadoViewModel>()
                .ForMember(dest => dest.Placa, from => from.MapFrom(src => src.ReboquePlaca))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.ReboqueFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboquistaViewModel, ReboquistaSimplificadoViewModel>()
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.ReboquistaNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.ReboquistaFlagAtivo));

            // ViewModel to Model
            CreateMap<CadastroCondutorViewModel, CondutorModel>()
                .ForMember(dest => dest.Email, from => from.MapFrom(s => s.Email.ToLowerTrim()))
                .AddTransform<string>(s => s
                    .ToNullIfEmpty()
                    .ToUpperTrim())
                .ForMember(dest => dest.Documento, from => from.MapFrom(s => s.Documento.GetNumbers()))
                .ForMember(dest => dest.Identidade, from => from.MapFrom(s => s.Identidade.GetNumbers()));

            CreateMap<UsuarioClienteDepositoReboqueViewModel, UsuarioClienteDepositoReboqueViewModel>();

            CreateMap<CadastroEnquadramentoInfracaoViewModel, EnquadramentoInfracaoGrvModel>()
                .ForMember(dest => dest.EnquadramentoInfracaoId, from => from.MapFrom(src => src.IdentificadorEnquadramentoInfracao));
        }
    }
}