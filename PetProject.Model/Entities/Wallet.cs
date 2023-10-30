namespace PetProject.Model.Entities
{
    public class Wallet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public float Balance { get; set; } = 100;
        public string UserId { get; set; }
        public List<WalletTransaction> WalletTransaction { get; set; }
        public List<Lending> lendings { get; set; }
        public ApplicationUser User { get; set; }
    }
}
