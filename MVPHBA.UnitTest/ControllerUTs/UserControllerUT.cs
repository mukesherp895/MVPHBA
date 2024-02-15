using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using MVPHBA.API.Controllers;
using MVPHBA.Common;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;

namespace MVP_HBA.UnitTest.ControllerUTs
{
    [TestClass]
    public class UserControllerUT
    {
        Mock<IUserStore<Users>> _userStoreMock;
        Mock<UserManager<Users>> _userManagerMock;
        Mock<IMapper> _mapperMock;
        Mock<SignInManager<Users>> _signInManagerMock;
        Mock<IConfiguration> _iconfigurationMock;
        public UserControllerUT()
        {
            _userStoreMock = new Mock<IUserStore<Users>>();
            _userStoreMock.Setup(x => x.FindByIdAsync("mukesherp895@gmail.com", CancellationToken.None)).ReturnsAsync(new Users()
            {
                UserName = "mukesherp895@gmail.com",
                Id = "c0187aa4-6912-430b-8213-c349ea53406f"
            });
            _userManagerMock = new Mock<UserManager<Users>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();
            _signInManagerMock = new Mock<SignInManager<Users>>(_userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<Users>>(), null, null, null, null);
            _iconfigurationMock = new Mock<IConfiguration>();
        }
        [TestMethod]
        public async Task RegisterTM()
        {
            UserController userController = new UserController(_userManagerMock.Object, _mapperMock.Object, _signInManagerMock.Object, _iconfigurationMock.Object);
            UserRegisterReqVM dto = new UserRegisterReqVM
            {
                FullName = "Unit Test",
                Email = "unittest@ut.com",
                PhoneNumber = "9845285895",
                Password = "UnitTest@123",
                ConfirmPassword = "UnitTest@123",
                UserType = UserTypes.Broker
            };
            var result = await userController.Register(dto);
            Assert.IsTrue(result is OkObjectResult);
            var okObjResult = result as OkObjectResult;
            var resp = okObjResult.Value as ResponseVM;
            Assert.IsTrue(resp.Status == "99" || resp.Status == "00");
        }
        [TestMethod]
        public async Task AuthenticationTM()
        {
            UserController userController = new UserController(_userManagerMock.Object, _mapperMock.Object, _signInManagerMock.Object, _iconfigurationMock.Object);
            UserAuthReqVM dto = new UserAuthReqVM
            {
                UserName = "mukesherp895@gmail.com",
                Password = "Mukesh@895",
                UserType = UserTypes.Broker
            };
            var result = await userController.Authentication(dto);
            Assert.IsTrue(result is OkObjectResult);
            var okObjResult = result as OkObjectResult;
            var resp = okObjResult.Value as ResponseVM;
            Assert.IsTrue(resp.Status == "99" || resp.Status == "00");
        }
    }
}