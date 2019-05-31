using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Domain.Models
{
    [Table("Organizations", Schema = "application")]
    public class Organization : BaseModel
    {
        public string Name { get; set; }
    }
}