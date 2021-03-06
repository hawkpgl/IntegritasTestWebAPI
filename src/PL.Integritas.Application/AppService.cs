﻿using Microsoft.Practices.ServiceLocation;
using PL.Integritas.Infra.Data.Interfaces;

namespace PL.Integritas.Application
{
    public class AppService
    {
        private IUnityOfWork _uow;

        public void BeginTransaction()
        {
            _uow = ServiceLocator.Current.GetInstance<IUnityOfWork>();
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.SaveChanges();
        }
    }
}
