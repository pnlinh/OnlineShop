using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class ThanhPhoVM:ThanhPho
    {
        [Required(ErrorMessage = "Hãy chọn 1 tỉnh hoặc thành phố.")]

        public int ID { get; set; }
        public string TenThanhPho { get; set; }
        public bool BiXoa { get; set; }
        public string NameShow { get; set; }
    }
}