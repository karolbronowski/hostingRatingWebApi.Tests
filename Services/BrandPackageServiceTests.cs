using System;
using System.Threading.Tasks;
using AutoMapper;
using hostingRatingWebApi.Commands;
using hostingRatingWebApi.Database;
using hostingRatingWebApi.DTO;
using hostingRatingWebApi.Models;
using hostingRatingWebApi.Repositories;
using hostingRatingWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Linq;

namespace hostingRatingWebApi.Tests.Services
{
    //Tests for BrandPackageService
    public class BrandPackageServiceTests
    {
        [Fact]
        public async Task brand_package_service_get_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_package_service_get_async_is_working")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
                BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(),new CreateBrandPackage(brand.Id,"Test","50","50","1","2","3","4",1,10));
                
                
                IBrandRepository repo2 = new BrandRepository(context);
                await repo2.CreateAsync(brand);

                IBrandPackageRepository repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);

                var mock = new Mock<IMapper>();
               
                var mockMapper = new MapperConfiguration(cfg =>{
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Brand,BrandDTO>();
                    cfg.CreateMap<BrandPackage,BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandPackageService(repo,mapper);

                var result = await service.GetAsync(brandPackage.Id);

                Assert.Equal(brandPackage.Id,result.Id);
            }
        }
        [Fact]
        public async Task brand_package_service_get_async_returns_null_if_not_exists()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_package_service_get_async_returns_null_if_not_exists")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                var randomGuid = Guid.NewGuid();

                Brand brand = new Brand(Guid.NewGuid(), "netia.pl", "url");
                BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(), new CreateBrandPackage(brand.Id, "Test", "50", "50", "1", "2", "3", "4", 1, 10));


                IBrandRepository repo2 = new BrandRepository(context);
                await repo2.CreateAsync(brand);

                IBrandPackageRepository repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);

                //make sure that one entity existing in table brandpackages
                var existing = await repo.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandPackageService(repo, mapper);

                var result = await service.GetAsync(randomGuid);

                Assert.Null(result);
            }
        }
        [Fact]
        public async Task brand_package_service_get_async_returns_correctType_brandPackageDTO()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_package_service_get_async_returns_correctType_brandPackageDTO")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {

                Brand brand = new Brand(Guid.NewGuid(), "netia.pl", "url");
                BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(), new CreateBrandPackage(brand.Id, "Test", "50", "50", "1", "2", "3", "4", 1, 10));


                IBrandRepository repo2 = new BrandRepository(context);
                await repo2.CreateAsync(brand);

                IBrandPackageRepository repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);

                //make sure that one entity existing in table brandpackages
                var existing = await repo.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandPackageService(repo, mapper);

                var result = await service.GetAsync(x.Id);

                Assert.IsType<BrandPackageDTO>(result);
            }
        }
        [Fact]
        public async Task brand_package_service_get_async_returns_correct_brand_package_name()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_package_service_get_async_returns_correct_brand_package_name")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {

                Brand brand = new Brand(Guid.NewGuid(), "netia.pl", "url");
                BrandPackage brandPackage = new BrandPackage(Guid.NewGuid(), new CreateBrandPackage(brand.Id, "Test", "50", "50", "1", "2", "3", "4", 1, 10));


                IBrandRepository repo2 = new BrandRepository(context);
                await repo2.CreateAsync(brand);

                IBrandPackageRepository repo = new BrandPackageRepository(context);
                await repo.CreateAsync(brandPackage);

                //make sure that one entity existing in table brandpackages
                var existing = await repo.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandPackageService(repo, mapper);

                var result = await service.GetAsync(x.Id);

                Assert.Equal("Test", result.PackageName);
            }
        }
    }
}