using AutoMapper;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento.ViewModel;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.ViewModel;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.ViewModel;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Usuario.ViewModel;
using WebZi.Plataform.Domain.ViewModel.GRV;

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

            CreateMap<List<LacreModel>, List<LacreResultViewModel>>();

            CreateMap<UsuarioModel, UsuarioViewModel>();
        }
    }
}