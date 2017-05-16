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
using Tools;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IUserService _userService;

        public ProductController(IProductService service, IUserService userService)
        {
            _productService = service;
            _userService = userService;
        }

        public IHttpActionResult Post([FromBody] Product product)
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
                        _productService.Add(product);
                        return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);

                    }
                    return BadRequest("Solo los administradores pueden agregar productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductMissingDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductWrongPriceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductDuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingCategoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }
        }

        public IHttpActionResult Put(Guid id, [FromBody]Product product)
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
                        Product existing = _productService.Get(id);
                        existing.Category = product.Category;
                        existing.Code = product.Code;
                        existing.Description = product.Description;
                        existing.Manufacturer = product.Manufacturer;
                        existing.Name = product.Name;
                        existing.Price = product.Price;
                        _productService.Modify(existing);
                        return CreatedAtRoute("DefaultApi", new { id = existing.Id }, existing);

                    }
                    return BadRequest("Solo los administradores pueden modificar productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductMissingDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductWrongPriceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductDuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingCategoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }
        }

        public IHttpActionResult Delete(Guid id)
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
                        _productService.Delete(id);
                        return Ok("Se quitó el producto " + id);

                    }
                    return BadRequest("Solo los administradores pueden eliminar productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Product/ChangeCategory", Name = "ChangeCategory")]
        [HttpPost]
        public IHttpActionResult ChangeCategory(JObject parameters)
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
                        dynamic json = parameters;
                        string productIdString = json.ProductId;
                        if (productIdString != null) {
                            string categoryIdString = json.CategoryId;
                            Guid productId = new Guid(productIdString);
                            if (categoryIdString != null)
                            {
                                Guid categoryId = new Guid(categoryIdString);
                                _productService.ChangeCategory(productId, categoryId);
                                return Ok("Al producto " + productIdString + " se le asignó la categoría " + categoryIdString);
                            }
                            else {
                                _productService.ChangeCategory(productId, Guid.Empty);
                                return Ok("Al producto " + productIdString + " se le sacó la categoría");
                            }

                        }
                        return BadRequest("Debes enviar un Id de Producto");
                    }
                    return BadRequest("Solo los administradores pueden cambiar categorías de productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductChangeCategoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest("Por favor verifique que los ids tienen 32 caracteres");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }
        }

       [Route("api/Product/AddAttribute", Name = "AddAttribute")]
        [HttpPost]
        public IHttpActionResult AddAttribute(JObject parameters)
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
                        dynamic json = parameters;
                        string productIdString = json.ProductId;
                        if (productIdString != null)
                        {
                            string featureIdString = json.FeatureId;
                            if (featureIdString != null)
                            {
                                string value = json.Value;
                                if (value != null) {
                                    Guid productId = new Guid(productIdString);
                                    Guid featureId = new Guid(featureIdString);
                                    ProductFeature pf = new ProductFeature();
                                    pf.ProductId = productId;
                                    pf.FeatureId = featureId;
                                    pf.Value = value;
                                    _productService.AddProductFeature(pf);
                                    return Ok("Al producto " + productIdString + " se le agregó el valor " + value+" para el atributo "+featureId);
                                }
                                else
                                {
                                    return BadRequest("El atributo de este producto debe tener un valor");
                                }

                            }
                            else {
                                return BadRequest("Debes enviar un Id de atributo");
                            }
                        }
                        return BadRequest("Debes enviar un Id de Producto");
                    }
                    return BadRequest("Solo los administradores pueden agregarle atributos a los productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductFeatureDuplicateFeature ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoFeatureException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductFeatureNoValueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductFeatureWrongValueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest("Por favor verifique que los ids tienen 32 caracteres");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Debes enviar todos los datos");
            }
        }
        [Route("api/Product/ChangeAttributeValue/{id}", Name = "ChangeAttributeValue")]
        [HttpPut]
        public IHttpActionResult ChangeAttributeValue(Guid id, [FromBody]ProductFeature productFeature) {
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
                        if (productFeature.Value != null)
                        {
                            _productService.ModifyProductFeatureValue(id, productFeature.Value);
                            return Ok("Se cambió el valor del atributo de producto con éxito");
                        }
                        return BadRequest("No se puede dejar un atributo de producto sin valor");
                    }
                    return BadRequest("Solo los administradores pueden modificar los atributos de los productos");
                }
                return BadRequest("Debes mandar el Token de sesión en los headers");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductFeatureWrongValueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoProductFeatureException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex) {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
             
        }

        [Route("api/Product/DeleteAttribute", Name = "DeleteAttribute")]
        [HttpDelete]
        public IHttpActionResult DeleteAttribute(JObject parameters)
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
                        dynamic json = parameters;
                        string productIdString = json.ProductId;
                        if (productIdString != null)
                        {
                            string featureIdString = json.FeatureId;
                            if (featureIdString != null)
                            {
                                Guid productId = new Guid(productIdString);
                                Guid featureId = new Guid(featureIdString);
                                _productService.RemoveFeatureFromProduct(productId, featureId);
                                return Ok("Al producto " + productIdString + " se le eliminó el atributo " + featureId);
                            }
                            else
                            {
                                return BadRequest("Debes enviar un Id de atributo");
                            }
                        }
                        return BadRequest("Debes enviar un Id de Producto");

                    }
                    return BadRequest("Solo los administradores pueden eliminar los atributos de los productos");
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
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoFeatureException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductWithoutFeatureException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Por favor asegurate de haber pasado un valor");
            }
            catch (RuntimeBinderException ex)
            {
                return BadRequest("Debes enviar algún dato");
            }

        }


        public IHttpActionResult Get()
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    List<Product> allWithReviews = _productService.GetAll(true);
                    return Ok(allWithReviews);
                }
                List<Product> allWithoutReviews = _productService.GetAll(false);
                return Ok(allWithoutReviews);
            }
            catch (NoUserWithTokenException ex)
            {
                List<Product> allWithoutReviews = _productService.GetAll(false);
                return Ok(allWithoutReviews);
            }
        }

        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    Product prod = _productService.Get(id, true);
                    return Ok(prod);

                }
                Product p = _productService.Get(id);
                return Ok(p);
            }
            catch (NoUserWithTokenException ex) {
                Product p = _productService.Get(id);
                return Ok(p);
            }
            catch (ProductNotExistingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Product/GetMostSold/{id}", Name = "GetMostSold")]
        [HttpGet]
        public IHttpActionResult GetMostSold(int id)
        {
            try
            {
                var re = Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    User loggedUser = _userService.GetFromToken(token);
                    List<Pair<Product, int>> mostSoldProducts = _productService.GetMostSold(id);
                    return Ok(mostSoldProducts);
                }
                return BadRequest("Solo los administradores pueden ver los productos más comprados");
            }
            catch (NoUserWithTokenException ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
