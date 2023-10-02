using AutoMapper;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.Usuario;

namespace WebZi.Plataform.Data.Services.AutoMapper
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            // CreateMap<Model, ViewModel>();

            CreateMap<AtendimentoModel, AtendimentoViewModel>();

            CreateMap<AgenciaBancariaModel, AgenciaBancariaViewModel>();

            CreateMap<BancoModel, BancoViewModel>();

            CreateMap<GrvModel, GrvViewModel>();

            // CreateMap<List<LacreModel>, List<LacreResultViewModel>>();

            CreateMap<UsuarioModel, UsuarioViewModel>();
        }
    }
}