using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication2.Comunication;

namespace WebApplication2.Models
{
    public class ImageWeb
    {
        private IClient m_client;
        private Config config; 
        public ImageWeb()
        {
            config = Config.Instance;
            string outputDir;
            outputDir = config.OutputDirectory;
            Photos photos = new Photos();
            photos.GetPictures(outputDir);
            PicturesNumber = photos.Pictures.Count;
            string currentLine;
            string[] strArr;
            Student st;
            StreamReader stream;
            // connecting to the server Image service .
            m_client = Client.Instance;
            // if the connection didn't stop then there's a connection .
            if (m_client.Stop == true)
            {
                Status = "Disconnected";
            } else
            {
                Status = "Connected";
            }
            Students = new List<Student>();

            try
            {
                stream = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/students.txt"));
                currentLine = stream.ReadLine();
                while(currentLine != null)
                {
                    strArr = currentLine.Split(' ');
                    st = new Student();
                    st.FirstName = strArr[0];
                    st.LastName = strArr[1];
                    st.ID = strArr[2];
                    Students.Add(st);
                    currentLine = stream.ReadLine();
                }
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
      
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PicturesNumber")]
        public int PicturesNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Students")]
        public List<Student> Students { get; set; }

    }
}