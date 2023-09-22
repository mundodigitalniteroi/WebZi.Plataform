using AutoMapper;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.ViewModel;

namespace WebZi.Plataform.Data.Services.AutoMapper
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            CreateMap<GrvModel, GrvViewModel>();
        }
    }
}