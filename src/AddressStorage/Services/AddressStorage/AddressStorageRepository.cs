using AddressStorage.Services.AddressStorage.DAL;
using AddressStorage.Services.AddressStorage.DTO;
using System.Linq;

namespace AddressStorage.Services.AddressStorage
{
    public interface IAddressStorageRepository
    {
        int AddAddress(AddressDto address);
        AddressDto GetAddressById(int id);
        StateDto GetStateByAbbreviation(string abbreviation);
    }

    public sealed class AddressStorageRepository : IAddressStorageRepository
    {
        private AddressStorageContext context;

        public AddressStorageRepository(string connectionString)
        {
            context = new AddressStorageContext(connectionString);
        }

        public int AddAddress(AddressDto address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
            return address.Id;
        }

        public AddressDto GetAddressById(int id)
        {
            return context.Addresses.SingleOrDefault(x => x.Id == id);
        }

        public StateDto GetStateByAbbreviation(string abbreviation)
        {
            return context.States.SingleOrDefault(x => x.Abbreviation.Equals(abbreviation, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
