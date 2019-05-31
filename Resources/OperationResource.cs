using System.Collections.Generic;

namespace OPS_API.Resources
{
    public class OperationResource
    {
        public string Id { get; set; }
        public string Information { get; set; }
        public MissingPersonDetails MissingPerson { get; set; }
        public IList<RescuerResource> Rescuers { get; set; }
        public IList<EquipmentResource> RequestedEquipment { get; set; }
    }
}