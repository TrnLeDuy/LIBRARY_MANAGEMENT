using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website_Thu_Vien.Models
{
    public class GioHang
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Describe { get; set; }
        public object CategoryID { get; set; }
        public DateTime? Publish { get; set; }
        public string Publisher { get; set; }
        public string BookImage { get; set; }

        public GioHang(int iBookID)
        {
            using (CNPM_QLTVEntities db = new CNPM_QLTVEntities())
            {
                this.BookID = iBookID;
                Book sp = db.Book.Single(n => n.Book == iBookID);
                this.BookName = sp.BookName;
                this.Author = sp.Author;
                this.Describe = sp.Describe;
                this.CategoryID = sp.CategoryID;
                this.Publish = sp.Publish;
                this.Publisher = sp.Publisher;
                this.BookImage = sp.BookImage;


            }
        }


    }
}