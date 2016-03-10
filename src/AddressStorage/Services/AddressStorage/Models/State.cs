namespace AddressStorage.Services.AddressStorage.Models
{
    public sealed class State
    {
        public int Id { get; internal set; }
        public string Abbreviation { get; set; }
        public string Name { get; internal set; }

        public static implicit operator State(string abbreviation)
        {
            return new State { Abbreviation = abbreviation };
        }
    }
}
