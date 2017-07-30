using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZNTtj.Interface.BLL
{
    public class zn_interface_blacklist
    {
        DAL.zn_interface_blacklist dal = new DAL.zn_interface_blacklist();
        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="black"></param>
        /// <returns></returns>
        public int Add(Model.zn_interface_blacklist black) {

            return dal.Add(black);
        }
        /// <summary>
        /// 查询是否存在相同IP
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public bool Exists(string ip) {
            return dal.Exists(ip);
        }
        /// <summary>
        /// 更新相同的ip地址次数加一
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="count">原有次数</param>
        /// <returns></returns>
        public bool Update(int id,int count) {
            return dal.Update(id, count);
        }
    }
}