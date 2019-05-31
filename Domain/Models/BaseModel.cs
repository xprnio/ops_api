using System;
using System.ComponentModel.DataAnnotations;

namespace OPS_API.Domain.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}