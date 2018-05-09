using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Web.Areas.Administration.Controllers;

namespace TwitterBackup.UnitTests.Controllers.AdminControllerTesting
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public void ReturnIndexAsViewResult_WhenInvoked()
        {
            //Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);

            var adminController = new AdminController(mockedMappingProvider.Object, mockedTwitterAccountService.Object, mockedUserManager.Object);

            //Act
            var result = adminController.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}