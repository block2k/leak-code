using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class StudentCreateDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public string? IDCard { get; set; }

        [Required]
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Email { get; set; }
        public string? DateOfIssue { get; set; }
        public string? PlaceOfIssue { get; set; }
        public string? Base64ImageString { get; set; }
    }
}