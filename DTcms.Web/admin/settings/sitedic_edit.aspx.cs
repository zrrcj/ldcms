using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
namespace DTcms.Web.admin.settings
{
    public partial class sitedic_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        private int pid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.id = DTRequest.GetQueryInt("id");
            this.pid = DTRequest.GetQueryInt("pid");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.dictionary().Exists(this.id))
                {
                    JscriptMsg("字典不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("site_dic", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.pid); //绑定导航菜单

                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    if (this.id > 0)
                    {
                        this.ddlParentId.SelectedValue = this.id.ToString();
                    }
                }
            }
        }

        #region 绑定导航菜单=============================
        private void TreeBind(int _pid)
        {
            BLL.dictionary bll = new BLL.dictionary();
            DataTable dt = bll.GetList(0);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级导航", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["dicname"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
            this.ddlParentId.SelectedValue = _pid.ToString();
        }
        #endregion


        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.dictionary bll = new BLL.dictionary();
            Model.dictionary model = bll.GetModel(_id);

            ddlParentId.SelectedValue = model.pid.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtRemark.Text = model.detail;          
            txtTitle.Text = model.dicname;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                Model.dictionary model = new Model.dictionary();
                BLL.dictionary bll = new BLL.dictionary();

                model.dicname = txtTitle.Text;
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.detail = txtRemark.Text.Trim();
                model.addtime = DateTime.Now;
                model.adduserid = GetAdminInfo().id;
                model.pid = int.Parse(ddlParentId.SelectedValue);

                if (bll.Add(model) > 0)
                {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加字典信息:" + model.dicname); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.dictionary bll = new BLL.dictionary();
                Model.dictionary model = bll.GetModel(_id);

                model.dicname = txtTitle.Text.Trim();
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.detail = txtRemark.Text.Trim();
                model.pid = int.Parse(ddlParentId.SelectedValue);

                if (bll.Update(model))
                {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "修改字典信息:" + model.dicname); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("site_dic", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改字典成功！", "site_dic.aspx?pid="+this.pid, "parent.loadMenuTree");
            }
            else //添加
            {
                ChkAdminLevel("site_dic", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加字典成功！", "site_dic.aspx?pid=" + this.pid, "parent.loadMenuTree");
            }
        }

    }
}