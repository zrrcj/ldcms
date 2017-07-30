using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DTcms.BLL
{
    /// <summary>
    /// dictionary
    /// </summary>
    public partial class dictionary
    {
        private List<DTcms.Model.dictionary> d_ms = new List<DTcms.Model.dictionary>();
        private readonly DTcms.DAL.dictionary dal = new DTcms.DAL.dictionary();
        public dictionary()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        public bool IsSelect(string id)
        {
            int pid = int.Parse(id);
            DataSet ds = GetList("pid=" + id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.dictionary model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dictionary model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dictionary GetModel(int id)
        {

            return dal.GetModel(id);
        }
        /// <summary>
        /// 取得所有类别列表(已经排序好)
        /// </summary>
        /// <param name="parent_id"></param>
        /// <returns></returns>
        public DataTable GetList(int parent_id)
        {
            return dal.GetList(parent_id);
        }
        /// <summary>
        /// 取得所有类别列表(已经排序好)(字典列表专用)
        /// </summary>
        /// <param name="parent_id"></param>
        /// <returns></returns>
        public DataTable GetList_s(int parent_id)
        {
            return dal.GetList_s(parent_id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dictionary> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dictionary> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.dictionary> modelList = new List<DTcms.Model.dictionary>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.dictionary model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 通过第三级获得其它级
        /// </summary>
        public string GetViewList(int id, string table_name)
        {
            DataSet ds = dal.GetViewList(id, table_name);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            else
            {
                string s = ds.Tables[0].Rows[0][1].ToString() + ">" + ds.Tables[0].Rows[0][2].ToString() + ">" + ds.Tables[0].Rows[0][3].ToString();
                return s;
            }
        }

        /// <summary>
        /// 获取dicname
        /// </summary>
        public string GetDicnames(string ids)
        {
            string[] id = ids.Trim(',').Split(',');
            ids = ",";
            for (int n = 0; n < id.Length; n++)
            {
                try
                {
                    ids += dal.GetDicname(int.Parse(id[n].ToString()));
                    ids += ",";
                }
                catch
                {
                    ids = "";
                }
            }
            return ids;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string strWhere)
        {
            return dal.Exists(strWhere);
        }

        /// <summary>
        /// 通过id获取专家类别
        /// </summary>
        public string getZj_Type(string ids)
        {
            string types = "";
            string[] ZJ_types = ids.Trim(',').Split(',');
            bool is1 = true;
            for (int n = 0; n < ZJ_types.Length; n++)
            {
                string name = GetViewList(int.Parse(ZJ_types[n].ToString()), "v_class");
                if (name == "")
                {
                    continue;
                }
                if (!is1)
                {
                    types += ",";
                }
                else
                {
                    is1 = false;
                }
                types += name;
            }
            return types;
        }
        /// <summary>
        /// 获取字典名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(int id)
        {
            try
            {
                return dal.GetModel(id).dicname;
            }
            catch {
                return "";
            }
        }
        /// <summary>
        /// 获取detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDteail(int id)
        {
            try
            {
                return dal.GetModel(id).detail;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 通过detail获取id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetID(string detail)
        {
            try
            {
                return dal.getID(detail);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 通过名字获取id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameID(string name)
        {
            try
            {
                return dal.getNameID(name);
            }
            catch
            {
                return "";
            }
        }
        #endregion  ExtensionMethod
    }
}
