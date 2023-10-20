using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.ServiceModel.Channels;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Mappings.Localizacao;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.ViewModel.Vistoria;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GrvCadastradoViewModel Create(GrvCadastroViewModel GrvCadastro)
        {
            GrvCadastradoViewModel ResultView = new();

            GrvModel Grv = new()
            {
                ClienteId = GrvCadastro.ClienteId,

                DepositoId = GrvCadastro.DepositoId,

                TipoVeiculoId = GrvCadastro.TipoVeiculoId,

                ReboquistaId = GrvCadastro.ReboquistaId,

                ReboqueId = GrvCadastro.ReboqueId,

                AutoridadeResponsavelId = GrvCadastro.AutoridadeResponsavelId,

                CorId = GrvCadastro.CorId,

                MarcaModeloId = GrvCadastro.MarcaModeloId,

                MotivoApreensaoId = GrvCadastro.MotivoApreensaoId,

                UsuarioCadastroId = GrvCadastro.UsuarioId,

                NumeroFormularioGrv = GrvCadastro.NumeroProcesso.Trim(),

                FaturamentoProdutoId = GrvCadastro.CodigoProduto,

                MatriculaAutoridadeResponsavel = GrvCadastro.MatriculaAutoridadeResponsavel.ToUpper().Trim(),

                NomeAutoridadeResponsavel = GrvCadastro.NomeAutoridadeResponsavel.ToUpper().Trim(),

                Placa = GrvCadastro.Placa.ToUpper(),

                PlacaOstentada = GrvCadastro.PlacaOstentada.ToUpper(),

                Chassi = GrvCadastro.Chassi.ToUpper().Trim(),

                Renavam = GrvCadastro.Renavam.ToUpper().Trim(),

                Rfid = GrvCadastro.Rfid.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoLogradouro = GrvCadastro.EnderecoLocalizacaoVeiculoLogradouro.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoNumero = GrvCadastro.EnderecoLocalizacaoVeiculoNumero.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoComplemento = GrvCadastro.EnderecoLocalizacaoVeiculoComplemento.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoBairro = GrvCadastro.EnderecoLocalizacaoVeiculoBairro.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoMunicipio = GrvCadastro.EnderecoLocalizacaoVeiculoMunicipio.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoUF = GrvCadastro.EnderecoLocalizacaoVeiculoUF.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoReferencia = GrvCadastro.EnderecoLocalizacaoVeiculoReferencia.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoPontoReferencia = GrvCadastro.EnderecoLocalizacaoVeiculoPontoReferencia.ToUpper().Trim(),

                NumeroChave = GrvCadastro.NumeroChave.ToUpper().Trim(),

                EstacionamentoSetor = GrvCadastro.EstacionamentoSetor.ToUpper().Trim(),

                EstacionamentoNumeroVaga = GrvCadastro.EstacionamentoNumeroVaga.ToUpper().Trim(),

                Latitude = GrvCadastro.Latitude.ToUpper().Trim(),

                Longitude = GrvCadastro.Longitude.ToUpper().Trim(),

                VeiculoUF = GrvCadastro.VeiculoUF.ToUpper().Trim(),

                DataHoraRemocao = GrvCadastro.DataHoraRemocao,

                LatitudeAcautelamento = GrvCadastro.LatitudeAcautelamento.ToUpper().Trim(),

                LongitudeAcautelamento = GrvCadastro.LongitudeAcautelamento.ToUpper().Trim(),

                FlagComboio = GrvCadastro.FlagVeiculoNaoUsouReboque,

                FlagVeiculoNaoIdentificado = GrvCadastro.FlagVeiculoNaoIdentificado,

                FlagVeiculoSemRegistro = GrvCadastro.FlagVeiculoSemRegistro,

                FlagVeiculoRoubadoFurtado = GrvCadastro.FlagVeiculoRoubadoFurtado,

                FlagEstadoLacre = GrvCadastro.FlagEstadoLacre,

                FlagVeiculoNaoOstentaPlaca = GrvCadastro.FlagVeiculoNaoOstentaPlaca,

                Condutor = _mapper.Map<CondutorModel>(GrvCadastro.Condutor),

                ListagemEnquadramentoInfracao = _mapper.Map<List<EnquadramentoInfracaoGrvModel>>(GrvCadastro.EnquadramentosInfracoes)
            };

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                EnderecoViewModel Endereco = new EnderecoService(_context, _mapper).GetByCEP(GrvCadastro.EnderecoLocalizacaoVeiculoCEP.Replace("-", ""));

                if (Endereco != null)
                {
                    Grv.EnderecoLocalizacaoVeiculoCEPId = Endereco.CEPId;

                    Grv.EnderecoLocalizacaoVeiculoLogradouro = Endereco.Logradouro;

                    Grv.EnderecoLocalizacaoVeiculoBairro = Endereco.Bairro;

                    Grv.EnderecoLocalizacaoVeiculoMunicipio = Endereco.MunicipioPtbr;

                    Grv.EnderecoLocalizacaoVeiculoUF = Endereco.UF;
                }
            }

            if (GrvCadastro.EnquadramentosInfracoes?.Count > 0)
            {
                GrvCadastro.EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                    .OrderBy(x => x.NumeroInfracao)
                    .ToList();

                GrvCadastro.EnquadramentosInfracoes.ForEach(x => x.NumeroInfracao = x.NumeroInfracao.ToUpper().Trim());
            }

            if (GrvCadastro.Lacres?.Count > 0)
            {
                GrvCadastro.Lacres = GrvCadastro.Lacres
                    .ConvertAll(x => x.ToUpper().Trim())
                    .OrderBy(x => x)
                    .ToList();

                foreach (string item in GrvCadastro.Lacres)
                {
                    Grv.ListagemLacre.Add(new LacreModel { UsuarioCadastroId = GrvCadastro.UsuarioId, Lacre = item });
                }
            }

            ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                .Where(x => x.ClienteId == GrvCadastro.ClienteId
                        && x.DepositoId == GrvCadastro.DepositoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (ClienteDeposito != null && ClienteDeposito.FlagCadastrarGrvBloqueado == "S")
            {
                Grv.StatusOperacaoId = "B";
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Grv.Add(Grv);

                    _context.SaveChanges();

                    transaction.Commit();

                    if (ClienteDeposito.Cliente != null && ClienteDeposito.Cliente.FlagClientePossuiCodigoIdentificacao == "S")
                    {
                        ClienteCodigoIdentificacaoModel ClienteCodigoIdentificacao = new()
                        {
                            GrvId = Grv.GrvId,

                            UsuarioCadastroId = GrvCadastro.UsuarioId,

                            CodigoIdentificacao = GrvCadastro.CodigoIdentificacaoCliente
                        };

                        _context.ClienteCodigoIdentificacao.Add(ClienteCodigoIdentificacao);

                        _context.SaveChanges();
                    }

                    ResultView.GrvId = Grv.GrvId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvCadastro.Fotos?.Count > 0)
            {
                SendFiles(new()
                {
                    GrvId = Grv.GrvId,

                    UsuarioId = Grv.UsuarioCadastroId,

                    Fotos = GrvCadastro.Fotos
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkCreate();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetById(int GrvId, int UsuarioId)
        {
            List<string> erros = new();

            if (GrvId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            GrvViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            ResultView.ListagemGrv = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrv(string NumeroFormularioGrv, int ClienteId, int DepositoId, int UsuarioId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(NumeroFormularioGrv) || Convert.ToInt64(NumeroFormularioGrv) <= 0)
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            GrvViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            ResultView.ListagemGrv = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvPesquisaResultViewModelList> Search(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListaCodigoProduto.Count == 0)
            {
                erros.Add("Informe ao menos um Código de Produto");
            }
            else
            {
                List<string> Produtos = await _context.FaturamentoProduto
                    .Select(s => s.FaturamentoProdutoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string Codigo in GrvPesquisa.ListaCodigoProduto)
                {
                    if (Produtos.FirstOrDefault(f => f == Codigo.ToUpper().Trim()) == null)
                    {
                        erros.Add($"Código de Produto não encontrado: {Codigo}");
                    }
                }
            }

            if (GrvPesquisa.ListaStatusOperacao.Count > 0)
            {
                List<string> StatusOperacoes = await _context.StatusOperacao
                    .Select(s => s.StatusOperacaoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string StatusOperacao in GrvPesquisa.ListaStatusOperacao)
                {
                    if (StatusOperacoes.FirstOrDefault(f => f == StatusOperacao.ToUpper().Trim()) == null)
                    {
                        erros.Add($"Status Operação não encontrado: {StatusOperacao}");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) &&
                (!NumberHelper.IsNumber(GrvPesquisa.NumeroProcesso) || Convert.ToInt64(GrvPesquisa.NumeroProcesso) <= 0))
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.Placa) && !VeiculoHelper.IsPlaca(GrvPesquisa.Placa))
            {
                erros.Add("Placa inválida");
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) && !VeiculoHelper.IsChassi(GrvPesquisa.Chassi))
            {
                erros.Add("Chassi inválido");
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) && GrvPesquisa.FlagVeiculoNaoIdentificado != "S" && GrvPesquisa.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvPesquisa.ClienteId < 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvPesquisa.DepositoId < 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvPesquisa.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (!GrvPesquisa.DataInicialRemocao.HasValue)
            {
                GrvPesquisa.DataInicialRemocao = DateTime.Now.AddDays(-30);
            }

            if (!GrvPesquisa.DataFinalRemocao.HasValue)
            {
                GrvPesquisa.DataFinalRemocao = DateTime.Now;
            }

            if (GrvPesquisa.DataInicialRemocao.Value.Date > GrvPesquisa.DataFinalRemocao.Value.Date)
            {
                erros.Add("A Data Inicial não pode ser maior do que a Data Final");
            }
            else if ((GrvPesquisa.DataFinalRemocao.Value.Date - GrvPesquisa.DataInicialRemocao.Value.Date).Days > 30)
            {
                erros.Add("O período de pesquisa não pode superar 30 dias");
            }

            GrvPesquisaResultViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            List<GrvModel> Grvs = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Include(i => i.UsuarioClienteDepositoGrv)
                .Where(w => w.UsuarioClienteDepositoGrv.UsuarioId == GrvPesquisa.UsuarioId && w.UsuarioClienteDepositoGrv.FaturamentoProdutoId == w.FaturamentoProdutoId &&
                            (w.DataHoraRemocao.Date >= GrvPesquisa.DataInicialRemocao.Value.Date && w.DataHoraRemocao.Date <= GrvPesquisa.DataFinalRemocao.Value.Date) &&
                            (GrvPesquisa.ListaCodigoProduto.Count > 0 ? GrvPesquisa.ListaCodigoProduto.Contains(w.FaturamentoProdutoId) : true) &&
                            (GrvPesquisa.ListaStatusOperacao.Count > 0 ? GrvPesquisa.ListaStatusOperacao.Contains(w.StatusOperacaoId) : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) ? w.NumeroFormularioGrv == GrvPesquisa.NumeroProcesso : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Placa) ? w.Placa == GrvPesquisa.Placa : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) ? w.Chassi == GrvPesquisa.Chassi : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) ? w.FlagVeiculoNaoIdentificado == GrvPesquisa.FlagVeiculoNaoIdentificado : true) &&
                            (GrvPesquisa.ClienteId > 0 ? w.ClienteId == GrvPesquisa.ClienteId : true) &&
                            (GrvPesquisa.DepositoId > 0 ? w.DepositoId == GrvPesquisa.DepositoId : true))
                .OrderBy(o => Convert.ToInt64(o.NumeroFormularioGrv))
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (Grvs?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("A pesquisa não retornou registro");

                return ResultView;
            }

            foreach (GrvModel Grv in Grvs)
            {
                ResultView.ListagemGrv.Add(new()
                {
                    GrvId = Grv.GrvId,

                    StatusOperacaoId = Grv.StatusOperacaoId,

                    NumeroProcesso = Grv.NumeroFormularioGrv,

                    Placa = Grv.Placa,

                    Chassi = Grv.Chassi,

                    StatusOperacao = Grv.StatusOperacao.Descricao,

                    DataHoraRemocao = Grv.DataHoraRemocao,

                    DataHoraGuarda = Grv.DataHoraGuarda.Value,

                    Cliente = Grv.Cliente.Nome,

                    Deposito = Grv.Deposito.Nome
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(Grvs.Count);

            return ResultView;
        }

        public bool GrvExists(int GrvId)
        {
            return _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public bool UserCanAccessGrv(int ClienteId, int DepositoId, int UsuarioId)
        {
            return _context.ViewUsuarioClienteDeposito
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId && w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public bool UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            return Grv != null && _context.ViewUsuarioClienteDeposito
                .Where(w => w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId && w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public MensagemViewModel SendFiles(GrvFotoViewModel Fotos)
        {
            List<string> erros = new();

            if (Fotos.GrvId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (Fotos.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (Fotos.Fotos.Count == 0)
            {
                erros.Add("Nenhuma imagem enviada para a API");
            }

            MensagemViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            GrvModel Grv = _context.Grv
                .Where(w => w.GrvId == Fotos.GrvId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Grv == null)
            {
                ResultView = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, Fotos.UsuarioId))
            {
                ResultView = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            new BucketArquivoService(_context)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.GrvId, Fotos.UsuarioId, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(GrvCadastroViewModel GrvCadastro)
        {
            #region Validações de IDs
            List<string> erros = new();

            if (GrvCadastro.ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvCadastro.DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvCadastro.TipoVeiculoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorTipoVeiculoInvalido);
            }

            if (GrvCadastro.FlagVeiculoNaoUsouReboque != "S"
                && GrvCadastro.FlagVeiculoNaoUsouReboque != "N")
            {
                erros.Add("Flag do Veículo não usou Reboque inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoNaoUsouReboque == "N")
            {
                if (GrvCadastro.ReboquistaId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
                }

                if (GrvCadastro.ReboqueId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
                }
            }
            else
            {
                if (GrvCadastro.ReboquistaId > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboquista");
                }

                if (GrvCadastro.ReboqueId > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboque");
                }
            }

            if (GrvCadastro.FlagVeiculoNaoOstentaPlaca != "S"
                && GrvCadastro.FlagVeiculoNaoOstentaPlaca != "N")
            {
                erros.Add("Flag do Veículo não ostenta Placa inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvCadastro.AutoridadeResponsavelId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAutoridadeResponsavelInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.MatriculaAutoridadeResponsavel))
            {
                erros.Add("Informe a Matrícula da Autoridade Responsável.");
            }

            if (GrvCadastro.CorId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorCorInvalido);
            }

            if (GrvCadastro.MarcaModeloId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMarcaModeloInvalido);
            }

            if (GrvCadastro.MotivoApreensaoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMotivoApreensaoInvalido);
            }

            if (GrvCadastro.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.CodigoProduto))
            {
                erros.Add("Informe o Código de Produto");
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.NumeroProcesso))
            {
                erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(GrvCadastro.NumeroProcesso) || Convert.ToInt64(GrvCadastro.NumeroProcesso) <= 0)
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (GrvCadastro.FlagVeiculoNaoIdentificado != "S"
                && GrvCadastro.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoNaoIdentificado == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Placa) || !string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Ao informar que o Veículo não foi identificado, não se deve informar a Placa nem o Chassi");
                }
            }
            else if (GrvCadastro.FlagVeiculoSemRegistro == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Placa))
                {
                    erros.Add("Ao informar que o Veículo não possui registro, não se deve informar a Placa");
                }
                else if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(GrvCadastro.Placa))
                {
                    erros.Add("Informe a Placa");
                }
                else if (!VeiculoHelper.IsPlaca(GrvCadastro.Placa))
                {
                    erros.Add("Placa inválida");
                }

                if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (GrvCadastro.FlagVeiculoSemRegistro != "S"
                && GrvCadastro.FlagVeiculoSemRegistro != "N")
            {
                erros.Add("Flag do Veículo sem registro inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoSemRegistro == "S")
            {
                if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(GrvCadastro.Chassi) && !VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                    {
                        erros.Add("Chassi inválido");
                    }
                }
            }

            if (GrvCadastro.FlagVeiculoRoubadoFurtado != "S"
                && GrvCadastro.FlagVeiculoRoubadoFurtado != "N")
            {
                erros.Add("Flag do Veículo Roubado/Furtado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                if (!LocalizacaoHelper.IsCEP(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
                {
                    erros.Add("CEP inválido");
                }
            }

            if (GrvCadastro.Condutor == null)
            {
                erros.Add("Informe os dados do Condutor");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Email)
                    && !EmailHelper.IsEmail(GrvCadastro.Condutor.Email))
                {
                    erros.Add($"E-mail do Condutor é inválido: {GrvCadastro.Condutor.Email}");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Documento))
                {
                    if (!DocumentHelper.IsCPF(GrvCadastro.Condutor.Documento))
                    {
                        erros.Add("CPF do Condutor inválido");
                    }
                }

                if (string.IsNullOrWhiteSpace(GrvCadastro.Condutor.StatusAssinaturaCondutor))
                {
                    erros.Add("Informe o Status da Assinatura do Condutor");
                }
                else
                {
                    List<TabelaGenericaModel> TabelaGenerica = await new SistemaService(_context)
                        .ListarTabelaGenerica("STATUS_ASSINATURA_CONDUTOR");

                    if (!TabelaGenerica.Select(x => x.Sigla).ToList().Contains(GrvCadastro.Condutor.StatusAssinaturaCondutor))
                    {
                        erros.Add("Status da Assinatura do Condutor inválido");
                    }
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && !ContactHelper.IsDDD(GrvCadastro.Condutor.TelefoneDDD))
                {
                    erros.Add("DDD do Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone)
                    && !ContactHelper.IsTelephoneOrCellphone(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Ao informar o DDD do Telefone do Condutor é necessário informar o Telefone do Condutor");
                }
                else if (string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && !string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Ao informar o Telefone do Condutor é necessário informar o DDD do Telefone do Condutor");
                }

                if (GrvCadastro.Condutor.FlagDocumentacaoVeiculo != "S"
                    && GrvCadastro.Condutor.FlagDocumentacaoVeiculo != "N")
                {
                    erros.Add("Flag da Documentação foi deixada no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }

                if (GrvCadastro.Condutor.FlagChaveVeiculo != "S"
                    && GrvCadastro.Condutor.FlagChaveVeiculo != "N")
                {
                    erros.Add("Flag da Chave ficou no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }
                else if (GrvCadastro.Condutor.FlagChaveVeiculo == "S"
                    && string.IsNullOrWhiteSpace(GrvCadastro.Condutor.NumeroChaveVeiculo))
                {
                    erros.Add("Ao informar que a Chave ficou no Veículo, é necessário informar o Número/Código da Chave");
                }
            }

            if (GrvCadastro.FlagEstadoLacre != "S"
                    && GrvCadastro.FlagEstadoLacre != "N")
            {
                erros.Add("Flag do Status dos Lacres inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvCadastro?.Lacres.Count == 0)
            {
                erros.Add("Informe os Lacres");
            }
            else
            {
                List<IGrouping<string, string>> Lacres = GrvCadastro.Lacres
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres duplicados");
                }

                Lacres = GrvCadastro.Lacres
                    .Where(x => string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres não informados");
                }
            }

            MensagemViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }
            #endregion Validações de IDs

            #region Validações do Usuário
            if (!new UsuarioService(_context).IsUserActive(GrvCadastro.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized();
            }
            #endregion Validações do Usuário

            #region Consultas
            ClienteModel Cliente = await _context.Cliente
                .Include(x => x.Endereco)
                .Include(x => x.ClientesDepositos)
                .Where(w => w.ClienteId == GrvCadastro.ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoCliente);
            }
            else if (Cliente.FlagClientePossuiCodigoIdentificacao == "S" && string.IsNullOrWhiteSpace(GrvCadastro.CodigoIdentificacaoCliente))
            {
                ResultView.AvisosImpeditivos.Add($"Informe o {Cliente.LabelClienteCodigoIdentificacao}");
            }
            else if (Cliente.ClientesDepositos != null && Cliente.ClientesDepositos.Count > 0)
            {
                if (Cliente.ClientesDepositos.FirstOrDefault().FlagCadastrarGrvBloqueado == "S")
                {
                    StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                        .Where(w => w.StatusOperacaoId == "B")
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    ResultView.AvisosInformativos.Add($"Esse GRV receberá o Status de Operação {StatusOperacao.Descricao} devido à configuração do Cliente");
                }
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == GrvCadastro.DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoDeposito);
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context, _mapper)
                .GetDataHoraPorDeposito(GrvCadastro.DepositoId);

            if (GrvCadastro.DataHoraRemocao.Date > DataHoraPorDeposito.Date)
            {
                ResultView.AvisosImpeditivos.Add("A Data da Remoção não pode ser maior do que a Data atual");
            }
            else if (GrvCadastro.DataHoraRemocao.Hour == 0 && GrvCadastro.DataHoraRemocao.Minute == 0)
            {
                ResultView.AvisosImpeditivos.Add("A Hora da Remoção não pode ser igual a 00:00.");
            }

            TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                .Where(w => w.TipoVeiculoId == GrvCadastro.TipoVeiculoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoVeiculo == null)
            {
                ResultView.AvisosImpeditivos.Add("Tipo do Veículo não encontrado");
            }

            ReboquistaModel Reboquista = await _context.Reboquista
                .Where(w => w.ReboquistaId == GrvCadastro.ReboquistaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboquista == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboquista não encontrado");
            }

            ReboqueModel Reboque = await _context.Reboque
                .Where(w => w.ReboqueId == GrvCadastro.ReboqueId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboque == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboque não encontrado");
            }

            AutoridadeResponsavelModel AutoridadeResponsavel = await _context.AutoridadeResponsavel
                .Include(x => x.OrgaoEmissor)
                .Where(w => w.AutoridadeResponsavelId == GrvCadastro.AutoridadeResponsavelId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (AutoridadeResponsavel == null)
            {
                ResultView.AvisosImpeditivos.Add("Autoridade Responsável não encontrada");
            }
            else if (AutoridadeResponsavel.OrgaoEmissor.UF != Cliente.Endereco.UF)
            {
                ResultView.AvisosImpeditivos.Add($"A Autoridade Responsável ({AutoridadeResponsavel.OrgaoEmissor.UF}) informada não pertence a mesma Unidade Federativa do cadastro do Cliente {Cliente.Nome} ({Cliente.Endereco.UF})");
            }

            CorModel Cor = await _context.Cor
                .Where(w => w.CorId == GrvCadastro.CorId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cor == null)
            {
                ResultView.AvisosImpeditivos.Add("Cor não encontrada");
            }

            MarcaModeloModel MarcaModelo = await _context.MarcaModelo
                .Where(w => w.MarcaModeloId == GrvCadastro.MarcaModeloId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MarcaModelo == null)
            {
                ResultView.AvisosImpeditivos.Add("Marca/Modelo não encontrado");
            }

            MotivoApreensaoModel MotivoApreensao = await _context.MotivoApreensao
                .Where(w => w.MotivoApreensaoId == GrvCadastro.MotivoApreensaoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Motivo de Apreensão não encontrado");
            }
            else if (MotivoApreensao.FlagDefault == "S")
            {
                if (GrvCadastro?.EnquadramentosInfracoes.Count == 0)
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Enquadramento da Infração");
                }
                else
                {
                    List<IGrouping<decimal, decimal>> EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => x.EnquadramentoInfracaoId <= 0)
                        .Select(x => x.EnquadramentoInfracaoId)
                        .GroupBy(x => x)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração com Identificador inválido");
                    }

                    EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => x.EnquadramentoInfracaoId > 0)
                        .Select(x => x.EnquadramentoInfracaoId)
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração duplicados");
                    }

                    List<IGrouping<string, string>> NumeroInfracao = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração não informados");
                    }

                    NumeroInfracao = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => !string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração duplicados");
                    }
                }
            }

            FaturamentoProdutoModel Produtos = await _context.FaturamentoProduto
                .Where(w => w.FaturamentoProdutoId == GrvCadastro.CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Código do Produto não encontrado");
            }

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                if (await _context.CEP
                    .Where(x => x.CEP == GrvCadastro.EnderecoLocalizacaoVeiculoCEP.Replace("-", ""))
                    .FirstOrDefaultAsync() == null)
                {
                    ResultView.AvisosImpeditivos.Add("CEP não encontrado");
                }
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv == GrvCadastro.NumeroProcesso && w.ClienteId == GrvCadastro.ClienteId && w.DepositoId == GrvCadastro.DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv != null)
            {
                ResultView.AvisosImpeditivos.Add("GRV já cadastrado");
            }
            #endregion Consultas

            if (ResultView.AvisosImpeditivos.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
            }
            else
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.Ok;
            }

            return ResultView;
        }

        public async Task<StatusAssinaturaCondutorViewModelList> ListarStatusAssinaturaCondutor()
        {
            StatusAssinaturaCondutorViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new SistemaService(_context)
                .ListarTabelaGenerica("STATUS_ASSINATURA_CONDUTOR");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.ListagemStatusAssinaturaCondutor.Add(new()
                {
                    StatusAssinaturaCondutorId = item.Sigla,
                    Descricao = item.Valor1
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<MensagemViewModel> VerificarAlteracaoStatusGRV(int GrvId, string StatusOperacaoId, int UsuarioId)
        {
            MensagemViewModel Mensagem = new();

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);
            }
            else
            {
                StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                    .Where(x => x.StatusOperacaoId == StatusOperacaoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (StatusOperacao == null)
                {
                    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoStatusOperacao);
                }
                else if (Grv.StatusOperacao.StatusOperacaoId != StatusOperacaoId)
                {
                    return MensagemViewHelper.GetBadRequest($"O Status da Operação foi alterado de \"{Grv.StatusOperacao.Descricao.ToUpper()}\" para \"{StatusOperacao.Descricao.ToUpper()}\"");
                }
            }

            return MensagemViewHelper.GetOk("O Status da Operação não foi alterado");
        }
    }
}