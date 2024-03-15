namespace TheST.Models
{
    public sealed class Device
    {
        public string Id { get; }
        public string Name { get; }
        public Device(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
