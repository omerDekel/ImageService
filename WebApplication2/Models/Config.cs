using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Config
    {
        /*    <add key="Handler" value="C:\Users\Omer Dekel\Desktop\dor;C:\Users\Omer Dekel\Pictures\pict"/>
    <add key="OutputDir" value="C:\Users\Omer Dekel\Desktop\IM"/>
    <add key="SourceName" value="ImageServiceSource"/>
    <add key="LogName" value="ImageServiceLog"/>
    <add key="ThumbnailSize" value="120"/>
*/
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DirectoryHandlers")]
        public List<string> DirectoryHandlers { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDirectory")]
        public string OutputDirectory { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize")]
        public string ThumbnailSize { get; set; }

    }
}