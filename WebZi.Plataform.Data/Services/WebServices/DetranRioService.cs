using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Principal;
using System.ServiceModel;
using WebZi.Plataform.CrossCutting.Linq;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.WsDetranRio;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.Models.WebServices.Rio;
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class DetranRioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DetranRioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetranRioVeiculoViewModel> GetByIdAsync(int DetranVeiculoId)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (DetranVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Veículo inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, string.Empty);
        }

        public async Task<DetranRioVeiculoViewModel> GetByPlacaAsync(string Placa)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Placa.IsPlaca())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Placa inválida");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Placa.NormalizePlaca());
        }

        public async Task<DetranRioVeiculoViewModel> GetByChassiAsync(string Chassi)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Chassi.IsChassi())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Chassi inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Chassi.NormalizeChassi());
        }

        private async Task<DetranRioVeiculoViewModel> GetByIdPlacaOrChassyAsync(int DetranVeiculoId, string PlacaChassi)
        {
            DetranRioVeiculoViewModel ResultView = new();

            string Placa = string.Empty;

            string Chassi = string.Empty;

            if (DetranVeiculoId <= 0)
            {
                if (PlacaChassi.IsPlaca())
                {
                    Placa = PlacaChassi;
                }
                else
                {
                    Chassi = PlacaChassi;
                }
            }

            DetranRioVeiculoModel DetranRioVeiculoBD;

            if (DetranVeiculoId <= 0)
            {
                await DeleteDuplicatedAsync(Placa, Chassi);
            }

            DetranRioVeiculoBD = await _context.DetranRioVeiculo
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                .AsNoTracking()
                .OrderByDescending(x => x.DetranVeiculoId)
                .FirstOrDefaultAsync(x => DetranVeiculoId > 0 ? x.DetranVeiculoId == DetranVeiculoId : !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi);

            if (DetranVeiculoId > 0)
            {
                if (DetranRioVeiculoBD == null)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                    return ResultView;
                }

                if (!DetranRioVeiculoBD.Placa.IsNullOrWhiteSpace())
                {
                    Placa = DetranRioVeiculoBD.Placa;
                }
                else
                {
                    Chassi = DetranRioVeiculoBD.Chassi;
                }
            }

            DetranRioVeiculoModel DetranRioVeiculoWS = new();

            try
            {
                if (DetranRioVeiculoBD == null || DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date)
                {
                    DetranRioVeiculoWS = await GetFromDetranAsync(Placa + Chassi, "ROOT");

                    if (DetranRioVeiculoWS == null)
                    {
                        ResultView.Mensagem = MensagemViewHelper.SetNotFound("Veículo não encontrado no Departamento Estadual de Trânsito");

                        return ResultView;
                    }
                }
                else
                {
                    DetranRioVeiculoWS = null;
                }

                
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex);

                return ResultView;
            }
            
            DetranRioVeiculoBD = await _context.DetranRioVeiculo
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                .AsNoTracking()
                .OrderByDescending(x => x.DetranVeiculoId)
                .FirstOrDefaultAsync(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi);

            //if (DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date && DetranRioVeiculoWS != null)
            //{
            //    DetranRioVeiculoBD = await UpdateRestricaoAsync(DetranRioVeiculoBD, DetranRioVeiculoWS);
            //}

            return await SetValuesToDetranRioVeiculoAsync(DetranRioVeiculoBD);
        }

        //private async Task<DetranRioVeiculoModel> UpdateRestricaoAsync(DetranRioVeiculoModel DetranRioVeiculoBD, DetranRioVeiculoModel DetranRioVeiculoWS)
        //{
        //    bool persistir = false;

        //    if (DetranRioVeiculoBD.InformacaoRoubo.ToStringIfNull() != DetranRioVeiculoWS.InformacaoRoubo.ToStringIfNull())
        //    {
        //        persistir = true;
        //    }

        //    if (DetranRioVeiculoBD.RestricaoEstelionato.ToStringIfNull() != DetranRioVeiculoWS.RestricaoEstelionato.ToStringIfNull())
        //    {
        //        persistir = true;
        //    }

        //    if (DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.CountItens() != DetranRioVeiculoWS.ListagemDetranRioVeiculoRestricao.CountItens())
        //    {
        //        persistir = true;
        //    }
        //    else if (DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.CountItens() == 1
        //        && DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.CountItens() == DetranRioVeiculoWS.ListagemDetranRioVeiculoRestricao.CountItens()
        //        && DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.FirstOrDefault().CodigoRestricao == 0)
        //    {
        //        DetranRioVeiculoRestricaoModel result = await _context.DetranRioVeiculoRestricao
        //            .FirstOrDefaultAsync(x => x.DetranVeiculoRestricaoId == DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.FirstOrDefault().DetranVeiculoRestricaoId);

        //        result.CodigoRestricao = DetranRioVeiculoWS.ListagemDetranRioVeiculoRestricao.FirstOrDefault().CodigoRestricao;

        //        using IDbContextTransaction transaction = _context.Database.BeginTransaction();

        //        _context.SetUserContextInfo(1);

        //        await _context.SaveChangesAsync();

        //        transaction.Commit();
        //    }

        //    if (!persistir)
        //    {
        //        return DetranRioVeiculoBD;
        //    }

        //    byte DetranVeiculoOrigemRestricaoId = await _context.DetranRioVeiculoOrigemRestricao
        //        .Where(x => x.Descricao == "DETRAN RJ")
        //        .Select(x => x.DetranVeiculoOrigemRestricaoId)
        //        .FirstOrDefaultAsync();

        //    DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

        //    if (DetranRioVeiculoWS.InformacaoRoubo.ToStringIfNull() != DetranRioVeiculoBD.InformacaoRoubo.ToStringIfNull())
        //    {
        //        if (DetranRioVeiculoWS.InformacaoRoubo.IsNullOrWhiteSpace())
        //        {
        //            await _context.DetranRioVeiculoRestricao
        //                .Where(x => x.DetranVeiculoId == DetranRioVeiculoBD.DetranVeiculoId && x.TipoRestricao == "R")
        //                .DeleteAsync();
        //        }
        //        else
        //        {
        //            DetranRioVeiculoRestricao = new()
        //            {
        //                DetranVeiculoId = DetranRioVeiculoBD.DetranVeiculoId,

        //                DetranVeiculoOrigemRestricaoId = DetranVeiculoOrigemRestricaoId,

        //                TipoRestricao = "R",

        //                Restricao = DetranRioVeiculoWS.InformacaoRoubo
        //            };

        //            await _context.DetranRioVeiculoRestricao.AddAsync(DetranRioVeiculoRestricao);

        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    if (DetranRioVeiculoWS.RestricaoEstelionato.ToStringIfNull() != DetranRioVeiculoBD.RestricaoEstelionato.ToStringIfNull())
        //    {
        //        if (DetranRioVeiculoWS.InformacaoRoubo.IsNullOrWhiteSpace())
        //        {
        //            await _context.DetranRioVeiculoRestricao
        //                .Where(x => x.DetranVeiculoId == DetranRioVeiculoBD.DetranVeiculoId && x.TipoRestricao == "E")
        //                .DeleteAsync();
        //        }
        //        else
        //        {
        //            DetranRioVeiculoRestricao = new()
        //            {
        //                DetranVeiculoId = DetranRioVeiculoBD.DetranVeiculoId,

        //                DetranVeiculoOrigemRestricaoId = DetranVeiculoOrigemRestricaoId,

        //                TipoRestricao = "E",

        //                Restricao = DetranRioVeiculoWS.RestricaoEstelionato
        //            };

        //            await _context.DetranRioVeiculoRestricao.AddAsync(DetranRioVeiculoRestricao);

        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    if (DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao.CountItens() == 0 && DetranRioVeiculoWS.ListagemDetranRioVeiculoRestricao.CountItens() > 0)
        //    {
        //        foreach (var item in DetranRioVeiculoWS.ListagemDetranRioVeiculoRestricao)
        //        {
        //            DetranRioVeiculoRestricao = new()
        //            {
        //                DetranVeiculoId = DetranRioVeiculoBD.DetranVeiculoId,

        //                DetranVeiculoOrigemRestricaoId = DetranVeiculoOrigemRestricaoId,

        //                TipoRestricao = item.TipoRestricao,

        //                CodigoRestricao = item.CodigoRestricao,

        //                Restricao = item.Restricao
        //            };

        //            await _context.DetranRioVeiculoRestricao.AddAsync(DetranRioVeiculoRestricao);

        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    else if (DetranRioVeiculoBD?.ListagemDetranRioVeiculoRestricao.CountItens() > 0 && DetranRioVeiculoWS?.ListagemDetranRioVeiculoRestricao.CountItens() == 0)
        //    {
        //        await _context.DetranRioVeiculoRestricao
        //            .Where(x => x.DetranVeiculoId == DetranRioVeiculoBD.DetranVeiculoId && (new[] { "A", "J" }.Contains(x.TipoRestricao)))
        //            .DeleteAsync();
        //    }

        //    DetranRioVeiculoModel DetranRioVeiculo = await _context.DetranRioVeiculo
        //        .Include(x => x.ListagemDetranRioVeiculoRestricao)
        //        .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
        //        .FirstOrDefaultAsync(x => !DetranRioVeiculoWS.Placa.IsNullOrWhiteSpace() ? x.Placa == DetranRioVeiculoWS.Placa : x.Chassi == DetranRioVeiculoWS.Chassi);

        //    DetranRioVeiculo.InformacaoRoubo = DetranRioVeiculoWS.InformacaoRoubo;

        //    DetranRioVeiculo.RestricaoEstelionato = DetranRioVeiculoWS.RestricaoEstelionato;

        //    DetranRioVeiculo.FlagRegistroNormalizado = "S";

        //    await _context.SaveChangesAsync();

        //    return DetranRioVeiculo;
        //}

        private async Task<DetranRioVeiculoViewModel> SetValuesToDetranRioVeiculoAsync(DetranRioVeiculoModel DetranRioVeiculoBD)
        {
            DetranRioVeiculoViewModel ResultView = _mapper.Map<DetranRioVeiculoViewModel>(DetranRioVeiculoBD);

            if (!DetranRioVeiculoBD.DescricaoTipo.IsNullOrWhiteSpace())
            {
                TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Descricao == DetranRioVeiculoBD.DescricaoTipo.ToUpperTrim());

                if (TipoVeiculo != null)
                {
                    ResultView.TipoVeiculo.IdentificadorTipoVeiculo = TipoVeiculo.TipoVeiculoId;

                    ResultView.TipoVeiculo.Descricao = TipoVeiculo.Descricao;

                    ResultView.TipoVeiculo.FlagNaoRequerCnhNaLiberacao = TipoVeiculo.FlagNaoRequerCnhNaLiberacao;
                }
                else
                {
                    ResultView.TipoVeiculo = null;
                }
            }
            else
            {
                ResultView.TipoVeiculo = null;
            }

            if (DetranRioVeiculoBD.Cor != null)
            {
                ResultView.Cor = new()
                {
                    IdentificadorCor = DetranRioVeiculoBD.Cor.CorId,

                    Cor = DetranRioVeiculoBD.Cor.Cor,

                    CorSecundaria = DetranRioVeiculoBD.Cor.CorSecundaria
                };
            }

            if (DetranRioVeiculoBD.MarcaModelo != null)
            {
                ResultView.MarcaModelo = new()
                {
                    IdentificadorMarcaModelo = DetranRioVeiculoBD.MarcaModelo.MarcaModeloId,

                    MarcaModelo = DetranRioVeiculoBD.MarcaModelo.MarcaModelo
                };
            }

            if (DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao != null)
            {
                List<TabelaGenericaModel> ListTipoRestricao = await new TabelaGenericaService(_context)
                    .ListAsync("DETRANRJ_TIPO_RESTRICAO");

                DetranRioVeiculoRestricaoViewModel DetranRioVeiculoRestricao = new();

                foreach (var item in DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao)
                {
                    DetranRioVeiculoRestricao = new()
                    {
                        IdentificadorRestricao = item.DetranVeiculoRestricaoId,

                        TipoRestricao = item.TipoRestricao,

                        CodigoRestricao = item.CodigoRestricao,

                        Restricao = item.Restricao
                    };

                    switch (item.TipoRestricao)
                    {
                        case "A":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "E":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "J":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "R":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;
                    }

                    DetranRioVeiculoRestricao.DetranRioVeiculoOrigemRestricao.IdentificadorOrigemRestricao = item.DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId;

                    DetranRioVeiculoRestricao.DetranRioVeiculoOrigemRestricao.Descricao = item.DetranRioVeiculoOrigemRestricao.Descricao;

                    ResultView.ListagemRestricao.Add(DetranRioVeiculoRestricao);
                }
            }

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        private async Task<DetranRioVeiculoModel> GetFromDetranAsync(string PlacaChassi, string Operador)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WSPatioxDetran");

            DetranRetornoConsultaModel DetranRioVeiculoWS = JsonHelper.DeserializeObject<DetranRetornoConsultaModel>(ClientConfig(WebServiceUrl)
                .ConsultarVeiculo(new ConsultarVeiculoRequest
                {
                    placa = PlacaChassi.ToUpperTrim(),

                    operador = Operador.ToUpperTrim()
                }).ConsultarVeiculoResult);

            if (DetranRioVeiculoWS.Retorno.StartsWith("VEICULO NAO CADASTRADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (DetranRioVeiculoWS.Retorno.StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (DetranRioVeiculoWS.Retorno.StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (!DetranRioVeiculoWS.Retorno.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            DetranRioVeiculoModel DetranRioVeiculo = new()
            {
                FlagRegistroNormalizado = "S",

                AnoFabricacao = DetranRioVeiculoWS.AnoFabricacao,

                AnoModelo = DetranRioVeiculoWS.AnoModelo,

                AnoUltimaLicenca = DetranRioVeiculoWS.AnoUltimaLicenca,

                CapacidadeCarga = DetranRioVeiculoWS.CapacidadeCarga,

                CapacidadePassageiros = DetranRioVeiculoWS.CapacidadePassageiros,

                ChassiRemarcado = DetranRioVeiculoWS.ChassiRemarcado.ToUpperTrim().Left(1),

                Renavam = DetranRioVeiculoWS.NumeroRenavam.Trim(),

                Chassi = DetranRioVeiculoWS.Chassi.ToUpperTrim(),

                Classificacao = DetranRioVeiculoWS.Classificacao.ToUpperTrim(),

                CodigoCategoria = DetranRioVeiculoWS.CodigoCategoria.ToUpperTrim(),

                DescricaoCategoria = DetranRioVeiculoWS.DescricaoCategoria.ToUpperTrim(),

                DescricaoTipo = DetranRioVeiculoWS.DescricaoTipo.ToUpperTrim(),

                InformacaoRoubo = DetranRioVeiculoWS.InformacaoRoubo.ToUpperTrim().ToNullIfEmpty(),

                PesoBrutoTotal = DetranRioVeiculoWS.PesoBrutoTotal.ToUpperTrim(),

                Placa = DetranRioVeiculoWS.Placa.ToUpperTrim(),

                RestricaoEstelionato = DetranRioVeiculoWS.RestricaoEstelionato.ToUpperTrim().ToNullIfEmpty(),

                Uf = DetranRioVeiculoWS.Uf.ToUpperTrim(),

                Cor = await _context.Cor
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CorSecundaria == DetranRioVeiculoWS.DescricaoCor.ToUpperTrim()),

                MarcaModelo = await _context.MarcaModelo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.MarcaModelo == DetranRioVeiculoWS.DescricaoMarca.ToUpperTrim())
            };

            DetranRioVeiculo.CorId = DetranRioVeiculo.Cor.CorId;

            DetranRioVeiculo.MarcaModeloId = DetranRioVeiculo.MarcaModelo.MarcaModeloId;

            DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao = await _context.DetranRioVeiculoOrigemRestricao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Descricao == "DETRAN RJ");

            DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

            if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace()
             || !DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace()
             || DetranRioVeiculoWS?.RestricoesAdministrativas.Count > 0
             || DetranRioVeiculoWS?.RestricoesJuridicas.Count > 0)
            {
                DetranRioVeiculo.ListagemDetranRioVeiculoRestricao = new List<DetranRioVeiculoRestricaoModel>();

                if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace() || !DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace())
                {
                    if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace())
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "R",

                            Restricao = DetranRioVeiculo.InformacaoRoubo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }

                    if (!DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace())
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "E",

                            Restricao = DetranRioVeiculo.RestricaoEstelionato
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }

                if (DetranRioVeiculoWS?.RestricoesAdministrativas.Count > 0)
                {
                    foreach (var item in DetranRioVeiculoWS.RestricoesAdministrativas)
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "A",

                            Restricao = item.Restricao,

                            CodigoRestricao = item.Codigo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }

                if (DetranRioVeiculoWS?.RestricoesJuridicas.Count > 0)
                {
                    foreach (var item in DetranRioVeiculoWS.RestricoesJuridicas)
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "J",

                            Restricao = item.Restricao,

                            CodigoRestricao = item.Codigo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }
            }

            return DetranRioVeiculo;
        }

        private async Task DeleteDuplicatedAsync(string Placa, string Chassi)
        {
            List<int> ids = await _context.DetranRioVeiculo
                .Where(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi)
                .Select(x => x.DetranVeiculoId)
                .ToListAsync();

            if (ids.Count > 1)
            {
                List<DetranRioVeiculoModel> ListDetranRioVeiculo = await _context.DetranRioVeiculo
                    .Include(x => x.ListagemDetranRioVeiculoRestricao)
                    .Where(x => x.DetranVeiculoId != ids.Last())
                    .Where(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi)
                    .ToListAsync();

                foreach (var item in ListDetranRioVeiculo)
                {
                    if (item?.ListagemDetranRioVeiculoRestricao.Count > 0)
                    {
                        await _context.DetranRioVeiculoRestricao
                            .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                            .DeleteAsync();
                    }

                    await _context.DetranRioVeiculo
                        .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                        .DeleteAsync();
                }
            }
        }

        private static WSPatioxDetranSoapClient ClientConfig(WebServiceUrlModel WebServiceUrl)
        {
            BasicHttpBinding httpBinding = new()
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue
            };

            httpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            WSPatioxDetranSoapClient client = new(httpBinding, new(new Uri(WebServiceUrl.Url)));

            client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            client.ClientCredentials.Windows.ClientCredential.Domain = string.Empty;
            client.ClientCredentials.Windows.ClientCredential.UserName = WebServiceUrl.Username;
            client.ClientCredentials.Windows.ClientCredential.Password = WebServiceUrl.Password;
            client.ClientCredentials.UserName.UserName = WebServiceUrl.Username;
            client.ClientCredentials.UserName.Password = WebServiceUrl.Password;

            client.ChannelFactory.CreateChannel();

            return client;
        }
    }
}