using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using DTcms.Common;


namespace DTcms.BLL
{
    public partial class interfaceconfig
    {
        private readonly DTcms.DAL.interfaceconfig dal = new DTcms.DAL.interfaceconfig();
        //web.cofig中配置的节点名称
        private static string interfacePath = "Interface";
        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.interfaceconfig loadConfig()
        {
            Model.interfaceconfig model = CacheHelper.Get<Model.interfaceconfig>(interfacePath);
            if (model == null)
            {
                CacheHelper.Insert(interfacePath, dal.loadConfig(Utils.GetXmlMapPath(interfacePath)),
                    Utils.GetXmlMapPath(interfacePath));
                model = CacheHelper.Get<Model.interfaceconfig>(interfacePath);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.interfaceconfig saveConifg(Model.interfaceconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(interfacePath));
        }

    }
}
