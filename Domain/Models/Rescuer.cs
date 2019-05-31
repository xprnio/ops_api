using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Domain.Models
{
    [Table("Rescuers", Schema = "application")]
    public class Rescuer : BaseModel
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [Column]
        public ushort Age { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime EstimatedTimeOfArrival { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime AvailableUntil { get; set; }

        [Column]
        public Guid OperationId { get; set; }

        [Column]
        public Guid? OrganizationId { set; get; }

        [ForeignKey("OperationId")]
        public Operation Operation { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        public List<EquipmentInventory> Inventory { get; set; }
    }
}