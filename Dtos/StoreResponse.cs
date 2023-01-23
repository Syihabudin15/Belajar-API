using System.Diagnostics.Contracts;

namespace BelajarRestApi.Dtos
{
    public class StoreResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string NoSiup { get; set; }
    }
}
