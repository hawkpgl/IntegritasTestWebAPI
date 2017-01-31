using AutoMapper;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.Domain.Entities;

namespace PL.Integritas.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            this.CreateMap<EntityBase, EntityBaseViewModel>()
                .ForMember(m => m.ValidationResult,
                             vm => vm.MapFrom(src => src.ValidationResult));

            this.CreateMap<Product, ProductViewModel>();

            this.CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();

            this.CreateMap<ShoppingCart, ShoppingCartViewModel>()
                .ForMember(vm => vm.ItemsViewModel,
                             m => m.MapFrom(src => src.Items));

            this.CreateMap<Purchase, PurchaseViewModel>(); 
        }
    }
}
