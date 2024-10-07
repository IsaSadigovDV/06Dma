using Api006.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Dtos.Product
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile File { get; set; }
        public Guid CategoryId { get; set; }

    }
}

