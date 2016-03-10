using AddressStorage.Services.AddressStorage.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressStorage.Services.AddressStorage.DTO
{
    [Table("AppCore_State")]
    public sealed class StateDto
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }

        public State ToModel()
        {
            return new State
            {
                Abbreviation = Abbreviation,
                Name = Name,
                Id = Id
            };
        }
    }
}
