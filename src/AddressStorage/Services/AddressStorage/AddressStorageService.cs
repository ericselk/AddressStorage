using AddressStorage.Services.AddressStorage.Models;
using System;

namespace AddressStorage.Services.AddressStorage
{
    public sealed class AddressStorageService
    {
        private IAddressStorageRepository repository;

        public AddressStorageService(IAddressStorageRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        public int Add(Address address)
        {
            ValidateAddress(address);

            var state = repository.GetStateByAbbreviation(address.State.Abbreviation);
            if(state == null)
            {
                throw new ArgumentException("unknown state abbreviation", "State.Abbreviation");
            }

            return repository.AddAddress(address.ToDto(state));
        }

        public Address GetById(int id)
        {
            var dto = repository.GetAddressById(id);
            if(dto == null)
            {
                return null;
            }

            return dto.ToModel();
        }

        private void ValidateAddress(Address address)
        {
            if(address.State == null)
            {
                throw new ArgumentNullException("State");
            }

            CheckRequiredField(address.Name, "Name");
            CheckRequiredField(address.AddressLine1, "AddressLine1");
            CheckRequiredField(address.City, "City");
            CheckRequiredField(address.State.Abbreviation, "State.Abbreviation");
            CheckRequiredField(address.Zip, "Zip");

            // TODO: Apply more fun BL
            if (address.Zip.Equals("90210"))
            {
                throw new ArgumentException("Zip code not allowed", "Zip");
            }
        }

        private void CheckRequiredField(string field, string fieldName)
        {
            if(string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentException("field required", fieldName);
            }
        }
    }
}