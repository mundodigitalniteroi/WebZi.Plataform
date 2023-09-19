using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.Data.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AtendimentoService, AtendimentoService>();

            services.AddScoped<CalculoDiariasService, CalculoDiariasService>();

            services.AddScoped<CEPService, CEPService>();

            services.AddScoped<ConfiguracaoService, ConfiguracaoService>();

            services.AddScoped<DepositoService, DepositoService>();

            services.AddScoped<FaturamentoService, FaturamentoService>();

            services.AddScoped<FeriadoService, FeriadoService>();

            services.AddScoped<GrvService, GrvService>();

            services.AddScoped<LeilaoService, LeilaoService>();

            services.AddScoped<QualificacaoResponsavelService, QualificacaoResponsavelService>();

            services.AddScoped<StatusOperacaoService, StatusOperacaoService>();

            services.AddScoped<TipoMeioCobrancaService, TipoMeioCobrancaService>();
        }
    }
}