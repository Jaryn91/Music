﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musiction.API.Entities
{
    public class Presentation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ICollection<Song> Songs { get; set; } = new List<Song>();

        [Required]
        public string Type { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreateBy { get; set; }
    }
}
