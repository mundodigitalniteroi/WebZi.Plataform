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
                .ForMember(dest => dest.IdentificadorAtendimento, from => from.MapFrom(property => property.AtendimentoId))
                .ForMember(dest => dest.IdentificadorGrv, from => from.MapFrom(property => property.GrvId))
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(property => property.QualificacaoResponsavelId));

            CreateMap<AgenciaBancariaModel, AgenciaBancariaViewModel>()
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(property => property.AgenciaBancariaId));

            CreateMap<AutoridadeResponsavelModel, AutoridadeResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(property => property.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(property => property.OrgaoEmissorId));

            CreateMap<BancoModel, BancoViewModel>()
                .ForMember(dest => dest.IdentificadorBanco, from => from.MapFrom(property => property.BancoId));

            CreateMap<ClienteModel, ClienteViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(property => property.AgenciaBancariaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(property => property.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(property => property.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(property => property.BairroId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(property => property.TipoMeioCobrancaId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(property => property.EmpresaId))
                .ForMember(dest => dest.IdentificadorOrgaoExecutivoTransito, from => from.MapFrom(property => property.OrgaoExecutivoTransitoId))
                .ForMember(dest => dest.IdentificadorTipoChavePIX, from => from.MapFrom(property => property.PixTipoChaveId));

            CreateMap<ClienteViewModel, ClienteSimplificadoViewModel>();

            CreateMap<CorModel, CorViewModel>()
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(property => property.CorId));

            CreateMap<DepositoModel, DepositoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(property => property.EmpresaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(property => property.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(property => property.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(property => property.BairroId))
                .ForMember(dest => dest.IdentificadorSistemaExterno, from => from.MapFrom(property => property.SistemaExternoId));

            CreateMap<DetranRioVeiculoModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.IdentificadorVeiculo, from => from.MapFrom(property => property.DetranVeiculoId))
                .ForMember(dest => dest.Classificacao, from => from.MapFrom(property => property.Classificacao.ToNullIfEmpty()))
                .ForMember(dest => dest.CodigoCategoria, from => from.MapFrom(property => property.CodigoCategoria.ToNullIfEmpty()))
                .ForMember(dest => dest.DescricaoCategoria, from => from.MapFrom(property => property.DescricaoCategoria.ToNullIfEmpty()))
                .ForMember(dest => dest.InformacaoRoubo, from => from.MapFrom(property => property.InformacaoRoubo.ToNullIfEmpty()))
                .ForMember(dest => dest.RestricaoEstelionato, from => from.MapFrom(property => property.RestricaoEstelionato.ToNullIfEmpty()))
                .ForMember(dest => dest.Placa, from => from.MapFrom(property => property.Placa.ToNullIfEmpty()))
                .ForMember(dest => dest.Chassi, from => from.MapFrom(property => property.Chassi.ToNullIfEmpty()))
                .ForMember(dest => dest.Uf, from => from.MapFrom(property => property.Uf.ToNullIfEmpty()));

            CreateMap<CorModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.Cor, from => from.MapFrom(x => x));

            CreateMap<MarcaModeloModel, DetranRioVeiculoViewModel>()
                .ForMember(dest => dest.MarcaModelo, from => from.MapFrom(x => x));

            CreateMap<EmpresaModel, EmpresaViewModel>()
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(property => property.EmpresaId))
                .ForMember(dest => dest.IdentificadorEmpresaMatriz, from => from.MapFrom(property => property.EmpresaMatrizId))
                .ForMember(dest => dest.IdentificadorEmpresaClassificacao, from => from.MapFrom(property => property.EmpresaClassificacaoId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(property => property.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(property => property.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorCNAE, from => from.MapFrom(property => property.CnaeId))
                .ForMember(dest => dest.IdentificadorCNAEListaServico, from => from.MapFrom(property => property.CnaeListaServicoId));

            CreateMap<EnquadramentoInfracaoModel, EnquadramentoInfracaoViewModel>()
                .ForMember(dest => dest.IdentificadorEnquadramentoInfracao, from => from.MapFrom(property => property.EnquadramentoInfracaoId))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(property => property.Status));

            CreateMap<FaturamentoProdutoModel, FaturamentoProdutoViewModel>()
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(property => property.FaturamentoProdutoId));

            CreateMap<GrvModel, GrvViewModel>()
                .ForMember(dest => dest.IdentificadorGrv, from => from.MapFrom(property => property.GrvId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId))
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(property => property.TipoVeiculoId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(property => property.ReboquistaId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(property => property.ReboqueId))
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(property => property.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(property => property.CorId))
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(property => property.MarcaModeloId))
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(property => property.MotivoApreensaoId))
                .ForMember(dest => dest.IdentificadorStatusOperacao, from => from.MapFrom(property => property.StatusOperacaoId))
                .ForMember(dest => dest.IdentificadorLiberacao, from => from.MapFrom(property => property.LiberacaoId))
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(property => property.FaturamentoProdutoId));

            CreateMap<LacreModel, LacreViewModel>()
                .ForMember(dest => dest.IdentificadorLacre, from => from.MapFrom(property => property.LacreId));

            CreateMap<MarcaModeloModel, MarcaModeloViewModel>()
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(property => property.MarcaModeloId));

            CreateMap<MotivoApreensaoModel, MotivoApreensaoViewModel>()
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(property => property.MotivoApreensaoId));

            CreateMap<OrgaoEmissorModel, OrgaoEmissorViewModel>()
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(property => property.OrgaoEmissorId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(property => property.Descricao));

            CreateMap<QualificacaoResponsavelModel, QualificacaoResponsavelViewModel>()
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(property => property.QualificacaoResponsavelId));

            CreateMap<ReboqueModel, ReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(property => property.ReboqueId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId));

            CreateMap<ReboquistaModel, ReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(property => property.ReboquistaId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId));

            CreateMap<TabelaGenericaModel, TabelaGenericaViewModel>()
                .ForMember(dest => dest.Identificador, from => from.MapFrom(property => property.TabelaGenericaId));

            CreateMap<TipoAvariaModel, TipoAvariaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoAvaria, from => from.MapFrom(property => property.TipoAvariaId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(property => property.TipoDocumentoIdentificacaoId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(property => property.TipoDocumentoIdentificacaoId));

            CreateMap<TipoMeioCobrancaModel, TipoMeioCobrancaViewModel>()
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(property => property.TipoMeioCobrancaId));

            CreateMap<TipoVeiculoModel, TipoVeiculoViewModel>()
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(property => property.TipoVeiculoId));

            CreateMap<UsuarioModel, UsuarioViewModel>()
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(property => property.UsuarioId));

            CreateMap<VistoriaSituacaoChassiModel, VistoriaSituacaoChassiViewModel>()
                .ForMember(dest => dest.IdentificadorSituacaoChassi, from => from.MapFrom(property => property.VistoriaSituacaoChassiId));

            CreateMap<ViewEnderecoCompletoModel, EnderecoViewModel>()
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(property => property.CEPId))
                .ForMember(dest => dest.IdentificadorMunicipio, from => from.MapFrom(property => property.MunicipioId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(property => property.BairroId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(property => property.TipoLogradouroId));

            CreateMap<ViewUsuarioClienteDepositoReboqueModel, UsuarioClienteDepositoReboqueViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(property => property.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(property => property.ReboqueId));

            CreateMap<ViewUsuarioClienteDepositoReboquistaModel, UsuarioClienteDepositoReboquistaViewModel>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(property => property.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(property => property.ReboquistaId));

            CreateMap<ViewUsuarioClienteDepositoModel, ClienteDepositoSimplificadoViewModel>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(property => property.DepositoId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(property => property.ClienteId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(property => property.DepositoNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(property => property.DepositoFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboqueViewModel, ReboqueSimplificadoViewModel>()
                .ForMember(dest => dest.Placa, from => from.MapFrom(property => property.ReboquePlaca))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(property => property.ReboqueFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboquistaViewModel, ReboquistaSimplificadoViewModel>()
                .ForMember(dest => dest.Nome, from => from.MapFrom(property => property.ReboquistaNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(property => property.ReboquistaFlagAtivo));

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
                .ForMember(dest => dest.EnquadramentoInfracaoId, from => from.MapFrom(property => property.IdentificadorEnquadramentoInfracao));
        }
    }
}