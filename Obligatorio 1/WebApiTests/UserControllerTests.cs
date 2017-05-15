using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Moq;
using Services;
using WebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using Exceptions;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp.RuntimeBinder;
using System.Web;
using System.Collections.Specialized;
using System.Web.Http.Controllers;
using System.Net.Http;

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
            Assert.AreEqual("Register", createdResult.RouteName);
            Assert.AreEqual(fakeUser.Id, createdResult.RouteValues["id"]);
            Assert.AreEqual(fakeUser, createdResult.Content);
        }

        [TestMethod]
        public void RegisterMissingDataTest()
        {
            User fakeUser = new User();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new MissingUserDataException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterWrongEmailTest()
        {
            User fakeUser = new User();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new WrongEmailFormatException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterExistingUserTest()
        {
            User fakeUser = new User();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new ExistingUserException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterWrongPasswordTest()
        {
            User fakeUser = new User();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new WrongPasswordException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterWrongNumberFormatTest()
        {
            User fakeUser = new User();
            fakeUser.Address = getFakeAddress();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new WrongNumberFormatException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterWrongAddressTest()
        {
            User fakeUser = new User();
            fakeUser.Address = new Address();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new MissingAddressDataException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void RegisterNullTest()
        {
            User fakeUser = new User();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Register(fakeUser, fakeUser.Address)).Throws(new NullReferenceException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Register(fakeUser);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<User>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void LoginEmailOkTest()
        {
            dynamic parameters = new JObject();
            parameters.Email = "matigru@gmail.com";
            parameters.Password = "ASDF";
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Login("matigru@gmail.com", "ASDF")).Returns("tokenNuevo");

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(parameters);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            mockUserService.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Loggueado con éxito, el token de seguridad es tokenNuevo", createdResult.Content);
        }

        [TestMethod]
        public void LoginUsernameOkTest()
        {
            dynamic parameters = new JObject();
            parameters.Username = "matigru";
            parameters.Password = "ASDF";
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Login("matigru", "ASDF")).Returns("tokenNuevo");

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(parameters);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            mockUserService.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Loggueado con éxito, el token de seguridad es tokenNuevo", createdResult.Content);
        }

        [TestMethod]
        public void LoginWrongPasswordTest()
        {
            dynamic parameters = new JObject();
            parameters.Username = "matigru";
            parameters.Password = "ASDF";
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Login("matigru", "ASDF")).Throws(new NoLoginDataMatchException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(parameters);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void LoginNoUserTest()
        {
            dynamic parameters = new JObject();
            parameters.Username = "matigru";
            parameters.Password = "ASDF";
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Login("matigru", "ASDF")).Throws(new NotExistingUserException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(parameters);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            mockUserService.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void LoginNullTest()
        {
            var mockUserService = new Mock<IUserService>();

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(null);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void LoginNullInfoTest()
        {
            var mockUserService = new Mock<IUserService>();
            dynamic parameters = new JObject();
            parameters.Username = null;
            parameters.Password = null;
            mockUserService.Setup(service => service.Login(null, null)).Throws(new NullReferenceException());

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult obtainedResult = controller.Login(parameters);
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void LogoutTest() {
            var mockUserService = new Mock<IUserService>();
            User u = getFakeUser();
            mockUserService.Setup(service => service.GetFromToken("aheup9obyd8xnu3xsd1lnxljgx8j7vt1")).Returns(u);
            mockUserService.Setup(service => service.Logout(u.Id));

            var controller = new UsersController(mockUserService.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Token", "aheup9obyd8xnu3xsd1lnxljgx8j7vt1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult obtainedResult = controller.Logout();
            var createdResult = obtainedResult as OkNegotiatedContentResult<String>;

            mockUserService.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Desloggueado con éxito", createdResult.Content);


        }

     

    }
}
