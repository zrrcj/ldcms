using DTcms.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ZNTtj.Interface.DAL
{
    /// <summary>
    /// 数据访问类：接口调用日志
    /// </summary>
    public class zn_interface_log
    {

        #region  基本方发
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            //生成数据

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from zn_interface_log");
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
        public int Add(Model.zn_interface_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into zn_interface_log(");
            strSql.Append("name,clientip,clienttype,clientversion,responsecode,requestinfo,begintime,endtime,timespans,responseinfo)");
            strSql.Append(" values (");
            strSql.Append("@name,@clientip,@clienttype,@clientversion,@responsecode,@requestinfo,@begintime,@endtime,@timespans,@responseinfo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@clientip", SqlDbType.NVarChar,20),
					new SqlParameter("@clienttype", SqlDbType.Int,4),
					new SqlParameter("@clientversion", SqlDbType.NVarChar,100),
					new SqlParameter("@responsecode", SqlDbType.NVarChar,50),
					new SqlParameter("@requestinfo", SqlDbType.NVarChar,500),
					new SqlParameter("@begintime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
					new SqlParameter("@timespans", SqlDbType.Int,4),
					new SqlParameter("@responseinfo", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.clientip;
            parameters[2].Value = model.clienttype;
            parameters[3].Value = model.clientversion;
            parameters[4].Value = model.responsecode;
            parameters[5].Value = model.requestinfo;
            parameters[6].Value = model.begintime;
            parameters[7].Value = System.DateTime.Now;
            parameters[8].Value = model.timespans;
            parameters[9].Value = model.responseinfo;

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
        public bool Update(Model.zn_interface_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update zn_interface_log set ");
            strSql.Append("name=@name,");
            strSql.Append("clientip=@clientip,");
            strSql.Append("clienttype=@clienttype,");
            strSql.Append("clientversion=@clientversion,");
            strSql.Append("responsecode=@responsecode,");
            strSql.Append("requestinfo=@requestinfo,");
            strSql.Append("begintime=@begintime,");
            strSql.Append("endtime=@endtime,");
            strSql.Append("timespans=@timespans,");
            strSql.Append("responseinfo=@responseinfo");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@clientip", SqlDbType.NVarChar,20),
					new SqlParameter("@clienttype", SqlDbType.Int,4),
					new SqlParameter("@clientversion", SqlDbType.NVarChar,100),
					new SqlParameter("@responsecode", SqlDbType.NVarChar,50),
					new SqlParameter("@requestinfo", SqlDbType.NVarChar,500),
					new SqlParameter("@begintime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
					new SqlParameter("@timespans", SqlDbType.Int,4),
					new SqlParameter("@responseinfo", SqlDbType.NVarChar,500),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.clientip;
            parameters[2].Value = model.clienttype;
            parameters[3].Value = model.clientversion;
            parameters[4].Value = model.responsecode;
            parameters[5].Value = model.requestinfo;
            parameters[6].Value = model.begintime;
            parameters[7].Value = model.endtime;
            parameters[8].Value = model.timespans;
            parameters[9].Value = model.responseinfo;
            parameters[10].Value = model.id;

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
            strSql.Append("delete from zn_interface_log ");
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
            strSql.Append("delete from zn_interface_log ");
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
        public Model.zn_interface_log GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,clientip,clienttype,clientversion,responsecode,requestinfo,begintime,endtime,timespans,responseinfo from zn_interface_log ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.zn_interface_log model = new Model.zn_interface_log();
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
        public Model.zn_interface_log DataRowToModel(DataRow row)
        {
            Model.zn_interface_log model = new Model.zn_interface_log();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["clientip"] != null)
                {
                    model.clientip = row["clientip"].ToString();
                }
                if (row["clienttype"] != null && row["clienttype"].ToString() != "")
                {
                    model.clienttype = int.Parse(row["clienttype"].ToString());
                }
                if (row["clientversion"] != null)
                {
                    model.clientversion = row["clientversion"].ToString();
                }
                if (row["responsecode"] != null)
                {
                    model.responsecode =int.Parse( row["responsecode"].ToString());
                }
                if (row["requestinfo"] != null)
                {
                    model.requestinfo = row["requestinfo"].ToString();
                }
                if (row["begintime"] != null && row["begintime"].ToString() != "")
                {
                    model.begintime = DateTime.Parse(row["begintime"].ToString());
                }
                if (row["endtime"] != null && row["endtime"].ToString() != "")
                {
                    model.endtime = DateTime.Parse(row["endtime"].ToString());
                }
                if (row["timespans"] != null && row["timespans"].ToString() != "")
                {
                    model.timespans = int.Parse(row["timespans"].ToString());
                }
                if (row["responseinfo"] != null)
                {
                    model.responseinfo = row["responseinfo"].ToString();
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
            strSql.Append("select id,name,clientip,clienttype,clientversion,responsecode,requestinfo,begintime,endtime,timespans,responseinfo ");
            strSql.Append(" FROM zn_interface_log ");
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
            strSql.Append(" id,name,clientip,clienttype,clientversion,responsecode,requestinfo,begintime,endtime,timespans,responseinfo ");
            strSql.Append(" FROM zn_interface_log ");
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
            strSql.Append("select count(1) FROM zn_interface_log ");
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
        ///  分页获取数据列表
        /// </summary>
        /// <param name="strWhere">分页条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="startIndex">开始页码</param>
        /// <param name="endIndex">结束页码</param>
        /// <returns></returns>
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
            strSql.Append(")AS Row, T.*  from zn_interface_log T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //根据策略校验数据

        //更新数据
        #endregion 基本方法
    }

}
