using System.ComponentModel.DataAnnotations;

namespace BAWASHARK.Models.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title should be atleast 5 chars")]
        [MaxLength(255, ErrorMessage = "Title should not be more than 255 characters")]
        public string? Title { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "content should not be more than a 100 chars")]
        public string? Content { get; set; }
    }
}
