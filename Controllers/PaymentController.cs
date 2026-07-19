using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Stripe API Ready");
        }

        [HttpPost("checkout")]
        public IActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card"
            },

                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Quantity = 1,

                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",

                        UnitAmount = 5000, // $50.00

                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Laptop Bag"
                        }
                    }
                }
            },

                Mode = "payment",

                SuccessUrl = "https://localhost:5001/success",

                CancelUrl = "https://localhost:5001/cancel"
            };

            var service = new SessionService();

            Session session = service.Create(options);

            return Ok(new
            {
                session.Id,
                session.Url
            });
        }
    }
}
