using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Comunication;

namespace WebApplication2.Models
{
    public class ImageWeb
    {

        private IClient m_client;
        //public IClient M_client { get; set; }

        public ImageWeb()
        {
            // connecting to the server Image service .
            m_client = Client.Instance;
            // if the connection didn't stop then there's a connection .
            IsConnected = !m_client.Stop;
        }
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