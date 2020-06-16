using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CsvLib;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BookAssistant.Util;
using LitJson;
using System.Globalization;
using BookAssistant.taobaoCSV;

namespace BookAssistant
{
    public partial class DanDanForm : Form
    {
        DataTable mydt = null;
        Thread runTaskThread = null;
        string fileName = "";
        string rootfolder = "";
        string imageFolder = "";
        DataTable duplicateDt = null;
        DataTable rewriteDT = null;
        string[] taobaoCSVHead = TBCSV.getTaobaoCSVHead();
        string[] taobaoCSVDefault = TBCSV.getTaobaoDefaultValue();
        int[] quoteMark = TBCSV.getQuoteMark();
        delegate void GetBookContentDelegate(DataTable mydt, ProgressBar pb);

        //int 
        string dangdangUrl = "http://search.dangdang.com/search_pub.php?key=";
        string dangdangProductUrl = "http://product.dangdang.com/product.aspx?product_id=";

        //string dangdangUrl = "http://search.dangdang.com/search.php?key=";
        //string dangdangProductUrl = "http://127.0.0.1/download.html";

        public DanDanForm()
        {
            InitializeComponent();
            DanDanForm.CheckForIllegalCrossThreadCalls = false;
            // System.Windows.Forms.CheckForIllegalCrossThreadCalls(
        }

