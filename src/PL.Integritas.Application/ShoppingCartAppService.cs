using AutoMapper;
using PL.Integritas.Application.Interfaces;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.Domain.Entities;
using PL.Integritas.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PL.Integritas.Application
{
    public class ShoppingCartAppService : AppService, IShoppingCartAppService
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartAppService(IShoppingCartService shoppingCartService,
                                      IShoppingCartItemService shoppingCartItemService,
                                      IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
            _productService = productService;
        }

        public ShoppingCartViewModel Add(ShoppingCartViewModel shoppingCartViewModel)
        {
           var shoppingCart = Mapper.Map<ShoppingCartViewModel, ShoppingCart>(shoppingCartViewModel);

            BeginTransaction();

            _shoppingCartService.Add(shoppingCart);

            Commit();

            return shoppingCartViewModel;
        }

        public ShoppingCartItemViewModel AddItemToCart(int cartNumber, int productId)
        {
            ShoppingCartViewModel shoppingCartViewModel = Mapper.Map<ShoppingCart, ShoppingCartViewModel>(_shoppingCartService.Search(x => x.Number == cartNumber)
                                                                                                                            .FirstOrDefault());
            
            if (shoppingCartViewModel == null)
            {
                Add(new ShoppingCartViewModel { Number = cartNumber });

                shoppingCartViewModel = Mapper.Map<ShoppingCart, ShoppingCartViewModel>(_shoppingCartService.Search(x => x.Number == cartNumber).FirstOrDefault());
            }

            BeginTransaction();

            ShoppingCartItem item = new ShoppingCartItem();
            item.ProductId = productId;
            item.ShoppingCartId = shoppingCartViewModel.Id;

            ShoppingCartItemViewModel shoppingCartItemViewModel = Mapper.Map<ShoppingCartItem, ShoppingCartItemViewModel>(_shoppingCartItemService.Add(item));
           
            Commit();

            return shoppingCartItemViewModel;
        }

        public ShoppingCartViewModel Update(ShoppingCartViewModel shoppingCartViewModel)
        {
            var shoppingCart = Mapper.Map<ShoppingCartViewModel, ShoppingCart>(shoppingCartViewModel);

            BeginTransaction();

            _shoppingCartService.Update(shoppingCart);

            Commit();

            return shoppingCartViewModel;
        }

        public void Remove(Int64 id)
        {
            BeginTransaction();

            var shoppingCart = _shoppingCartService.GetById(id);

            shoppingCart.Active = false;

            _shoppingCartService.Update(shoppingCart);

            Commit();
        }

        public ShoppingCartViewModel GetById(Int64 id)
        {
            return Mapper.Map<ShoppingCart, ShoppingCartViewModel>(_shoppingCartService.GetById(id));
        }

        public IEnumerable<ShoppingCartViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartViewModel>>(_shoppingCartService.Search(x => x.Active == true));
        }

        public IEnumerable<ShoppingCartItemViewModel> GetItemsByCartNumber(int cartNumber)
        {
            ShoppingCartViewModel shoppingCartViewModel = Mapper.Map<ShoppingCart, ShoppingCartViewModel>(_shoppingCartService.Search(x => x.Number == cartNumber)
                                                                                                                          .FirstOrDefault());

            if (shoppingCartViewModel == null)
            {
                Add(new ShoppingCartViewModel { Number = cartNumber });

                shoppingCartViewModel = Mapper.Map<ShoppingCart, ShoppingCartViewModel>(_shoppingCartService.Search(x => x.Number == cartNumber).FirstOrDefault());
            }

            IEnumerable<ShoppingCartItemViewModel> itemsViewModel =
                    Mapper.Map<IEnumerable<ShoppingCartItem>, IEnumerable<ShoppingCartItemViewModel>>(_shoppingCartItemService.Search(x => x.ShoppingCart.Number == cartNumber));

            foreach (var item in itemsViewModel)
            {
                item.Name = _productService.GetById(item.ProductId).Name;
            }

            return itemsViewModel;
        }

        public IEnumerable<ShoppingCartViewModel> GetRange(int skip, int take)
        {
            return Mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartViewModel>>(_shoppingCartService.GetRange(skip, take));
        }

        public IEnumerable<ShoppingCartViewModel> Search(Expression<Func<ShoppingCartViewModel, bool>> predicate)
        {
            var predicateShoppingCart = Mapper.Map<Expression<Func<ShoppingCartViewModel, bool>>, Expression<Func<ShoppingCart, bool>>>(predicate);
            return Mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartViewModel>>(_shoppingCartService.Search(predicateShoppingCart));
        }
        
        public void Dispose()
        {
            _shoppingCartService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
