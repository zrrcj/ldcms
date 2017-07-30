using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
namespace DTcms.DAL
    {
    //dt_msg_record
    public partial class dt_msg_record
        {

        public bool Exists (int id)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_msg_record");
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
        public int Add (DTcms.Model.dt_msg_record model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_msg_record(");
            strSql.Append("uid,toid,content,add_time,status,isgroupmsg");
            strSql.Append(") values (");
            strSql.Append("@uid,@toid,@content,@add_time,@status,@isgroupmsg");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@uid", SqlDbType.Int,4) ,            
                        new SqlParameter("@toid", SqlDbType.Int,4) ,            
                        new SqlParameter("@content", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@add_time", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@status", SqlDbType.Int,4) ,            
                        new SqlParameter("@isgroupmsg", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.uid;
            parameters[1].Value = model.toid;
            parameters[2].Value = model.content;
            parameters[3].Value = model.add_time;
            parameters[4].Value = model.status;
            parameters[5].Value = model.isgroupmsg;

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
        public bool Update (DTcms.Model.dt_msg_record model)
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_msg_record set ");

            strSql.Append(" uid = @uid , ");
            strSql.Append(" toid = @toid , ");
            strSql.Append(" content = @content , ");
            strSql.Append(" add_time = @add_time , ");
            strSql.Append(" status = @status , ");
            strSql.Append(" isgroupmsg = @isgroupmsg  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@uid", SqlDbType.Int,4) ,            
                        new SqlParameter("@toid", SqlDbType.Int,4) ,            
                        new SqlParameter("@content", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@add_time", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@status", SqlDbType.Int,4) ,            
                        new SqlParameter("@isgroupmsg", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.uid;
            parameters[2].Value = model.toid;
            parameters[3].Value = model.content;
            parameters[4].Value = model.add_time;
            parameters[5].Value = model.status;
            parameters[6].Value = model.isgroupmsg;
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
            strSql.Append("delete from dt_msg_record ");
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
            strSql.Append("delete from dt_msg_record ");
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
            strSql.Append("delete from dt_msg_record ");
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
        public DTcms.Model.dt_msg_record GetModel (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, uid, toid, content, add_time, status, isgroupmsg  ");
            strSql.Append("  from dt_msg_record ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_msg_record model = new DTcms.Model.dt_msg_record();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["uid"].ToString() != "")
                    {
                    model.uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["toid"].ToString() != "")
                    {
                    model.toid = int.Parse(ds.Tables[0].Rows[0]["toid"].ToString());
                    }
                model.content = ds.Tables[0].Rows[0]["content"].ToString();
                model.add_time = ds.Tables[0].Rows[0]["add_time"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                    {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["isgroupmsg"].ToString() != "")
                    {
                    model.isgroupmsg = int.Parse(ds.Tables[0].Rows[0]["isgroupmsg"].ToString());
                    }

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
        public DTcms.Model.dt_msg_record GetModelId (int id)
            {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, uid, toid, content, add_time, status, isgroupmsg  ");
            strSql.Append("  from dt_msg_record ");
            strSql.Append(" where caseid=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            DTcms.Model.dt_msg_record model = new DTcms.Model.dt_msg_record();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["uid"].ToString() != "")
                    {
                    model.uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["toid"].ToString() != "")
                    {
                    model.toid = int.Parse(ds.Tables[0].Rows[0]["toid"].ToString());
                    }
                model.content = ds.Tables[0].Rows[0]["content"].ToString();
                model.add_time = ds.Tables[0].Rows[0]["add_time"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                    {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                    }
                if (ds.Tables[0].Rows[0]["isgroupmsg"].ToString() != "")
                    {
                    model.isgroupmsg = int.Parse(ds.Tables[0].Rows[0]["isgroupmsg"].ToString());
                    }

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
            strSql.Append(" FROM dt_msg_record ");
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
            strSql.Append(" FROM dt_msg_record ");
            if (strWhere.Trim() != "")
                {
                strSql.Append(" where " + strWhere);
                }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
            }


        }
    }

