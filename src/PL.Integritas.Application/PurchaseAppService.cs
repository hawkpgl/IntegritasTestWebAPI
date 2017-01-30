using AutoMapper;
using PL.Integritas.Application.Interfaces;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.CrossCutting.ExtensionMethods;
using PL.Integritas.Domain.Entities;
using PL.Integritas.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PL.Integritas.Application
{
    public class PurchaseAppService : AppService, IPurchaseAppService
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IShoppingCartService _shoppingCartService;

        public PurchaseAppService(IPurchaseService purchaseService,
                                  IShoppingCartService shoppingCartService)
        {
            _purchaseService = purchaseService;
            _shoppingCartService = shoppingCartService;
        }

        public PurchaseViewModel Add(PurchaseViewModel purchaseViewModel)
        {
            ShoppingCart shoppingCart = _shoppingCartService.Search(x => x.Number == purchaseViewModel.CartNumber).FirstOrDefault();

            if (shoppingCart == null)
            {
                throw new Exception("Shopping Cart doesn't exists.");
            }

            var purchase = Mapper.Map<PurchaseViewModel, Purchase>(purchaseViewModel);

            purchase.ShoppingCartId = shoppingCart.Id;
            
            BeginTransaction();

            _purchaseService.Add(purchase);

            Commit();

            return purchaseViewModel;
        }

        public PurchaseViewModel Update(PurchaseViewModel purchaseViewModel)
        {
            var purchase = Mapper.Map<PurchaseViewModel, Purchase>(purchaseViewModel);
            
            BeginTransaction();
            
            if (!purchase.ValidationResult.IsValid)
            {
                return purchaseViewModel;
            }

            _purchaseService.Update(purchase);

            Commit();

            return purchaseViewModel;
        }

        public void Remove(Int64 id)
        {
            BeginTransaction();

            var purchase = _purchaseService.GetById(id);

            purchase.Active = false;

            _purchaseService.Update(purchase);

            Commit();
        }

        public PurchaseViewModel GetById(Int64 id)
        {
            return Mapper.Map<Purchase, PurchaseViewModel>(_purchaseService.GetById(id));
        }

        public IEnumerable<PurchaseViewModel> GetAll()
        {
            IEnumerable<PurchaseViewModel> purchases = Mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseViewModel>>(_purchaseService.Search(x => x.Active == true));

            foreach (var purchase in purchases.ToList())
            {
                purchase.CartNumber = _shoppingCartService.GetById(purchase.ShoppingCartId).Number;
            }

            return purchases;
        }

        public IEnumerable<PurchaseViewModel> GetRange(int skip, int take)
        {
            return Mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseViewModel>>(_purchaseService.GetRange(skip, take));
        }

        public IEnumerable<PurchaseViewModel> Search(Expression<Func<PurchaseViewModel, bool>> predicate)
        {
            var predicatePurchase = Mapper.Map<Expression<Func<PurchaseViewModel, bool>>, Expression<Func<Purchase, bool>>>(predicate);
            return Mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseViewModel>>(_purchaseService.Search(predicatePurchase));
        }
        
        public void Dispose()
        {
            _purchaseService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
