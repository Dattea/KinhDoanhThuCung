using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinhDoanhThuCung.Models;
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;
namespace KinhDoanhThuCung.Controllers
{
    public class KDThuCungController : Controller
    {
        ThuCungDataContext data = new ThuCungDataContext();
        private List<THUCUNG> hotPet(int count)
        {
            return data.THUCUNGs.OrderByDescending(tc => tc.SLTON).Take(count).ToList();
        }
        public ActionResult Index(int? page)
        {
            int pagesize = 5;
            int pagenum = (page ?? 1);
            var hotpet = hotPet(6);
            return View(hotpet.ToPagedList(pagenum, pagesize));
        }
        public ActionResult Nhacungcap()
        {
            var nhacungcap = from ncc in data.NCCGIONGs select ncc;
            return PartialView(nhacungcap);
        }
        public ActionResult Loai()
        {
            var loai = from l in data.LOAIs select l;
            return PartialView(loai);
        }
        public ActionResult NCC()
        {
            var nhacungcap = from ncc in data.NCCGIONGs select ncc;
            return PartialView(nhacungcap);
        }
        public ActionResult TCtheoLoai(int? page, string id)
        {
            int pagesize = 5;
            int pagenum = (page ?? 1);
            var thucung = from tc in data.THUCUNGs where tc.GIONG.MALOAI == id.ToString() select tc;
            return View(thucung.ToPagedList(pagenum, pagesize));
        }
        public ActionResult TCtheoNCC(int? page, string id)
        {
            int pagesize = 5;
            int pagenum = (page ?? 1);
            var thucung = from tc in data.THUCUNGs where tc.MANCC == id select tc;
            return View(thucung.ToPagedList(pagenum, pagesize));
        }
        public ActionResult Details(string id)
        {
            var thucung = from tc in data.THUCUNGs
                       where tc.MATC == id
                       select tc;
            return View(thucung.Single());
        }
        
    }
}