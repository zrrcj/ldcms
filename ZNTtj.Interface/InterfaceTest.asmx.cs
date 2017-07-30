﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ZNTtj.Interface
{
    /// <summary>
    /// InterfaceTest 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class InterfaceTest : WebServiceBase
    {
       [WebMethod]
        public string HelloWorld(string Json)
        {
            BLL.zn_interface_log interface_log = new BLL.zn_interface_log(HttpContext.Current);
            this._interfaceLog.name = "helloworld";
            this._interfaceLog.requestinfo = Json;
            this._interfaceLog.begintime = DateTime.Now;
            if (interface_log.InvokeIsValid(this._interfaceLog) == -1)
            {
                return "调用异常";
            }
            this._interfaceLog.endtime = DateTime.Now;
            System.TimeSpan ts= this._interfaceLog.endtime - this._interfaceLog.begintime;
            this._interfaceLog.timespans = ts.Seconds;
            this._interfaceLog.responseinfo = "";//接口返回信息
            interface_log.Update(this._interfaceLog);
            return "Hello World";
        }
        //判断移动设备类型
        public static bool IsMobile
        {
            get
            {
                bool result = false;
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    HttpRequest request = context.Request;
                    string useragent = request.UserAgent;
                    if (useragent.IndexOf("android") > -1 || useragent.IndexOf("ipod") > -1 || useragent.IndexOf("iphone") > -1 || useragent.IndexOf("ipad") > -1 || useragent.IndexOf("ucweb") > -1)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }

    }
}
