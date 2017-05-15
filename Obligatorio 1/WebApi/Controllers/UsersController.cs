using Entities;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Exceptions;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    /*
     private IProductRepository _repository;

    public ProductsController(IProductRepository repository)  
    {
        _repository = repository;
    }
         */
     
    public class UsersController : ApiController
    {
        private IUserService _userService;

        public UsersController(IUserService service) {
            _userService = service;
        }

        [Route("api/Users/Register", Name = "Register")]
        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            try
            {
                _userService.Register(user, user.Address);
                return CreatedAtRoute("Register", new { id = user.Id }, user);
            }
            catch (MissingUserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExistingUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WrongPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WrongNumberFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WrongEmailFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MissingAddressDataException ex) {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex) {
                return BadRequest("Debes enviar todos los datos");
            }
        }

        [Route("api/Users/Login", Name = "Login")]
        [HttpPost]
        public IHttpActionResult Login(JObject parameters)
        {
            string identifier = null;
            dynamic json = parameters;
            string password = json.Password;
            if (json.Email != null)
                identifier = json.Email;
            else if (json.Username != null)
                identifier = json.Username;

            string token = _userService.Login(identifier, password);
            return Ok("Loggueado con éxito, el token de seguridad es " + token);
        }
    }
}
