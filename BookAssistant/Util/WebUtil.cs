using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using RE = System.Text.RegularExpressions.Regex;
using System.Security.Cryptography.X509Certificates;
/***************************************************************************************************************************************************
 * *�ļ�����HttpProc.cs
 * *�����ˣ�HeDaode
 * *�� �ڣ�2007.09.01
 * *�� ����ʵ��HTTPЭ���е�GET��POST����
 * *ʹ �ã�HttpProc.WebClient client = new HttpProc.WebClient();
            client.Encoding = System.Text.Encoding.Default;//Ĭ�ϱ��뷽ʽ��������Ҫ������������
            client.OpenRead("http://www.baidu.com");//��ͨget����
            MessageBox.Show(client.RespHtml);//��ȡ���ص���ҳԴ����
            client.DownloadFile("http://www.codepub.com/upload/163album.rar",@"C:\163album.rar");//�����ļ�
            client.OpenRead("http://passport.baidu.com/?login","username=zhangsan&password=123456");//�ύ�����˴��ǵ�¼�ٶȵ�ʾ��
            client.UploadFile("http://hiup.baidu.com/zhangsan/upload", @"file1=D:\1.mp3");//�ϴ��ļ�
            client.UploadFile("http://hiup.baidu.com/zhangsan/upload", "folder=myfolder&size=4003550",@"file1=D:\1.mp3");//�ύ���ı�����ļ���ı�
*****************************************************************************************************************************************************/
namespace BookAssistant.Util
{
    ///<summary>
    ///�ϴ��¼�ί��
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    public delegate void WebClientUploadEvent(object sender, UploadEventArgs e);
 
    ///<summary>
    ///�����¼�ί��
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    public delegate void WebClientDownloadEvent(object sender, DownloadEventArgs e);
 
 
    ///<summary>
    ///�ϴ��¼�����
    ///</summary>
    public struct UploadEventArgs
    {
        ///<summary>
        ///�ϴ������ܴ�С
        ///</summary>
        public long totalBytes;
        ///<summary>
        ///�ѷ����ݴ�С
        ///</summary>
        public long bytesSent;
        ///<summary>
        ///���ͽ���(0-1)
        ///</summary>
        public double sendProgress;
        ///<summary>
        ///�����ٶ�Bytes/s
        ///</summary>
        public double sendSpeed;
    }
 
    ///<summary>
    ///�����¼�����
    ///</summary>
    public struct DownloadEventArgs
    {
        ///<summary>
        ///���������ܴ�С
        ///</summary>
        public long totalBytes;
        ///<summary>
        ///�ѽ������ݴ�С
        ///</summary>
        public long bytesReceived;
        ///<summary>
        ///�������ݽ���(0-1)
        ///</summary>
        public double ReceiveProgress;
        ///<summary>
        ///��ǰ����������
        ///</summary>
        public byte[] receivedBuffer;
        ///<summary>
        ///�����ٶ�Bytes/s
        ///</summary>
        public double receiveSpeed;
    }
    ///<summary>
    ///ʵ����WEB���������ͺͽ�������
    ///</summary>
    public class WebUtil
    {
        private WebHeaderCollection requestHeaders, responseHeaders;
        private TcpClient clientSocket;
        private MemoryStream postStream;
        private Encoding encoding = Encoding.UTF8;
        private const string BOUNDARY = "--HEDAODE--";
        private const int SEND_BUFFER_SIZE = 10245;
        private const int RECEIVE_BUFFER_SIZE = 10245;
        private string cookie = "";
        private string respHtml = "";
        private string strRequestHeaders = "";
        private string strResponseHeaders = "";
        private int statusCode = 0;
        private bool isCanceled = false;
        public event WebClientUploadEvent UploadProgressChanged;
        public event WebClientDownloadEvent DownloadProgressChanged;
 
