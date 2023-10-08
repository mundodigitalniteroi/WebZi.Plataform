using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.GRV;

class Program
{
    static void Main()
    {
        Console.WriteLine("WebZi Plataform");

        AppDbContext _context = new();

        List<GrvModel> result = _context.Grv
            .Include(i => i.UsuarioClienteDepositoGrvModel)
            .Where(w => w.UsuarioClienteDepositoGrvModel.UsuarioId == 3190)
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