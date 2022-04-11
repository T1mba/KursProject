using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursIgimbaev.Models
{
    public class Product
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string CharCode { get; set; }
        public ProductType CurrentProductType { get; set; }
        public Uri ImageView
        {
            get
            {
                var ImageName = Environment.CurrentDirectory + (Image ?? "");
                return System.IO.File.Exists(ImageName) ? new Uri(ImageName) : null;
            }
        }
    }
}
