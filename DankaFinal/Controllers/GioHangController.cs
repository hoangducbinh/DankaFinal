using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DankaFinal.Common;
using System.Web.UI.WebControls;
using DankaFinal.Models;

namespace DankaFinal.Controllers
{
    public class GiohangController : Controller
    {
        DanKaEDMEntities db = new DanKaEDMEntities();
        // GET: Giohang
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGiohang()
        {

            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang == null)
            {
                lstgiohang = new List<GioHang>();
                Session["GioHang"] = lstgiohang;
            }
            return lstgiohang;
        }
        public ActionResult ThemGioHang(int ms, string url)
        {
            if (Session["tenDn"] == null || Session["tenDn"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }

            List<GioHang> lstGiohang = LayGiohang();
            GioHang sp = lstGiohang.Find(n => n.maKhoahoc == ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                lstGiohang.Add(sp);
            }
            else
            {
                sp.soLuong++;
            }
            return Redirect(url);
        }
        private int TongSoLuong()
        {
            int sum = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                sum = lstgiohang.Sum(n => n.soLuong);
            }
            return sum;
        }
        private double TongTien()
        {
            double sum = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                sum = lstgiohang.Sum(n => n.thanhTien);
            }
            return sum;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstgiohang = LayGiohang();
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstgiohang);
        }
        public ActionResult XoaSPKhoiGioHang(int imakhoahoc)
        {
            List<GioHang> lstGioHang = LayGiohang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.maKhoahoc == imakhoahoc);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.maKhoahoc == imakhoahoc);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult Giohang_partial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult CapNhatGioHang(int imakhoahoc, FormCollection f)
        {
            List<GioHang> lstGiohang = LayGiohang();
            GioHang sp = lstGiohang.SingleOrDefault(n => n.maKhoahoc == imakhoahoc);
            if (sp != null)
            {
                sp.soLuong = int.Parse(f["Soluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["tenDn"] == null || Session["tenDn"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            //Kiểm tra có hàng trong giỏ chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<GioHang> lstGioHang = LayGiohang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            dondathang ddh = new dondathang();
            khachhang kh = (khachhang)Session["tenDn"];

            List<GioHang> lstGioHang = LayGiohang();
            ddh.maKh = kh.maKh;
            ddh.ngayDh = DateTime.Now;
            ddh.htThanhtoan = false;
            ddh.triGia = (decimal)TongTien();
            ddh.htGiaohang = true;
            db.dondathangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                ctdathang ctdh = new ctdathang();
                ctdh.soDh = ddh.soDh;
                ctdh.maKhoahoc = item.maKhoahoc;
                ctdh.soLuong = item.soLuong;
                ctdh.donGia = (decimal)item.donGia;
                ctdh.thanhTien = (decimal)item.thanhTien;
                db.ctdathangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}