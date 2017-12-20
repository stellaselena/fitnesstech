using AutoMapper;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;

namespace FitnessTech.Data
{
    public class FitnessMappingProfile : Profile
    {
        public FitnessMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();

        }

    }
}
