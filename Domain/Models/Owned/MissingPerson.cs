using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace OPS_API.Domain.Models.Owned
{

    [Owned]
    public class MissingPerson
    {
        [Required]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public ushort Age { get; set; }

        [Required]
        public ushort Height { get; set; }

        [Required]
        public string BodyType { get; set; }

        [Required]
        public string Information { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime LastSeen { get; set; }

        [Column]
        public string LastSeenAddress { get; set; }

        [Column]
        public string LastSeenInformation { get; set; }

        [Column]
        public Point LastSeenCoordinates { get; set; }
    }
}