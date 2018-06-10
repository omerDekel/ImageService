using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ImageWeb
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PicturesNumber")]
        public int PicturesNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "IsConnected")]
        public bool IsConnected { get; set; }

    }
}