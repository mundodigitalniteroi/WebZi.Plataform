using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;

class Program
{
    static void Main()
    {
        Console.WriteLine("WebZi Plataform");

        AppDbContext _context = new();

        //var result = _context.Empresa
        //    .Include(x => x.CEP)
        //    .ThenInclude(x => x.Bairro)
        //    .ThenInclude(x => x.Municipio)
        //    .ThenInclude(x => x.Estado)
        //    .ThenInclude(x => x.Pais)
        //    .Include(x => x.CEP)
        //    .ThenInclude(x => x.Municipio)
        //    .ThenInclude(x => x.Feriados)
        //    .Include(x => x.CEP)
        //    .ThenInclude(x => x.TipoLogradouro)
        //    .Where(x => x.CNPJ == "27947093000112")
        //    .ToList();

        var result = _context.Empresa
            .Include(x => x.Endereco)
            .Include(x => x.EmpresaClassificacao)
            .Where(x => x.EmpresaId == 27)
            .ToList();

        Console.WriteLine("TOTAL:" + result.Count);

        // _context.SaveChanges();

        if (true)
        {

        }

        if (true)
        {

        }
    }
}