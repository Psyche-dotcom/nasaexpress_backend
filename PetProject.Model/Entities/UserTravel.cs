using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Model.Entities
{
    public class UserTravel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("DriverId")]
        public ApplicationUser Driver { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }
        public string ClientId { get; set; }
        public string TripId { get; set; }
        public Trip Trip { get; set; }
        public string Status { get; set; } = "Begin";
    }
}
