using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.Web.Controllers;

namespace TwitterBackup.UnitTesting.Controllers.TwitterAccountControllerTesting
{
    [TestClass]
    public class DeleteAccountAction_Should
    {
        [TestMethod]
        public void CallDeleteMethodOfTwitterAccountServiceWithCorrectParams_WhenInvoked()
        {
            //Arrange
            var mockedTwitterApiService = new Mock<ITwitterApiService>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var mockedMemoryCache = new Mock<IMemoryCache>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            var mockedUser = new Mock<User>();

            var twitterAccountController = new TwitterAccountController(mockedTwitterApiService.Object, mockedTwitterAccountService.Object,
                mockedMemoryCache.Object, mockedMappingProvider.Object, mockedUserManager.Object);

            //Act
            mockedUser.Setup(x => x.Id).Returns(1);
            mockedUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(mockedUser.Object.Id.ToString());
            mockedTwitterAccountService.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            //Assert
            twitterAccountController.DeleteAccount(1);
            mockedTwitterAccountService.Verify(x => x.Delete(1, mockedUser.Object.Id), Times.Once);
        }

        [TestMethod]
        public void ReturnOk_WhenInvokedWithCorrectParams()
        {
            //Arrange
            var mockedTwitterApiService = new Mock<ITwitterApiService>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var mockedMemoryCache = new Mock<IMemoryCache>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            var mockedUser = new Mock<User>();

            var twitterAccountController = new TwitterAccountController(mockedTwitterApiService.Object, mockedTwitterAccountService.Object,
                mockedMemoryCache.Object, mockedMappingProvider.Object, mockedUserManager.Object);

            //Act
            mockedUser.Setup(x => x.Id).Returns(1);
            mockedUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(mockedUser.Object.Id.ToString());
            mockedTwitterAccountService.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            //Assert
            IActionResult result = twitterAccountController.DeleteAccount(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}