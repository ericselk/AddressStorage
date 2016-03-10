using AddressStorage.Services.AddressStorage;
using AddressStorage.Services.AddressStorage.DTO;
using AddressStorage.Services.AddressStorage.Models;
using System;
using Xunit;

namespace AddressStorage.Tests.Services
{
    public class AddressStorageServiceTests
    {
        [Theory]
        [InlineData("John Doe", "Hershy of America", "123 E. Main St", "Suite 200", "Tucson", "AZ", "85741")]
        [InlineData("Jåne Doe", "ABC, Inc!", "123 E. Main St", null, "Tucson", "CA", "91234")]
        [InlineData("Påck Man", null, "123 E. Main St", "", "Seattle", "WA", "98020")]
        void CanAdd(string name, string company, string line1, string line2, string city, string state, string zip)
        {
            var target = new AddressStorageService(new TestIAddressStorageRepository());
            var id = target.Add(new Address
            {
                Name = name,
                Company = company,
                AddressLine1 = line1,
                AddressLine2 = line2,
                City = city,
                State = state,
                Zip = zip
            });
            Assert.Equal(555, id);
        }

        [Theory]
        [InlineData("", "Hershy of America", "123 E. Main St", "Suite 200", "Tucson", "AZ", "85741")]
        [InlineData("Påck Man", null, "123 E. Main St", "", "Seattle", "WA", null)]
        [InlineData("Jåne Doe", "ABC, Inc!", "", null, "Tucson", "CA", "91234")]
        [InlineData("Jåne Doe", "ABC, Inc!", "123 E. Main St", null, null, "CA", "91234")]
        [InlineData("Påck Man", null, "123 E. Main St", "", "Seattle", null, "98020")]
        [InlineData("John Doe", "Hershy of America", "123 E. Main St", "Suite 200", "Tucson", "AZ", null)]
        [InlineData("John Doe", "Hershy of America", "123 E. Main St", "Suite 200", "Tucson", "XX", "85741")]
        void CannotAdd(string name, string company, string line1, string line2, string city, string state, string zip)
        {
            var target = new AddressStorageService(new TestIAddressStorageRepository());
            Assert.Throws(typeof(ArgumentException), () =>
            {
                target.Add(new Address
                {
                    Name = name,
                    Company = company,
                    AddressLine1 = line1,
                    AddressLine2 = line2,
                    City = city,
                    State = state,
                    Zip = zip
                });

            });
        }

        [Fact]
        void CanGetById()
        {
            var target = new AddressStorageService(new TestIAddressStorageRepository());
            var ret = target.GetById(1567);
            Assert.Equal(1567, ret.Id);
            Assert.Equal("test", ret.AddressLine1);
            Assert.Equal("Washington", ret.State.Name);
            Assert.Equal("WA", ret.State.Abbreviation);
            Assert.Equal(777, ret.State.Id);
        }

        [Fact]
        void CannotGetById()
        {
            var target = new AddressStorageService(new TestIAddressStorageRepository());
            var ret = target.GetById(123);
            Assert.Null(ret);
        }

        private class TestIAddressStorageRepository : IAddressStorageRepository
        {
            public int AddAddress(AddressDto address)
            {
                return 555;
            }

            public AddressDto GetAddressById(int id)
            {
                if (id == 123)
                {
                    return null;
                }

                return new AddressDto
                {
                    Id = id,
                    AddressLine1 = "test",
                    State = new StateDto
                    {
                        Id = 777,
                        Abbreviation = "WA",
                        Name = "Washington"
                    }
                };
            }

            public StateDto GetStateByAbbreviation(string abbreviation)
            {
                if (abbreviation == "XX")
                {
                    return null;
                }
                return new StateDto
                {
                    Id = 1,
                    Abbreviation = abbreviation,
                    Name = abbreviation + "foo"
                };
            }
        }
    }
}
