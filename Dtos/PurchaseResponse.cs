using BelajarRestApi.Entities;

namespace BelajarRestApi.Dtos
{
    public class PurchaseResponse
    {
        public string Id { get; set; }
        public string TransDate { get; set; }
        public string CustomerId { get; set; }
        public List<PurchaseDetailResponse> PurchaseDetails { get; set; }
    }
}
