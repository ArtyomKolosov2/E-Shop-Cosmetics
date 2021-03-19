using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Tests.RepositoriesTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace E_Shop_Cosmetic.Tests.RepositoriesTests
{
    public class ProductRepositoryTest : IDisposable
    {
        private readonly ProductRepositoryFixture _fixture;

        public ProductRepositoryTest()
        {
            // Arrange
            _fixture = new ProductRepositoryFixture();
        }

        [Fact]
        public async Task GetAll_Ok()
        {
            // Act
            var result = await _fixture.Repository.GetAll();

            // Assert

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public Task Create_Fail()
        {
            // Act, Arrange

            return Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Add(null));
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            const string createdName = "CreatedProduct";
            var createdEntity = new Product
            {
                Name = createdName,
                Price = 200,
                CategoryId = 1
            };

            // Act
            var result = await _fixture.Repository.Add(createdEntity);

            await _fixture.Context.SaveChangesAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(createdName, result.Name);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal(200, result.Price);
        }

        [Fact]
        public async Task Update_Ok()
        {
            // Arrange
            const string updatedName = "UpdatedProduct";
            var updatedEntity = await _fixture.Context.Products.FindAsync(_fixture.UpdateId);
            updatedEntity.Name = updatedName;

            // Act

            var result = await _fixture.Repository.Update(updatedEntity);
            await _fixture.Context.SaveChangesAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(updatedName, updatedEntity.Name);
            Assert.Equal(_fixture.UpdateId, updatedEntity.Id);

        }

        [Fact]
        public Task Update_IncorrectData_Fail()
        {
            // Act, Arrange
            return Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Update(null));
        }
        
        [Fact]
        public async Task GetById_Ok()
        {
            // Act
            var result = await _fixture.Repository.GetById(_fixture.UpdateId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestProduct2", result.Name); 
            Assert.Equal(_fixture.UpdateId, result.Id); 
        }

        [Fact]
        public async Task GetById_IncorrectId_Fail()
        {
            // Act
            var result = await _fixture.Repository.GetById(int.MaxValue);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            var deletedEntity = await _fixture.Context.Products.FindAsync(_fixture.DeleteId);

            // Act
            await _fixture.Repository.Delete(deletedEntity);

            // Assert

            Assert.Null(await _fixture.Context.Products.FindAsync(_fixture.DeleteId));
        }

        [Fact]
        public Task Delete_IncorrectData_Fail()
        {
            // Act, Assert

            return Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Delete(null));
        }

        #region Disposable

        public void Dispose()
        {
            _fixture.Dispose();
        }

        #endregion Disposable
    }
}
