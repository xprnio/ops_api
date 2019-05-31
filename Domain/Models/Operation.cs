using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OPS_API.Domain.Models.Owned;

namespace OPS_API.Domain.Models
{
    [Table("Operations", Schema = "application")]
    public class Operation : BaseModel
    {
        [Column]
        public string Information { get; set; }

        public MissingPerson MissingPerson { get; set; }
        public List<EquipmentRequest> RequestedEquipment { get; set; }
        
        public List<Rescuer> Rescuers { get; set; }
    }
}