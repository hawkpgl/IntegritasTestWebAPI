﻿using Microsoft.Practices.ServiceLocation;
using PL.Integritas.Domain.Interfaces.Repositories;
using PL.Integritas.Infra.Data.Context;
using PL.Integritas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PL.Integritas.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IntegritasContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository()
        {
            var contextManager = ServiceLocator.Current.GetInstance<IContextManager>();
            Db = contextManager.GetContext();
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
        }

        public virtual void Remove(Int64 id)
        {
            DbSet.Remove(GetById(id));
        }

        public virtual TEntity GetById(Int64 id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public IEnumerable<TEntity> GetRange(int skip, int take)
        {
            return DbSet.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
