using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Domain.Models
{
    [Table("Equipment", Schema = "application")]
    public class Equipment : BaseModel
    {
        [Column]
        [Required]
        public string Name { get; set; }
    }

    [Table("EquipmentRequests", Schema = "application")]
    public class EquipmentRequest : BaseModel
    {
        public Operation Operation { get; set; }
        public Equipment Equipment { get; set; }
    }

    [Table("EquipmentInventory", Schema = "application")]
    public class EquipmentInventory : BaseModel
    {
        public EquipmentRequest EquipmentRequest { get; set; }
        public Rescuer Rescuer { get; set; }
    }
}