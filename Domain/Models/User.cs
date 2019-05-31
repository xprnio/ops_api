using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Domain.Models
{
    [Table("Users", Schema = "application")]
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}