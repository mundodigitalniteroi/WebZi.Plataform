using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.AutoMapper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;

namespace WebZi.Plataform.Data.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperService));

            services.AddScoped<AgenciaBancariaService>();

            services.AddScoped<AtendimentoService>();

            services.AddScoped<BancoService>();

            services.AddScoped<BucketArquivoService>();

            services.AddScoped<CalculoDiariasService>();

            services.AddScoped<EmpresaService>();

            services.AddScoped<EnderecoService>();

            services.AddScoped<ClienteService>();

            services.AddScoped<DepositoService>();

            services.AddScoped<FaturamentoBoletoService>();

            services.AddScoped<FaturamentoGuiaPagamentoReboqueEstadiaService>();

            services.AddScoped<FaturamentoService>();

            services.AddScoped<FeriadoService>();

            services.AddScoped<GgvService>();

            services.AddScoped<GrvService>();

            services.AddScoped<LeilaoService>();

            services.AddScoped<PixDinamicoService>();

            services.AddScoped<PixEstaticoService>();

            services.AddScoped<QualificacaoResponsavelService>();

            services.AddScoped<ServicoService>();

            services.AddScoped<SistemaService>();

            services.AddScoped<TipoAvariaService>();

            services.AddScoped<TipoMeioCobrancaService>();

            services.AddScoped<UsuarioService>();

            services.AddScoped<VeiculoService>();

            services.AddScoped<VistoriaService>();
        }
    }
}