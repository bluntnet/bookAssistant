using BookAssistant.taobaoCSV;
using BookAssistant.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace BookAssitant.Util {
    class ParseList {
        /*
        string searchWord;
        string searchKey;
        private void SearchTask() {
            string searchKeyContent = HttpRequestUtil.requestURL(SEARCH_HOST_URL);

            //searchWord = keywordTB.Text;
            if (searchWord.Trim() == "") {
                //MessageBox.Show("请输入搜索的内容");
                return;
            }
            searchKey = GetHXSDFromContent(searchKeyContent);

            ShowSearchDisplayResultByPage(1);

        }

        private void searchBtn_Click(object sender, EventArgs e) {


        }
        private string GetHXSDFromContent(string content) {
            //<input type="hidden" name="plu_key" size="50" value="0x288d2fb0716b092d037f0d678aa9dd43" class=input>

            string keyInputStart = "<input type=\"hidden\" name=\"plu_key\" size=\"50\" value=\"";
            string temp = content.Substring(content.IndexOf(keyInputStart));
            return temp.Substring(keyInputStart.Length, 34);
        }

        private List<BookInfo> GetSearchFromContentByPage(int pageNumber) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("plu_title", searchWord);
            dict.Add("plu_key", searchKey);
            dict.Add("page", pageNumber + "");
            dict.Add("order", 2 + "");
            string searchContent = HttpRequestUtil.requestURL(SEARCH_HOST_URL, dict);
            return ParseContnetToBookList(searchContent);
        }


        private void ShowSearchDisplayResultByPage(int pageNumber) {
            List<BookInfo> list = GetSearchFromContentByPage(pageNumber);
            DataTable dt = CreateBookDataTable();
            foreach (BookInfo book in list) {
                CreateDataRow(dt, book);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["书名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private List<BookInfo> ParseContnetToBookList(string searchContent) {
            List<BookInfo> list = new List<BookInfo>();

            Regex regex1 = new Regex(@"<tr onMouseOver=""style.background='#eeffff'"" onMouseOut=""style.background='#ffffff'"">\r\n<td width=""10%"" align=center>\r\n\r\n<img src=""(?<imageUrl>[\s\S]*?)"" width=""90"" height=""120""></img>\r\n\r\n</td>\r\n<td width=""70%"" align=center>\r\n<table width=""95%"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT""><br><h3><a href=""(?<bookUrl>[\s\S]*?)"" target=""_blank"">(?<bookName>[\s\S]*?)</a></td></tr>\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT"">(?<isbn>[\s\S]*?)</td>\r\n</tr>\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT"">(?<version>[\s\S]*?)</td>\r\n</tr>\r\n<tr>\r\n<td width=""95%"" height=""3"">\r\n<hr noshade size=""1"">\r\n</td>\r\n</tr>\r\n</table>\r\n</td>");

            MatchCollection mc = regex1.Matches(searchContent);


            foreach (Match match in mc) {
                BookInfo book = new BookInfo();
                //string imageUrl = 
                book.ImageUrl = match.Groups["imageUrl"].Value;
                book.BookUrl = match.Groups["bookUrl"].Value;
                book.Name = match.Groups["bookName"].Value;
                book.Desc = match.Groups["isbn"].Value + "" + match.Groups["version"].Value;
                list.Add(book);
            }
            return list;
        }

        private static void CreateDataRow(DataTable dt, BookInfo book) {
            DataRow dr = dt.NewRow();
            if (book.ImageUrl != null && book.ImageUrl.Length > 10) {
                Stream imagestream = HttpWebRequest.Create(book.ImageUrl).GetResponse().GetResponseStream();
                book.Image = Image.FromStream(imagestream);
                dr["缩略图"] = new Bitmap(book.Image, 45, 60);
            }
            dr["书名"] = HtmlUtils.ReplaceHtmlTag(book.Name);
            dr["isbn"] = book.Desc;
            dt.Rows.Add(dr);
        }

        private DataTable CreateBookDataTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("缩略图", typeof(Bitmap));
            dt.Columns.Add("书名", typeof(string));
            dt.Columns.Add("isbn", typeof(string));
            dataGridView1.RowTemplate.Height = 60;
            return dt;
        }
        */
    }
}
