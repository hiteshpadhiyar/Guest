using System.ComponentModel.DataAnnotations;

namespace Guest.Models
{
    public class GuestsItem
    {
        public Guid? Id { get; set; }
        public TitleType Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The FirstName must be a string with the maximum length of 100.")]
        public string FirstName { get; set; }
        [StringLength(100, ErrorMessage = "The LastName must be a string with the maximum length of 100.")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Email must be a string with the maximum length of 100.")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string Email { get; set; }
        public List<string> Phone_Numbers { get; set; } = new List<string>();
    }
    public class GuestsPhoneItem
    {
        public Guid Id { get; set; }
        public List<string> Phone_Numbers { get; set; } = new List<string>();
    }
}
