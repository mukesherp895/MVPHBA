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
using MVPHBA.Service.Interfaces;

namespace MVPHBA.UnitTest.ControllerUTs
{
    [TestClass]
    public class PropertyControllerUT
    {
        Mock<IUserStore<Users>> _userStoreMock;
        Mock<UserManager<Users>> _userManagerMock;
        Mock<IMapper> _mapperMock;
        Mock<SignInManager<Users>> _signInManagerMock;
        Mock<IConfiguration> _iconfigurationMock;
        Mock<IPropertyInfoService> _propertyInfoMock;
        public PropertyControllerUT()
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
            _propertyInfoMock = new Mock<IPropertyInfoService>();

        }
        [TestMethod]
        public async Task PropertyListGetTM()
        {
            PropertyInfoController propertyInfoController = new PropertyInfoController(_propertyInfoMock.Object, _userManagerMock.Object, _mapperMock.Object);
            var result = await propertyInfoController.PropertyListGet(0, 100, "asc", 0, "", 0, "");
            Assert.IsTrue(result is OkObjectResult);
            var okObjResult = result as OkObjectResult;
            var resp = okObjResult.Value as ResponseVM;
            Assert.IsTrue(resp.Status == "99" || resp.Status == "00");
        }
    }
}
