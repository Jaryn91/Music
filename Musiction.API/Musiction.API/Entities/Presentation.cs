using Auth0.AuthenticationApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Musiction.API.Entities
{
    public class Presentation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string FinalFileName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreateBy { get; set; }

        public List<LinkSongToPresentation> LinkSongToPresentation { get; set; } = new List<LinkSongToPresentation>();


        public Presentation()
        {

        }


        public Presentation(string finalFileName, UserInfo user)
        {
            FinalFileName = finalFileName;
            Type = Path.GetExtension(finalFileName).Split('.')[1];
            CreateBy = user.FullName;
            CreatedDate = DateTime.Now;
        }
    }
}
