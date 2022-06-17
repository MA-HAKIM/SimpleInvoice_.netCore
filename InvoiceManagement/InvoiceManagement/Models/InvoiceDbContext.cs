using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagement.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        
        public int InvoiceNo { get; set; }
        [Required,Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        [StringLength(20),Display(Name ="Contact"),DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class ProductDetails
    {
        public int Id { get; set; }
        [Display(Name ="Product Code")]
        public string ProductCode { get; set; }
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
    }
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required,Display(Name ="Product Name"),StringLength(40)]
        public string ProductName { get; set; }
        [Display(Name ="Purchase Date"),DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
        public float TotalAmount { get; set; }
        
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
    public class InvoiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options): base(options) { }
    }

}
