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
    public class ProductAppService : AppService, IProductAppService
    {
        private readonly IProductService _productService;

        public ProductAppService(IProductService productService)
        {
            _productService = productService;
        }

        public ProductViewModel Add(ProductViewModel productViewModel)
        {
           var product = Mapper.Map<ProductViewModel, Product>(productViewModel);

            BeginTransaction();
            
            if (!product.ValidationResult.IsValid)
            {
                return productViewModel;
            }

            _productService.Add(product);

            Commit();

            return productViewModel;
        }

        public ProductViewModel Update(ProductViewModel productViewModel)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            
            BeginTransaction();
            
            if (!product.ValidationResult.IsValid)
            {
                return productViewModel;
            }

            _productService.Update(product);

            Commit();

            return productViewModel;
        }

        public void Remove(Int64 id)
        {
            BeginTransaction();

            var product = _productService.GetById(id);

            product.Active = false;

            _productService.Update(product);

            Commit();
        }

        public ProductViewModel GetById(Int64 id)
        {
            return Mapper.Map<Product, ProductViewModel>(_productService.GetById(id));
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.Search(x => x.Active == true));
        }

        public IEnumerable<ProductViewModel> GetRange(int skip, int take)
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.GetRange(skip, take));
        }

        public IEnumerable<ProductViewModel> Search(Expression<Func<ProductViewModel, bool>> predicate)
        {
            var predicateProduct = Mapper.Map<Expression<Func<ProductViewModel, bool>>, Expression<Func<Product, bool>>>(predicate);
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.Search(predicateProduct));
        }
        
        public void Dispose()
        {
            _productService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
