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
using Microsoft.CSharp.RuntimeBinder;

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
            try
            {
                string identifier = null;
                dynamic json = parameters;
                string password = json.Password;
                if (json.Email != null)
                    identifier = json.Email;
                else if (json.Username != null)
                    identifier = json.Username;

                string uppercasePass = password.ToUpper();

                string token = _userService.Login(identifier, uppercasePass);
                return Ok("Loggueado con éxito, el token de seguridad es " + token);
            }
            
            catch (NotExistingUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoLoginDataMatchException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }
            catch (RuntimeBinderException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }

        }
        [Route("api/Users/Logout", Name = "Logout")]
        [HttpPost]
        public IHttpActionResult Logout()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Token"))
            {
                string token = headers.GetValues("Token").First();
                User u = _userService.GetFromToken(token);
                _userService.Logout(u.Id);
                return Ok("Desloggueado con éxito");

            }
            return BadRequest("Debes mandar un el Token de sesión en los headers");
        }
    }
}
