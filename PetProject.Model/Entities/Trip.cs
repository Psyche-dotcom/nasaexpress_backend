namespace PetProject.Model.Entities
{
    public class Trip
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PickUp { get; set; }
        public string Destination { get; set; }
        public float Amount { get; set; }
        public List<UserTravel> UserTravel { get; set; }
    }
}
