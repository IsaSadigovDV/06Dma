using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category:BaseModel
    {
        [Required]
        [StringLength(30,ErrorMessage ="Name max length should be 30 characters")]
         public string Name { get; set; }
    }
}
