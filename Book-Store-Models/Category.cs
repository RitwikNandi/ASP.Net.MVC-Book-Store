using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        [MaxLength(90)]
        public required string CategoryName { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be within the range of 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
