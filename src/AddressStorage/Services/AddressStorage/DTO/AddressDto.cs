using AddressStorage.Services.AddressStorage.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressStorage.Services.AddressStorage.DTO
{
    [Table("AppCore_Address")]
    public sealed class AddressDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public StateDto State { get; set; }
        public string Zip { get; set; }

        public Address ToModel()
        {
            return new Address
            {
                Id = Id,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                Company = Company,
                Name = Name,
                Zip = Zip,
                State = State.ToModel()
            };
        }
    }
}
