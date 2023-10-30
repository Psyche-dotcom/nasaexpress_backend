using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProject.Data.Context;
using PetProject.Model.DTO;
using PetProject.Model.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace PetProject.Controllers
{
    [Route("api/book")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly PetProjectDbContext _context;

        public BookingController(PetProjectDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("getqoute")]
        public async Task<IActionResult> GetQoute(string pickup, string destination)
        {
          
            var retrievetrip = await _context.Trips.FirstOrDefaultAsync(c => c.PickUp == pickup && c.Destination == destination);
            return Ok(retrievetrip);

        }
        [HttpGet("getalltrip")]
        public async Task<IActionResult> GetTrip()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrievetrip = await _context.UserTravels.Include(c=>c.Trip).Where(u=> u.ClientId == userIdClaim).ToListAsync();
            return Ok(retrievetrip);

        }
        [HttpPost("takeride")]
        public async Task<IActionResult> BookRide(BookRideDto bookRide)
        {

            var retrievetrip = await _context.Trips.FirstOrDefaultAsync(c =>c.Id == bookRide.TripId);
            var retrievewallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == bookRide.ClientId);
            var createTravel = await _context.UserTravels.AddAsync(new UserTravel()
            {
                ClientId = bookRide.ClientId,
                DriverId = bookRide.DriverId,
                TripId = bookRide.TripId
            });
            
            var createtnx = await _context.WalletTransactions.AddAsync(new WalletTransaction()
            {
                Amount = retrievetrip.Amount.ToString(),
                WalletId = retrievewallet.Id,
                Narration = "Withdrawer for transport",
                TransactionType = "Debit"
            });
            retrievewallet.Balance -= retrievetrip.Amount;
            _context.Update(retrievewallet);
            await _context.SaveChangesAsync();

            return Ok("Booking Completed Successfully");

        }
    }
}
