using AutoMapper;
using PL.Integritas.Application.Interfaces;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.Domain.Entities;
using PL.Integritas.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PL.Integritas.Application
{
    public class ShoppingCartItemAppService : AppService, IShoppingCartItemAppService
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartItemAppService(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
        }

        public ShoppingCartItemViewModel Add(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
           var shoppingCartItem = Mapper.Map<ShoppingCartItemViewModel, ShoppingCartItem>(shoppingCartItemViewModel);

            BeginTransaction();
            
            if (!shoppingCartItem.ValidationResult.IsValid)
            {
                return shoppingCartItemViewModel;
            }

            _shoppingCartItemService.Add(shoppingCartItem);

            Commit();

            return shoppingCartItemViewModel;
        }

        public ShoppingCartItemViewModel Update(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            var shoppingCartItem = Mapper.Map<ShoppingCartItemViewModel, ShoppingCartItem>(shoppingCartItemViewModel);
            
            BeginTransaction();
            
            if (!shoppingCartItem.ValidationResult.IsValid)
            {
                return shoppingCartItemViewModel;
            }

            _shoppingCartItemService.Update(shoppingCartItem);

            Commit();

            return shoppingCartItemViewModel;
        }

        public void Remove(Int64 id)
        {
            BeginTransaction();

            var shoppingCartItem = _shoppingCartItemService.GetById(id);

            shoppingCartItem.Active = false;

            _shoppingCartItemService.Update(shoppingCartItem);

            Commit();
        }

        public ShoppingCartItemViewModel GetById(Int64 id)
        {
            return Mapper.Map<ShoppingCartItem, ShoppingCartItemViewModel>(_shoppingCartItemService.GetById(id));
        }

        public IEnumerable<ShoppingCartItemViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<ShoppingCartItem>, IEnumerable<ShoppingCartItemViewModel>>(_shoppingCartItemService.Search(x => x.Active == true));
        }

        public IEnumerable<ShoppingCartItemViewModel> GetRange(int skip, int take)
        {
            return Mapper.Map<IEnumerable<ShoppingCartItem>, IEnumerable<ShoppingCartItemViewModel>>(_shoppingCartItemService.GetRange(skip, take));
        }

        public IEnumerable<ShoppingCartItemViewModel> Search(Expression<Func<ShoppingCartItemViewModel, bool>> predicate)
        {
            var predicateShoppingCartItem = Mapper.Map<Expression<Func<ShoppingCartItemViewModel, bool>>, Expression<Func<ShoppingCartItem, bool>>>(predicate);
            return Mapper.Map<IEnumerable<ShoppingCartItem>, IEnumerable<ShoppingCartItemViewModel>>(_shoppingCartItemService.Search(predicateShoppingCartItem));
        }
        
        public void Dispose()
        {
            _shoppingCartItemService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
