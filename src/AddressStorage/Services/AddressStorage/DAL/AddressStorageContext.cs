using AddressStorage.Services.AddressStorage.DTO;
using System.Data.Entity;

namespace AddressStorage.Services.AddressStorage.DAL
{
    internal sealed class AddressStorageContext : DbContext
    {
        public AddressStorageContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<AddressDto> Addresses { get; set; }
        public DbSet<StateDto> States { get; set; }
    }
}
