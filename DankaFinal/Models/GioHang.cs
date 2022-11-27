using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DankaFinal.Models;

namespace DankaFinal.Models
{
    public class GioHang
    {
        DanKaEDMEntities db = new DanKaEDMEntities();
        public int maKhoahoc { get; set; }
        public string tenKhoahoc { get; set; }
        public string Anhbia { get; set; }
        public double donGia { get; set; }
        public int soLuong { get; set; }
        public double thanhTien
        {
            get { return soLuong * donGia; }
        }
        public GioHang(int edm)
        {
            maKhoahoc = edm;
            Khoahocedm kedm = db.Khoahocedms.Single(n => n.maKhoahoc == maKhoahoc);
            tenKhoahoc = kedm.tenKhoahoc;
            Anhbia = kedm.hinhMinhhoa;
            donGia = double.Parse(kedm.donGia.ToString());
            soLuong = 1;
        }

    }
}




