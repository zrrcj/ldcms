using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.BLL
    {
    //dt_manager_info
    public partial class dt_manager_info
        {

        private readonly DTcms.DAL.dt_manager_info dal = new DTcms.DAL.dt_manager_info();
        public dt_manager_info ()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists (int id)
            {
            return dal.Exists(id);
            }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add (DTcms.Model.dt_manager_info model)
            {
            return dal.Add(model);

            }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update (DTcms.Model.dt_manager_info model)
            {
            return dal.Update(model);
            }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete (int id)
            {

            return dal.Delete(id);
            }
        /// <summary>
        ///根据主表主键 删除一条数据
        /// </summary>
        public bool DeleteId (int id)
            {

            return dal.DeleteId(id);
            }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_manager_info GetModel (int id)
            {

            return dal.GetModel(id);
            }
        /// <summary>
        /// 根据主表关联主键得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_manager_info GetModelId (int id)
            {

            return dal.GetModelId(id);
            }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList (string strWhere)
            {
            return dal.GetList(strWhere);
            }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList (int Top, string strWhere, string filedOrder)
            {
            return dal.GetList(Top, strWhere, filedOrder);
            }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_manager_info> GetModelList (string strWhere)
            {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
            }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_manager_info> DataTableToList (DataTable dt)
            {
            List<DTcms.Model.dt_manager_info> modelList = new List<DTcms.Model.dt_manager_info>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
                {
                DTcms.Model.dt_manager_info model;
                for (int n = 0; n < rowsCount; n++)
                    {
                    model = new DTcms.Model.dt_manager_info();
                    if (dt.Rows[n]["id"].ToString() != "")
                        {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                        }
                    if (dt.Rows[n]["mid"].ToString() != "")
                        {
                        model.mid = int.Parse(dt.Rows[n]["mid"].ToString());
                        }
                    model.name = dt.Rows[n]["name"].ToString();
                    model.avatar = dt.Rows[n]["avatar"].ToString();
                    model.sign = dt.Rows[n]["sign"].ToString();


                    modelList.Add(model);
                    }
                }
            return modelList;
            }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList ()
            {
            return GetList("");
            }
        #endregion

        }
    }