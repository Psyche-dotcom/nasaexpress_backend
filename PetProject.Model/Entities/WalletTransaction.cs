using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Model.Entities
{
    public class WalletTransaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Narration { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public Wallet Wallet { get; set; }
        public string WalletId { get; set; }
    }
}
