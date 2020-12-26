using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWebCourse
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selected = TextBox1.Text;
            Service webClient = new Service();
            GridView1.DataSource = webClient.Res(selected);
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string selected = TextBox2.Text;
            Service webClient = new Service();
            GridView1.DataSource = webClient.Res2(selected);
            GridView1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string selected = TextBox3.Text;
            Service webClient = new Service();
            GridView1.DataSource = webClient.Res3(selected);
            GridView1.DataBind();
        }
    }
}