using System.ComponentModel.DataAnnotations;

namespace OndeFoi.DTOs
{
    public class EditarRendaDto
    {
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Renda { get; set; }
    }
}