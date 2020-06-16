using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAssistant.taobaoCSV
{
    class BookInfo
    {
        public static string BOOK_VIEW = "http://pub.xhsd.com.cn/books/";
        private string imageUrl;
        private Image image;
        private string name;
        private string isbn;
        private string desc;
        private string bookUrl;

        public Image Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Desc { get => desc; set => desc = value; }
        public string BookUrl { get => bookUrl; set => bookUrl = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}
