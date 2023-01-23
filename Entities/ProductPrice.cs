using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarRestApi.Entities
{
    [Table("m_product_price")]
    public class ProductPrice
    {
        [Key, Column(name: "productPrice_id")]
        public Guid ProductPriceId { get; set; }

        [Column(name: "price")]
        public long Price { get; set; }

        [Column(name: "stock")]
        public int Stock { get; set; }

        [Column(name: "product_id")]
        public Guid ProductId { get; set; }

        [Column(name: "store_id")]
        public Guid StoreId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Store? Store { get; set; }
    }
}
