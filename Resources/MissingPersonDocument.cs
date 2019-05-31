using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPS_API.Resources
{
    public class MissingPersonDetails
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public ushort Age { get; set; }
        public ushort Height { get; set; }
        public string BodyType { get; set; }
        public DateTime LastSeen { get; set; }
        public string LastSeenInformation { get; set; }
        public string LastSeenAddress { get; set; }
        public MapCoordinates LastSeenCoordinates { get; set; }
        public string Information { get; set; }
    }

    public class MissingPersonDocument
    {
        public MissingPersonDetails MissingPerson { get; set; }
        public string Information { get; set; }

        [NotMapped]
        public Guid[] EquipmentRequests { get; set; }
    }
}