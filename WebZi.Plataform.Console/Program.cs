using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento;

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

        try
        {
            Testar(_context);
        }
        catch (Exception ex)
        {
            if (true)
            {

            }
        }

        int GrvId = 375251;

        try
        {
            var result = _context.FaturamentoServicosGrvs
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

            var foo = _context.FaturamentoServicosAssociados

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

    private static async Task<bool> Testar(AppDbContext _context)
    {
        AtendimentoViewModel AtendimentoInput = new()
        {
            GrvId = 1145732,
            QualificacaoResponsavelId = 3,
            UsuarioCadastroId = 1,
            ResponsavelNome = "TESTE API NOME RESPONSÁVEL",
            ResponsavelDocumento = "96637737585",
            ResponsavelCnh = "96637737585",
            ResponsavelEndereco = "RUA JÚLIA FERREIRA DA SILVA",
            ResponsavelNumero = "135",
            ResponsavelComplemento = "COMPLEMENTO",
            ResponsavelBairro = "MONTE CASTELO",
            ResponsavelMunicipio = "CABEDELO",
            ResponsavelUf = "PB",
            ResponsavelCep = "58101025",
            ResponsavelDdd = "71",
            ResponsavelTelefone = "998887777",
            ProprietarioNome = "TESTE API NOME PROPRIETÁRIO",
            ProprietarioTipoDocumentoId = 2,
            ProprietarioDocumento = "96637737585",
            ProprietarioEndereco = "RUA JÚLIA FERREIRA DA SILVA",
            ProprietarioNumero = "135",
            ProprietarioComplemento = "COMPLEMENTO",
            ProprietarioBairro = "MONTE CASTELO",
            ProprietarioMunicipio = "CABEDELO",
            ProprietarioUf = "PB",
            ProprietarioCep = "58101025",
            ProprietarioDdd = "71",
            ProprietarioTelefone = "998887777",
            DataHoraInicioAtendimento = DateTime.Now,
            TipoMeioCobrancaId = 1
        };

        AtendimentoService atendimentoService = new(_context, null);

        string Atendimento = await atendimentoService
                .Cadastrar(AtendimentoInput);

        return true;
    }
}