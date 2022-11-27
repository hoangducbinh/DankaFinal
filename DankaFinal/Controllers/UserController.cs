using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using DankaFinal.Models;
namespace DankaFinal.Controllers
{
    public class UserController : Controller
    {
        DanKaEDMEntities data = new DanKaEDMEntities();
        // GET: User






        [DataContract]
        public class RecaptchaResult
        {
            //attribute có tên trùng với field của json trả về
            [DataMember(Name = "success")]
            public bool Success { get; set; }

            //attribute có tên trùng với field của json trả về
            [DataMember(Name = "error-codes")]
            public string[] ErrorCodes { get; set; }
        }





        private bool IsValidRecaptcha(string recaptcha)
        {
            if (string.IsNullOrEmpty(recaptcha))
            {
                return false;
            }
            var secretKey = "6LcikxsjAAAAAGikOZDDcFoQzNTzDK3vVjBz1V78";//Mã bí mật
            string remoteIp = Request.ServerVariables["REMOTE_ADDR"];
            string myParameters = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, recaptcha, remoteIp);
            RecaptchaResult captchaResult;
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var json = wc.UploadString("https://www.google.com/recaptcha/api/siteverify", myParameters);
                var js = new DataContractJsonSerializer(typeof(RecaptchaResult));
                var ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
                captchaResult = js.ReadObject(ms) as RecaptchaResult;
                if (captchaResult != null && captchaResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }














        [HttpGet]
        public ActionResult Dangky()
        {
            
            return View();
        }



        [HttpPost]
        public ActionResult DangKy(FormCollection collection, khachhang kh)
        {


            if (IsValidRecaptcha(Request["g-recaptcha-response"]))
            {
                ViewBag.Status = "Mã Recaptcha hợp lệ";
            }
            else
            {
                ViewBag.Status = "Không hợp lệ";
            }



            var hoten = collection["HoTen"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["MatKhauNL"];
            var diachi = collection["DiaChi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["err1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["err2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (matkhau != matkhaunhaplai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không đúng";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["err5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["err6"] = "Phải nhập điện thoại";
            }
            else if (data.khachhangs.SingleOrDefault(n => n.tenDn == tendn) != null)
            {
                ViewBag.Thongbao = "Tên đăng nhập đã tồn tại";
            }
            else if (data.khachhangs.SingleOrDefault(n => n.email == email) != null)
            {
                ViewBag.Thongbao = "Email đã được sử dụng";
            }
            else
            {
                kh.hoTenkh = hoten;
                kh.tenDn = tendn;
                kh.matKhau = matkhau;
                kh.email = email;
                kh.diaChikh = diachi;
                kh.dienThoaikh = dienthoai;
                kh.ngaySinh = DateTime.Parse(ngaysinh);
                data.khachhangs.Add(kh);
                data.SaveChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }




        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection clq)
        {
            khachhang kh = (khachhang)Session["tenDn"];
            var hoTen = clq["tenDn"];
            var matKhau = clq["matKhau"];
            int state = int.Parse(Request.QueryString["id"]);
            if (kh != null)
            {
                Session["tenDn"] = kh;
                if (state == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("DatHang", "Giohang");
                }

            }
            if (String.IsNullOrEmpty(hoTen))
            {
                ViewData["er1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["er2"] = "Bạn chưa nhập mật khẩu";
            }
            else
            {
                khachhang user = data.khachhangs.SingleOrDefault(n => n.tenDn == hoTen && n.matKhau == matKhau);
                if (user is null)
                {
                    ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
                else
                {
                    ViewBag.thongbao = "Bạn đã đăng nhập thành công";
                    Session["tenDn"] = user;
                }
                return RedirectToAction("Index", "Home");
            }
            return View();

        }


    }
}