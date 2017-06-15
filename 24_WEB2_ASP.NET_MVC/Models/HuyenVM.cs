using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class HuyenVM
    {
        [Required(ErrorMessage ="Hãy chọn một huyện-thị xã.")]
        public int ID { get; set; }
        public string TenHuyen { get; set; }
        public Nullable<bool> BiXoa { get; set; }
        public Nullable<int> idThanhPho { get; set; }
        public virtual ThanhPho ThanhPho { get; set; }
    }
}