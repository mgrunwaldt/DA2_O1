using Entities;
using Entities.Statuses_And_Roles;
using Exceptions;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OrderController : ApiController
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public OrderController(IOrderService service, IUserService userService)
        {
            _orderService = service;
            _userService = userService;
        }

        [Route("api/Order/AddProduct", Name = "AddProduct")]
        [HttpPost]
        public IHttpActionResult AddProduct(JObject parameters)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    dynamic json = parameters;
                    string productIdString = json.ProductId;
                    if (productIdString != null)
                    {
                        Guid productId = new Guid(productIdString);
                        string quantityString = json.Quantity;
                        int quantity = 1;
                        if (quantityString != null) {
                            int intVal;
                            if (Int32.TryParse(quantityString, out intVal))
                                quantity = intVal;
                        }
                        _orderService.AddProduct(loggedUser, productId, quantity);
                        return Ok("Se agregó al producto " + productIdString + " a la órden activa del usuario " + loggedUser.Id);
                    }
                    return BadRequest("Debes enviar un Id de Producto");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest("Por favor verifique que los ids tienen 32 caracteres");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (RuntimeBinderException ex)
            {
                return BadRequest("Debes enviar algún dato");
            }
            catch (WrongProductQuantityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InactiveProductException ex)
            {
                return BadRequest(ex.Message);
            }
            
            catch (NotExistingProductException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Order/DeleteProduct", Name = "DeleteProduct")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(JObject parameters)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    dynamic json = parameters;
                    string productIdString = json.ProductId;
                    if (productIdString != null)
                    {
                        Guid productId = new Guid(productIdString);
                        _orderService.DeleteProduct(loggedUser.Id, productId);
                       
                        return Ok("Se eliminó al producto " + productIdString + " de la órden activa del usuario " + loggedUser.Id);
                    }
                    return BadRequest("Debes enviar un Id de Producto");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest("Por favor verifique que los ids tienen 32 caracteres");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (RuntimeBinderException ex)
            {
                return BadRequest("Debes enviar algún dato");
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingProductException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NotExistingProductInOrderException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/Order/ChangeProductQuantity", Name = "ChangeProductQuantity")]
        [HttpPut]
        public IHttpActionResult ChangeProductQuantity(JObject parameters)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    dynamic json = parameters;
                    string productIdString = json.ProductId;
                    if (productIdString != null)
                    {
                        Guid productId = new Guid(productIdString);
                        string quantityString = json.Quantity;
                        int quantity = 1;
                        if (quantityString != null)
                        {
                            int intVal;
                            if (Int32.TryParse(quantityString, out intVal)) {
                                quantity = intVal;
                                _orderService.ChangeProductQuantity(loggedUser, productId, quantity);
                                return Ok("Se cambió la cantidad del producto " + productIdString + " de la órden activa del usuario " + loggedUser.Id+" a "+quantity);
                            }
                            return BadRequest("La cantidad especificada debe ser un número");
                                
                        }
                        return BadRequest("Debes especificar una cantidad");
                    }
                    return BadRequest("Debes enviar un Id de Producto");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest("Por favor verifique que los ids tienen 32 caracteres");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (RuntimeBinderException ex)
            {
                return BadRequest("Debes enviar algún dato");
            }
            catch (WrongProductQuantityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NotExistingProductInOrderException ex)
            {
                return BadRequest(ex.Message);
            }
        }
     
        [Route("api/Order/GetAllProductsFromActive", Name = "GetAllProductsFromActive")]
        [HttpGet]
        public IHttpActionResult GetAllProductsFromActive()
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    List<Product> orderProducts = _orderService.ViewAllProductsFromOrder(loggedUser);
                    return Ok(orderProducts);
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Order/GetAllProducts/{id}", Name = "GetAllProducts")]
        [HttpGet]
        public IHttpActionResult GetAllProducts(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    List<Product> orderProducts = _orderService.ViewAllProductsFromOrder(loggedUser,id);
                    return Ok(orderProducts);
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/Order/SetAddress/{id}", Name = "SetAddress")]
        [HttpPut]
        public IHttpActionResult SetAddress(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    _orderService.SetAddress(loggedUser, id);
                    return Ok("Se le agregó una dirección a la orden del usuario " + loggedUser.Id + ", por lo que esta orden ahora espera envío");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserDoesntHaveAddressException ex) {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (NotExistingProductInOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/Order/Ship/{id}", Name = "Ship")]
        [HttpPut]
        public IHttpActionResult Ship(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    if (loggedUser.Role == UserRoles.ADMIN || loggedUser.Role == UserRoles.SUPERADMIN) {
                        _orderService.Ship(id);
                        return Ok("Se envió la órden "+id);
                    }
                    return BadRequest("Solo los administradores pueden enviar una orden");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (NotExistingOrderWithCorrectStatusException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [Route("api/Order/ReceivePayment/{id}", Name = "ReceivePayment")]
        [HttpPut]
        public IHttpActionResult ReceivePayment(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    if (loggedUser.Role == UserRoles.ADMIN || loggedUser.Role == UserRoles.SUPERADMIN)
                    {
                        _orderService.Pay(id);
                        return Ok("Se pagó la órden " + id);
                    }
                    return BadRequest("Solo los administradores pueden dar una orden como paga");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (NotExistingOrderWithCorrectStatusException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Order/Cancel/{id}", Name = "Cancel")]
        [HttpPut]
        public IHttpActionResult Cancel(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    _orderService.Cancel(loggedUser, id);
                    return Ok("Se canceló la órden " + id);
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (IncorrectOrderStatusException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/Order/GetCategoryStatistics", Name = "GetCategoryStatistics")]
        [HttpGet]
        public IHttpActionResult GetCategoryStatistics(JObject parameters)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    if (loggedUser.Role == UserRoles.ADMIN || loggedUser.Role == UserRoles.SUPERADMIN) {
                        DateTime now = DateTime.Now;
                        DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        DateTime from = firstDayOfMonth;
                        DateTime to = lastDayOfMonth;
                        if (parameters != null)
                        {
                            dynamic json = parameters;
                            if (json.StartDate != null && json.EndDate != null) {
                                DateTime fromParam;
                                if (DateTime.TryParse(json.StartDate, out fromParam))
                                {
                                    DateTime toParam;
                                    if (DateTime.TryParse(json.EndDate, out toParam))
                                    {
                                        from = fromParam;
                                        to = toParam;
                                    }
                                }
                            }
                        }
                        List<string> details = _orderService.GetCategoryStatistics(from, to);
                        return Ok(details);
                    }
                    return BadRequest("Solo los administradores pueden ver las estadísticas por categoría");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
