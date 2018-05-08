using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Web.Areas.Administration.Controllers;

namespace TwitterBackup.UnitTesting.Controllers.AdminControllerTesting
{
    [TestClass]
    public class SavedTwittersAction_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenNullAsParameterIsPassed()
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
                adminController.SavedTwitters(null);
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RedirectToListAllAccountsAction_WhenInvokedWithCorectParameters()
        {
            //Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedTwitterAccountService = new Mock<ITwitterAccountService>();
            var userStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            var mockedUser = new Mock<User>();

            var adminController = new AdminController(mockedMappingProvider.Object, mockedTwitterAccountService.Object, mockedUserManager.Object);

            //Act
            IActionResult result = adminController.SavedTwitters("Valid data!");
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}