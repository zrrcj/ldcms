using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZNTtj.Interface
{
    public class WebServiceBase
    {
        protected Model.zn_interface_log _interfaceLog;

        public WebServiceBase()
        {
            _interfaceLog = new Model.zn_interface_log();
            _interfaceLog.clienttype = getTerrace(getUserAgent());
            _interfaceLog.clientip = getIp();
            _interfaceLog.clientversion = getUserAgent();
        }

        private string getUserAgent()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                HttpRequest request = context.Request;
                return request.UserAgent;
            }
            return "";
        }
        /// <summary>
        /// 判断请求平台
        /// </summary>
        /// <param name="useragent"></param>
        /// <returns></returns>
        private int getTerrace(string useragent)
        {
            if (useragent!=null)
            {
                if (useragent.IndexOf("ucweb") > -1)
                {
                    return 1;
                }
                else if (useragent.IndexOf("Android") > -1)
                {
                    return 2;
                }
                else if (useragent.IndexOf("Ipod") > -1 || useragent.IndexOf("iPhone") > -1 || useragent.IndexOf("Ipad") > -1)
                {
                    return 3;
                }
                else
                {
                    return 0;//PC
                }
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        private string getIp()
        {
            string UserIP;
            if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
            {
                UserIP = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))  //得到穿过代理服务器的ip地址
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].ToString();
                else
                    UserIP = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
            }
            else
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                else
                    UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                UserIP = UserIP.Replace("；", ",");
                UserIP = UserIP.Replace(";", ",");
                UserIP = UserIP.Replace("，", ",");

                UserIP = (UserIP).Split(',')[0].ToString();
            }
            return UserIP;
        }
    }
}