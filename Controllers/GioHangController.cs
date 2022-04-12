using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinhDoanhThuCung.Models;

namespace KinhDoanhThuCung.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        ThuCungDataContext data = new ThuCungDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;

            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(string sMATC, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.sMATC == sMATC);
            if (sanpham == null)
            {
                sanpham = new GioHang(sMATC);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSOLUONGMUA++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSOLUONGMUA);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dTHANHTIEN);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "KDThuCung");

            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(string matc)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang thucung = lstGioHang.SingleOrDefault(tc => tc.sMATC == matc);
            if(thucung != null)
            {
                lstGioHang.RemoveAll(tc => tc.sMATC == matc);
                return RedirectToAction("GioHang");
            }
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "KDThuCung");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(string matc, FormCollection f)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang thucung = lstGioHang.SingleOrDefault(tc => tc.sMATC == matc);
            if (thucung != null)
            {
                thucung.iSOLUONGMUA = int.Parse(f["txtSoLuongMua"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "KDThuCung");
        }
    }
}