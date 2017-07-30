using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTcms.DBUtility;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:dictionary
    /// </summary>
    public partial class dictionary
    {
        public dictionary()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_dictionary");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.dictionary model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into dt_dictionary(");
                        strSql.Append("pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail)");
                        strSql.Append(" values (");
                        strSql.Append("@pid,@dicname,@addtime,@adduserid,@sort_id,@class_list,@class_layer,@detail)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@dicname", SqlDbType.NVarChar,500),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@adduserid", SqlDbType.Int,4),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@class_list", SqlDbType.NVarChar,50),
					new SqlParameter("@class_layer", SqlDbType.Int,4),
                    new SqlParameter("@detail", SqlDbType.NVarChar)};
                        parameters[0].Value = model.pid;
                        parameters[1].Value = model.dicname;
                        parameters[2].Value = model.addtime;
                        parameters[3].Value = model.adduserid;
                        parameters[4].Value = model.sort_id;
                        parameters[5].Value = model.class_list;
                        parameters[6].Value = model.class_layer;
                        parameters[7].Value = model.detail;

                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务

                        model.id = Convert.ToInt32(obj); //得到刚插入的新ID
                        if (model.pid > 0)
                        {
                            Model.dictionary model2 = GetModel(conn, trans, model.pid); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        //修改节点列表和深度
                        DbHelperSQL.ExecuteSql(conn, trans, "update dt_dictionary set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
                        //如无异常则提交事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
        }
        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id">待查询的节点</param>
        /// <param name="parent_id">父节点</param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_dictionary ");
            strSql.Append(" where class_list like '%," + id + ",%' and id=" + parent_id);
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 修改子节点的ID列表及深度（自身迭代）
        /// </summary>
        /// <param name="parent_id"></param>
        private void UpdateChilds(SqlConnection conn, SqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.dictionary model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
            string strSql = "select id from dt_dictionary where pid=" + parent_id;
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperSQL.ExecuteSql(conn, trans, "update dt_dictionary set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务

                    //调用自身迭代
                    this.UpdateChilds(conn, trans, id); //带事务
                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dictionary model)
        {
            Model.dictionary oldModel = GetModel(model.id); //旧的数据
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //先判断选中的父节点是否被包含
                        if (IsContainNode(model.id, model.pid))
                        {
                            //查找旧父节点数据
                            string class_list = "," + model.pid + ",";
                            int class_layer = 1;
                            if (oldModel.pid > 0)
                            {
                                Model.dictionary oldParentModel = GetModel(conn, trans, oldModel.pid); //带事务
                                class_list = oldParentModel.class_list + model.pid + ",";
                                class_layer = oldParentModel.class_layer + 1;
                            }
                            //先提升选中的父节点
                            DbHelperSQL.ExecuteSql(conn, trans, "update dt_dictionary set pid=" + oldModel.pid + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.pid); //带事务
                            UpdateChilds(conn, trans, model.pid); //带事务
                        }
                        //更新子节点
                        if (model.pid > 0)
                        {
                            Model.dictionary model2 = GetModel(conn, trans, model.pid); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update dt_dictionary set ");
                        strSql.Append("pid=@pid,");
                        strSql.Append("dicname=@dicname,");
                        strSql.Append("addtime=@addtime,");
                        strSql.Append("adduserid=@adduserid,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("class_list=@class_list,");
                        strSql.Append("class_layer=@class_layer,");
                        strSql.Append("detail=@detail");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@dicname", SqlDbType.NVarChar,500),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@adduserid", SqlDbType.Int,4),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@class_list", SqlDbType.NVarChar,50),
					new SqlParameter("@class_layer", SqlDbType.Int,4),
					new SqlParameter("@detail", SqlDbType.NVarChar,500),
					new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.pid;
                        parameters[1].Value = model.dicname;
                        parameters[2].Value = model.addtime;
                        parameters[3].Value = model.adduserid;
                        parameters[4].Value = model.sort_id;
                        parameters[5].Value = model.class_list;
                        parameters[6].Value = model.class_layer;
                        parameters[7].Value = model.detail;
                        parameters[8].Value = model.id;

                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //更新子节点
                        UpdateChilds(conn, trans, model.id);
                        //如无发生错误则提交事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_dictionary set " + strValue);
            strSql.Append(" where id=" + id);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_dictionary ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_dictionary ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.dictionary GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail from dt_dictionary ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.dictionary model = new Model.dictionary();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pid"].ToString() != "")
                {
                    model.pid = int.Parse(ds.Tables[0].Rows[0]["pid"].ToString());
                }
                model.dicname = ds.Tables[0].Rows[0]["dicname"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = (DateTime)ds.Tables[0].Rows[0]["addtime"];
                }
                if (ds.Tables[0].Rows[0]["adduserid"].ToString() != "")
                {
                    model.adduserid = int.Parse(ds.Tables[0].Rows[0]["adduserid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                model.class_list = ds.Tables[0].Rows[0]["class_list"].ToString();
                if (ds.Tables[0].Rows[0]["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(ds.Tables[0].Rows[0]["class_layer"].ToString());
                }
                model.detail = ds.Tables[0].Rows[0]["detail"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dictionary GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail from dt_dictionary ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            DTcms.Model.dictionary model = new DTcms.Model.dictionary();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
       

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dictionary DataRowToModel(DataRow row)
        {
            DTcms.Model.dictionary model = new DTcms.Model.dictionary();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["pid"] != null && row["pid"].ToString() != "")
                {
                    model.pid = int.Parse(row["pid"].ToString());
                }
                if (row["dicname"] != null)
                {
                    model.dicname = row["dicname"].ToString();
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = (DateTime)row["addtime"];
                }
                if (row["adduserid"] != null && row["adduserid"].ToString() != "")
                {
                    model.adduserid = int.Parse(row["adduserid"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["class_list"] != null)
                {
                    model.class_list = row["class_list"].ToString();
                }
                if (row["class_layer"] != null && row["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(row["class_layer"].ToString());
                }
                if (row["detail"] != null)
                {
                    model.detail = row["detail"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 取得所有类别列表(已经排序好)
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail ");
            strSql.Append(" FROM dt_dictionary ");
            strSql.Append(" order by sort_id asc,id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds_s(oldData, newData, parent_id);
            return newData;
        }
        /// <summary>
        /// 取得所有类别列表(已经排序好)(字典列表专用)
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetList_s(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail ");
            strSql.Append(" FROM dt_dictionary ");
            strSql.Append(" order by sort_id asc,id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            if (parent_id == 0)
            {
                GetChilds_s(oldData, newData, parent_id);
            }
            //调用迭代组合成DAGATABLE
            else
            {
                GetChilds(oldData, newData, parent_id);
            }
            return newData;
        }
        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("pid=" + parent_id + " or id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["pid"] = int.Parse(dr[i]["pid"].ToString());
                row["dicname"] = dr[i]["dicname"].ToString();
                row["addtime"] = (DateTime)dr[i]["addtime"];
                row["adduserid"] = int.Parse(dr[i]["adduserid"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["detail"] = dr[i]["detail"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                int id = int.Parse(dr[i]["id"].ToString());
                if (id != parent_id)
                {
                    this.GetChilds_s(oldData, newData, id);
                }

            }
        }
        private void GetChilds_s(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("pid=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["pid"] = int.Parse(dr[i]["pid"].ToString());
                row["dicname"] = dr[i]["dicname"].ToString();
                row["addtime"] = (DateTime)dr[i]["addtime"];
                row["adduserid"] = int.Parse(dr[i]["adduserid"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["detail"] = dr[i]["detail"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds_s(oldData, newData, int.Parse(dr[i]["id"].ToString()));
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail ");
            strSql.Append(" FROM dt_dictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,pid,dicname,addtime,adduserid,sort_id,class_list,class_layer,detail ");
            strSql.Append(" FROM dt_dictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_dictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from dt_dictionary T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 通过第三级获得其它级
        /// </summary>
        public DataSet GetViewList(int id, string table_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + table_name);
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 获取dicname
        /// </summary>
        public string GetDicname(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select dicname from dt_dictionary");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0].Rows[0]["dicname"].ToString();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_dictionary");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 通过Detail获取ID
        /// </summary>
        public string getID(string detail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from dt_dictionary where detail='" + detail + "' and class_layer=4");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0].Rows[0]["id"].ToString();
        }
        /// <summary>
        /// 通过名字获取ID
        /// </summary>
        public string getNameID(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from dt_dictionary where dicname='" + name + "' and class_layer=4");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0].Rows[0]["id"].ToString();
        }
        #endregion  ExtensionMethod
    }
}

