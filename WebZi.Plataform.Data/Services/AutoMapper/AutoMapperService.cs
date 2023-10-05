using AutoMapper;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.Deposito;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.ViewModel.Servico;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.Views.Localizacao;

namespace WebZi.Plataform.Data.Services.AutoMapper
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            // CreateMap<Model, ViewModel>();

            CreateMap<AtendimentoModel, AtendimentoViewModel>();

            CreateMap<AgenciaBancariaModel, AgenciaBancariaViewModel>();

            CreateMap<AutoridadeResponsavelModel, AutoridadeResponsavelViewModel>();

            CreateMap<BancoModel, BancoViewModel>();

            CreateMap<ClienteModel, ClienteViewModel>();

            CreateMap<DepositoModel, DepositoViewModel>();

            CreateMap<ViewEnderecoCompletoModel, EnderecoViewModel>();

            CreateMap<GrvModel, GrvViewModel>();

            CreateMap<LacreModel, LacreViewModel>();

            CreateMap<ReboqueModel, ReboqueViewModel>();

            CreateMap<ReboquistaModel, ReboquistaViewModel>();

            CreateMap<UsuarioModel, UsuarioViewModel>();
        }
    }
}