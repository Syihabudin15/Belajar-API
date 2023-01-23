using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BelajarRestApi.Entities
{
    [Table("m_customer")]
    public class Customer
    {
        [Key]
        [Column(name: "customer_id")]
        public Guid CustomerId { get; set; }

        [Column(name: "customer_name", TypeName = "NVarchar(100)")]
        public string CustomerName { get; set; }

        [Column(name: "phone_number", TypeName = "NVarchar(15)")]
        public string PhoneNumber { get; set; }

        [Column(name: "address", TypeName = "NVarchar(100)")]
        public string Address { get; set; }

        [Column(name: "email", TypeName = "NVarchar(100)")]
        public string Email { get; set; }

        public string ToString()
        {
            return $"Id : {CustomerId} | Name : {CustomerName} | Address : {Address} | PhoneNumber : {PhoneNumber} | Email : {Email}";
        }
    }
}
