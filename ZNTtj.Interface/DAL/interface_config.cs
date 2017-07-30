using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;
using System.Data;
using System.Xml;
using System.Reflection;
using ZNTtj.Interface.Model;
namespace ZNTtj.Interface.DAL
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class interface_config
    {
        private static object lockHelper = new object();

        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="entity"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public List<T> PutAllVal<T>(T entity, DataSet ds) where T : new()
        {
            List<T> lists = new List<T>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lists.Add(PutVal(new T(), row));
                }
            }
            return lists;
        }

        public T PutVal<T>(T entity, DataRow row) where T : new()
        {
            //初始化 如果为null
            if (entity == null)
            {
                entity = new T();
            }
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                //给属性赋值
                if (row[item.Name] != null && row[item.Name] != DBNull.Value)
                {
                    if (item.PropertyType == typeof(System.Nullable<System.DateTime>))
                    {
                        item.SetValue(entity, Convert.ToDateTime(row[item.Name].ToString()), null);
                    }
                    else
                    {
                        item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                    }
                }
            }
            return entity;
        }

        //    return PutAllVal(new blacklistModel(), dataSet);
        //}
        /// <summary>
        /// 获取配置信息的列表
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <param name="file">web.config配置文件地址名称</param>
        /// <param name="nodeName">xml节点名称</param>
        /// <returns></returns>
        public List<T> getList<T>(T t, string file, string nodeName)where T:new()
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(file);
            XmlNodeList xnl = xd.GetElementsByTagName(nodeName);
            System.Data.DataSet dataSet = null;
            if (xnl.Count > 0)
            {
                XmlNode xn = xnl.Item(0);
                XmlNodeReader xnr = new XmlNodeReader(xn);
                dataSet = new DataSet();
                dataSet.ReadXml(xnr);
            }

            return PutAllVal(t, dataSet);
        }

    }
}
