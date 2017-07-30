using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
namespace DTcms.DAL
    {
    //dt_group
    public partial class dt_group
        {

        public bool Exists (int id)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_group");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add (DTcms.Model.dt_group model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_group(");
            strSql.Append("groupname,add_time,avatar");
            strSql.Append(") values (");
            strSql.Append("@groupname,@add_time,@avatar");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@groupname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@add_time", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@avatar", SqlDbType.VarChar,150)             
              
            };

            parameters[0].Value = model.groupname;
            parameters[1].Value = model.add_time;
            parameters[2].Value = model.avatar;

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
        public bool Update (DTcms.Model.dt_group model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_group set ");

            strSql.Append(" groupname = @groupname , ");
            strSql.Append(" add_time = @add_time , ");
            strSql.Append(" avatar = @avatar  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@groupname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@add_time", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@avatar", SqlDbType.VarChar,150)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.groupname;
            parameters[2].Value = model.add_time;
            parameters[3].Value = model.avatar;
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
        public bool Delete (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_group ");
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
        /// 根据主表主键删除一条数据
        /// </summary>
        public bool DeleteId (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_group ");
            strSql.Append(" where caseid=@id");
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList (string idlist)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_group ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public DTcms.Model.dt_group GetModel (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, groupname, add_time, avatar  ");
            strSql.Append("  from dt_group ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_group model = new DTcms.Model.dt_group();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                model.groupname = ds.Tables[0].Rows[0]["groupname"].ToString();
                model.add_time = ds.Tables[0].Rows[0]["add_time"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();

                return model;
                }
            else
                {
                return null;
                }
            }

        /// <summary>
        /// 根据主表主键得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_group GetModelId (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, groupname, add_time, avatar  ");
            strSql.Append("  from dt_group ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_group model = new DTcms.Model.dt_group();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                model.groupname = ds.Tables[0].Rows[0]["groupname"].ToString();
                model.add_time = ds.Tables[0].Rows[0]["add_time"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();

                return model;
                }
            else
                {
                return null;
                }
            }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList (string strWhere)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM dt_group ");
            if (strWhere.Trim() != "")
                {
                strSql.Append(" where " + strWhere);
                }
            return DbHelperSQL.Query(strSql.ToString());
            }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList (int Top, string strWhere, string filedOrder)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
                {
                strSql.Append(" top " + Top.ToString());
                }
            strSql.Append(" * ");
            strSql.Append(" FROM dt_group ");
            if (strWhere.Trim() != "")
                {
                strSql.Append(" where " + strWhere);
                }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
            }


        }
    }

