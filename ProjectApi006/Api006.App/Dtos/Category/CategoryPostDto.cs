using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Api006.App.Dtos
{
    public record CategoryPostDto
    {
        public string Name { get; set; }
    }
}
