using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
namespace DTcms.DAL
    {
    //dt_manager_info
    public partial class dt_manager_info
        {

        public bool Exists (int id)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_manager_info");
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
        public int Add (DTcms.Model.dt_manager_info model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_manager_info(");
            strSql.Append("mid,name,avatar,sign");
            strSql.Append(") values (");
            strSql.Append("@mid,@name,@avatar,@sign");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@mid", SqlDbType.Int,4) ,            
                        new SqlParameter("@name", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@avatar", SqlDbType.VarChar,550) ,            
                        new SqlParameter("@sign", SqlDbType.VarChar,150)             
              
            };

            parameters[0].Value = model.mid;
            parameters[1].Value = model.name;
            parameters[2].Value = model.avatar;
            parameters[3].Value = model.sign;

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
        public bool Update (DTcms.Model.dt_manager_info model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_manager_info set ");
            strSql.Append(" name = @name , ");
            strSql.Append(" avatar = @avatar , ");
            strSql.Append(" sign = @sign  ");
            strSql.Append(" where mid=@mid ");

            SqlParameter[] parameters = {           
                        new SqlParameter("@mid", SqlDbType.Int,4) ,            
                        new SqlParameter("@name", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@avatar", SqlDbType.VarChar,550) ,            
                        new SqlParameter("@sign", SqlDbType.VarChar,150)             
              
            };
            parameters[0].Value = model.mid;
            parameters[1].Value = model.name;
            parameters[2].Value = model.avatar;
            parameters[3].Value = model.sign;
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
            strSql.Append("delete from dt_manager_info ");
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
            strSql.Append("delete from dt_manager_info ");
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
            strSql.Append("delete from dt_manager_info ");
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
        public DTcms.Model.dt_manager_info GetModel (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, mid, name, avatar, sign  ");
            strSql.Append("  from dt_manager_info ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_manager_info model = new DTcms.Model.dt_manager_info();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["mid"].ToString() != "")
                    {
                    model.mid = int.Parse(ds.Tables[0].Rows[0]["mid"].ToString());
                    }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
                model.sign = ds.Tables[0].Rows[0]["sign"].ToString();

                return model;
                }
            else
                {
                return null;
                }
            }

        /// <summary>
        /// 根据主表关联主键得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_manager_info GetModelId (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, mid, name, avatar, sign  ");
            strSql.Append("  from dt_manager_info ");
            strSql.Append(" where mid=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_manager_info model = new DTcms.Model.dt_manager_info();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["mid"].ToString() != "")
                    {
                    model.mid = int.Parse(ds.Tables[0].Rows[0]["mid"].ToString());
                    }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
                model.sign = ds.Tables[0].Rows[0]["sign"].ToString();

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
            strSql.Append(" FROM dt_manager_info ");
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
            strSql.Append(" FROM dt_manager_info ");
            if (strWhere.Trim() != "")
                {
                strSql.Append(" where " + strWhere);
                }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
            }


        }
    }

