using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Documento;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Liberacao;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;

namespace WebZi.Plataform.Data.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperService));

            services.AddHttpClient();

            services.AddScoped<AgenciaBancariaService>();

            services.AddScoped<AtendimentoService>();

            services.AddScoped<BancoService>();

            services.AddScoped<BucketService>();

            services.AddScoped<CalculoDiariasService>();

            services.AddScoped<DocumentoService>();

            services.AddScoped<EmpresaService>();

            services.AddScoped<EnderecoService>();

            services.AddScoped<ExclusaoHierarquicaService>();

            services.AddScoped<ClienteService>();

            services.AddScoped<DepositoService>();

            services.AddScoped<BoletoService>();

            services.AddScoped<GuiaPagamentoReboqueEstadiaService>();

            services.AddScoped<FaturamentoService>();

            services.AddScoped<FeriadoService>();

            services.AddScoped<GgvService>();

            services.AddScoped<GrvService>();

            services.AddScoped<LeilaoService>();

            services.AddScoped<LiberacaoService>();

            services.AddScoped<PessoaService>();

            services.AddScoped<PixDinamicoService>();

            services.AddScoped<PixEstaticoService>();

            services.AddScoped<ServicoService>();

            services.AddScoped<SistemaService>();

            services.AddScoped<TabelaGenericaService>();

            services.AddScoped<TipoAvariaService>();

            services.AddScoped<TipoMeioCobrancaService>();

            services.AddScoped<UsuarioService>();

            services.AddScoped<VeiculoService>();

            services.AddScoped<VistoriaService>();

            #region WebServices
            services.AddScoped<DetranAlagoasService>();

            services.AddScoped<DetranRioService>();
            #endregion
        }
    }
}