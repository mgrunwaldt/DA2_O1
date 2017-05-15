using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Moq;
using Services;
using WebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApiTests
{
    [TestClass]
    public class UserControllerTests
    {
        private User getFakeUser() {
            User u = new User();
            u.FirstName = "Luis";
            u.LastName = "Suarez";
            u.Password = "contra1234";
            u.PhoneNumber = "094555000";
            u.Username = "luchoTricolor";
            u.Email = "suarez@barcelona.com";
            return u;
        }

        private Address getFakeAddress() {
            Address a = new Address();
            a.Street = "Rambla";
            a.StreetNumber = "1256";
            a.PhoneNumber = "2555666777";
            return a;
        }


        [TestMethod]
        public void RegisterOkTest()
        {
            User fakeUser = getFakeUser();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser,fakeUser.Address));

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("api", createdResult.RouteName);
            Assert.AreEqual(fakeUser.Id, createdResult.RouteValues["id"]);
            Assert.AreEqual(fakeUser, createdResult.Content);

        }
        /*
         [TestMethod]
public void CreateNewBreedTest()
{
    //Arrange
    var fakeBreed = GetAFakeBreed();

    var mockBreedsBusinessLogic = new Mock<IBreedsBusinessLogic>();
    mockBreedsBusinessLogic
        .Setup(bl => bl.Add(fakeBreed))
        .Returns(fakeBreed.Id);

    var controller = new BreedsController(mockBreedsBusinessLogic.Object);

    //Act
    IHttpActionResult obtainedResult = controller.Post(fakeBreed);
    var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<Breed>;

    //Assert
    mockBreedsBusinessLogic.VerifyAll();
    Assert.IsNotNull(createdResult);
    Assert.AreEqual("DefaultApi", createdResult.RouteName);
    Assert.AreEqual(fakeBreed.Id, createdResult.RouteValues["id"]);
    Assert.AreEqual(fakeBreed, createdResult.Content);
}
         */
    }
}
