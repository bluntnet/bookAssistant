using BookAssistant.taobaoCSV;
using BookAssistant.Util;
using CefSharp;
using CefSharp.WinForms;
using CsvLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BookAssistant {
    public partial class XHForm : Form {
        private ChromiumWebBrowser browser;

        public static string hostUrl = "http://pub.xhsd.com.cn/";

        public static string webUrl = "http://pub.xhsd.com.cn/books/";
        public static string webViewUrl = "http://pub.xhsd.com.cn/books/views.asp";
        public static string SEARCH_HOST_URL = "http://pub.xhsd.com.cn/books/searchfull.asp";
        public static string QUERY_SEPARATE = "&amp;";
        //delegate string WebBroswerDelegate();
        public static string wbHtml = "";
        private string csvFileName = "";
        private string searchKey = "";
        private string searchWord = "";
        
        public string CsvFileName {
            get { return csvFileName; }
            set { csvFileName = value; }
        }
        public XHForm() {
            InitializeComponent();
            XHForm.CheckForIllegalCrossThreadCalls = false;

        }
        

        private void button1_Click(object sender, EventArgs e) {

            //MessageBox.Show("如果要搜索，请到网站上完成。谢谢！ ^_^");
            //return;



            //webBrowser1.Document.Write(client.RespHtml);
        }


        private void logger(string temp) {
            richTextBox1.AppendText(temp);
        }
        private void loggerLine(string temp) {
            richTextBox1.AppendText(temp + "\r\n");
        }
        private string getBookNameFromHtml(string html) {
            string key = "<td width=\"100%\" bgcolor=ccccff valign=\"top\" align=center colspan=\"2\">";
            string returnValue = "";
            string temp = html;
            if (html.IndexOf(key) != -1) {
                temp = html.Substring(html.IndexOf(key) + key.Length);
                returnValue = temp.Substring(5, temp.IndexOf("</td>") - 11);
            } else {
                MessageBox.Show("没有找到书名");
            }
            return returnValue;
        }
        private string getBookImageFromHtml(string bookHtml) {
            string imageHeade = "<td width=\"30%\" bgcolor=ffffff valign=\"center\" align=center colspan=\"1\">\r\n\r\n<img src=\"";
            string bookString = "";
            string bookImageUrl = "";
            if (bookHtml.IndexOf(imageHeade) != -1) {
                bookString = bookHtml.Substring(bookHtml.IndexOf(imageHeade)+ imageHeade.Length);
                bookImageUrl = bookString.Substring(0, bookString.IndexOf("\" width=\"300\"></img>"));
            } 
            if (bookImageUrl != "") {
                string imgName = Encrypt(bookImageUrl) + ".tbi";
                string imageFolder = csvFileName.Substring(0, csvFileName.LastIndexOf("."));
                if (!Directory.Exists(imageFolder)) {
                    Directory.CreateDirectory(imageFolder);
                }

                if (downfile(bookImageUrl, imageFolder + "\\" + imgName)) {
                    return imgName;
                } else {
                    return "";
                }
            }
            return "";

        }

        private string Encrypt(string pToEncrypt) {
            try {
                MD5 m = new MD5CryptoServiceProvider();
                byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(pToEncrypt));
                string t = BitConverter.ToString(s);
                string r = "";
                string[] ss = t.Split('-');
                for (int i = 0; i < ss.Length; i++) {
                    r += ss[i];
                }
                return r.ToLower();

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            return "";
        }
        private bool downfile(string url, string LocalPath) {

            string StrUrl = url;//文件下载网址
            string StrFileName = LocalPath;//下载文件保存地址 
            long lStartPos = 0; //返回上次下载字节
            long lCurrentPos = 0;//返回当前下载字节
            long lDownloadFile;//返回当前下载文件长度
            System.IO.FileStream fs;
            if (System.IO.File.Exists(StrFileName)) {
                System.IO.File.Delete(StrFileName);
                //lStartPos = fs.Length;
                //fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
                //移动文件流中的当前指针 
            } else {
                //fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
                lStartPos = 0;
            }
            fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
            lStartPos = 0;
            try {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(StrUrl);
                long length = request.GetResponse().ContentLength;
                lDownloadFile = length;
                if (lStartPos > 0)
                    request.AddRange((int)lStartPos); //设置Range值

                //向服务器请求，获得服务器回应数据流 
                System.IO.Stream ns = request.GetResponse().GetResponseStream();

                byte[] nbytes = new byte[512];
                int nReadSize = 0;
                nReadSize = ns.Read(nbytes, 0, 512);
                while (nReadSize > 0) {
                    fs.Write(nbytes, 0, nReadSize);
                    nReadSize = ns.Read(nbytes, 0, 512);
                    lCurrentPos = fs.Length;
                }

                fs.Close();
                ns.Close();
                return true;
            } catch {


                return false;
            } finally {
                fs.Close();
            }
        }
        private string getBookPriceFromHtml(string html) {
            string key = "<div><b> &nbsp; 【定价】</b></div>\r\n</td>\r\n<td width=\"67%\"> &nbsp; ￥";
            string returnValue = "";
            string temp = html;
            if (html.IndexOf(key) != -1) {
                temp = html.Substring(html.IndexOf(key) + key.Length);
                returnValue = temp.Substring(0, temp.IndexOf("</td>") - 1);
            } else {
                logger("没有找到定价");
            }
            return returnValue;
        }
        private string getBookISBNFromHtml(string html) {
            string key = "<div><b> &nbsp; 【I S B N】</b></div>\r\n</td>\r\n<td width=\"67%\"> &nbsp; ";
            string returnValue = "";
            string temp = html;
            if (html.IndexOf(key) != -1) {
                temp = html.Substring(html.IndexOf(key) + key.Length);
                returnValue = temp.Substring(0, temp.IndexOf("</td>"));
            } else {
                logger("没有找到ISBN");
            }
            return returnValue;
        }
        /**
         * <a href="searchfull.asp?plu_title=%E4%BA%BA%E6%B0%91%E5%8D%AB%E7%94%9F%E5%87%BA%E7%89%88%E7%A4%BE&ctype=0&plu_key=0x62c4ec0ee5380546dd23545b1c4d5ad9"">人民卫生</a>   &nbsp; <a href="javascript:movemaps(11642917,3985772,'人民卫生出版社')"><img src="../images/ditu_a.gif" border="0" alt="点击查看 [人民卫生出版社] 电子地图"></a>
         */
        private string getPublishHouse(string html) {
            string key = "<div><b> &nbsp; 【出版社】</b></div>\r\n</td>\r\n<td width=\"67%\"> &nbsp; ";
            string returnValue = "国内";
            string temp = html;
            if (html.IndexOf(key) != -1) {
                temp = html.Substring(html.IndexOf(key) + key.Length);
                returnValue = temp.Substring(0, temp.IndexOf("</td>"));
                int start = returnValue.IndexOf(">");
                int end = returnValue.IndexOf("</a>");
                if (start > 0 && end > 0) {
                    returnValue = returnValue.Substring(start + 1, end - start - 1);
                }

            } else {
                logger("没有找到出版社");
            }
            return returnValue;
        }

        private string getBookInfo(string html) {

            string key = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" bgcolor=\"#ccccff\">\r\n<tr bgcolor=#ffffff>\r\n<td width=\"33%\">\r\n<div><b> &nbsp; 【I S B N】</b></div>";
            string returnValue = "";
            string endString = "<td width=\"33%\">\r\n<div><b> &nbsp; 【中图分类】</b></div>\r\n</td>\r\n<td width=\"67%\">";
            string temp = html;
            if (html.IndexOf(key) != -1) {
                temp = html.Substring(html.IndexOf(key));
                if (temp.IndexOf(endString) != -1) {
                    returnValue = temp.Substring(0, temp.IndexOf(endString)) + "</table>";
                } else {
                    logger("没有找到Book简介");
                }
            } else {
                logger("没有找到Book简介");
            }
            return returnValue;
        }

        private void button3_Click(object sender, EventArgs e) {
            saveCsvFile.Filter = "csv文件(*.csv)|*.csv";
            if (saveCsvFile.ShowDialog() == DialogResult.OK) {
                CsvFileName = saveCsvFile.FileName;

                try {
                    button3.Enabled = false;
                    richTextBox1.Text = "";
                    GetWebBroswerHtml();
                    

                    Thread runTaskThread = new Thread(new ThreadStart(downloadTask));
                    runTaskThread.Start();
                } catch (Exception ee) {
                    MessageBox.Show("如果问题请联系13811110041\r\n" + ee.ToString());
                }
            }
        }

        private void GetWebBroswerHtml()
        {
            //browser.ViewSource();
            
            browser.GetSourceAsync().ContinueWith( code =>
            {
                wbHtml = code.Result;
            });
            while (wbHtml.Length < 10) {
                Thread.Sleep(100);
            }
            //MessageBox.Show(wbHtml.Substring(0,20));
        }

        private void downloadTask() {

            //Console.Write(webBrowser1.DocumentText);

            //string htmlString = webBrowser1.DocumentText;

            string htmlString = wbHtml;
            string formString = "";
            string formStart = "<form method=\"POST\" action=\"searchfull.asp?plu_title=";
            if (htmlString.IndexOf(formStart) == -1) {
                logger("没有找到Form起点！");
                return;
            }
            formString = htmlString.Substring(htmlString.IndexOf(formStart));
            if (formString.IndexOf("</form>") == -1) {
                logger("没有找到Form终点！");
                return;
            }
            formString = formString.Substring(0, formString.IndexOf("</form>"));
            Console.WriteLine("formString========" + formString);
            string searchString = "";
            string totalPageString = "";
            if (formString.IndexOf("共") == -1) {
                logger("没有找到总页数！");
                return;
            }
            totalPageString = formString.Substring(formString.IndexOf("共") + 1);
            if (formString.IndexOf("页") == -1) {
                logger("没有找到总页数！");
                return;
            }
            totalPageString = totalPageString.Substring(0, totalPageString.IndexOf("页")).Trim();
            loggerLine("共 " + totalPageString + "页 ");
            int totalPage = Convert.ToInt32(totalPageString);
            Console.WriteLine("totalPage====" + totalPage);
            if (formString.IndexOf("searchfull.asp") == -1) {
                logger("关键字没找到");
                return;
            }
            searchString = formString.Substring(formString.IndexOf("searchfull.asp"));
            if (formString.IndexOf(QUERY_SEPARATE+"page=") == -1) {
                logger("关键字没找到！");
                return;
            }
            searchString = searchString.Substring(0, searchString.IndexOf(QUERY_SEPARATE+"page="));
            Console.WriteLine("searchString====" + searchString);
            string orderString = "";
            if (formString.IndexOf("order=") == -1) {
                logger("Order没有找到！");
                return;
            }
            orderString = formString.Substring(formString.IndexOf("order=") + 6);
            if (formString.IndexOf("\">") == -1) {
                logger("关键字没找到！");
                return;
            }
            orderString = orderString.Substring(0, orderString.IndexOf("\">"));

            Console.WriteLine("orderString====" + orderString);

            string pageHtml = "";
            List<string> urlList = new List<string>();
            int[] pageArray = new int[] { 1, totalPage };
            string pageNumber = pageRange.Text;
            if (pageNumber != "") {
                string[] tempArray = pageNumber.Split('-');
                if (tempArray.Length == 1) {
                    pageArray[0] = Convert.ToInt32(tempArray[0]);
                    pageArray[1] = Convert.ToInt32(tempArray[0]);
                } else {
                    pageArray[0] = Convert.ToInt32(tempArray[0]);
                    pageArray[1] = Convert.ToInt32(tempArray[1]);
                }
            }
            for (int i = Math.Max(pageArray[0], 1); i <= Math.Min(pageArray[1], totalPage); i++) {
                logger("查找第" + i + "页的图书...");
                pageHtml = getPageContent(searchString, i, orderString);
                string detailPage = "href=\"views.asp";

                while (pageHtml.IndexOf(detailPage) != -1) {

                    pageHtml = pageHtml.Substring(pageHtml.IndexOf(detailPage) + detailPage.Length);
                    string url = pageHtml.Substring(0, pageHtml.IndexOf("\" target="));
                    urlList.Add(webViewUrl + url);
                }


                loggerLine("完成!");
            }


            /////////////
            TBCSV csv = new TBCSV();
            DataTable mydt = csv.getDefaultDataTable();


            string[] values = TBCSV.getTaobaoDefaultValue();

            // WebUtil client = new WebUtil();
            string htmlTemp = "";
            int m = 1;
            foreach (string urlstring in urlList) {
                logger(urlstring + "第[" + m + "]本书..");
                try {

                    htmlTemp = HttpRequestUtil.requestURL(urlstring, "utf-8");
                    if (htmlTemp == null || htmlTemp == "") {
                        logger("出错，请手动下载！！");
                        continue;
                    }
                    m++;
                    DataRow dr = mydt.NewRow();
                    for (int j = 0; j < values.Length; j++) {
                        dr[j] = values[j];
                    }
                    string bookName = getBookNameFromHtml(htmlTemp);//书名
                    dr["title"] = bookName;
                    //logger(dr[0]+">>");
                    //Console.WriteLine("name==" + dr[0]);

                    dr["price"] = getBookPriceFromHtml(htmlTemp);

                    string author = getAuthorFromHtml(htmlTemp);
                    //Console.WriteLine("price==" + dr[7]);
                    string ISBN = getBookISBNFromHtml(htmlTemp);
                    dr["outer_id"] = ISBN;//商家编吗isbn 
                    string publishHouse = getPublishHouse(htmlTemp);//出版社
                    //下面两行的赋值顺序不能改动
                    //dr["inputPids"] = "1636953,2043183,2043189,122216620";
                    dr["inputPids"] = "1636953,2043183,2043189,122216620";
                    dr["inputValues"] = ISBN.Replace("-", "") + "," + bookName + "," + author + "," + publishHouse;

                    //dr["inputValues"] = ISBN.Replace("-", "");

                    string desc = getBookInfo(htmlTemp);//描述
                    string bookNameHtml = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" bgcolor=\"#ccccff\">\r\n<tr bgcolor=#ccccff>\r\n<td>" + bookName + "</td></tr></table>";
                    dr["description"] = bookNameHtml + desc;//描述

                    //Console.WriteLine("isbn==" + dr[40]);
                    string bookImage = getBookImageFromHtml(htmlTemp);
                    if (bookImage != "") {
                        bookImage = bookImage.Substring(0, bookImage.Length - 4) + ":1:0:|;";
                        logger("图片下载成功...");
                    } else {
                        logger("图片下载失败...");
                    }
                    dr["picture_status"] = "1;";
                    dr["picture"] = bookImage;
                    mydt.Rows.Add(dr);
                } catch (Exception e) {
                    loggerLine(e.ToString());
                }
                loggerLine("下载完成!");
            }

            CsvStreamWriter csw = new CsvStreamWriter(CsvFileName, System.Text.Encoding.Unicode);
            csw.QuoteMark(TBCSV.getQuoteMark());
            csw.AddData(mydt, 1);
            csw.FirstRowQuote = 2;
            csw.Save();
            MessageBox.Show("保存成功,生成" + (mydt.Rows.Count - 1) + "条数据");
            button3.Enabled = true;


        }
        private string getPageContent(string url, int pageNumber, string order) {
            order = order.Replace("&amp;", "&");
            string clientUrl = webUrl + url + "&page=" + pageNumber + "&order=" + order;

            return HttpRequestUtil.requestURL(clientUrl, "utf-8");
        }

        private string getAuthorFromHtml(string htmlTemp) {
            //Regex regex1 = new Regex(@"<tr onMouseOver=""style.background='#eeffff'"" onMouseOut=""style.background='#ffffff'"">\r\n<td width=""10%"" align=center>\r\n\r\n<img src=""(?<imageUrl>[\s\S]*?)"" width=""90"" height=""120""></img>\r\n\r\n</td>\r\n<td width=""70%"" align=center>\r\n<table width=""95%"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT""><br><h3><a href=""(?<bookUrl>[\s\S]*?)"" target=""_blank"">(?<bookName>[\s\S]*?)</a></td></tr>\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT"">(?<isbn>[\s\S]*?)</td>\r\n</tr>\r\n<tr>\r\n<td WIDTH=""95%"" ALIGN=""LEFT"">(?<version>[\s\S]*?)</td>\r\n</tr>\r\n<tr>\r\n<td width=""95%"" height=""3"">\r\n<hr noshade size=""1"">\r\n</td>\r\n</tr>\r\n</table>\r\n</td>");
            Regex regex = new Regex(@"<tr bgcolor=#ffffff>\r\n<td width=""33%"">\r\n<div><b> &nbsp; 【作者】</b></div>\r\n</td>\r\n<td width=""67%""> &nbsp; <a href=""(?<searchUrl>[\s\S]*?)"""">(?<author>[\s\S]*?)</a></td>\r\n</tr>");

            Match match = regex.Match(htmlTemp);
            string author = "";
            if (match.Success) {
                author = match.Groups["author"].Value;
            }
            return author;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void Debug(string msg) => System.Diagnostics.Debug.WriteLine(msg);

        private void XHForm_Load(object sender, EventArgs e) {
            
            initBrowswer();
        }

        private void initBrowswer()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser(hostUrl);
            browser.Parent = groupBox1;
            browser.Dock = DockStyle.Fill;
        }

        private void searchBtn_Click(object sender, EventArgs e) {

            browser.Load(hostUrl);
        }
    }
}