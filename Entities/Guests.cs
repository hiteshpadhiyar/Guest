using Guest.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Guest.Entities
{
    [Table("Guests")]
    public class Guests
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TitleType Title { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumbers { get; set; }
    }
}