        ///<summary>
        ///��ʼ��WebClient��
        ///</summary>
        public WebUtil()
        {
            responseHeaders = new WebHeaderCollection();
            requestHeaders = new WebHeaderCollection();
        }
 
 
        ///<summary>
        ///��ȡָ��URL���ı�
        ///</summary>
        ///<param name="URL">����ĵ�ַ</param>
        ///<returns>��������Ӧ�ı�</returns>
        public string OpenRead(string URL)
        {
            requestHeaders.Add("Connection", "close");
            SendRequestData(URL, "GET");
            return GetHtml();
        }
 
 
        //���֤������޷����ʵ�����
        class CertPolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint srvpt, X509Certificate cert, WebRequest req, int certprb)
            { return true; }
        }
 
        ///<summary>
        ///����httpsЭ���������
        ///</summary>
        ///<param name="URL">url��ַ</param>
        ///<param name="strPostdata">���͵�����</param>
        ///<returns></returns>
        public string OpenReadWithHttps(string URL,string strPostdata)
        {
            ServicePointManager.CertificatePolicy = new CertPolicy();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.CookieContainer = new CookieContainer();
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType="application/x-www-form-urlencoded";
            byte[] buffer = this.encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);
            this.respHtml = reader.ReadToEnd();
            foreach (System.Net.Cookie ck in response.Cookies)
            {
                this.cookie += ck.Name + "=" + ck.Value + ";";
            }
            reader.Close();
            return respHtml;
        }
 
        ///<summary>
        ///��ȡָ��URL���ı�
        ///</summary>
        ///<param name="URL">����ĵ�ַ</param>
        ///<param name="postData">����������͵��ı�����</param>
        ///<returns>��������Ӧ�ı�</returns>
        public string OpenRead(string URL, string postData)
        {
            byte[] sendBytes = encoding.GetBytes(postData);
            foreach (byte b in sendBytes)
            {
                Console.WriteLine("||==" + b);
            }
            postStream = new MemoryStream();
            postStream.Write(sendBytes, 0, sendBytes.Length);
 
            requestHeaders.Add("Content-Length", postStream.Length.ToString());
            requestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            requestHeaders.Add("Connection", "close");
 
            SendRequestData(URL, "POST");
            return GetHtml();
        }
 
 
        ///<summary>
        ///��ȡָ��URL����
        ///</summary>
        ///<param name="URL">����ĵ�ַ</param>
        ///<param name="postData">����������͵�����</param>
        ///<returns>��������Ӧ��</returns>
        public Stream GetStream(string URL, string postData)
        {
            byte[] sendBytes = encoding.GetBytes(postData);
            postStream = new MemoryStream();
            postStream.Write(sendBytes, 0, sendBytes.Length);
 
            requestHeaders.Add("Content-Length", postStream.Length.ToString());
            requestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            requestHeaders.Add("Connection", "close");
 
            SendRequestData(URL, "POST");
 
            MemoryStream ms = new MemoryStream();
            SaveNetworkStream(ms);
            return ms;
        }
 
 
        ///<summary>
        ///�ϴ��ļ���������
        ///</summary>
        ///<param name="URL">����ĵ�ַ</param>
        ///<param name="fileField">�ļ���(��ʽ��:file1=C:\test.mp3&file2=C:\test.jpg)</param>
        ///<returns>��������Ӧ�ı�</returns>
        public string UploadFile(string URL, string fileField)
        {
            return UploadFile(URL, "", fileField);
        }
 
        ///<summary>
        ///�ϴ��ļ������ݵ�������
        ///</summary>
        ///<param name="URL">�����ַ</param>
        ///<param name="textField">�ı���(��ʽΪ:name1=value1&name2=value2)</param>
        ///<param name="fileField">�ļ���(��ʽ��:file1=C:\test.mp3&file2=C:\test.jpg)</param>
        ///<returns>��������Ӧ�ı�</returns>
        public string UploadFile(string URL, string textField, string fileField)
        {
            postStream = new MemoryStream();
 
            if (textField != "" && fileField != "")
            {
                WriteTextField(textField);
                WriteFileField(fileField);
            }
            else if (fileField != "")
            {
                WriteFileField(fileField);
            }
            else if (textField != "")
            {
                WriteTextField(textField);
            }
            else
                throw new Exception("�ı�����ļ�����ͬʱΪ�ա�");
 
            //д��������
            byte[] buffer = encoding.GetBytes("--" + BOUNDARY + "--\r\n");
            postStream.Write(buffer, 0, buffer.Length);
 
            //��������ͷ
            requestHeaders.Add("Content-Length", postStream.Length.ToString());
            requestHeaders.Add("Content-Type", "multipart/form-data; boundary=" + BOUNDARY);
            requestHeaders.Add("Connection", "Keep-Alive");
 
            //������������
            SendRequestData(URL, "POST", true);
 
            //������Ӧ�ı�
            return GetHtml();
        }
 
 
        ///<summary>
        ///�����ı�����ӵ�������
        ///</summary>
        ///<param name="textField">�ı���</param>
        private void WriteTextField(string textField)
        {
            string[] strArr = RE.Split(textField, "&");
            textField = "";
            foreach (string var in strArr)
            {
                Match M = RE.Match(var, "([^=]+)=(.+)");
                textField += "--" + BOUNDARY + "\r\n";
                textField += "Content-Disposition: form-data; name=\"" + M.Groups[1].Value + "\"\r\n\r\n" + M.Groups[2].Value + "\r\n";
            }
            byte[] buffer = encoding.GetBytes(textField);
            postStream.Write(buffer, 0, buffer.Length);
        }
 
        ///<summary>
        ///�����ļ�����ӵ�������
        ///</summary>
        ///<param name="fileField">�ļ���</param>
        private void WriteFileField(string fileField)
        {
            string filePath = "";
            int count = 0;
            string[] strArr = RE.Split(fileField, "&");
            foreach (string var in strArr)
             {
                Match M = RE.Match(var, "([^=]+)=(.+)");
                filePath = M.Groups[2].Value;
                fileField = "--" + BOUNDARY + "\r\n";
                fileField += "Content-Disposition: form-data; name=\"" + M.Groups[1].Value + "\"; filename=\"" + Path.GetFileName(filePath) + "\"\r\n";
                fileField += "Content-Type: image/jpeg\r\n\r\n";
 
                byte[] buffer = encoding.GetBytes(fileField);
                postStream.Write(buffer, 0, buffer.Length);
 
                //����ļ�����
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                buffer = new byte[50000];
 
                do
                {
                    count = fs.Read(buffer, 0, buffer.Length);
                    postStream.Write(buffer, 0, count);
 
                } while (count > 0);
 
                fs.Close();
                fs.Dispose();
                fs = null;
 
                buffer = encoding.GetBytes("\r\n");
                postStream.Write(buffer, 0, buffer.Length);
            }
        }
 
        ///<summary>
        ///��ָ��URL����������
        ///</summary>
        ///<param name="URL">�����ַ</param>
        ///<returns>������</returns>
        public Stream DownloadData(string URL)
        {
            requestHeaders.Add("Connection", "close");
            SendRequestData(URL, "GET");
            MemoryStream ms = new MemoryStream();
            SaveNetworkStream(ms, true);
            return ms;
        }
 
 
        ///<summary>
        ///��ָ��URL�����ļ�
        ///</summary>
        ///<param name="URL">�ļ�URL��ַ</param>
        ///<param name="fileName">�ļ�����·��,���ļ���(��:C:\test.jpg)</param>
        public void DownloadFile(string URL, string fileName)
        {
            requestHeaders.Add("Connection", "close");
            SendRequestData(URL, "GET");
            FileStream fs = new FileStream(fileName, FileMode.Create);
            SaveNetworkStream(fs, true);
            fs.Close();
            fs = null;
        }
 
        ///<summary>
        ///���������������
        ///</summary>
        ///<param name="URL">�����ַ</param>
        ///<param name="method">POST��GET</param>
        ///<param name="showProgress">�Ƿ���ʾ�ϴ�����</param>
        private void SendRequestData(string URL, string method, bool showProgress)
        {
            clientSocket = new TcpClient();
            Uri URI = new Uri(URL);
            clientSocket.Connect(URI.Host, URI.Port);
 
            requestHeaders.Add("Host", URI.Host);
            byte[] request = GetRequestHeaders(method + " " + URI.PathAndQuery + " HTTP/1.1");
            clientSocket.Client.Send(request);
 
            //����ʵ�����ݾͷ�����
            if (postStream != null)
            {
                byte[] buffer = new byte[SEND_BUFFER_SIZE];
                int count = 0;
                Stream sm = clientSocket.GetStream();
                postStream.Position = 0;
 
                UploadEventArgs e = new UploadEventArgs();
                e.totalBytes = postStream.Length;
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();//��ʱ��
                timer.Start();
                do
                {
                    //���ȡ�����Ƴ�
                    if (isCanceled) { break; }
 
                    //��ȡҪ���͵�����
                    count = postStream.Read(buffer, 0, buffer.Length);
                    //���͵�������
                    sm.Write(buffer, 0, count);
 
                    //�Ƿ���ʾ����
                    if (showProgress)
                    {
                        //�����¼�
                        e.bytesSent += count;
                        e.sendProgress = (double)e.bytesSent / (double)e.totalBytes;
                        double t = timer.ElapsedMilliseconds / 1000;
                        t = t <= 0 ? 1 : t;
                        e.sendSpeed = (double)e.bytesSent / t;
                        if (UploadProgressChanged != null) { UploadProgressChanged(this, e); }
                    }
 
                } while (count > 0);
                timer.Stop();
                postStream.Close();
                //postStream.Dispose();
                postStream = null;
 
            }//end if
 
        }
 
        ///<summary>
        ///���������������
        ///</summary>
        ///<param name="URL">����URL��ַ</param>
        ///<param name="method">POST��GET</param>
        private void SendRequestData(string URL, string method)
        {
            SendRequestData(URL, method, false);
        }
 
 
        ///<summary>
        ///��ȡ����ͷ�ֽ�����
        ///</summary>
        ///<param name="request">POST��GET����</param>
        ///<returns>����ͷ�ֽ�����</returns>
        private byte[] GetRequestHeaders(string request)
        {
            requestHeaders.Add("Accept", "*/*");
            requestHeaders.Add("Accept-Language", "zh-cn");
            requestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
 
           string headers = request + "\r\n";
 
            foreach (string key in requestHeaders)
            {
                headers += key + ":" + requestHeaders[key] + "\r\n";
            }
 
            //��Cookie�ʹ���Cookie
            if (cookie != "") { headers += "Cookie:" + cookie + "\r\n"; }
 
            //���У�����ͷ����
            headers += "\r\n";
 
            strRequestHeaders = headers;
            requestHeaders.Clear();
            return encoding.GetBytes(headers);
        }
 
 
 
        ///<summary>
        ///��ȡ��������Ӧ�ı�
        ///</summary>
        ///<returns>��������Ӧ�ı�</returns>
        private string GetHtml()
        {
            MemoryStream ms = new MemoryStream();
            SaveNetworkStream(ms);//�����������浽�ڴ���
            StreamReader sr = new StreamReader(ms, encoding);
            respHtml = sr.ReadToEnd();
            sr.Close(); ms.Close();
            return respHtml;
        }
 
        ///<summary>
        ///�����������浽ָ����
        ///</summary>
        ///<param name="toStream">����λ��</param>
        ///<param name="needProgress">�Ƿ���ʾ����</param>
        private void SaveNetworkStream(Stream toStream, bool showProgress)
        {
            //��ȡҪ�����������
            NetworkStream NetStream = clientSocket.GetStream();
 
            byte[] buffer = new byte[RECEIVE_BUFFER_SIZE];
            int count = 0, startIndex = 0;
 
            MemoryStream ms = new MemoryStream();
            for (int i = 0; i < 3; i++)
            {
                count = NetStream.Read(buffer, 0, 500);
                ms.Write(buffer, 0, count);
            }
 
            if (ms.Length == 0) { NetStream.Close(); throw new Exception("Զ�̷�����û����Ӧ"); }
 
            buffer = ms.GetBuffer();
            count = (int)ms.Length;
 
            GetResponseHeader(buffer, out startIndex);//������Ӧ����ȡ��Ӧͷ����Ӧʵ��
            count -= startIndex;
            toStream.Write(buffer, startIndex, count);
 
            DownloadEventArgs e = new DownloadEventArgs();
 
            if (responseHeaders["Content-Length"] != null)
            { e.totalBytes = long.Parse(responseHeaders["Content-Length"]); }
            else
            { e.totalBytes = -1; }
 
            //������ʱ��
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
 
            do
            {
                //���ȡ�����Ƴ�
                if (isCanceled) { break; }
 
                //��ʾ���ؽ���
                if (showProgress)
                {
                    e.bytesReceived += count;
                    e.ReceiveProgress = (double)e.bytesReceived / (double)e.totalBytes;
 
                    byte[] tempBuffer = new byte[count];
                    Array.Copy(buffer, startIndex, tempBuffer, 0, count);
                    e.receivedBuffer = tempBuffer;
 
                    double t = (timer.ElapsedMilliseconds + 0.1)/1000;
                    e.receiveSpeed = (double)e.bytesReceived / t;
 
                    startIndex = 0;
                    if (DownloadProgressChanged != null) { DownloadProgressChanged(this, e); }
                }
 
                //��ȡ��·���ݵ�������
                count = NetStream.Read(buffer, 0, buffer.Length);
 
                //�����������ݱ��浽ָ����
                toStream.Write(buffer, 0, count);
            } while (count > 0);
 
            timer.Stop();//�رռ�ʱ��
 
            if (responseHeaders["Content-Length"] != null)
            {
                toStream.SetLength(long.Parse(responseHeaders["Content-Length"]));
            }
            //else
            //{
            //    toStream.SetLength(toStream.Length);
            //    responseHeaders.Add("Content-Length", toStream.Length.ToString());//�����Ӧ��ͷ
            //}
 
            toStream.Position = 0;
 
            //�ر�����������������
            NetStream.Close();
            clientSocket.Close();
        }
 
 
        ///<summary>
        ///�����������浽ָ����
        ///</summary>
        ///<param name="toStream">����λ��</param>
        private void SaveNetworkStream(Stream toStream)
        {
            SaveNetworkStream(toStream, false);
        }
 
 
 
        ///<summary>
        ///������Ӧ����ȥ����Ӧͷ
        ///</summary>
        ///<param name="buffer"></param>
        private void GetResponseHeader(byte[] buffer, out int startIndex)
        {
            responseHeaders.Clear();
            string html = encoding.GetString(buffer);
            StringReader sr = new StringReader(html);
 
            int start = html.IndexOf("\r\n\r\n") + 4;//�ҵ�����λ��
            strResponseHeaders = html.Substring(0, start);//��ȡ��Ӧͷ�ı�
 
            //��ȡ��Ӧ״̬��
            //
            if (sr.Peek() > -1)
            {
                //����һ���ַ���
                string line = sr.ReadLine();
 
                //���������ַ���,��ȡ��������Ӧ״̬��
                Match M = RE.Match(line, @"\d\d\d");
                if (M.Success)
                {
                    statusCode = int.Parse(M.Value);
                }
            }
 
            //��ȡ��Ӧͷ
            //
            while (sr.Peek() > -1)
            {
                //��һ���ַ���
                string line = sr.ReadLine();
 
                //���ǿ���
                if (line != "")
               {
                    //���������ַ�������ȡ��Ӧ��ͷ
                    Match M = RE.Match(line, "([^:]+):(.+)");
                    if (M.Success)
                    {
                        try
                        {        //�����Ӧ��ͷ������
                            responseHeaders.Add(M.Groups[1].Value.Trim(), M.Groups[2].Value.Trim());
                        }
                        catch
                        { }
 
 
                        //��ȡCookie
                        if (M.Groups[1].Value == "Set-Cookie")
                        {
                            M = RE.Match(M.Groups[2].Value, "[^=]+=[^;]+");
                            cookie += M.Value.Trim() + ";";
                        }
                    }
 
                }
                //���ǿ��У�������Ӧͷ������Ӧʵ�忪ʼ������Ӧͷ����Ӧʵ�����һ���и�����
                else
                {
                    //�����Ӧͷ��û��ʵ���С��ͷ�����Զ���Ӧʵ���һ�л�ȡʵ���С
                    if (responseHeaders["Content-Length"] == null && sr.Peek() > -1)
                    {
                        //����Ӧʵ���һ��
                        line = sr.ReadLine();
 
                        //�������п��Ƿ����ʵ���С
                        Match M = RE.Match(line, "~[0-9a-fA-F]{1,15}");
 
                        if (M.Success)
                        {
                            //��16���Ƶ�ʵ���С�ַ���ת��Ϊ10����
                            int length = int.Parse(M.Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                            responseHeaders.Add("Content-Length", length.ToString());//�����Ӧ��ͷ
                            strResponseHeaders += M.Value + "\r\n";
                        }
                    }
                    break;//����ѭ�� 
                }//End If
            }//End While
 
            sr.Close();
 
            //ʵ�忪ʼ����
            startIndex = encoding.GetBytes(strResponseHeaders).Length;
        }
 
 
        ///<summary>
        ///ȡ���ϴ�������,Ҫ������ʼ�����Start����
        ///</summary>
        public void Cancel()
        {
            isCanceled = true;
        }
 
        ///<summary>
        ///�����ϴ������أ�Ҫȡ�������Cancel����
        ///</summary>
        public void Start()
        {
            isCanceled = false;
        }
 
        //*************************************************************
        //����Ϊ����
        //*************************************************************
 
        ///<summary>
        ///��ȡ����������ͷ
        ///</summary>
        public WebHeaderCollection RequestHeaders
        {
            set { requestHeaders = value; }
            get { return requestHeaders; }
        }
 
        ///<summary>
        ///��ȡ��Ӧͷ����
        ///</summary>
        public WebHeaderCollection ResponseHeaders
        {
            get { return responseHeaders; }
        }
 
        ///<summary>
        ///��ȡ����ͷ�ı�
        ///</summary>
        public string StrRequestHeaders
        {
            get { return strRequestHeaders; }
        }
 
        ///<summary>
        ///��ȡ��Ӧͷ�ı�
        ///</summary>
        public string StrResponseHeaders
        {
            get { return strResponseHeaders; }
        }
 
        ///<summary>
        ///��ȡ������Cookie
        ///</summary>
        public string Cookie
        {
            set { cookie = value; }
            get { return cookie; }
        }
 
        ///<summary>
        ///��ȡ�����ñ��뷽ʽ(Ĭ��ΪϵͳĬ�ϱ��뷽ʽ)
        ///</summary>
        public Encoding Encoding
        {
            set { encoding = value; }
            get { return encoding; }
        }
 
        ///<summary>
        ///��ȡ��������Ӧ�ı�
        ///</summary>
        public string RespHtml
        {
            get { return respHtml; }
        }
 
 
        ///<summary>
        ///��ȡ��������Ӧ״̬��
        ///</summary>
        public int StatusCode
        {
            get { return statusCode; }
        }
    }
}
