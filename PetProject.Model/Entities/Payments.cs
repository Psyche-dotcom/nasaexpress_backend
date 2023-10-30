using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetProject.Model.Entities
{
    public class Payments
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Amount { get; set; }
        public string OrderReferenceId { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; } = "PayPal";
        public DateTime CreatedPaymentTime { get; set; } = DateTime.UtcNow;
        public DateTime CompletePaymentTime { get; set; } 
        public bool IsActive { get; set; } = true;
        public string PaymentStatus { get; set; } = "CREATED";

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public string PaymentChannel { get; set; }
    }
}