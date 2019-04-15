using System;
using System.Linq;
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

namespace hostingRatingWebApi.Tests.Services
{
    public class BrandServiceTests
    {
        [Fact]
        public async Task get_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "get_async_is_working")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
                IBrandRepository repo = new BrandRepository(context);
                var created = await repo.CreateAsync(brand);
                var mock = new Mock<IMapper>();
               
                var mockMapper = new MapperConfiguration(cfg =>{
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Brand,BrandDTO>();
                    cfg.CreateMap<BrandPackage,BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();
                //Assert.NotNull(created);
                var service = new BrandService(repo,mapper);

                var result = await service.GetAsync(created.Id);
                Assert.Equal(brand.Id,result.Id);
            }
        }
        [Fact]
        public async Task brand_service_browse_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_service_browse_async_is_working")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
                Brand brand = new Brand(Guid.NewGuid(),"netia.pl","url");
                Brand brand2 = new Brand(Guid.NewGuid(),"netia.pl","url");
                IBrandRepository repo = new BrandRepository(context);
                var created = await repo.CreateAsync(brand);
                var created2 = await repo.CreateAsync(brand2);
                var mock = new Mock<IMapper>();
               
                var mockMapper = new MapperConfiguration(cfg =>{
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Brand,BrandDTO>();
                    cfg.CreateMap<BrandPackage,BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();
                //Assert.NotNull(created);
                var service = new BrandService(repo,mapper);

                var result = await service.BrowseAsync();
                Assert.Equal(2,result.Count());
            }
        }
        [Fact]
        public async Task brand_service_create_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_service_create_async_is_working")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
               
                IBrandRepository repo = new BrandRepository(context);

                var mockMapper = new MapperConfiguration(cfg =>{
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Brand,BrandDTO>();
                    cfg.CreateMap<BrandPackage,BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();
                //Assert.NotNull(created);
                var service = new BrandService(repo,mapper);

                var result = await service.CreateAsync(new Brand(Guid.NewGuid(),"netia.pl","url"));
                Assert.NotNull(result);
            }
        }
        [Fact]
        public async Task brand_service_delete_async_is_working()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_service_delete_async_is_working")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {
               
                IBrandRepository repo = new BrandRepository(context);

                var mockMapper = new MapperConfiguration(cfg =>{
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Brand,BrandDTO>();
                    cfg.CreateMap<BrandPackage,BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();
                //Assert.NotNull(created);
                var service = new BrandService(repo,mapper);

                var result = await service.CreateAsync(new Brand(Guid.NewGuid(),"netia.pl","url"));
                await service.DeleteAsync(result.Id);
                var deleted = await service.GetAsync(result.Id);
                Assert.Null(deleted);
            }
        }
        [Fact]
        public async Task brand_service_get_async_returns_correctType_brandDTO()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_package_service_get_async_returns_correctType_brandPackageDTO")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DatabaseContext(options))
            {

                Brand brand = new Brand(Guid.NewGuid(), "netia.pl", "url");
              
                IBrandRepository repo = new BrandRepository(context);
                await repo.CreateAsync(brand);

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

                var service = new BrandService(repo, mapper);

                var result = await service.GetAsync(x.Id);

                Assert.IsType<BrandDTO>(result);
            }
        }
        [Fact]
        public async Task brand_package_service_get_async_includes_brand_packages()
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
                var existing = await repo2.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandService(repo2, mapper);

                var result = await service.GetAsync(x.Id);

                //Assert.InRange(result.BrandPackages.Count(),1,1);
                Assert.Single(result.BrandPackages);
            }
        }
        [Fact]
        public async Task brand_service_get_async_returns_correct_brand_name()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "brand_service_get_async_returns_correctType_brandPackageDTO")
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
                var existing = await repo2.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandService(repo2, mapper);

                var result = await service.GetAsync(x.Id);

                Assert.Equal("netia.pl",result.Name);
            }
        }
        [Fact]
        public async Task brand_package_service_browse_async_includes_brand_packages()
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
                var existing = await repo2.BrowseAsync();
                var x = existing.ToList().FirstOrDefault();
                var mock = new Mock<IMapper>();

                var mockMapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Brand, BrandDTO>();
                    cfg.CreateMap<BrandPackage, BrandPackageDTO>();
                });
                var mapper = mockMapper.CreateMapper();

                var service = new BrandService(repo2, mapper);

                var results = await service.BrowseAsync();
                var firstResult = results.FirstOrDefault();

                //Assert.InRange(result.BrandPackages.Count(),1,1);
                Assert.Single(firstResult.BrandPackages);
            }
        }
    }
}