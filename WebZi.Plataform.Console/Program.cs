using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using Z.EntityFramework.Plus;

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

        FaturamentoModel Faturamento = _context.Faturamento
            .Where(w => w.FaturamentoId == 909674)
            .AsNoTracking()
            .FirstOrDefault();

        GrvModel Grv = _context.Grv
            .Include(i => i.Cliente)
            .ThenInclude(t => t.Endereco)
            .Include(i => i.Deposito)
            .ThenInclude(t => t.Endereco)
            .Include(i => i.Reboque)
            .Include(i => i.Reboquista)
            .Include(i => i.MarcaModelo)
            .Include(i => i.Cor)
            .Include(i => i.TipoVeiculo)
            .Include(i => i.Atendimento)
            .ThenInclude(t => t.Faturamentos.Where(w => w.FaturamentoId == Faturamento.FaturamentoId))
            .Include(i => i.Atendimento)
            .ThenInclude(t => t.QualificacaoResponsavel)
            .Where(w => w.Atendimento.AtendimentoId == Faturamento.AtendimentoId)
            .AsNoTracking()
            .FirstOrDefault();

        if (true)
        {

        }

        //GrvModel Grv = _context.Grv
        //    .Include(i => i.Atendimento)
        //    .ThenInclude(t => t.Faturamentos.Where(w => w.FaturamentoId == FaturamentoId))
        //    .Include(t => t.Atendimento)
        //    .AsNoTracking()
        //    .FirstOrDefault();


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

            List<WebZi.Plataform.Domain.Models.Faturamento.FaturamentoServicoAssociadoModel> foo = _context.FaturamentoServicoAssociado

                .Take(1)

                .Include(i => i.FaturamentoServicoTipo)
                .ThenInclude(t => t.FaturamentoProduto)

                .Include(i => i.Cliente)
                .ThenInclude(t => t.TipoMeioCobranca)

                .Include(i => i.Deposito)

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