using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZNTtj.Interface.Model;

namespace ZNTtj.Interface.BLL
{
    public class zn_interface_log
    {
        private HttpContext _context;
        private object lockobject = new object();

        BLL.interfaceconfig bll = new BLL.interfaceconfig();
        BLL.zn_interface_blacklist bl = new BLL.zn_interface_blacklist();
        Model.zn_interface_blacklist black = new Model.zn_interface_blacklist();
        DAL.zn_interface_log dal = new DAL.zn_interface_log();

        public zn_interface_log(HttpContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <returns></returns>
        public int Add(Model.zn_interface_log interfaceLog)
        {
            //添加记录
            return dal.Add(interfaceLog);
        }
        /// <summary>
        ///  更新记录
        /// </summary>
        /// <returns></returns>
        public void Update(Model.zn_interface_log interfaceLog)
        {
            dal.Update(interfaceLog);
        }
        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="interfaceLog"></param>
        /// <returns></returns>
        public int Add(Model.zn_interface_blacklist interfaceLog)
        {
            //添加记录
            DAL.zn_interface_blacklist dal = new DAL.zn_interface_blacklist();
            if (bl.Exists(interfaceLog.ip))
            {
                interfaceLog = dal.GetModel(interfaceLog.ip);
                bl.Update(interfaceLog.id, interfaceLog.count);
                return 0;
            }
            else
            {
                return dal.Add(interfaceLog);
            }
        }
        /// <summary>
        /// 判断黑名单
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private bool isblack(Model.zn_interface_log log)
        {
            interfaceModel i = bll.getInterFace(log.name);//接口的配置
            if (i.isblack == 1)//是否开启黑名单
            {
                if (bl.Exists(log.clientip))//判断IP是否在黑名单
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int InvokeIsValid(Model.zn_interface_log log)
        {
            interfaceModel i = bll.getInterFace(log.name);//接口的配置
            if (i==null)
            {
                return 0;
            }
            log.responsecode = 0;
            //初始化缓存
            CacheInfo cacheInfo = new CacheInfo(this._context);
            //过滤黑名单
            if (i.isblack == 1 && isblack(log))
            {
                log.responseinfo = "ip属于黑名单";
                log.responsecode = -1;
                log.id = Add(log);//记录日志
                return -1;
            }

            bool issuccess = true;
            //添加锁定防止出现并发
            lock (lockobject)
            {
                int times = cacheInfo.GetTimesLastInvoke(log.clientip, log.name, log.begintime);//返回相同IP两次调用时间间隔时
                if (times < int.Parse(i.invokespace) && times > 0)
                {
                    issuccess = false;
                    log.responseinfo = "两次调用间隔小于" + i.invokespace + "毫秒！";
                    log.responsecode = -1;
                    cacheInfo.AddRecord(log);

                }

                if (issuccess == true && cacheInfo.GetInvokeCount(log.clientip, log.name) > int.Parse(i.invokerate)) //一分钟调用超过3次不再添加缓存记录
                {
                    issuccess = false;
                    log.responseinfo = "1分钟内调用超过" + i.invokerate + "次！";
                    log.responsecode = -1;
                    cacheInfo.AddRecord(log);

                }

                cacheInfo.AddRecord(log);
            }

            //处理参数加密
            if (i.paramenc == 1)//判断是否加密
            {
                if (!string.IsNullOrEmpty(i.enckey))
                {
                    log.requestinfo = DESEncrypt.Decrypt(log.requestinfo, i.enckey);
                }
            }
            if (i.isblack == 1)
            {
                black.ip = log.clientip;
                black.addtime = DateTime.Now;
                black.count = 1;
                Add(black);//添加黑名单
            }
            if (i.islog == 1)
            {
                log.id = Add(log);//记录日志
            }

            if (issuccess == false)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

    }
}