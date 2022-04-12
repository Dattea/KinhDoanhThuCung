using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinhDoanhThuCung.Models
{
    public class GioHang
    {
        ThuCungDataContext data = new ThuCungDataContext();
        public string sMATC { set; get; }
        public string sMAGIONG { set; get; }
        public string sTENLOAI { set; get; }
        public string sANHTC { set; get; }
        public string sTENGIONG { set; get; }
        public Double dDONGIA { set; get; }
        public int iSOLUONGMUA { set; get; }
        public Double dTHANHTIEN
        {
            get { return iSOLUONGMUA * dDONGIA; }
        }
        public GioHang(string MATC)
        {
            sMATC = MATC;
            THUCUNG thucung = data.THUCUNGs.Single(n => n.MATC == sMATC);
            sMAGIONG = thucung.GIONG.MAGIONG;
            sTENLOAI = thucung.GIONG.LOAI.TENLOAI;
            sANHTC = thucung.ANH;
            sTENGIONG = thucung.GIONG.TENGIONG;
            dDONGIA = double.Parse(thucung.DONGIA.ToString());
            iSOLUONGMUA = 1;
        }
    }
}