using System;
using System.Linq;
using System.Threading.Tasks;
using hostingRatingWebApi.Database;
using hostingRatingWebApi.Models;
using hostingRatingWebApi.Repositories;
using hostingRatingWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace hostingRatingWebApi.Tests.Repositories
{
    public class BrandRepositoryTests
    {
        [Fact]
        public async Task create_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandRepository(context);
                await repo.CreateAsync(brand);
                var result = await repo.GetAsync(brand.Id);
                Assert.Equal(result.Id,brand.Id);
            }
        }
        [Fact]
        public async Task delete_async_is_working()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
           
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandRepository(context);
                await repo.CreateAsync(brand);

                await repo.DeleteAsync(brand);
                var result = await repo.GetAsync(brand.Id);
                //Assert
                Assert.Null(result);
            }
        }
        [Fact]
        public async Task browse_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            Brand brand2 = new Brand(Guid.NewGuid(),"home.pl","url");
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandRepository(context);
                await repo.CreateAsync(brand);
                await repo.CreateAsync(brand2);
                var result = await repo.BrowseAsync();
                //Assert
                Assert.Equal(2,result.Count());
            }
        }
        [Fact]
        public async Task get_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            //Act
            // Use a clean instance of the context to run the test
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandRepository(context);
                await repo.CreateAsync(brand);
                var result = await repo.GetAsync(brand.Id);

                //Assert
                Assert.Equal(result.Id,brand.Id);
            }
        }
        [Fact]
        public async Task  get_async_return_null_if_not_found()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            //Act
            // Use a clean instance of the context to run the test
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandRepository(context);
               
                // Action act = async () => await repo.GetAsync(brand.Id);
                var result = await repo.GetAsync(brand.Id);
                //Assert
                Assert.Null(result);
            }
        }
    }
}