
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebSiteBanHang.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string ImageUrl { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; }
    }


}
