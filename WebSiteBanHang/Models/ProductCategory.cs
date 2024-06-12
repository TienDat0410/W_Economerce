using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteBanHang.Models
{
    public class ProductCategory
    {
        [Key]
<<<<<<< HEAD
        [Column(Order = 0)]
=======
        public int ProductCategoryId { set; get; };
>>>>>>> cb233e311d832e59174e10df733f5b61776fd36d
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
