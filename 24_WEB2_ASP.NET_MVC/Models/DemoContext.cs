using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<SanPham> Item { get; set; }
        public DbSet<ChiTiet_HoaDon> Cart { get; set; }
    }
}