using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarRestApi.Entities
{
    [Table("m_product")]
    public class Product
    {
        [Key, Column(name: "product_id")]
        public Guid ProductId { get; set; }

        [Column(name: "product_name", TypeName = "NVarchar(50)")]
        public string ProductName { get; set; }

        [Column(name: "description", TypeName = "NVarchar(100)")]
        public string Description { get; set; }

        public ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
