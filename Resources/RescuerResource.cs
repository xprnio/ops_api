using System.Collections.Generic;

namespace OPS_API.Resources
{
    public class RescuerResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ushort Age { get; set; }
        
        public string EstimatedTimeOfArrival { get; set; }
        public string AvailableUntil { get; set; }
        
        public OrganizationResource Organization { get; set; }
        public IList<EquipmentResource> Inventory { get; set; }
    }
}