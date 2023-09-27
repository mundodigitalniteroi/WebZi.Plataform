using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.AutoMapper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;

namespace WebZi.Plataform.Data.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperService));

            services.AddScoped<AgenciaBancariaService, AgenciaBancariaService>();

            services.AddScoped<AtendimentoService, AtendimentoService>();

            services.AddScoped<BancoService, BancoService>();

            services.AddScoped<FaturamentoBoletoService, FaturamentoBoletoService>();

            services.AddScoped<CalculoDiariasService, CalculoDiariasService>();

            services.AddScoped<CEPService, CEPService>();

            services.AddScoped<ClienteService, ClienteService>();

            services.AddScoped<ConfiguracaoService, ConfiguracaoService>();

            services.AddScoped<DepositoService, DepositoService>();

            services.AddScoped<FaturamentoService, FaturamentoService>();

            services.AddScoped<FeriadoService, FeriadoService>();

            services.AddScoped<GrvService, GrvService>();

            services.AddScoped<LacreService, LacreService>();

            services.AddScoped<LeilaoService, LeilaoService>();

            services.AddScoped<QualificacaoResponsavelService, QualificacaoResponsavelService>();

            services.AddScoped<StatusOperacaoService, StatusOperacaoService>();

            services.AddScoped<TipoMeioCobrancaService, TipoMeioCobrancaService>();

            services.AddScoped<UsuarioService, UsuarioService>();
        }
    }
}