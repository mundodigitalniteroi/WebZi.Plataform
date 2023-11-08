using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using System.Text;
using WebZi.Plataform.CrossCutting.Secutity;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Database;
using WebZi.Plataform.Domain.Models.GRV;

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

        //var result = _context.AgenciaBancaria
        //    .Include(x => x.UsuarioCadastro)
        //    .Include(x => x.UsuarioAlteracao)
        //    .Include(x => x.Banco)
        //    .ThenInclude(x => x.UsuarioCadastro)
        //    .Include(x => x.Banco)
        //    .ThenInclude(x => x.UsuarioAlteracao)
        //    // .Where(x => x.)
        //    .ToList();

        //DataTable result = _context.GetForeingKeys("tb_dep_grv");

        //Console.WriteLine("TOTAL:" + result.Rows.Count);

        // var result = _context.GetForeingKeys("tb_dep_grv");

        // _context.SaveChanges();        

        byte[] textoAsBytes = Encoding.ASCII.GetBytes("OEXTERMINADORDOFUTURO12345678901");

        string key = Convert.ToBase64String(textoAsBytes);

        string encrypted = CryptographyHelper.EncryptString(key, "TESTE");

        string decrypted = CryptographyHelper.DecryptString(key, encrypted);

        if (true)
        {

        }
    }
}