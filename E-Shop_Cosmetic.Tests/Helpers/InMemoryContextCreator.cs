using System;
using System.Collections.Generic;
using System.Text;
using E_Shop_Cosmetic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace E_Shop_Cosmetic.Tests.Helpers
{
    public static class InMemoryContextCreator
    {
        public static AppDBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: $"Test {Guid.NewGuid()}")
                .Options;

            return new AppDBContext(options);
        }
    }
}
