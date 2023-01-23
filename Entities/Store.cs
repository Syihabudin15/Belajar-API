using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BelajarRestApi.Entities
{
    [Table("m_store", Schema = "dbo")]

    public class Store
    {
        [Key]
        [Column(name: "store_id")]
        public Guid StoreId { get; set; }

        [Column(name: "store_name", TypeName = "NVarchar(100)")]
        public string StoreName { get; set; }

        [Column(name: "phone_number", TypeName = "NVarchar(15)")]
        public string PhoneNumber { get; set; }

        [Column(name: "address", TypeName = "NVarchar(100)")]
        public string Address { get; set; }

        [Column(name: "email", TypeName = "NVarchar(100)")]
        public string Email { get; set; }

        [Column(name: "no_siup", TypeName = "NVarchar(50)")]
        public string NoSiup { get; set; }
    }
}
