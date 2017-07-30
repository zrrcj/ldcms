using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;//Please add references
namespace ZNTtj.Interface.DAL
{
    /// <summary>
    /// 数据访问类:zn_interface_blacklist
    /// </summary>
    public partial class zn_interface_blacklist
    {
        public zn_interface_blacklist()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from zn_interface_blacklist");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool Exists(string ip)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from zn_interface_blacklist");
            strSql.Append(" where ip=@ip and isvalid=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ip", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = ip;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZNTtj.Interface.Model.zn_interface_blacklist model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into zn_interface_blacklist(");
            strSql.Append("ip,addtime,isvalid,count)");
            strSql.Append(" values (");
            strSql.Append("@ip,@addtime,@isvalid,@count)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ip", SqlDbType.NVarChar,20),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@isvalid", SqlDbType.Int,4),
					new SqlParameter("@count", SqlDbType.Int,4)};
            parameters[0].Value = model.ip;
            parameters[1].Value = model.addtime;
            parameters[2].Value = model.isvalid;
            parameters[3].Value = model.count;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(ZNTtj.Interface.Model.zn_interface_blacklist model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update zn_interface_blacklist set ");
            strSql.Append("ip=@ip,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("isvalid=@isvalid,");
            strSql.Append("count=@count");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@ip", SqlDbType.NVarChar,20),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@isvalid", SqlDbType.Int,4),
					new SqlParameter("@count", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.ip;
            parameters[1].Value = model.addtime;
            parameters[2].Value = model.isvalid;
            parameters[3].Value = model.count;
            parameters[4].Value = model.id;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(int id, int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update zn_interface_blacklist set ");
            strSql.Append("count=@count");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = count+1;
            parameters[1].Value = id;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from zn_interface_blacklist ");
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
            strSql.Append("delete from zn_interface_blacklist ");
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
        /// 得到一个对象实体
        /// </summary>
        public ZNTtj.Interface.Model.zn_interface_blacklist GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,ip,addtime,isvalid,count from zn_interface_blacklist ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            ZNTtj.Interface.Model.zn_interface_blacklist model = new ZNTtj.Interface.Model.zn_interface_blacklist();
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
        public ZNTtj.Interface.Model.zn_interface_blacklist GetModel(string ip)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,ip,addtime,isvalid,count from zn_interface_blacklist ");
            strSql.Append(" where ip=@ip");
            SqlParameter[] parameters = {
					new SqlParameter("@ip", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = ip;

            ZNTtj.Interface.Model.zn_interface_blacklist model = new ZNTtj.Interface.Model.zn_interface_blacklist();
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
        public ZNTtj.Interface.Model.zn_interface_blacklist DataRowToModel(DataRow row)
        {
            ZNTtj.Interface.Model.zn_interface_blacklist model = new ZNTtj.Interface.Model.zn_interface_blacklist();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["ip"] != null)
                {
                    model.ip = row["ip"].ToString();
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["isvalid"] != null && row["isvalid"].ToString() != "")
                {
                    model.isvalid = int.Parse(row["isvalid"].ToString());
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ip,addtime,isvalid,count ");
            strSql.Append(" FROM zn_interface_blacklist ");
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
            strSql.Append(" id,ip,addtime,isvalid,count ");
            strSql.Append(" FROM zn_interface_blacklist ");
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
            strSql.Append("select count(1) FROM zn_interface_blacklist ");
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
            strSql.Append(")AS Row, T.*  from zn_interface_blacklist T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "zn_interface_blacklist";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

