using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoService
    {
        private readonly AppDbContext _context;

        public FaturamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FaturamentoModel> Calcular(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            //int primeiroFaturamentoId = 0;

            //int ultimoFaturamentoId = 0;

            //// INDICA QUE É PRECISO FATURAR CONTENDO DIÁRIA, REBOCADA E QUILOMETRAGEM
            //bool flagFaturamentoCompleto = false;

            //bool faturamentoAdicional = false;

            //CalculoDiariasModel CalculoDiarias = await _provider
            //    .GetService<CalculoDiariasService>()
            //    .Calcular(ParametrosCalculoFaturamento.Grv);

            CalculoDiariasModel CalculoDiarias = await new CalculoDiariasService(_context)
                .Calcular(ParametrosCalculoFaturamento.Grv, ParametrosCalculoFaturamento.DataHoraAtualPorDeposito);

            ParametrosCalculoFaturamento.DataHoraGuarda = CalculoDiarias.DataHoraInicialParaCalculo;

            // TODO: Aplicar desconto
            //if (ParametrosCalculoFaturamento.StatusOperacaoId == "L")
            //{

            //}

            // TODO: Verificar necessidade de Alteração da Quantidade dos Serviços selecionados
            // ParametrosCalculoFaturamentoModel.FaturamentoQuantidadeAlteradaList = RetornarAlteracaoQuantidadeServico();

            CalculoFaturamentoModel CalculoFaturamento = new();

            if (ParametrosCalculoFaturamento.DataLiberacao == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataLiberacao = ParametrosCalculoFaturamento.DataHoraAtualPorDeposito;
            }

            List<FaturamentoServicoAssociadoModel> FaturamentoServicosGrv = await SelecionarServicoAssociadoGrv(ParametrosCalculoFaturamento.Grv.GrvId, FaturamentoProdutoId: "DEP", FlagSelecionarSomenteServicosAtivos: false, "N");

            List<FaturamentoServicoAssociadoModel> FaturamentoServicosVeiculo = await SelecionarServicoAssociadoVeiculo(ParametrosCalculoFaturamento.Grv);

            //try
            //{
            //    FaturamentoServicosGrvs = ViewFaturamentoServicosGRVController.Listar
            //    (
            //        ParametrosCalculoFaturamentoModel.id_grv,
            //        Grv.faturamento_produto_codigo,
            //        false,
            //        'N'
            //    );

            //    if (FaturamentoServicosGrvs == null)
            //    {
            //        throw new Exception("Não foi encontrado Serviço cadastrado para este GRV");
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return null;
        }

        private async Task<List<FaturamentoServicoAssociadoModel>> SelecionarServicoAssociadoGrv(int GrvId, string FaturamentoProdutoId, bool FlagSelecionarSomenteServicosAtivos = true, string FlagTributacao = "")
        {
            try
            {
                var foo =  await _context.FaturamentoServicosAssociados
                    
                    .Take(1)
                    
                    .Include(i => i.FaturamentoServicoTipo)
                    .ThenInclude(t => t.FaturamentoProduto)
                    
                    .Include(i => i.Cliente)
                    .ThenInclude(t => t.TipoMeioCobranca)

                    .Include(i => i.Deposito)

                    .Include(i => i.FaturamentoServicosTiposVeiculos)
                    //.ThenInclude(t => t.TipoVeiculo)

                    //.Include(i => i.FaturamentosServicosTiposVeiculos)
                    .ThenInclude(i => i.FaturamentoServicosGrvs.Where(w => w.GrvId == GrvId))

                    //.Include(i => i.FaturamentoRegra)
                    //.ThenInclude(t => t.FaturamentoRegraTipo)

                    //.Where(w => w.FaturamentoServicoTipo.FaturamentoProduto.FaturamentoProdutoId == FaturamentoProdutoId &&
                    //            (FlagSelecionarSomenteServicosAtivos ? w.DataVigenciaFinal == null : (w.DataVigenciaFinal == null || w.DataVigenciaFinal != null)) &&
                    //            (!string.IsNullOrWhiteSpace(FlagTributacao) ? w.FaturamentoServicoTipo.FlagTributacao == FlagTributacao : w.FaturamentoServicoTipo.FlagTributacao != null)
                    //)

                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {

            }

            return await _context.FaturamentoServicosAssociados
                .Include(i => i.FaturamentoServicoTipo)
                .ThenInclude(t => t.FaturamentoProduto)

                .Include(i => i.Cliente)
                .ThenInclude(t => t.TipoMeioCobranca)

                .Include(i => i.Deposito)

                .Include(i => i.FaturamentoServicosTiposVeiculos)
                .ThenInclude(t => t.TipoVeiculo)

                .Include(i => i.FaturamentoServicosTiposVeiculos)
                .ThenInclude(i => i.FaturamentoServicosGrvs.Where(w => w.GrvId == GrvId))

                .Include(i => i.FaturamentoRegra)
                .ThenInclude(t => t.FaturamentoRegraTipo)

                .Where(w => w.FaturamentoServicoTipo.FaturamentoProduto.FaturamentoProdutoId == FaturamentoProdutoId &&
                            (FlagSelecionarSomenteServicosAtivos ? w.DataVigenciaFinal == null : (w.DataVigenciaFinal == null || w.DataVigenciaFinal != null)) &&
                            (!string.IsNullOrWhiteSpace(FlagTributacao) ? w.FaturamentoServicoTipo.FlagTributacao == FlagTributacao : w.FaturamentoServicoTipo.FlagTributacao != null)
                )

                .AsNoTracking()
                .ToListAsync();
        }

        private async Task<List<FaturamentoServicoAssociadoModel>> SelecionarServicoAssociadoVeiculo(GrvModel Grv)
        {
            return await _context.FaturamentoServicosAssociados
                .Include(i => i.FaturamentoServicoTipo)
                .ThenInclude(t => t.FaturamentoProduto)

                .Include(i => i.Cliente)
                .ThenInclude(t => t.TipoMeioCobranca)

                .Include(i => i.Deposito)

                .Include(i => i.FaturamentoServicosTiposVeiculos)
                .ThenInclude(t => t.TipoVeiculo.TipoVeiculoId == Grv.TipoVeiculoId)

                .Include(i => i.FaturamentoServicosTiposVeiculos)
                .ThenInclude(t => t.FaturamentoServicosGrvs)

                .Include(i => i.FaturamentoRegra)
                .ThenInclude(t => t.FaturamentoRegraTipo)

                .Where(w => w.ClienteId == Grv.ClienteId &&
                            w.DepositoId == Grv.DepositoId &&
                            w.FaturamentoServicoTipo.FaturamentoProduto.FaturamentoProdutoId == Grv.FaturamentoProdutoId)

                .AsNoTracking()
                .ToListAsync();
        }
    }
}