using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;

class Program
{
    static void Main()
    {
        Console.WriteLine("WezZi Plataform");

        AppDbContext _context = new();

        //var Bairro = _context.Bairros
        //    .Include(i => i.Municipio)
        //    .Where(w => w.Nome.Equals("COPACABANA"))
        //    .FirstOrDefault();

        //ContinenteModel result = _context.Continentes
        //    .Include(i => i.Paises.Where(w => w.PaisNumcode.Equals("076")))
        //    .ThenInclude(t => t.Estados.Where(w => w.Uf.Equals("RJ")))
        //    .ThenInclude(t => t.Regiao)
        //    .ThenInclude(t => t.Estados.Where(w => w.Uf.Equals("RJ")))
        //    .ThenInclude(t => t.Municipios.Where(w => w.Nome.Equals("RIO DE JANEIRO")))
        //    .ThenInclude(t => t.Bairros.Where(w => w.Nome.Equals("COPACABANA")))
        //    .ThenInclude(t => t.CEP)
        //    .ThenInclude(t => t.TipoLogradouro)
        //    .Where(w => w.ContinenteId.Equals("ASU"))
        //    .FirstOrDefault();

        // public async Task<string> Cadastrar(AtendimentoViewModel AtendimentoInput)

        int GrvId = 375251;

        try
        {
            WebZi.Plataform.Domain.Models.Faturamento.FaturamentoServicoGrvModel result = _context.FaturamentoServicoGrv
                .Include(i => i.Grv)
                .Include(i => i.FaturamentoServicoTipoVeiculo)
                .ThenInclude(i => i.FaturamentoServicoAssociado)
                .ThenInclude(i => i.FaturamentoServicoTipo)
                .ThenInclude(i => i.FaturamentoProduto)

                .Include(i => i.FaturamentoServicoTipoVeiculo)
                .ThenInclude(i => i.TipoVeiculo)

                .Where(w => w.GrvId == 375251)
                .Take(10)
                .FirstOrDefault();

            if (true)
            {

            }

            //var FaturamentosServicosTiposVeiculos = _context
            //    .FaturamentoServicosTiposVeiculos
            //    .Include(i => i.FaturamentoServicosGrvs.Where(w => w.GrvId == GrvId))
            //    .Take(10)
            //    .Where(w => w.FaturamentoServicosTiposVeiculos.)
            //    .ToList();

            List<WebZi.Plataform.Domain.Models.Faturamento.FaturamentoServicoAssociadoModel> foo = _context.FaturamentoServicoAssociado

                .Take(1)

                .Include(i => i.FaturamentoServicoTipo)
                .ThenInclude(t => t.FaturamentoProduto)

                .Include(i => i.Cliente)
                .ThenInclude(t => t.TipoMeioCobranca)

                .Include(i => i.Deposito)

                //.Include(i => i.FaturamentoRegra)
                //.ThenInclude(t => t.FaturamentoRegraTipo)

                //.Where(w => w.FaturamentoServicoTipo.FaturamentoProduto.FaturamentoProdutoId == FaturamentoProdutoId &&
                //            (FlagSelecionarSomenteServicosAtivos ? w.DataVigenciaFinal == null : (w.DataVigenciaFinal == null || w.DataVigenciaFinal != null)) &&
                //            (!string.IsNullOrWhiteSpace(FlagTributacao) ? w.FaturamentoServicoTipo.FlagTributacao == FlagTributacao : w.FaturamentoServicoTipo.FlagTributacao != null)
                //)

                .AsNoTracking()
                .ToList();

            if (true)
            {

            }
        }
        catch (Exception ex)
        {
            if (true)
            {

            }
        }

        if (true)
        {

        }
    }
}