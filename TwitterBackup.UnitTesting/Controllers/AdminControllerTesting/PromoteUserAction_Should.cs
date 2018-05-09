using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Web.Areas.Administration.Controllers;

namespace TwitterBackup.UnitTesting.Controllers.AdminControllerTesting
{
    [TestClass]
    public class PromoteUserAction_Should
    {
        [TestMethod]
        public async Task ThrowArgumentException_WhenNullAsParameterIsPassed()
        {
            //Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);

            var adminController = new AdminController(mockedMappingProvider.Object, mockedTwitterAccountService.Object, mockedUserManager.Object);

            //Act & Assert
            try
            {
                await adminController.PromoteUser(null);
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task ThrowArgumentException_WhenUserWithThatIdDoesNotExists()
        {
            //Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);

            var adminController = new AdminController(mockedMappingProvider.Object, mockedTwitterAccountService.Object, mockedUserManager.Object);

            //Act & Assert
            try
            {
                await adminController.PromoteUser("Not valid user id!");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task RedirectToShowAllUsersAction_WhenInvokedWithCorectParameters()
        {
            //Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            var mockedUser = new Mock<User>();

            var adminController = new AdminController(mockedMappingProvider.Object, mockedTwitterAccountService.Object, mockedUserManager.Object);

            //Act
            mockedUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(mockedUser.Object));
            IActionResult result = await adminController.PromoteUser("Valid data!");
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}