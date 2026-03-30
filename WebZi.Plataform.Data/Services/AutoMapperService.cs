using AutoMapper;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Deposito;
using WebZi.Plataform.Domain.DTO.Documento;
using WebZi.Plataform.Domain.DTO.DRFA;
using WebZi.Plataform.Domain.DTO.Empresa;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.Faturamento.Cadastro;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Liberacao;
using WebZi.Plataform.Domain.DTO.Localizacao;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.Vistoria;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.DRFA;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Nfe;
using WebZi.Plataform.Domain.Models.Pessoa.Contato;
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

            CreateMap<AtendimentoSaidaParaReparoModel, AtendimentoSaidaParaReparoDTO>()
                .ForMember(dest => dest.IdentificadorAtendimento, from => from.MapFrom(src => src.AtendimentoId))
                .ForMember(dest => dest.IdentificadorSaidaReparo, from => from.MapFrom(src => src.Id));
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
                .ForMember(dest => dest.IdentificadorSistemaExterno, from => from.MapFrom(src => src.SistemaExternoId))
                .ForMember(dest => dest.Cep, from => from.MapFrom(src => src.Endereco != null ? src.Endereco.CEP : null))
                .ForMember(dest => dest.UF, from => from.MapFrom(src => src.Endereco != null ? src.Endereco.UF : null))
                .ForMember(dest => dest.Municipio, from => from.MapFrom(src => src.Endereco != null ? src.Endereco.Municipio : null));

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

            CreateMap<NfeModel, NfeDTO>();

            CreateMap<NfeModel, NFERetornoFaturamentoDTO>()
                .ForMember(dest => dest.Url, from => from.MapFrom(src => src.Url))
                .ForMember(dest => dest.NumeroNotaFiscal, from => from.MapFrom(src => src.NumeroNotaFiscal));
            

            CreateMap<NfeDTO, WSNfseGerarNotaFiscalDTO>()
                .ForMember(dest => dest.CnpjPrestador, from => from.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.Ref, from => from.MapFrom(src => src.Referencia))
                .ForMember(dest => dest.NumeroRps, from => from.MapFrom(src => src.NumeroRps))
                .ForMember(dest => dest.SerieRps, from => from.MapFrom(src => src.SerieRps))
                .ForMember(dest => dest.Status, from => from.MapFrom(src => src.Status));
            CreateMap<EnquadramentoInfracaoModel, EnquadramentoInfracaoDTO>()
                .ForMember(dest => dest.IdentificadorEnquadramentoInfracao, from => from.MapFrom(src => src.EnquadramentoInfracaoId))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.Status));

            CreateMap<FaturamentoModel, SimulacaoFaturamentoDTO>();

            CreateMap<FaturamentoModel, FaturamentoCadastroDTO>()
                .ForMember(dest => dest.IdentificadorFaturamento, from => from.MapFrom(src => src.FaturamentoId))
                .ForMember(dest => dest.IdentificadorTipoMeioCobranca, from => from.MapFrom(src => src.TipoMeioCobrancaId))
                .ForMember(dest => dest.ListagemServico, from => from.MapFrom(src => src.ListagemFaturamentoComposicao));

            CreateMap<FaturamentoComposicaoModel, SimulacaoFaturamentoComposicaoDTO>()
                .ForMember(dest => dest.IdentificadorFaturamentoServicoTipoVeiculo, from => from.MapFrom(src => src.FaturamentoServicoTipoVeiculoId))
                .ForMember(dest => dest.TipoServico, from => from.MapFrom(src => src.TipoComposicao))
                .ForMember(dest => dest.QuantidadeServico, from => from.MapFrom(src => src.QuantidadeComposicao))
                .ForMember(dest => dest.ValorTipoServico, from => from.MapFrom(src => src.ValorTipoComposicao))
                .ForMember(dest => dest.TipoDesconto, from => from.MapFrom(src => src.TipoDesconto))
                .ForMember(dest => dest.ValorDesconto, from => from.MapFrom(src => src.ValorDesconto));

            CreateMap<DRFAModel, DRFADTO>()
                .ForMember(dest => dest.IdentificadorDRFA, opt => opt.MapFrom(src => src.GrvDrfaId))
                .ForMember(dest => dest.IdentificadorProcesso, opt => opt.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorTipoRegistro, opt => opt.MapFrom(src => src.TipoRegistroId))
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, opt => opt.MapFrom(src => src.OrgaoEmissorId))
                .ForMember(dest => dest.IdentificadorAutoridadeDivisao, opt => opt.MapFrom(src => src.AutoridadeDivisaoId))
                .ForMember(dest => dest.IdentificadorUsuarioCadastrado, opt => opt.MapFrom(src => src.UsuarioCadastroId))
                .ForMember(dest => dest.IdentificadorUsuarioAlteracao, opt => opt.MapFrom(src => src.UsuarioAlteracaoId ?? 0))
                .ForMember(dest => dest.AutoridadeDivisaoComplemento, opt => opt.MapFrom(src => src.AutoridadeDivisaoComplemento))
                .ForMember(dest => dest.NumeroRegistroRouboFurto, opt => opt.MapFrom(src => src.NumeroRegistroRouboFurto))
                .ForMember(dest => dest.MatriculaAgente, opt => opt.MapFrom(src => src.RegistroRouboFurtoMatriculaAgente))
                .ForMember(dest => dest.NomeAgente, opt => opt.MapFrom(src => src.RegistroRouboFurtoNomeAgente))
                .ForMember(dest => dest.LocalRemocaoEnderecoCompleto, opt => opt.MapFrom(src => src.LocalRemocaoEnderecoCompleto))
                .ForMember(dest => dest.LocalRemocaoReferencia, opt => opt.MapFrom(src => src.LocalRemocaoReferencia))
                .ForMember(dest => dest.LocalRemocaoLatitude, opt => opt.MapFrom(src => src.LocalRemocaoLatitude))
                .ForMember(dest => dest.LocalRemocaoLongitude, opt => opt.MapFrom(src => src.LocalRemocaoLongitude))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.DataAlteracao.HasValue ? src.DataAlteracao.Value.ToString("yyyy-MM-dd HH:mm:ss") : null))
                .ForMember(dest => dest.FlagRegistroRecuperacao, opt => opt.MapFrom(src => src.FlagRegistroRecuperacao))
                .ForMember(dest => dest.FlagRegistroAgendamento, opt => opt.MapFrom(src => src.FlagRegistroAgendado))
                // Objetos filhos serão preenchidos manualmente na service
                .ForMember(dest => dest.RegistroRecuperacao, opt => opt.Ignore())
                .ForMember(dest => dest.AgendamentoRetirada, opt => opt.Ignore());
            CreateMap<FaturamentoComposicaoModel, FaturamentoCadastroComposicaoDTO>()
                .ForMember(dest => dest.IdentificadorServico, from => from.MapFrom(src => src.FaturamentoComposicaoId))
                .ForMember(dest => dest.IdentificadorFaturamentoServicoTipoVeiculo, from => from.MapFrom(src => src.FaturamentoServicoTipoVeiculoId))
                .ForMember(dest => dest.TipoServico, from => from.MapFrom(src => src.TipoComposicao))
                .ForMember(dest => dest.QuantidadeServico, from => from.MapFrom(src => src.QuantidadeComposicao))
                .ForMember(dest => dest.ValorTipoServico, from => from.MapFrom(src => src.ValorTipoComposicao));

            CreateMap<FaturamentoProdutoModel, FaturamentoProdutoDTO>()
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<FaturamentoProdutoModel, SimulacaoProdutoDTO>()
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId));

            CreateMap<LiberacaoEspecialModel, LiberacaoEspecialDTO>();
            CreateMap<EquipamentoOpcionalModel, EquipamentoOpcionalDTO>()
                .ForMember(dest => dest.IdentificadorEquipamentoOpcional,
                    opt => opt.MapFrom(src => src.EquipamentoOpcionalId))
                .ForMember(dest => dest.OrdemVistoria,
                    opt => opt.MapFrom(src => src.OrdemVistoria))
                .ForMember(dest => dest.Descricao,
                    opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.ItemObrigatorio,
                    opt => opt.MapFrom(src => src.ItemObrigatorio))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status));
            CreateMap<CondutorEquipamentoOpcionalModel, CondutorEquipamentoOpcionalDTO>()
                .ForMember(dest => dest.GrvId,
                    opt => opt.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.EquipamentoOpcionalId,
                    opt => opt.MapFrom(src => src.EquipamentoOpcionalId))
                .ForMember(dest => dest.UsuarioCadastroId,
                    opt => opt.MapFrom(src => src.UsuarioCadastroId))
                .ForMember(dest => dest.UsuarioAlteracaoId,
                    opt => opt.MapFrom(src => src.UsuarioAlteracaoId))
                .ForMember(dest => dest.CodigoAvaria,
                    opt => opt.MapFrom(src => src.CodigoAvaria))
                .ForMember(dest => dest.FlagEquipamentoAvariado,
                    opt => opt.MapFrom(src => src.FlagEquipamentoAvariado))
                .ForMember(dest => dest.DataCadastro,
                    opt => opt.MapFrom(src => src.DataCadastro))
                .ForMember(dest => dest.DataAtualizacao,
                    opt => opt.MapFrom(src => src.DataAtualizacao))
                .ForMember(dest => dest.FlagPossuiEquipamento,
                    opt => opt.MapFrom(src => src.FlagPossuiEquipamento))
                .ForMember(dest => dest.EquipamentoOpcional,
                    opt => opt.MapFrom(src => src.EquipamentoOpcional))
                .ForMember(dest => dest.ListagemCondutorEquipamentoOpcionalNaoConformidade,
                    opt => opt.MapFrom(src => src.ListagemCondutorEquipamentoOpcionalNaoConformidade));
            CreateMap<GrvModel, GrvDTO>()
                .ForMember(dest => dest.IdentificadorProcesso, from => from.MapFrom(src => src.GrvId))
                .ForMember(dest => dest.IdentificadorCliente, from => from.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.IdentificadorDeposito, from => from.MapFrom(src => src.DepositoId))
                .ForMember(dest => dest.IdentificadorTipoVeiculo, from => from.MapFrom(src => src.TipoVeiculoId))
                .ForMember(dest => dest.IdentificadorReboquista, from => from.MapFrom(src => src.ReboquistaId))
                .ForMember(dest => dest.IdentificadorReboque, from => from.MapFrom(src => src.ReboqueId))
                .ForMember(dest => dest.NumeroFormularioProcesso, from => from.MapFrom(src => src.NumeroFormularioGrv))
                .ForMember(dest => dest.IdentificadorAutoridadeResponsavel, from => from.MapFrom(src => src.AutoridadeResponsavelId))
                .ForMember(dest => dest.IdentificadorEnderecoLocalizacaoVeiculoCEP, from => from.MapFrom(src => src.EnderecoLocalizacaoVeiculoCEPId))
                .ForMember(dest => dest.IdentificadorCor, from => from.MapFrom(src => src.CorId))
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(src => src.MarcaModeloId))
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(src => src.MotivoApreensaoId))
                .ForMember(dest => dest.IdentificadorStatusOperacao, from => from.MapFrom(src => src.StatusOperacaoId))
                .ForMember(dest => dest.IdentificadorLiberacao, from => from.MapFrom(src => src.LiberacaoId))
                .ForMember(dest => dest.CodigoProduto, from => from.MapFrom(src => src.FaturamentoProdutoId))
                .ForMember(dest => dest.Infracoes, opt => opt.MapFrom(src => src.ListagemEnquadramentoInfracao.Select(li => li.EnquadramentoInfracao)))
                .ForMember(dest => dest.Condutor, opt => opt.MapFrom(src => src.Condutor))
                .ForMember(dest => dest.EquipamentoOpcional, opt => opt.MapFrom(src => src.ListagemCondutorEquipamentoOpcional))
                .ForMember(dest => dest.ListagemLacres, opt => opt.MapFrom(src => src.ListagemLacre));

            CreateMap<LacreModel, LacreDTO>()
                .ForMember(dest => dest.IdentificadorLacre, from => from.MapFrom(src => src.LacreId));

            CreateMap<MarcaModeloModel, MarcaModeloDTO>()
                .ForMember(dest => dest.IdentificadorMarcaModelo, from => from.MapFrom(src => src.MarcaModeloId));

            CreateMap<MotivoApreensaoModel, MotivoApreensaoDTO>()
                .ForMember(dest => dest.IdentificadorMotivoApreensao, from => from.MapFrom(src => src.MotivoApreensaoId));

            CreateMap<OrgaoEmissorModel, OrgaoEmissorDTO>()
                .ForMember(dest => dest.IdentificadorOrgaoEmissor, from => from.MapFrom(src => src.OrgaoEmissorId))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.Descricao));

            CreateMap<PixDinamicoModel, PixDinamicoDTO>()
                .ForMember(dest => dest.IdentificadorPixDinamico, from => from.MapFrom(src => src.PixDinamicoId))
                .ForMember(dest => dest.IdentificadorPixDinamicoTipoStatusGeracao, from => from.MapFrom(src => src.PixDinamicoTipoStatusGeracaoId));

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
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Login, from => from.MapFrom(src => src.Login));

            CreateMap<SistemaPerfilAcessoUsuariosModel, PerfisAcessoUsuarioDTO>()
                .ForMember(d => d.PerfilAcessoId, o => o.MapFrom(s => s.PerfilAcessoId))
                .ForMember(d => d.Descricao, o => o.MapFrom(s => s.PerfilAcesso.Descricao));

            CreateMap<TiposContatoPessoaModel, TiposContatosPessoaDTO>()
                .ForMember(d => d.TipoContaotId, o => o.MapFrom(s => s.TipoContatoId))
                .ForMember(d => d.TipoContato, o => o.MapFrom(s => s.TiposContatos.Descricao))
                .ForMember(d => d.Contato, o => o.MapFrom(s => s.Descricao))
                .ForMember(d => d.FlagContatoPrincipal, o => o.MapFrom(s => s.FlagContatoPrincipal));

            CreateMap<UsuarioModel, UsuarioPorNomeOuLoginDTO>()
                .ForMember(dest => dest.IdentificadorUsuario, from => from.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Login, from => from.MapFrom(src => src.Login))
                .ForMember(dest => dest.Nome, from => from.MapFrom(src => src.Pessoa.Nome))
                .ForMember(dest => dest.DataUltimoAcesso, from => from.MapFrom(src => src.DataUltimoAcesso))
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.FlagAtivo));

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
                .ForMember(dest => dest.FlagAtivo, from => from.MapFrom(src => src.DepositoFlagAtivo))
                ;
            CreateMap<TipoRegistroModel, TipoRegistroDTO>()
            .ForMember(dest => dest.IdentificadorTipoRegistro,
                   opt => opt.MapFrom(src => src.IdentificadorTipoRegistro));

            CreateMap<AutoridadeDivisaoModel, AutoridadesDivisoesDTO>();

            CreateMap<CondutorModel, CondutorDTO>()
                .ForMember(dest => dest.Grv, opt => opt.Ignore());
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