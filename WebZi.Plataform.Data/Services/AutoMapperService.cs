using AutoMapper;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Deposito;
using WebZi.Plataform.Domain.DTO.Documento;
using WebZi.Plataform.Domain.DTO.Empresa;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Localizacao;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.Vistoria;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
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
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.Views.Faturamento;
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

            // Exemplos:
            // List<DestinyModel> list = _mapper.Map<List<DestinyModel>>(SourceResult.OrderBy(x => x.Property).ToList());
            // DestinyModel model = _mapper.Map<DestinyModel>(SourceResult);

            // Model to ViewModel
            CreateMap<AtendimentoModel, AtendimentoDTO>()
                .ForMember(dest => dest.IdentificadorAtendimento, from => from.MapFrom(src => src.AtendimentoId))
                .ForMember(dest => dest.IdentificadorProcesso, from => from.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<AgenciaBancariaModel, AgenciaBancariaDTO>()
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(src => src.AgenciaBancariaId));

            CreateMap<AutoridadeResponsavelModel, AutoridadeResponsavelDTO>()
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(src => src.OrgaoEmissorId));

            CreateMap<BancoModel, BancoDTO>()
                .ForMember(dest => dest.IdentificadorBanco, from => from.MapFrom(src => src.BancoId));

            CreateMap<ClienteModel, ClienteDTO>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorAgenciaBancaria, from => from.MapFrom(src => src.AgenciaBancariaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorOrgaoExecutivoTransito, from => from.MapFrom(src => src.OrgaoExecutivoTransitoId))
                .ForMember(dest => dest.IdentificadorTipoChavePIX, from => from.MapFrom(src => src.PixTipoChaveId));

            CreateMap<ClienteDTO, ClienteSimplificadoDTO>();

            CreateMap<CorModel, CorDTO>()
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(src => src.CorId));

            CreateMap<DepositoModel, DepositoDTO>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorSistemaExterno, from => from.MapFrom(src => src.SistemaExternoId));

            CreateMap<DetranRioVeiculoModel, DetranRioVeiculoDTO>()
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

            CreateMap<CorModel, DetranRioVeiculoDTO>()
                .ForMember(dest => dest.Cor, from => from.MapFrom(x => x));

            CreateMap<MarcaModeloModel, DetranRioVeiculoDTO>()
                .ForMember(dest => dest.MarcaModelo, from => from.MapFrom(x => x));

            CreateMap<EmpresaModel, EmpresaDTO>()
                .ForMember(dest => dest.IdentificadorEmpresa, from => from.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.IdentificadorEmpresaMatriz, from => from.MapFrom(src => src.EmpresaMatrizId))
                .ForMember(dest => dest.IdentificadorEmpresaClassificacao, from => from.MapFrom(src => src.EmpresaClassificacaoId))
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.IdentificadorCNAE, from => from.MapFrom(src => src.CnaeId))
                .ForMember(dest => dest.IdentificadorCNAEListaServico, from => from.MapFrom(src => src.CnaeListaServicoId));

            CreateMap<EnquadramentoInfracaoModel, EnquadramentoInfracaoDTO>()
                .ForMember(dest => dest.IdentificadorEnquadramentoInfracao, from => from.MapFrom(src => src.EnquadramentoInfracaoId))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.Status));

            CreateMap<FaturamentoModel, FaturamentoDTO>()
                .ForMember(dest => dest.IdentificadorFaturamento, from => from.MapFrom(src => src.FaturamentoId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId));

            CreateMap<FaturamentoComposicaoModel, FaturamentoComposicaoDTO>()
                .ForMember(dest => dest.IdentificadorFaturamentoServico, from => from.MapFrom(src => src.FaturamentoComposicaoId))
                .ForMember(dest => dest.IdentificadorFaturamentoServicoTipoVeiculo, from => from.MapFrom(src => src.FaturamentoServicoTipoVeiculoId))
                .ForMember(dest => dest.IdentificadorUsuarioDesconto, from => from.MapFrom(src => src.UsuarioDescontoId))
                .ForMember(dest => dest.IdentificadorUsuarioAlteracaoQuantidade, from => from.MapFrom(src => src.UsuarioAlteracaoQuantidadeId))
                .ForMember(dest => dest.TipoServico, from => from.MapFrom(src => src.TipoComposicao))
                .ForMember(dest => dest.QuantidadeServico, from => from.MapFrom(src => src.QuantidadeComposicao))
                .ForMember(dest => dest.ValorTipoServico, from => from.MapFrom(src => src.ValorTipoComposicao))
                .ForMember(dest => dest.ValorServico, from => from.MapFrom(src => src.ValorComposicao));

            CreateMap<FaturamentoProdutoModel, FaturamentoProdutoDTO>()
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<GrvModel, GrvDTO>()
                .ForMember(dest => dest.IdentificadorProcesso, from => from.MapFrom(src => src.GrvId))
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

            CreateMap<LacreModel, LacreDTO>()
                .ForMember(dest => dest.IdentificadorLacre, from => from.MapFrom(src => src.LacreId));

            CreateMap<MarcaModeloModel, MarcaModeloDTO>()
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(src => src.MarcaModeloId));

            CreateMap<MotivoApreensaoModel, MotivoApreensaoDTO>()
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(src => src.MotivoApreensaoId));

            CreateMap<OrgaoEmissorModel, OrgaoEmissorDTO>()
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(src => src.OrgaoEmissorId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.Descricao));

            CreateMap<QualificacaoResponsavelModel, QualificacaoResponsavelDTO>()
                .ForMember(dest => dest.IdentificadorQualificacaoResponsavel, from => from.MapFrom(src => src.QualificacaoResponsavelId));

            CreateMap<ReboqueModel, ReboqueDTO>()
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId));

            CreateMap<ReboquistaModel, ReboquistaDTO>()
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId));

            CreateMap<TabelaGenericaModel, TabelaGenericaDTO>()
                .ForMember(dest => dest.Identificador, from => from.MapFrom(src => src.TabelaGenericaId));

            CreateMap<TipoAvariaModel, TipoAvariaDTO>()
                .ForMember(dest => dest.IdentificadorTipoAvaria, from => from.MapFrom(src => src.TipoAvariaId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoDTO>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoDocumentoIdentificacaoModel, TipoDocumentoIdentificacaoSimplificadoDTO>()
                .ForMember(dest => dest.IdentificadorTipoDocumentoIdentificacao, from => from.MapFrom(src => src.TipoDocumentoIdentificacaoId));

            CreateMap<TipoMeioCobrancaModel, TipoMeioCobrancaDTO>()
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId));

            CreateMap<TipoVeiculoModel, TipoVeiculoDTO>()
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(src => src.TipoVeiculoId));

            CreateMap<UsuarioClienteDepositoReboqueDTO, ReboqueSimplificadoDTO>()
                .ForMember(dest => dest.Placa, from => from.MapFrom(src => src.ReboquePlaca))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.ReboqueFlagAtivo));

            CreateMap<UsuarioClienteDepositoReboquistaDTO, ReboquistaSimplificadoDTO>()
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.ReboquistaNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.ReboquistaFlagAtivo));

            CreateMap<UsuarioModel, UsuarioDTO>()
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId));

            CreateMap<VistoriaSituacaoChassiModel, VistoriaSituacaoChassiDTO>()
                .ForMember(dest => dest.IdentificadorSituacaoChassi, from => from.MapFrom(src => src.VistoriaSituacaoChassiId));

            CreateMap<ViewEnderecoCompletoModel, EnderecoDTO>()
                .ForMember(dest => dest.IdentificadorCEP, from => from.MapFrom(src => src.CEPId))
                .ForMember(dest => dest.IdentificadorMunicipio, from => from.MapFrom(src => src.MunicipioId))
                .ForMember(dest => dest.IdentificadorBairro, from => from.MapFrom(src => src.BairroId))
                .ForMember(dest => dest.IdentificadorTipoLogradouro, from => from.MapFrom(src => src.TipoLogradouroId));

            CreateMap<ViewFaturamentoServicoAssociadoVeiculoModel, ViewFaturamentoServicoGrvModel>();

            CreateMap<ViewUsuarioClienteDepositoReboqueModel, UsuarioClienteDepositoReboqueDTO>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId));

            CreateMap<ViewUsuarioClienteDepositoReboquistaModel, UsuarioClienteDepositoReboquistaDTO>()
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId));

            CreateMap<ViewUsuarioClienteDepositoModel, ClienteDepositoSimplificadoDTO>()
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.DepositoNome))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.DepositoFlagAtivo));

            // ViewModel to Model
            CreateMap<CondutorParameters, CondutorModel>()
                .ForMember(dest => dest.Email, from => from.MapFrom(s => s.Email.ToLowerTrim()))
                .AddTransform<string>(s => s
                    .ToNullIfEmpty()
                    .ToUpperTrim())
                .ForMember(dest => dest.Documento, from => from.MapFrom(s => s.Documento.GetNumbers()))
                .ForMember(dest => dest.Identidade, from => from.MapFrom(s => s.Identidade.GetNumbers()));

            CreateMap<UsuarioClienteDepositoReboqueDTO, UsuarioClienteDepositoReboqueDTO>();

            CreateMap<EnquadramentoInfracaoParameters, EnquadramentoInfracaoGrvModel>()
                .ForMember(dest => dest.EnquadramentoInfracaoId, from => from.MapFrom(src => src.IdentificadorEnquadramentoInfracao));
        }
    }
}