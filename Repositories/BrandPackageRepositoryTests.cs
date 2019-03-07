using System;
using System.Linq;
using System.Threading.Tasks;
using hostingRatingWebApi.Commands;
using hostingRatingWebApi.Database;
using hostingRatingWebApi.Models;
using hostingRatingWebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace hostingRatingWebApi.Tests.Repositories
{
    public class BrandPackageRepositoryTests
    {
        [Fact]
        public async Task create_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "create_async_is_working")
                .Options;
            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandPackageRepository(context);
                var repo2 = new BrandRepository(context);

                await repo2.CreateAsync(brand);
                await repo.CreateAsync(brandPackage);
                var result = await repo.GetAsync(brandPackage.Id);
                //Assert
                Assert.Equal(result.Id,brandPackage.Id);
            }
        }
        [Fact]
        public async Task delete_async_is_working()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "delete_async_is_working")
                .Options;

          Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
        BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
           
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);

                await repo.DeleteAsync(brandPackage);
                var result = await repo.GetAsync(brandPackage.Id);
                //Assert
                Assert.Null(result);
            }
        }
        [Fact]
        public async Task browse_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "browse_async_is_working")
                .Options;

            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
            BrandPackage brandPackage2 = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                
                var repo2 = new BrandRepository(context);

                await repo2.CreateAsync(brand);

                var repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);
                await repo.CreateAsync(brandPackage2);

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
                .UseInMemoryDatabase(databaseName: "get_async_is_working")
                .Options;
            Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
            BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
            //Act
            // Use a clean instance of the context to run the test
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandPackageRepository(context);
                var repo2 = new BrandRepository(context);

                await repo2.CreateAsync(brand);
                
                await repo.CreateAsync(brandPackage);
                var result = await repo.GetAsync(brandPackage.Id);

                //Assert
                Assert.Equal(result.Id,brandPackage.Id);
            }
        }
        [Fact]
        public async Task get_async_return_null_if_not_found()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "get_async_return_null_if_not_found")
                .Options;

            //Act
            // Use a clean instance of the context to run the test
            using (var context = new DatabaseContext(options))
            {
                var repo = new BrandPackageRepository(context);
               
                // Action act = async () => await repo.GetAsync(brandPackage.Id);
                var result = await repo.GetAsync(Guid.NewGuid());
                //Assert
                Assert.Null(result);
            }
        }
    }
}