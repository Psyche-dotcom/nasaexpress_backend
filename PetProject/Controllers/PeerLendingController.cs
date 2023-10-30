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
    [Route("api/peerlend/")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PeerLendingController : ControllerBase
    {
        private readonly PetProjectDbContext _context;

        public PeerLendingController(PetProjectDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("alluser")]
        public async Task<IActionResult> GetAllUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrieveuser = await _context.Users.Where(c=>c.Id != userIdClaim).ToListAsync();
            return Ok(retrieveuser);
        }
        [HttpGet("all/fundrequest")]
        public async Task<IActionResult> getowning()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrieveuser = await _context.Lendings.Include(c=>c.Lender).Where(c => c.BorrowId == userIdClaim).ToListAsync();
            return Ok(retrieveuser);
        }
        [HttpGet("all/borrowfund")]
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrieveuser = await _context.Lendings.Include(c => c.Lender).Where(c => c.LenderId == userIdClaim).ToListAsync();
            return Ok(retrieveuser);
        }
        [HttpPost("requestfund")]
        public async Task<IActionResult> RequestFund(float amount, string lenderid)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrievewallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == lenderid);
            var lend = await _context.Lendings.AddAsync(new Lending()
            {
                Amount = amount,
                BorrowId = userIdClaim,
                LenderId = lenderid,
                WalletId = retrievewallet.Id,
            });
             await _context.SaveChangesAsync();
            return Ok("Lending Request Send Successfully");
        }
        [HttpPost("grantfund")]
        public async Task<IActionResult> Grantfund(string approval, string requestid)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrievelend = await _context.Lendings.FirstOrDefaultAsync(c => c.Id == requestid);
            var retrieveborrowwallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == retrievelend.BorrowId);
            var retrievecreditorwallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == retrievelend.LenderId);
            if(approval == "APPROVED") { 
            var lender = await _context.WalletTransactions.AddAsync(new WalletTransaction()
            {
                WalletId = retrievecreditorwallet.Id,
                Amount = retrievelend.Amount.ToString(),
                Narration = "Debit for giving user fund",
                TransactionType = "Debit"
            });
            var borrower = await _context.WalletTransactions.AddAsync(new WalletTransaction()
            {
                WalletId = retrieveborrowwallet.Id,
                Amount = retrievelend.Amount.ToString(),
                Narration = "Credit for lending money from user",
                TransactionType = "Credit"
            });
            retrieveborrowwallet.Balance += retrievelend.Amount;
            retrievecreditorwallet.Balance -= retrievelend.Amount;
            
            retrievelend.Status = "Lend Accepted";
                _context.Lendings.Update(retrievelend);
                await _context.SaveChangesAsync();
            return Ok("Lending Request Accepted/Granted Successfully");
            }
            retrievelend.Status = "Lend Rejected";
            _context.Lendings.Update(retrievelend);
            await _context.SaveChangesAsync();
            return Ok("Lending Request Rejected Successfully");
        }
        [HttpPost("recover/lend")]
        public async Task<IActionResult> Grantfund(MoneyRequestDto requestDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrievelend = await _context.Lendings.FirstOrDefaultAsync(c => c.Id == requestDto.requestid);
            var retrieveborrowwallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == retrievelend.BorrowId);
            var retrievecreditorwallet = await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == retrievelend.LenderId);
            if (retrieveborrowwallet.Balance > retrievelend.Amount)
            {
                var lender = await _context.WalletTransactions.AddAsync(new WalletTransaction()
                {
                    WalletId = retrievecreditorwallet.Id,
                    Amount = retrievelend.Amount.ToString(),
                    Narration = "Recover fund for peer lending from user",
                    TransactionType = "Credit"
                });
                var borrower = await _context.WalletTransactions.AddAsync(new WalletTransaction()
                {
                    WalletId = retrieveborrowwallet.Id,
                    Amount = retrievelend.Amount.ToString(),
                    Narration = "Debit for peer lending repayment",
                    TransactionType = "Debit"
                });
                retrieveborrowwallet.Balance -= retrievelend.Amount;
                retrievecreditorwallet.Balance += retrievelend.Amount;

                retrievelend.Status = "Lend Recover";
                _context.Lendings.Update(retrievelend);
                await _context.SaveChangesAsync();
                return Ok("Lending Fund Retrieve Successfully");
            }
            
            return BadRequest("Borrow having low balance");
        }

    }
}
