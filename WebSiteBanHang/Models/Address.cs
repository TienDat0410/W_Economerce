using System.ComponentModel.DataAnnotations;

namespace WebSiteBanHang.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
