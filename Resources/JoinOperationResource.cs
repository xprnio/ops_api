using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Resources
{
    public class JoinOperationResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public ushort Age { get; set; }

        [Required]
        public DateTime EstimatedTimeOfArrival { get; set; }

        public DateTime? AvailableUntil { get; set; }

        public Guid? OrganizationId { get; set; }

        [NotMapped]
        public Guid[] EquipmentInventory { get; set; }
    }
}