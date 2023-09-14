﻿using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.Data.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<GrvService, GrvService>();

            services.AddScoped<AtendimentoService, AtendimentoService>();

            services.AddScoped<QualificacaoResponsavelService,QualificacaoResponsavelService > ();

            services.AddScoped<StatusOperacaoService, StatusOperacaoService>();

            services.AddScoped<TipoMeioCobrancaService, TipoMeioCobrancaService>();
        }
    }
}