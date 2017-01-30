using AutoMapper;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.Domain.Entities;

namespace PL.Integritas.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {
            this.CreateMap<EntityBaseViewModel, EntityBase>()
                .ForMember(m => m.ValidationResult,
                             vm => vm.MapFrom(src => src.ValidationResult));

            this.CreateMap<ProductViewModel, Product>();

            this.CreateMap<ShoppingCartItemViewModel, ShoppingCartItem>()
                .ForMember(m => m.ShoppingCartId,
                             vm => vm.MapFrom(src => src.ShoppingCartId))
                .ForMember(m => m.ProductId,
                             vm => vm.MapFrom(src => src.ProductId));

            this.CreateMap<ShoppingCartViewModel, ShoppingCart>()
                .ForMember(m => m.Items,
                             vm => vm.MapFrom(src => src.ItemsViewModel));

            this.CreateMap<PurchaseViewModel, Purchase>()
                 .ForMember(m => m.ShoppingCartId,
                             vm => vm.MapFrom(src => src.ShoppingCartId));
        }
    }
}
