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
        private Image image;
        private string name;
        private string isbn;
        private string desc;

        public Image Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Desc { get => desc; set => desc = value; }
    }
}
