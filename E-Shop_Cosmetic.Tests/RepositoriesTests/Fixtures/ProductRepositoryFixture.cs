using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Repository;
using E_Shop_Cosmetic.Tests.RepositoriesTests.Fixtures.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Tests.RepositoriesTests.Fixtures
{
    public class ProductRepositoryFixture : BaseRepositoryFixture<IProductsRepository>
    {
        public static int Price => 100;
        public static string Name => "TestProduct1";
        public int DeleteId { get; private set; }
        public int UpdateId { get; private set; }

        protected override IProductsRepository CreateRepository() => new ProductRepository(Context);

        protected override void InitDatabase()
        {
            DeleteId = Context.Products.Add(new Product
            {
                Name = "TestProduct1",
                Price = 200,
                CategoryId = 1
            })
            .Entity.Id;

            UpdateId = Context.Products.Add(new Product
            {
                Name = "TestProduct2",
                Price = 100,
                CategoryId = 2
            })
            .Entity.Id;

            Context.SaveChanges();
        }
    }
}
