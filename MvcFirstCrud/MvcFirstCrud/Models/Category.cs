using MvcFirstCrud.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MvcFirstCrud.Models
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
