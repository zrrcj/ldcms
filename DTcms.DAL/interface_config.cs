using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Model;
using DTcms.Common;
namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class interfaceconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.interfaceconfig loadConfig(string configFilePath)
        {
            return (Model.interfaceconfig)SerializationHelper.Load(typeof(Model.interfaceconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.interfaceconfig saveConifg(Model.interfaceconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
