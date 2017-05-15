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
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Address> _addressRepository;

        private IUserService _userService;
        public UsersController(IGenericRepository<User> userRepo,IGenericRepository<Address> addressRepo) {
            _userRepository = userRepo;
            _addressRepository = addressRepo;
            _userService = new UserService(_userRepository, _addressRepository);
        }

        [Route("api/Users/Register", Name = "Register")]
        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            try
            {
                if (user != null)
                {
                    _userService.Register(user, user.Address);
                    return Ok("Usuario Registrado con éxito");
                }
                return BadRequest("Debes enviar todos los datos");

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
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        /*
          [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/Users/Register", Name="Register")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = userService.CreateUser(user);
            return Ok(user);
        }
         */
        /*
         public IEnumerable<Product> Get()
    {
        return _repository.GetAll();
    }

    public IHttpActionResult Get(int id)
    {
        var product = _repository.GetByID(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
         */
    }
}
