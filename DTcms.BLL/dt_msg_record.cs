using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.BLL
    {
    //dt_msg_record
    public partial class dt_msg_record
        {

        private readonly DTcms.DAL.dt_msg_record dal = new DTcms.DAL.dt_msg_record();
        public dt_msg_record ()
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
        public int Add (DTcms.Model.dt_msg_record model)
            {
            return dal.Add(model);

            }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update (DTcms.Model.dt_msg_record model)
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
        public DTcms.Model.dt_msg_record GetModel (int id)
            {

            return dal.GetModel(id);
            }
        /// <summary>
        /// 根据主表主键得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_msg_record GetModelId (int id)
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
        public List<DTcms.Model.dt_msg_record> GetModelList (string strWhere)
            {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
            }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_msg_record> DataTableToList (DataTable dt)
            {
            List<DTcms.Model.dt_msg_record> modelList = new List<DTcms.Model.dt_msg_record>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
                {
                DTcms.Model.dt_msg_record model;
                for (int n = 0; n < rowsCount; n++)
                    {
                    model = new DTcms.Model.dt_msg_record();
                    if (dt.Rows[n]["id"].ToString() != "")
                        {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                        }
                    if (dt.Rows[n]["uid"].ToString() != "")
                        {
                        model.uid = int.Parse(dt.Rows[n]["uid"].ToString());
                        }
                    if (dt.Rows[n]["toid"].ToString() != "")
                        {
                        model.toid = int.Parse(dt.Rows[n]["toid"].ToString());
                        }
                    model.content = dt.Rows[n]["content"].ToString();
                    model.add_time = dt.Rows[n]["add_time"].ToString();
                    if (dt.Rows[n]["status"].ToString() != "")
                        {
                        model.status = int.Parse(dt.Rows[n]["status"].ToString());
                        }
                    if (dt.Rows[n]["isgroupmsg"].ToString() != "")
                        {
                        model.isgroupmsg = int.Parse(dt.Rows[n]["isgroupmsg"].ToString());
                        }


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