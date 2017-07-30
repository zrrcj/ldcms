using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
    {
    public partial class site_dic : Web.UI.ManagePage
        {
        private int pid = 0;
        protected void Page_Load (object sender, EventArgs e)
            {
            this.pid = DTRequest.GetQueryInt("pid");
            if (!Page.IsPostBack)
                {
                ChkAdminLevel("site_dic", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind();
                RptBind(this.pid);
                }
            }
        #region 绑定菜单=============================
        private void TreeBind ()
            {
            BLL.dictionary bll = new BLL.dictionary();
            DataTable dt = bll.GetList(0);

            this.ddltype.Items.Clear();
            this.ddltype.Items.Add(new ListItem("查看所有", "0"));
            foreach (DataRow dr in dt.Rows)
                {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["dicname"].ToString().Trim();

                if (ClassLayer == 1)
                    {
                    this.ddltype.Items.Add(new ListItem(Title, Id));
                    }
                }
            }
        #endregion
        //数据绑定
        private void RptBind (int _pid)
            {
            ddltype.SelectedValue = _pid.ToString();
            BLL.dictionary bll = new BLL.dictionary();
            this.rptList.DataSource = bll.GetList_s(_pid);
            this.rptList.DataBind();
            }

        //美化列表
        protected void rptList_ItemDataBound (object sender, RepeaterItemEventArgs e)
            {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                //string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg1 = "";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                    {
                    LitFirst.Text = LitImg1;
                    }
                else
                    {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                    }
                }
            }

        //保存排序
        protected void btnSave_Click (object sender, EventArgs e)
            {
            ChkAdminLevel("site_dic", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.dictionary bll = new BLL.dictionary();
            for (int i = 0; i < rptList.Items.Count; i++)
                {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                    {
                    sortId = 99;
                    }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
                }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存字典排序"); //记录日志
            JscriptMsg("保存排序成功！", "site_dic.aspx?pid=" + ddltype.SelectedValue);
            }

        //删除字典
        protected void btnDelete_Click (object sender, EventArgs e)
            {
            ChkAdminLevel("site_dic", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.dictionary bll = new BLL.dictionary();
            for (int i = 0; i < rptList.Items.Count; i++)
                {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                    {
                    bll.Delete(id);
                    }
                }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除字典信息"); //记录日志
            JscriptMsg("删除数据成功！", "site_dic.aspx?pid=" + ddltype.SelectedValue, "parent.loadMenuTree");
            }

        protected void ddltype_SelectedIndexChanged (object sender, EventArgs e)
            {
            Response.Redirect(Utils.CombUrlTxt("site_dic.aspx", "pid={0}", ddltype.SelectedValue));
            }
        }
    }