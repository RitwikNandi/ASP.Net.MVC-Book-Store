using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }

        [Display(Name = "List Price")]
        [Range(1, 10000)]
        public double? ListPrice { get; set; }
        
        [Display(Name = "Price for books orders between the quantity of 1 to 50")]
        [Range(1, 10000)]
        public double? List50 { get; set; }
        
        [Display(Name = "Price if you order 100 or more books")]
        [Range(1, 10000)]
        public double? Price100 { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
