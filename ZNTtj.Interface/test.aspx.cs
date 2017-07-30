using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZNTtj.Interface
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <1000; i++)
            {
                Thread t1 = new Thread(new ThreadStart(UpdateContactSign));
                t1.IsBackground = true;
                t1.Start();
                q.Text = i.ToString();
            }
            

        }
        void UpdateContactSign()
        {
            string ServerPage = "http://localhost:7000/InterfaceTest.asmx/HelloWorld";
            try
            {
           string strXml="aaa";//第一个参数
           string res = HttpConnectToServer(ServerPage, strXml, "");
           Console.WriteLine(res);
            }
            catch (Exception ex)
            {
            }
        }
        //发送消息到服务器
        public string HttpConnectToServer(string ServerPage, string strXml, string strData)
        {
            string postData = "Json=" + strXml;
            byte[] dataArray = Encoding.UTF8.GetBytes(postData);
            //创建请求
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ServerPage);
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            int timeout = 5000;
           // Int32.TryParse(Settings.Default.LoginTimeOut, out timeout);
            if (timeout < 5000 || timeout > 15000)
                timeout = 5000;
            request.Timeout = timeout;
            request.ContentLength = dataArray.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            //创建输入流
            Stream dataStream = null;
            try
            {
                dataStream = request.GetRequestStream();
            }
            catch (Exception ex)
            {
                return null;//连接服务器失败
            }
            //发送请求
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            //读取返回消息
            string res = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                res = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                return null;//连接服务器失败
            }
            return res;
        }

    }
}