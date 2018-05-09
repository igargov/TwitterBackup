using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.Web.Controllers;

namespace TwitterBackup.UnitTests.Controllers.TwitterAccountControllerTesting
{
    [TestClass]
    public class ListAllAccountsAction_Should
    {
        [TestMethod]
        public void GetCurrentUserId_WhenNullAsParameterIsPassed()
        {
            //Arrange
            var mockedTwitterApiService = new Mock<ITwitterApiService>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var mockedMemoryCache = new Mock<IMemoryCache>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            var mockedUser = new Mock<User>();
            var fakeTwitterAccountViewModels = new List<TwitterAccountViewModel>();

            var twitterAccountController = new TwitterAccountController(mockedTwitterApiService.Object, mockedTwitterAccountService.Object,
                mockedMemoryCache.Object, mockedMappingProvider.Object, mockedUserManager.Object);

            //Act
            mockedUser.Setup(x => x.Id).Returns(1);
            mockedUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(mockedUser.Object.Id.ToString());
            mockedTwitterAccountService.Setup(x => x.GetAll(mockedUser.Object.Id)).Returns(fakeTwitterAccountViewModels);
            twitterAccountController.ListAllAccounts();

            //Assert
            mockedTwitterAccountService.Verify(x => x.GetAll(mockedUser.Object.Id), Times.Once);
        }
    }
}