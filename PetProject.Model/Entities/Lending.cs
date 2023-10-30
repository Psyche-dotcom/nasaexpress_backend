using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Model.Entities
{
    public class Lending
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public float Amount { get; set; }
        [ForeignKey("BorrowerId")]
        public ApplicationUser Borrow { get; set; }
        public string BorrowId { get; set; }

        [ForeignKey("LenderId")]
        public ApplicationUser Lender { get; set; }
        public string LenderId { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public string Status { get; set; } = "Request";
    }
}
