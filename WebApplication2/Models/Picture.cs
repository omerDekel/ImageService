using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Picture
    {
        /// <summary>
        /// This is the constractor of picture.
        /// </summary>
        public Picture()
        { }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ThumbPath")]
        public string ThumbPath { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "RelativeThumbPath")]
        public string RelativeThumbPath { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Path")]
        public string Path { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "RelativePath")]
        public string RelativePath { get; set; }
    }

}