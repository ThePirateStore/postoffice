﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace POSTOFFICE3
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            string sqlQuery;
            string username;
            if (Session["username"] == null)
            {
                Response.Redirect("Home.aspx");


            }
            loggedin.Text = "Currently logged in as: " + Session["username"];
            username = Session["username"].ToString();

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostOffice"].ToString();
                conn = new SqlConnection(connectionString);

                conn.Open();

                sqlQuery = "SELECT dbo.CUSTOMER.Customer_ID FROM dbo.CUSTOMER WHERE dbo.CUSTOMER.Email = @username";
                command = new SqlCommand(sqlQuery, conn);
                command.Parameters.AddWithValue("@username", username);
                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Session["customer_id"] = dataReader.GetValue(dataReader.GetOrdinal("Customer_ID")).ToString();
                    }
                }
                dataReader.Close();
                command.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                loggedin.Text = "ERROR FOR USERNAME LOGIN";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }
    }
}