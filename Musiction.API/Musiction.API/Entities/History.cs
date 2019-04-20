using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musiction.API.Entities
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
        public string Information { get; set; }


    }
}
