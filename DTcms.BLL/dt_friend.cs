using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.BLL
    {
    //dt_friend
    public partial class dt_friend
        {

        private readonly DTcms.DAL.dt_friend dal = new DTcms.DAL.dt_friend();
        public dt_friend ()
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
        public int Add (DTcms.Model.dt_friend model)
            {
            return dal.Add(model);

            }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update (DTcms.Model.dt_friend model)
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
        public DTcms.Model.dt_friend GetModel (int id)
            {

            return dal.GetModel(id);
            }
        /// <summary>
        /// 根据主表主键得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_friend GetModelId (int id)
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
        public List<DTcms.Model.dt_friend> GetModelList (string strWhere)
            {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
            }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_friend> DataTableToList (DataTable dt)
            {
            List<DTcms.Model.dt_friend> modelList = new List<DTcms.Model.dt_friend>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
                {
                DTcms.Model.dt_friend model;
                for (int n = 0; n < rowsCount; n++)
                    {
                    model = new DTcms.Model.dt_friend();
                    if (dt.Rows[n]["id"].ToString() != "")
                        {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                        }
                    model.friend_groupname = dt.Rows[n]["friend_groupname"].ToString();
                    model.add_time = dt.Rows[n]["add_time"].ToString();
                    if (dt.Rows[n]["mid"].ToString() != "")
                        {
                        model.mid = int.Parse(dt.Rows[n]["mid"].ToString());
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.FriendItem> GetFriendItemList (string mid)
            {
            var Lists = new List<Model.FriendItem>();
            var frigrop = new BLL.dt_friend().GetModelList("mid=" + mid);
            BLL.dt_manager_info manager_info_bll = new BLL.dt_manager_info();
            var friend_info = new BLL.dt_friend_info();
            BLL.manager manager = new BLL.manager();
            foreach (var i in frigrop)
                {
                var Friend = new Model.FriendItem();
                Friend.id = i.id;
                Friend.groupname = i.friend_groupname;
                Friend.online = 2;
                Friend.list = new List<Model.ListItem>();
                var group = friend_info.GetModelList("zid=" + Friend.id);
                foreach (var item in group)
                    {
                    var m = new Model.ListItem();
                    m.avatar = manager_info_bll.GetModelId(item.fid).avatar;
                    m.id = item.id.ToString();
                    m.sign = manager_info_bll.GetModelId(item.fid).sign;
                    m.username = manager.GetModel(item.fid).real_name;
                    Friend.list.Add(m);
                    }
                Lists.Add(Friend);
                }
            return Lists;
            }
        }
    }