using Entities;
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
    public class ReviewController : ApiController
    {
        private IReviewService _reviewService;
        private IUserService _userService;

        public ReviewController(IReviewService service, IUserService userService)
        {
            _reviewService = service;
            _userService = userService;
        }


        [Route("api/Review/EvaluateProduct", Name = "EvaluateProduct")]
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
                    string orderIdString = json.OrderId;
                    string reviewText = json.ReviewText;
                    if (productIdString != null)
                    {
                        if (orderIdString != null) {
                            if(reviewText != null)
                            {
                                Guid productId = new Guid(productIdString);
                                Guid orderId = new Guid(orderIdString);
                                _reviewService.Evaluate(loggedUser, productId, orderId, reviewText);
                                return Ok("Se evaluó al producto " + productId + " para la orden " + orderId);
                            }
                            return BadRequest("Debes enviar un comentario para evaluar el producto");
                        }
                        return BadRequest("Debes enviar un Id de órden");
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
            catch (ProductAlreadyEvaluatedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IncorrectOrderStatusException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NotExistingProductInOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoTextForReviewException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotExistingOrderException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NotExistingProductException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
