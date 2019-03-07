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

namespace hostingRatingWebApi.Tests.Services
{
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
    }
}