using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.Models.Database
{
    public class GenericIntModel
    {
        [Key]
        public int Value { get; set; }
    }
}