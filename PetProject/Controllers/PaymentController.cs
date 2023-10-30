using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProject.Data.Context;
using PetProject.Model.DTO;
using PetProject.Model.Entities;
using Stripe;
using Stripe.Checkout;


namespace PetProject.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PetProjectDbContext _context;
        private readonly IConfiguration _configuration;

        public PaymentController(PetProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("initializepayment")]
        public async Task<IActionResult> InitiatePayment(string userid, float amount)
        {
            amount = amount / 50;
            var description = $"Minute order purchase for {amount}$";
            StripeConfiguration.ApiKey = _configuration["StripeKey:SecretKey"];
            var stripePayment = new Payments();
            var response = new ResponseDto<string>();
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = $"https://nasaexpress.vercel.app/confirm_payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = "https://nasaexpress.vercel.app/",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(amount * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = description
                        }
                    },
                    Quantity = 1
                };

                options.LineItems.Add(sessionListItem);

                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                if (session.Status != "open")
                {
                    return BadRequest("Transaction was not initialized");
                }
                stripePayment.PaymentChannel = "STRIPE";
                stripePayment.OrderReferenceId = session.Id;
                stripePayment.Amount = amount.ToString("0.00");
                stripePayment.IsActive = true;
                stripePayment.Description = description;
                stripePayment.UserId = userid;
                _context.Payments.Add(stripePayment);
                await _context.SaveChangesAsync();
                return Ok(session.Url);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("confirmpayment")]
        public async Task<IActionResult> ConfirmPayment(string session_id)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKey:SecretKey"];
            try
            {
                var retrievePayment = await _context.Payments.FirstOrDefaultAsync(c=>c.OrderReferenceId == session_id);
                if (retrievePayment == null)
                {
                    return BadRequest("Invalid session Id");
                }
                var service = new SessionService();
                var session = await service.GetAsync(session_id);
                if (retrievePayment.IsActive == false)
                {
                    return BadRequest("Session id already used");
                }
                if (session.PaymentStatus == "paid")
                {
                    retrievePayment.IsActive = false;
                    retrievePayment.PaymentStatus = session.PaymentStatus;
                    retrievePayment.CompletePaymentTime = DateTime.UtcNow;
                     _context.Payments.Update(retrievePayment);
                    var nasa = float.Parse(retrievePayment.Amount) * 50;

                    var retrieveuserwallet = await _context.Wallets.FirstOrDefaultAsync(u => u.UserId == retrievePayment.UserId);
                    retrieveuserwallet.Balance += nasa;
                    _context.Wallets.Update(retrieveuserwallet);
                    
                    await _context.SaveChangesAsync();
                    return Ok("Payment Successfully completed");

                }
                retrievePayment.CompletePaymentTime = DateTime.UtcNow;
                retrievePayment.IsActive = false;
                retrievePayment.PaymentStatus = session.PaymentStatus;
                    await _context.SaveChangesAsync();
                return BadRequest("Invalid Transaction");

            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }

}
