using System.ComponentModel.DataAnnotations;

namespace A4WebApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }=string.Empty;
        [Required]
        public string? Author { get; set; } = string.Empty;
        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid year between 1900 and 2100.")]
        public int YearPublished { get; set; } = 2000;
        [Required]
        public string? Content { get; set; } = string.Empty;
    }
}
