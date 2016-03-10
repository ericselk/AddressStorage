using AddressStorage.Services.AddressStorage.DTO;

namespace AddressStorage.Services.AddressStorage.Models
{
    public sealed class Address
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }

        public AddressDto ToDto(StateDto state)
        {
            return new AddressDto
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                StateId = state.Id,
                State = state,
                Zip = Zip,
                Name = Name,
                Company = Company
            };
        }
    }
}