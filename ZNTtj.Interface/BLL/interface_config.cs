using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using DTcms.Common;
using ZNTtj.Interface.Model;


namespace ZNTtj.Interface.BLL
{
    public partial class interfaceconfig
    {
        private readonly DAL.interface_config dal = new DAL.interface_config();
        //web.cofig中配置的节点名称
        //private static string interfacePath = "Interface";
        ///// <summary>
        /////  读取配置文件
        ///// </summary>
        //public Model.interfaceconfig loadConfig()
        //{
        //    Model.interfaceconfig model = CacheHelper.Get<Model.interfaceconfig>(interfacePath);
        //    if (model == null)
        //    {
        //        CacheHelper.Insert(interfacePath, dal.loadConfig(Utils.GetXmlMapPath(interfacePath)),
        //            Utils.GetXmlMapPath(interfacePath));
        //        model = CacheHelper.Get<Model.interfaceconfig>(interfacePath);
        //    }
        //    return model;
        //}

        ///// <summary>
        /////  保存配置文件
        ///// </summary>
        //public Model.interfaceconfig saveConifg(Model.interfaceconfig model)
        //{
        //    return dal.saveConifg(model, Utils.GetXmlMapPath(interfacePath));
        //}
        /// <summary>
        /// 根据接口名称返回接口配置
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public Model.interfaceModel getInterFace(string name)
        {
            DAL.interface_config s = new DAL.interface_config();
            List<interfaceModel> li = dal.getList(new interfaceModel(), Utils.GetXmlMapPath("Interface"), "interfaceconfig");//获取接口配置信息
            Predicate<interfaceModel> findValue = delegate(interfaceModel p)
            {
                return p.method.IndexOf("," + name + ",") != -1 ? true : false;
            };
            return li.Find(findValue);
        }

    }
}
