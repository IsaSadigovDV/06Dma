using Api006.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public double Price {  get; set; }
        public string Image {  get; set; }
        public string ImageUrl { get; set; }
        //relations
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
