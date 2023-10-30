using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetProject.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? DriverLicence { get; set; }
        public DateTime? DriverLicenceExpiryDate { get; set; }
        public Wallet Wallet { get; set; }
        [InverseProperty("Lender")]
        public List<Lending> UserLending { get; set; }
        [InverseProperty("Borrow")]
        public List<Lending> UserBorrow { get; set; }
        public ConfirmEmailToken ConfirmEmailToken { get; set; }
        [InverseProperty("Driver")]
        public List<UserTravel> DrivenTravels { get; set; }
        [InverseProperty("Client")]
        public List<UserTravel> ClientTravels { get; set; }
        public List<Payments> Payments { get; set; }
    }
}
