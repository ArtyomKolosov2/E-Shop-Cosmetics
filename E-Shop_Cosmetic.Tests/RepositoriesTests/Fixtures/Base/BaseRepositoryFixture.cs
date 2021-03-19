using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Tests.RepositoriesTests.Fixtures.Base
{
    public abstract class BaseRepositoryFixture<TRepository> : IDisposable
    {
        private TRepository _repository;
        public AppDBContext Context { get; }
        public TRepository Repository => _repository ??= CreateRepository();

        protected BaseRepositoryFixture()
        {
            Context = InMemoryContextCreator.CreateContext();

            InitDatabase();
        }

        protected abstract void InitDatabase();

        protected abstract TRepository CreateRepository();


        #region Disposable
        public void Dispose()
        {
            Context?.Dispose();
        }

        #endregion Disposable
    }
}
