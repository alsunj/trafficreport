using App.BLL;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit.Abstractions;

namespace App.Test;

[Collection("NonParallel")]
public class ApiControllerVehicleViolationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly AppDbContext _ctx;

    private readonly IAppBLL _bll;

    private readonly IAppUnitOfWork _uow;
    // sut
    private TrafficReport.ApiControllers.VehicleViolationController _controller;
    private readonly UserManager<AppUser> _userManager;

    public ApiControllerVehicleViolationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        
        var configUow = new MapperConfiguration(cfg => 
            cfg.CreateMap<Domain.Violations.VehicleViolation, DAL.DTO.VehicleViolation>().ReverseMap());
        var mapperUow = configUow.CreateMapper();
        
        var configBll = new MapperConfiguration(cfg => 
            cfg.CreateMap<DAL.DTO.VehicleViolation, BLL.DTO.VehicleViolation>().ReverseMap());
        var mapperBll = configBll.CreateMapper();
        
        var configWeb = new MapperConfiguration(cfg => 
            cfg.CreateMap<BLL.DTO.VehicleViolation, DTO.v1_0.VehicleViolation>().ReverseMap());
        var mapperWeb = configWeb.CreateMapper();
        
        _ctx = new AppDbContext(optionsBuilder.Options);
        _uow = new AppUOW(_ctx, mapperUow);
        _bll = new AppBLL(_uow, mapperBll);

        var storeStub = Substitute.For<IUserStore<AppUser>>();
        var optionsStub = Substitute.For<IOptions<IdentityOptions>>();
        var hasherStub = Substitute.For<IPasswordHasher<AppUser>>();

        var validatorStub = Substitute.For<IEnumerable<IUserValidator<AppUser>>>();
        var passwordStup = Substitute.For<IEnumerable<IPasswordValidator<AppUser>>>();
        var lookupStub = Substitute.For<ILookupNormalizer>();
        var errorStub = Substitute.For<IdentityErrorDescriber>();
        var serviceStub = Substitute.For<IServiceProvider>();
        var loggerStub = Substitute.For<ILogger<UserManager<AppUser>>>();

        _userManager = Substitute.For<UserManager<AppUser>>(
            storeStub, 
            optionsStub, 
            hasherStub,
            validatorStub, passwordStup, lookupStub, errorStub, serviceStub, loggerStub
        );


        _controller = new TrafficReport.ApiControllers.VehicleViolationController(_bll, _userManager, mapperWeb);
        _userManager.GetUserId(_controller.User).Returns(Guid.NewGuid().ToString());
    }
    
    [Fact]
    public async Task GetTest()
    {
        var result = await _controller.GetVehicleViolations();
        var okRes = result.Result as OkObjectResult;
        var val = okRes!.Value as IEnumerable<DTO.v1_0.VehicleViolation>;
        Assert.Empty(val!);
        _testOutputHelper.WriteLine(result.ToString());

    }
}