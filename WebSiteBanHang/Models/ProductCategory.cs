using System.ComponentModel.DataAnnotations;

namespace WebSiteBanHang.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { set; get; };
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
