using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DTcms.Common;

namespace ZNTtj.Interface
{
    public class CacheInfo
    {
        private HttpContext _context;
        private DataTable _table;

        public CacheInfo(HttpContext context)
        {
            this._context = context;
            this._table = CacheHelper.Get<DataTable>("_table");
            if (this._table == null)
            {
                this._table = new DataTable();
                //初始化_talbe
                this._table.Columns.Add("ip", System.Type.GetType("System.String"));
                this._table.Columns.Add("name", System.Type.GetType("System.String"));
                this._table.Columns.Add("begintime", System.Type.GetType("System.DateTime"));
                CacheHelper.Insert("_table", this._table);
            }
        }

        /// <summary>
        /// 添加一条缓存记录
        /// </summary>
        /// <param name="log"></param>
        public void AddRecord(Model.zn_interface_log log)
        {
            //将数据加入缓存
            DataRow dr = this._table.NewRow();
            dr["ip"] = log.clientip;
            dr["name"] = log.name;
            dr["begintime"] = log.begintime;
            this._table.Rows.Add(dr);
            CacheHelper.Insert("_table", this._table);

            //清除过时的数据
            DataRow[] drs = this._table.Select("begintime<'" + log.begintime.AddMinutes(-1) + "'");
            foreach (DataRow drw in drs)
            {
                this._table.Rows.Remove(drw);
            }
            CacheHelper.Insert("_table", this._table);
        }

        /// <summary>
        /// 查询缓存记录数
        /// </summary>
        /// <param name="ip">客户端IP</param>
        /// <param name="funname">接口名称</param>
        /// <returns>获取最近一次的调用间隔，毫秒</returns>
        public int GetTimesLastInvoke(string ip, string funname, DateTime begintime)
        {
            try
            {
                DataRow[] drs = _table.Select("ip='" + ip + "' and name = '" + funname + "'", " begintime asc");
                if (drs.Count() > 0)
                {
                    if (drs[0]["begintime"] != null)
                    {
                        DateTime lastbegintime = Convert.ToDateTime(drs[0]["begintime"]);
                        TimeSpan ts1 = new TimeSpan(lastbegintime.Ticks);
                        TimeSpan ts2 = new TimeSpan(begintime.Ticks);
                        TimeSpan ts = ts2.Subtract(ts1).Duration();
                        return (int)ts.TotalMilliseconds;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }
        /// <summary>
        /// 获取一分钟内的调用次数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="funname"></param>
        /// <returns></returns>
        public int GetInvokeCount(string ip, string funname)
        {
            return this._table.Select("ip='" + ip + "' and name = '" + funname + "'").Count();
        }
    }
}