using System;
namespace DTcms.Model
{
    /// <summary>
    /// 会员主表
    /// </summary>
    [Serializable]
    public partial class interfaceconfig
    {
        public interfaceconfig()
        { }
        #region Model
        private int _isuse=0;
        private int _paramenc = 0;
        private string _enckey;

        /// <summary>
        /// 是否启用
        /// </summary>
        public int isuse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
        /// <summary>
        /// 是否加密
        /// </summary>
        public int paramenc
        {
            set { _paramenc = value; }
            get { return _paramenc; }
        }
        /// <summary>
        /// 加密Key
        /// </summary>
        public string enckey
        {
            set { _enckey = value; }
            get { return _enckey; }
        }
        #endregion

    }
}