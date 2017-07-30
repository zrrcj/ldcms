using System;
namespace DTcms.Model
{
    /// <summary>
    /// yq_dictionary:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dictionary
    {
        public dictionary()
        { }
        #region Model
        private int _id;
        private int _pid;
        private string _dicname;
        private DateTime _addtime;
        private int _adduserid;
        private int _sort_id;
        private string _class_list;
        private int _class_layer;
        private string _detail;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dicname
        {
            set { _dicname = value; }
            get { return _dicname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int adduserid
        {
            set { _adduserid = value; }
            get { return _adduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string class_list
        {
            set { _class_list = value; }
            get { return _class_list; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int class_layer
        {
            set { _class_layer = value; }
            get { return _class_layer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string detail
        {
            set { _detail = value; }
            get { return _detail; }
        }
        #endregion Model

    }
}

