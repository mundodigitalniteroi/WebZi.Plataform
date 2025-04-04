﻿using System.Diagnostics;
using System.Text;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.CrossCutting.Linq;
using WebZi.Plataform.CrossCutting.Secutity;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

class Program
{
    static void Main()
    {
        Console.WriteLine("WebZi Plataform");

        AppDbContext _context = new();


        var faturamentoService = new FaturamentoService(_context);

        var parametros = new SimulacaoParameters()
        {
            CodigoProduto = "DEP",
            Placa = "KOP1904",
            IdentificadorUsuario = 1,
            IdentificadorCliente = 1,
            IdentificadorDeposito = 1,
        };

        var result = faturamentoService.SimularAsync(parametros).Result;

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

        //Debugger.Break();

        //var aux = CreateSecret();

        //List<string> strings = new()
        //{
        //    "e",

        //    "B     ",

        //    "",

        //    " ",

        //    null,

        //    null,

        //    "a"
        //};

        //List<int> numbers = new()
        //{
        //   1
        //};

        //bool result = false;

        //result = numbers.ContainsNegativeOrZeroNumbers();

        //result = strings.ContainsDuplicates();

        //List<string> newStrings = LinqHelper.GetList(strings, LinqHelper.LinqListFlags.OrderByDesc | LinqHelper.LinqListFlags.Trim | LinqHelper.LinqListFlags.ToNullIfWhiteSpace);

        //foreach (string s in newStrings)
        //{
        //    WriteLine(s == null ? "null" : s + ".");
        //}

        //Debugger.Break();
    }

    static void WriteLine(string message)
    {
        Debug.WriteLine(message);

        Console.WriteLine(message);
    }

    static string CreateSecret()
    {
        byte[] textoAsBytes = Encoding.ASCII.GetBytes("AliCE_PAiS#no%MAraViLHas12345678");

        string key = Convert.ToBase64String(textoAsBytes);

        key = AppSettingsHelper.GetValue("Segredo", "QRCode");

        string encrypted = CryptographyHelper.EncryptString(key, "TESTE");

        string decrypted = CryptographyHelper.DecryptString(key, encrypted);

        return decrypted;
    }
}