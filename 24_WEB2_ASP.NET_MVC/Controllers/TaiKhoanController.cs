using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Models;
using _24_WEB2_ASP.NET_MVC.Helpers;

namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        private OnlineShopEntities db = new OnlineShopEntities();
        public ActionResult DangKy()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");
            var model = new IndexViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(string Command, IndexViewModel model)
        {
            ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");
            if (ModelState.IsValid)
            {
                var password = StringUtils.Md5(model.TestTwo.Pass);
                if (Request.Form["Command"] != null)
                {
                    TaiKhoan t = new TaiKhoan
                    {
                        Email = model.TestTwo.Email,
                        Pass = password,
                        Name = model.TestTwo.Name,
                        PhanQuyen = false,
                        Phone = model.TestTwo.Phone,
                        Address = model.TestTwo.Address,
                        idThanhPho = model.TestOne.ID,
                        idHuyen=model.TestThree.ID,
                        BiXoa = false
                    };

                    db.TaiKhoans.Add(t);
                    db.SaveChanges();

                }
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            if (Request.Form["Command1"] != null)
            {
                ModelState.Clear();
                return RedirectToAction("DangKy", "TaiKhoan");
            }
            return View(model);
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            
            return RedirectToAction("DangNhap", "TaiKhoan");
            
        }
        [HttpPost]
        public JsonResult GetStates(string id, FormCollection formCollection)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            var stateList = this.Getstate(Convert.ToInt32(id));
            var stateData = stateList.Select(m => new SelectListItem()
            {
                Text = m.TenHuyen,
                Value = m.ID.ToString(),
            });
            return Json(stateData, JsonRequestBehavior.AllowGet);
        }

        public IList<Huyen> Getstate(int CountryId)
        {
            return db.Huyens.Where(m => m.idThanhPho == CountryId).ToList();
        }
        
        public ActionResult DangNhap()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index","TrangChu");
            }
            if (Session["UserID"] == null)
            {
                var a = db.ChiTiet_HoaDon.Where(s => s.idHoaDon == null).ToList();
                foreach (var item in a)
                {
                    db.ChiTiet_HoaDon.Remove(item);
                }
                db.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(DangNhapVM login, string Command)
        {
            if (ModelState.IsValid)
            {
                string password = StringUtils.Md5(login.Pass);
                var obj = db.TaiKhoans.Where(a => a.Email.Equals(login.Email) && a.Pass.Equals(password) &&(a.BiXoa==false)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.ID.ToString();
                    Session["UserName"] = obj.Name.ToString();
                    Session["UserEmail"] = obj.Email.ToString();
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                {
                    ModelState.AddModelError("Pass", "Mật khẫu không đúng");
                }


            }
            return View(login);
        }
        [AllowAnonymous]
        public ActionResult CheckUserEmail(DangNhapVM UserName)
        {
            bool UserExists = false;

            try
            {
                using (var dbcontext = new OnlineShopEntities())
                {
                    var nameexits = from temprec in dbcontext.TaiKhoans
                                    where temprec.Email.Equals(UserName.Email)
                                    select temprec;
                    if (nameexits.Count() > 0)
                    {
                        UserExists = true;
                    }
                    else
                    {
                        UserExists = false;
                    }
                }
                return Json(UserExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        // hàm kiểm tra email đã tồn tại chưa
        public ActionResult CheckUserEmail1(IndexViewModel UserName)
        {
            bool UserExists = false;

            try
            {
                using (var dbcontext = new OnlineShopEntities())
                {
                    var nameexits = from temprec in dbcontext.TaiKhoans
                                    where temprec.Email.Equals(UserName.TestTwo.Email)
                                    select temprec;
                    if (nameexits.Count() > 0)
                    {
                        UserExists = true;
                    }
                    else
                    {
                        UserExists = false;
                    }
                }
                return Json(!UserExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ThongTin()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThongTin(DoiMatKhauVM model)
        {
            if (ModelState.IsValid)
            {
                var password = StringUtils.Md5(model.Pass);
                var getEmail = Session["UserEmail"].ToString();
                var account = db.TaiKhoans.Where(s => s.Email == getEmail && (s.Pass == password)).Select(s=>new {s }).FirstOrDefault();
                if (account == null)
                {
                    ModelState.AddModelError("Pass", "Mật khẩu cũ không đúng.");
                }
                if(account!=null)
                {
                   account.s.Pass = StringUtils.Md5(model.ConfirmNewPass);
                   db.SaveChanges();
                    return RedirectToAction("Index","TrangChu");
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult QuenMatKhau()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult QuenMatKhau(DangNhapVM model)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChiTiet()
        {

            ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                var model = new IndexViewModel();
                var mail = Session["UserEmail"].ToString();
                var acc = db.TaiKhoans.Where(s => s.Email == mail).FirstOrDefault();
                model.TaiKhoan = acc;
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChiTiet(IndexViewModel model)
        {
            ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index","TrangChu");
            }
            else
            {
                var mail = Session["UserEmail"].ToString();
                var acc = db.TaiKhoans.Where(s=>s.Email==mail).FirstOrDefault();
                acc.Address = model.TestTwo.Address;
                acc.Phone = model.TestTwo.Phone;
                acc.Name = model.TestTwo.Name;
                db.SaveChanges();
                model.TaiKhoan = acc;
            }
            return View(model);
        }
    }
}