        private void openFile_Click(object sender, EventArgs e)
        {

            //csvFileDialog.InitialDirectory = "d:\\";
            csvFileDialog.Filter = "csv file(*.csv)|*.csv";
            if (csvFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = csvFileDialog.FileName;
                textBox1.Text = fileName;
                DanDanForm.ActiveForm.Text = "图书辅助管理软件--" + fileName;
                //inputFileName.Text = fileName;
                //DataTable csvDt = new DataTable();
                rootfolder = fileName.Substring(0, fileName.LastIndexOf("\\"));
                imageFolder = fileName.Substring(fileName.LastIndexOf('\\') + 1, fileName.LastIndexOf(".") - fileName.LastIndexOf('\\') - 1) + "新下载图片";
                CsvStreamReader read = new CsvStreamReader(fileName);
                if (read.RowCount == 0)
                {
                    MessageBox.Show("空文件");
                    
                    if (csvView.Rows.Count > 0)
                    {
                        csvView.DataSource = new DataTable();
                    }
                    if (mydt != null)
                    {
                        mydt.Clear();
                    }
                    return;
                }
                else
                {
                    mydt = read[1, read.RowCount, 1, read.ColCount];
                    csvView.DataSource = mydt;
                    MessageBox.Show("导入成功，共导入" + mydt.Rows.Count + "条数据");
                }

            }
            else
            {
                fileName = "";
                MessageBox.Show("请选择文件!");
            }
        }
        public void snatch()
        {

            // this.progressBar1.Value = 100;
            int mydtcount = mydt.Rows.Count - 1;
            for (int i = 1; i < mydt.Rows.Count; i++)
            {

                int percent = (int)(((float)i / (mydt.Rows.Count - 1)) * 100);

                progressBar1.Value = percent;
                string bookContent = "";
                string bookImage = "";
                DataRow dr = mydt.Rows[i];
                string shopCode = dr[40].ToString();
                try
                {
                    if (dangdangAvailable.Checked)
                    {

                        if (picChk.Checked || contentChk.Checked)
                        {
                            Console.WriteLine("shopCode==" + shopCode);
                            string[] bookInfo = getBookURL_FromDanDanByCode(shopCode);
                            if (bookInfo == null)
                            {
                                logger.AppendText("第" + i + "本 共" + mydtcount + "本书号:[" + shopCode + "] 没有找到" + "\r\n");
                                continue;
                            }
                            string bookHTML = getContent(dangdangProductUrl + bookInfo[0]);
                            Console.WriteLine(dangdangProductUrl + bookInfo[0]);
                            //MessageBox.Show("LocahImage="+bookImage);

                            if (picChk.Checked)
                            {
                                //folder  File folderfile=File(
                                //System.IOCompletionCallback
                                System.IO.DirectoryInfo fileFolder = new DirectoryInfo(imageFolder);
                                if (!fileFolder.Exists)
                                {
                                    fileFolder.Create();
                                }
                                bookImage = getBookImageFromHTML(bookHTML);
                                // MessageBox.Show("bookImage="+bookImage);
                                if (bookImage != "")
                                {
                                    logger.AppendText("第" + i + "本 共" + mydtcount + "本《" + dr[0].ToString() + "》书号:[" + shopCode + "] 图片抓取成功" + "\r\n");
                                    bookImage = bookImage.Substring(0, bookImage.Length - 4) + ":0:0:|;";
                                    mydt.Rows[i][35] = bookImage;
                                }
                                else
                                {
                                    logger.AppendText("第" + i + "本 共" + mydtcount + "本《" + dr[0].ToString() + "》书号:[" + shopCode + "] 图片抓取成功" + "\r\n");
                                }
                            }
                            if (contentChk.Checked)
                            {
                                
                                string bookInfo1 = getBookContentFromBookHTML(bookHTML);
                                

                                bookContent =bookInfo1+ getBookContentFromShopCode(bookInfo[0]);
                                if (bookContent != "")
                                {
                                    logger.AppendText("第" + i + "本 共" + mydtcount + "本《" + dr[0].ToString() + "》书号:[" + shopCode + "] 简介抓取成功" + "\r\n");
                                    mydt.Rows[i][24] = bookContent;
                                }
                                else
                                {
                                    logger.AppendText("第" + i + "本 共" + mydtcount + "本《" + dr[0].ToString() + "》书号:[" + shopCode + "] 简介抓取失败" + "\r\n");
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("啥也不抓？你傻了吧。");
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.AppendText("发生错误，请检查:" + e.ToString() + "\r\n");
                }
            }
            this.csvView.DataSource = mydt;
            MessageBox.Show("抓取已经完成，此软件为测试版本，请注意备份！");
            button1.Enabled = true;

        }

        private string getBookContentFromShopCode(string shopCode)
        {
            Random random=new Random();
            random.NextDouble();
            string url = "http://product.dangdang.com/callback.php?type=detail&product_id=" + shopCode + "&page_type=book&t=" + random.NextDouble();
            Console.WriteLine("bookContent========" + url);
            string jsonString = getContent(url);
            //Console.Write(jsonString);
            JsonData jd = JsonMapper.ToObject(jsonString);
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("content========" + jd["content"]["show"]);
            Console.WriteLine("abstract========" + jd["abstract"]["show"]);
            Console.WriteLine("authorintro========" + jd["authorintro"]["show"]);
            Console.WriteLine("catalog========" + jd["catalog"]["show"]);
            Console.WriteLine("mediafeedback========" + jd["mediafeedback"]["show"]);
            //Console.WriteLine("done=="+convertUnicodeToNormal((string)jd["content"]["show"]));
            sb.AppendLine((string)jd["content"]["show"]);
            sb.AppendLine((string)jd["abstract"]["show"]);
            sb.AppendLine((string)jd["authorintro"]["show"]);
            sb.AppendLine((string)jd["catalog"]["show"]);
            return sb.ToString();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (mydt == null || mydt.Rows.Count == 0)
            {
                MessageBox.Show("请先导入需要抓取的内容");
                button1.Enabled = true;
                return;
            }
            try
            {
                runTaskThread = new Thread(new ThreadStart(snatch));
                // CheckForIllegalCrossThreadCalls 
                runTaskThread.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (mydt == null || mydt.Rows.Count == 0)
            {
                MessageBox.Show("请先导入需要抓取的内容");
                return;
            }
            DataTable tempDt = mydt.Clone();

            for (int i = 0; i < mydt.Rows.Count; i++)
            {


                DataRow dr = mydt.Rows[i];

                if (dr[24].ToString() != "")
                {
                    tempDt.ImportRow(mydt.Rows[i]);
                }
            }
            //MessageBox.Show(tempDt.Columns.Count+"=="+mydt.Rows[3].ItemArray.Length);


            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                //saveCsvFile.Filter = "csv file|*.csv";
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1 };


                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                //csw.QuoteMark(quoteMark);
                csw.AddData(tempDt, 1);
                csw.Save();
                MessageBox.Show("保存成功,生成" + (tempDt.Rows.Count - 1) + "条数据");
            }
            else
            {
                MessageBox.Show("没保存");
            }


        }
        private string[] getBookURL_FromDanDanByCode(string code)
        {

            string url = dangdangUrl + code;
            string bookSearch = getContent(url);
            Console.Write("ShowCode==" + url);
            if (bookSearch == null || bookSearch.IndexOf("<li class=\"maintitle\">") == -1)
            {
                return null;
            }
            bookSearch = bookSearch.Substring(bookSearch.IndexOf("<li class=\"maintitle\">"));
            bookSearch = bookSearch.Substring(0, bookSearch.IndexOf("</li>"));

            bookSearch = bookSearch.Substring(bookSearch.IndexOf("product_id=") + 11);
            string productID = bookSearch.Substring(0, bookSearch.IndexOf("&ref=search-1-pub"));

            string ss="name=\"p_name\">";

            bookSearch = bookSearch.Substring(bookSearch.IndexOf(ss) + ss.Length+1);
            string bookName = bookSearch.Substring(0, bookSearch.IndexOf("</a>"));
            return new string[] { productID, bookName };
        }
        

        private string getBookContentFromBookHTML(string bookHTML)
        {

            string bookString = bookHTML;// getContent(dangdangProductUrl + url);
            //string imagePath = getBookImageFromHTML(bookString);

            //string ImgBtnChgPrd_Click
            // string bookString=bookString.Substring(bookString.IndexOf("ImgBtnChgPrd_Click"));

            string bookDetail = bookString.Substring(bookString.IndexOf("<div class=\"book_detailed\" name=\"__Property_pub\">"));
            //取得图片
            //string ImgBtnChgPrd_Click
            //取得信息
            string bookInfo = "<h4>图书信息</h4><br>" + bookDetail.Substring(0, bookDetail.IndexOf("</div>"));
            //取得定价

            //MessageBox.Show() + "");
            string bookPrice = bookString.Substring(bookString.IndexOf("<p class=\"price_m\">定　　价：￥"));
            bookPrice = bookPrice.Substring(0, bookPrice.IndexOf("<span>")) ;


            //bookString = bookString.Substring(bookString.IndexOf("<div id=\"__zhinengbiaozhu_bk\">"));
            //string bookContent ="<h4>目录</h4><br>"+ bookString.Substring(0, bookString.IndexOf("<div class=\"empty_box\"></div>"));

            return bookInfo + "<br><br>" + bookPrice+"<br>";// "<br><br><b>本书目录</b>" + bookContent;

            // return bookHTML;
        }

        private string getBookImageFromHTML(string bookHtml)
        {
            //MessageBox.Show("Index=" + bookHtml.IndexOf("name=\"bigpicture_bk\"") + "|" + bookHtml.IndexOf("id=\"img_show_prd\""));
            string keyString = "name=\"__bigpic_pub\"><img src=\"";
            string bookString = bookHtml.Substring(bookHtml.IndexOf(keyString) + keyString.Length);
            string bookImageUrl = bookString.Substring(0, bookString.IndexOf("\" alt=\"\""));
            //MessageBox.Show(bookImageUrl);
            //int length=bookImageUrl.LastIndexOf(".");
            Console.WriteLine("bookImageUrl==" + bookImageUrl);

            //string bookImageUrl = "http://127.0.0.1/Pic_00120.jpg";
            string imgPath = Encrypt(bookImageUrl) + ".tbi";
            //MessageBox.Show("imgPath" + imgPath);
            //MessageBox.Show(bookImageUrl+"|"+imgPath);
            if (downfile(bookImageUrl, rootfolder + "\\" + imageFolder + "\\" + imgPath))
            {
                return imgPath;
            }
            else
            {
                return "";
            }
        }
        public static string getContent(string url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;

                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch
            {
                throw;
            }
            return strResult;
        }
        private void pauseBtn_Click(object sender, EventArgs e)
        {
            if (runTaskThread != null && runTaskThread.IsAlive)
            {
                if (runTaskThread != null && runTaskThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    runTaskThread.Suspend();
                    pauseBtn.Text = "运行";
                }
                else
                {
                    runTaskThread.Resume();
                    pauseBtn.Text = "暂停";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mydt == null || mydt.Rows.Count == 0)
            {
                MessageBox.Show("请先导入需要抓取的内容");
                return;
            }
            DataTable tempDt = mydt.Clone();

            for (int i = 0; i < mydt.Rows.Count; i++)
            {

                DataRow dr = mydt.Rows[i];
                if (dr[24].ToString() == "" || i == 0)
                {
                    tempDt.ImportRow(mydt.Rows[i]);
                }
            }

            //MessageBox.Show(tempDt.Columns.Count+"=="+mydt.Rows[3].ItemArray.Length);

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                //saveCsvFile.Filter = "csv file|*.csv";
                string filename = saveCsvFile.FileName;
                /*string[] taobaoCSVHead ={ "宝贝名称", "宝贝类目", "店铺类目", "新旧程度", "省", "城市", "出售方式", "宝贝价格", "加价幅度", "宝贝数量", 
                    "有效期", "运费承担", "平邮", "EMS", "快递", "付款方式","支付宝", "发票", "保修", "自动重发", 
                    "放入仓库", "橱窗推荐", "开始时间", "心情故事", "宝贝描述","宝贝图片","宝贝属性","团购价","最小团购件数",
                    "邮费模版ID","会员打折","修改时间","上传状态","图片状态","返点比例","新图片","视频","销售属性组合","用户输入ID串",
                    "用户输入名-值对","商家编码","销售属性别名","宝贝编号"};
               // int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 
                                   1, 1, 1, 1, 1,1,1,1,1,1,
                                   1,1,1,1};
                */
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(tempDt, 1);
                csw.Save();
                MessageBox.Show("保存成功,生成" + (tempDt.Rows.Count - 1) + "条数据");

            }
            else
            {
                MessageBox.Show("没保存");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (mydt == null || mydt.Rows.Count == 0)
            {
                MessageBox.Show("请先导入基础csv数据文件!");
                return;
            }
            if (duplicateDt == null || duplicateDt.Rows.Count == 0)
            {
                MessageBox.Show("请导入需要去除的重复的csv文件!");
            }

            DataTable tempDt = duplicateDt.Clone();

            for (int i = 0; i < duplicateDt.Rows.Count; i++)
            {

                DataRow dr = duplicateDt.Rows[i];

                if (chkName.Checked && chkPrice.Checked)
                {
                    DataColumn[] dc = new DataColumn[] { mydt.Columns[0], mydt.Columns[7] };
                    mydt.PrimaryKey = dc;
                    if (!isSameRecord(mydt, new Object[] { dr[0].ToString(), dr[1].ToString() }))
                    {
                        tempDt.ImportRow(dr);
                    }

                }
                else if (chkName.Checked)
                {
                    //mydt
                    mydt.PrimaryKey = new DataColumn[1] { mydt.Columns[0] };
                    if (!isSameRecord(mydt, new Object[] { dr[0].ToString() }))
                    {
                        tempDt.ImportRow(dr);
                    }
                }
                else if (chkPrice.Checked)
                {
                    mydt.PrimaryKey = new DataColumn[1] { mydt.Columns[7] };
                    if (!isSameRecord(mydt, new Object[] { dr[7].ToString() }))
                    {
                        tempDt.ImportRow(dr);
                    }
                }
            }

            //MessageBox.Show(tempDt.Columns.Count+"=="+mydt.Rows[3].ItemArray.Length);

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                //saveCsvFile.Filter = "csv file|*.csv";
                string filename = saveCsvFile.FileName;
               // int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(tempDt, 1);
                csw.FirstRowQuote = 2;
                csw.Save();
            }
            else
            {
                MessageBox.Show("没保存");
            }
            MessageBox.Show("共生成" + (tempDt.Rows.Count - 1) + "条数据");
        }
        private bool isSameRecord(DataTable dt, object[] name)
        {


            return dt.Rows.Contains(name);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (runTaskThread != null)
            {
                try
                {
                    runTaskThread.Abort();
                }
                catch (ThreadStateException)
                {
                    runTaskThread.Resume();
                }

                button1.Enabled = true;
                progressBar1.Value = 0;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void eraseLogger_Click(object sender, EventArgs e)
        {
            logger.Text = "";
        }
        private bool downfile(string url, string LocalPath)
        {

            string StrUrl = url;//文件下载网址
            string StrFileName = LocalPath;//下载文件保存地址 
            string strError;//返回结果
            long lStartPos = 0; //返回上次下载字节
            long lCurrentPos = 0;//返回当前下载字节
            long lDownloadFile;//返回当前下载文件长度
            System.IO.FileStream fs;
            if (System.IO.File.Exists(StrFileName))
            {
                System.IO.File.Delete(StrFileName);
                //lStartPos = fs.Length;
                //fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
                //移动文件流中的当前指针 
            }
            else
            {
                //fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
                lStartPos = 0;
            }
            fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
            lStartPos = 0;
            try
            {
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
                while (nReadSize > 0)
                {
                    fs.Write(nbytes, 0, nReadSize);
                    nReadSize = ns.Read(nbytes, 0, 512);
                    lCurrentPos = fs.Length;
                }

                fs.Close();
                ns.Close();
                strError = "下载完成";

                return true;
            }
            catch
            {
                fs.Close();

                return false;
            }
        }
        public string Encrypt(string pToEncrypt)
        {
            try
            {
                MD5 m = new MD5CryptoServiceProvider();
                byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(pToEncrypt));
                string t = BitConverter.ToString(s);
                string r = "";
                string[] ss = t.Split('-');
                for (int i = 0; i < ss.Length; i++)
                {
                    r += ss[i];
                }
                return r.ToLower();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Encrypt Error");
            }
            return "";
        }

        private void rmDuplicateBtn_Click(object sender, EventArgs e)
        {
            duplicateCSVOpenFile.InitialDirectory = "d:\\";
            duplicateCSVOpenFile.Filter = "csv file|*.csv";

            if (duplicateCSVOpenFile.ShowDialog() == DialogResult.OK)
            {
                fileName = duplicateCSVOpenFile.FileName;
                duplicateFileName.Text = fileName;

                //inputFileName.Text = fileName;
                //DataTable csvDt = new DataTable();
                //folder = fileName.Substring(0, fileName.LastIndexOf('\\'));
                CsvStreamReader read = new CsvStreamReader(fileName);
                if (read.RowCount == 0)
                {
                    MessageBox.Show("空文件");
                    return;

                }
                else
                {
                    duplicateDt = read[1, read.RowCount, 1, read.ColCount];
                    MessageBox.Show("导入成功，共导入" + duplicateDt.Rows.Count + "条数据");
                }

            }
            else
            {
                fileName = "";
                MessageBox.Show("请选择文件!");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //mydt
            if (mydt == null || mydt.Rows.Count == 0)
            {
                MessageBox.Show("请先导入基础csv数据文件!");
                return;
            }
            DataTable exportDT = new DataTable();
            foreach (string head in taobaoCSVHead)
            {
                exportDT.Columns.Add(head);
            }
            DataRow drHead = exportDT.NewRow();
            for (int i = 0; i < taobaoCSVHead.Length; i++)
            {
                drHead[i] = taobaoCSVHead[i];
            }
            exportDT.Rows.Add(drHead);
            for (int i = 0; i < mydt.Rows.Count; i++)
            {

                DataRow dr = exportDT.NewRow();
                for (int j = 0; j < taobaoCSVHead.Length; j++)
                {

                    //mydt.Rows[i][0].ToString();
                    dr[j] = taobaoCSVDefault[j];
                    Debug.WriteLine("dr[" + j + "]===" + dr[j]);
                }
                dr[0] = mydt.Rows[i][0].ToString();
                dr[40] = mydt.Rows[i][1].ToString();
                exportDT.Rows.Add(dr);
                //Debug.WriteLine("dr[0]+dr[40]" + dr[0] + dr[40]);
            }
            Debug.WriteLine("exportDT.Rows.Count==" + exportDT.Rows.Count);
            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(exportDT, 1);
                //csw.FirstRowQuote = 1;
                csw.Save();
            }
        }

        private void choseCSVfileBtn_Click(object sender, EventArgs e)
        {
            duplicateCSVOpenFile.InitialDirectory = "d:\\";
            duplicateCSVOpenFile.Filter = "csv file|*.csv";

            if (duplicateCSVOpenFile.ShowDialog() == DialogResult.OK)
            {
                fileName = duplicateCSVOpenFile.FileName;
                tbRewrite.Text = fileName;

                //inputFileName.Text = fileName;
                //DataTable csvDt = new DataTable();
                //folder = fileName.Substring(0, fileName.LastIndexOf('\\'));
                CsvStreamReader read = new CsvStreamReader(fileName);
                if (read.RowCount == 0)
                {
                    MessageBox.Show("空文件");
                    return;

                }
                else
                {
                    rewriteDT = read[1, read.RowCount, 1, read.ColCount];
                    MessageBox.Show("导入成功，共导入" + rewriteDT.Rows.Count + "条数据");
                }

            }
            else
            {
                fileName = "";
                MessageBox.Show("请选择文件!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //mydt

            foreach (DataRow dr1 in mydt.Rows)
            {
                foreach (DataRow dr2 in rewriteDT.Rows)
                {
                    if (dr1[0].ToString() == dr2[0].ToString())
                    {
                        dr1[40] = dr2[1];
                        dr1[7] = dr2[2];
                    }
                }    
            }

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                saveCsvFile.Filter = "csv file(*.csv)|*.csv";
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(mydt, 1);
                //csw.FirstRowQuote = 1;
                csw.Save();
            }
            else
            {
                MessageBox.Show("没保存");
            }
            MessageBox.Show("共生成" + (mydt.Rows.Count - 1) + "条数据");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr1 in mydt.Rows)
            {
                
                 // logger.AppendText("==>"+dr1[24].ToString());
                string price=  getPriceFromDesc(dr1[24].ToString());
                //logger.AppendText("price==>"+price);
                 if (price != "0")
                 {
                     dr1[7] = price;
                 }
                 //logger.AppendText(
            }

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                saveCsvFile.Filter = "csv file(*.csv)|*.csv";
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(mydt, 1);
                //csw.FirstRowQuote = 1;
                csw.Save();
            }
            else
            {
                MessageBox.Show("没保存");
            }
            MessageBox.Show("共生成" + (mydt.Rows.Count - 1) + "条数据");
        }
        private string getPriceFromDesc(string bookDesc)
        {
            string price = "0";
            string source = bookDesc;
            //MessageBox.Show(source);
            if (source != null && source != "")
            {
                if (source.IndexOf("￥") > 0)
                {
                    source = source.Substring(bookDesc.IndexOf("￥"));
                    
                    Regex priceRegex = new Regex(@"￥\s*(\d+\.?\d*)");
                    
                    if (priceRegex.IsMatch(source))
                    {
                        Match match = priceRegex.Match(source);
                        price = priceRegex.Match(source).Groups[1].ToString();
                    }
                    //price = priceRegex.Replace(source, "$1");
                }
                //bookDesc
            }
            //MessageBox.Show("price=="+price);
            return price;
        }
        private string convertUnicodeToNormal(string str)
        {
            StringBuilder sb = new StringBuilder();

            while(str.IndexOf("\\u")>0)
            {
                int pos = str.IndexOf("\\u");
                sb.Append(str.Substring(0, pos));
                
                sb.Append((char)int.Parse(str.Substring(pos, 4), NumberStyles.HexNumber));
                str = str.Substring(pos);
            }

            /*
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }*/
            return sb.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //duplicateCSVOpenFile.InitialDirectory = "d:\\";
            duplicateCSVOpenFile.Filter = "csv file|*.csv";

            if (duplicateCSVOpenFile.ShowDialog() == DialogResult.OK)
            {
                fileName = duplicateCSVOpenFile.FileName;
                tbCsvPath.Text = duplicateCSVOpenFile.FileName;
                duplicateFileName.Text = fileName;

                //inputFileName.Text = fileName;
                //DataTable csvDt = new DataTable();
                //folder = fileName.Substring(0, fileName.LastIndexOf('\\'));
                CsvStreamReader read = new CsvStreamReader(fileName);
                if (read.RowCount == 0)
                {
                    MessageBox.Show("空文件");
                    return;

                }
                else
                {

                    Console.WriteLine("最大行=="+read.RowCount);
                    duplicateDt = read[1, read.RowCount, 1, read.ColCount];
                    MessageBox.Show("导入成功，共导入" + duplicateDt.Rows.Count + "条数据");
                }

            }
            else
            {
                fileName = "";
                MessageBox.Show("请选择文件!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (mydt == null || mydt.Rows.Count == 0)
                {
                    MessageBox.Show("请先导入基础csv数据文件!");
                    return;
                }
                if (duplicateDt == null || duplicateDt.Rows.Count == 0)
                {
                    MessageBox.Show("请导入需要去除的重复的csv文件!");
                }

                DataTable tempDt = duplicateDt.Clone();

                //DataTable exportDT = new DataTable();
                /*
                foreach (string head in taobaoCSVHead)
                {
                    tempDt.Columns.Add(head);
                }
                 */
                DataRow drHead = tempDt.NewRow();


                for (int i = 0; i < duplicateDt.Columns.Count; i++)
                {
                    DataRow dr = duplicateDt.Rows[0];
                    drHead[i] = dr[i];
                }
                tempDt.Rows.Add(drHead);
                //tempDt.Rows.Add(
                for (int i = 0; i < duplicateDt.Rows.Count; i++)
                {

                    DataRow dr = duplicateDt.Rows[i];

                    if (chkName.Checked && chkPrice.Checked)
                    {
                        DataColumn[] dc = new DataColumn[] { mydt.Columns[0], mydt.Columns[7] };
                        mydt.PrimaryKey = dc;
                        if (!isSameRecord(mydt, new Object[] { dr[0].ToString(), dr[1].ToString() }))
                        {
                            tempDt.ImportRow(dr);
                        }

                    }
                    else if (chkName.Checked)
                    {
                        //mydt
                        
                        bool isFind=false;
                        foreach (DataRow mydr in mydt.Rows)
                        {
                            if (dr[0].ToString() == mydr[0].ToString())
                            {
                                isFind = true;
                                break;
                            }
                        }
                        if (!isFind)
                        {
                            tempDt.ImportRow(dr);
                        }
                        /*
                        mydt.PrimaryKey = new DataColumn[1] { mydt.Columns[0] };
                        if (!isSameRecord(mydt, new Object[] { dr[0].ToString() }))
                        {
                            tempDt.ImportRow(dr);
                        }
                         */

                    }
                    else if (chkPrice.Checked)
                    {
                        mydt.PrimaryKey = new DataColumn[1] { mydt.Columns[7] };
                        if (!isSameRecord(mydt, new Object[] { dr[7].ToString() }))
                        {
                            tempDt.ImportRow(dr);
                        }
                    }
                }
                        

            //MessageBox.Show(tempDt.Columns.Count+"=="+mydt.Rows[3].ItemArray.Length);

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                //saveCsvFile.Filter = "csv file|*.csv";
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(tempDt, 1);
                csw.FirstRowQuote = 2;
                csw.Save();
            }
            else
            {
                MessageBox.Show("没保存");
            }
            MessageBox.Show("共生成" + (tempDt.Rows.Count - 1) + "条数据");
        }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

            
            //DataTable dt = new DataTable();
            //DataTable newDt = new DataTable();
            List<String> fileNameList = new List<String>();
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                DataRow dr1 = mydt.Rows[i];
                string fileName = (String)dr1[0];

                if (fileNameList.Contains(fileName.Trim()))
                {
                    dr1.Delete();
                    i--;
                }
                else
                {
                    fileNameList.Add(fileName.Trim());
                }
            }

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                saveCsvFile.Filter = "csv file(*.csv)|*.csv";
                string filename = saveCsvFile.FileName;
                //int[] quoteMark ={ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 };
                CsvStreamWriter csw = new CsvStreamWriter(filename, System.Text.Encoding.Unicode);
                csw.QuoteMark(TBCSV.getQuoteMark());
                csw.AddData(mydt, 1);
                //csw.FirstRowQuote = 1;
                csw.Save();
            }
            else
            {
                MessageBox.Show("没保存");
            }
            MessageBox.Show("共生成" + (mydt.Rows.Count) + "条数据");
        }
    }
}