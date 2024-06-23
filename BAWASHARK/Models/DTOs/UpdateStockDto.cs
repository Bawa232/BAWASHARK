using System.ComponentModel.DataAnnotations;

namespace BAWASHARK.Models.DTOs
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be more than 10 chars")]
        public string? Symbol { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "company name cannot be greater than 50 characters")]
        public string? CompanyName { get; set; }
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "industry cannot be greater than 25 characters")]
        public string? Industry { get; set; }
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
