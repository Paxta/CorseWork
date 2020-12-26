using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;


[WebService(Namespace = "http://payobsh/WebSite2")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Service : System.Web.Services.WebService
{
    private string str1 = "Data Source=LAPTOP-CMSS62U7\\SQLEXPRESS;Initial Catalog=TP_COURSE;Persist Security Info=True;User ID=Parviz;Password=aaaaa0;";
    public Service()
    {

    }

    [WebMethod]
    public DataSet Res(string ID)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(str1);
        SqlDataAdapter Avia = new SqlDataAdapter("Res", con);
        Avia.SelectCommand.CommandType = CommandType.StoredProcedure;
        Avia.SelectCommand.Parameters.Add("@FIO", SqlDbType.NChar, 50).Value = ID;
        Avia.Fill(ds);
        con.Close();
        return ds;
    }

    [WebMethod]
    public DataSet Res2(string subject)
    {
        DataSet ds = new DataSet();

        SqlConnection con = new SqlConnection(str1);
        SqlDataAdapter Avia = new SqlDataAdapter("Res2", con);

        Avia.SelectCommand.CommandType = CommandType.StoredProcedure;
        Avia.SelectCommand.Parameters.Add("@subject", SqlDbType.NChar, 50).Value = subject;
        Avia.Fill(ds);
        con.Close();
        return ds;
    }

    [WebMethod]
    public DataSet Res3(string IDProf)
    {
        DataSet ds = new DataSet();

        SqlConnection con = new SqlConnection(str1);
        SqlDataAdapter Avia = new SqlDataAdapter("Res3", con);

        Avia.SelectCommand.CommandType = CommandType.StoredProcedure;
        Avia.SelectCommand.Parameters.Add("@IDProf", SqlDbType.NChar, 50).Value = IDProf;
        Avia.Fill(ds);
        con.Close();
        return ds;
    }

}
