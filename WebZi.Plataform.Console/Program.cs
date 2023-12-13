using System.Text;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.CrossCutting.Secutity;
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

        byte[] textoAsBytes = Encoding.ASCII.GetBytes("AliCE_PAiS#no%MAraViLHas12345678");

        string key = Convert.ToBase64String(textoAsBytes);

        key = AppSettingsHelper.GetValue("Segredo", "QRCode");

        string encrypted = CryptographyHelper.EncryptString(key, "TESTE");

        string decrypted = CryptographyHelper.DecryptString(key, encrypted);

        if (true)
        {

        }
    }
}