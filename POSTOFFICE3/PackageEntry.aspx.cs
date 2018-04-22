﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace POSTOFFICE3
{
    public partial class PackageEntry : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader dataReader;
        string sqlQuery;
        string senderAddressID;
        string receiverAddressID;
        string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            username = Session["username"].ToString();
        }
            
        protected void Button1_Click(object sender, EventArgs e)
        {
            checkAddress();
            createPackage();
        }
        private void checkAddress()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostOffice"].ToString();
            conn = new SqlConnection(connectionString);

            conn.Open();

            sqlQuery = "DECLARE @adID int SET @adID = (SELECT ADDRESS.Address_ID FROM ADDRESS WHERE ADDRESS.Street = @Street) IF @adID IS NULL BEGIN " +
                "INSERT INTO ADDRESS(Facility_ID, Street, City,State,Zip,aptn,Postal_code) VALUES('1',@Street,@City,@State,@Zip,@aptn,@Postal_code) SET @adID = SCOPE_IDENTITY() END SELECT @adID";

                command = new SqlCommand(sqlQuery, conn);
            command.Parameters.AddWithValue("@Street", S_Address_TextBox.Text);
            command.Parameters.AddWithValue("@City", S_City_TextBox.Text);
            command.Parameters.AddWithValue("@State", S_State_TextBox.Text);
            if(S_Zip_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@Zip", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Zip", Convert.ToDecimal(S_Zip_TextBox.Text));
            }
            if(S_Apt_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@aptn", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@aptn", Convert.ToInt32(S_Apt_TextBox.Text));
            }
            if(S_PostalCode_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@Postal_code", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Postal_code", Convert.ToDecimal(S_PostalCode_TextBox.Text));
            }
            
            dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    senderAddressID = dataReader[0].ToString();
                }
            }
            

            dataReader.Close();

            command.Dispose();

            sqlQuery = "DECLARE @adID int SET @adID = (SELECT ADDRESS.Address_ID FROM ADDRESS WHERE ADDRESS.Street = @Street) IF @adID IS NULL BEGIN " +
               "INSERT INTO ADDRESS(Facility_ID, Street, City,State,Zip,aptn,Postal_code) VALUES('1',@Street,@City,@State,@Zip,@aptn,@Postal_code) SET @adID = SCOPE_IDENTITY() END SELECT @adID";
            command = new SqlCommand(sqlQuery, conn);
            command.Parameters.AddWithValue("@Street", R_Address_TextBox.Text);
            command.Parameters.AddWithValue("@City", R_City_TextBox.Text);
            command.Parameters.AddWithValue("@State", R_State_TextBox.Text);
            if (R_Zip_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@Zip", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Zip", Convert.ToDecimal(R_Zip_TextBox.Text));
            }
            if (R_Apt_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@aptn", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@aptn", Convert.ToInt32(R_Apt_TextBox.Text));
            }
            if (R_PostalCode_TextBox.Text == string.Empty)
            {
                command.Parameters.AddWithValue("@Postal_code", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Postal_code", Convert.ToDecimal(R_PostalCode_TextBox.Text));
            }

            dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    receiverAddressID = dataReader[0].ToString();
                }
            }
            
            command.Dispose();
            conn.Close();

        }
        private void createPackage()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostOffice"].ToString();
            conn = new SqlConnection(connectionString);

            conn.Open();

            sqlQuery = "INSERT INTO dbo.PACKAGE(Sender_ID,Cost,Types,Priority,Sender_Address_ID,Receiver_Address_ID)" +
                "VALUES((SELECT dbo.CUSTOMER.Customer_ID FROM dbo.CUSTOMER WHERE dbo.CUSTOMER.Email= @email), '1.10',@type,@priority,@Sender_Address_ID,@Receiver_Address_ID)";


            command = new SqlCommand(sqlQuery, conn);
            command.Parameters.AddWithValue("@email", username);
            command.Parameters.AddWithValue("@type", DropDownList1.SelectedItem.Value.ToString());
            command.Parameters.AddWithValue("@priority", DropDownList2.SelectedItem.Value.ToString());
            command.Parameters.AddWithValue("@Receiver_Address_ID",receiverAddressID);
            command.Parameters.AddWithValue("@Sender_Address_ID",senderAddressID);
            int count = command.ExecuteNonQuery();
            if (count == 1)
            {
                Label1.Text = "Package sucessfully created";
            }
            else
            {
                Label1.Text = "Nope";
            }
            



            
            command.Dispose();
            conn.Close();


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}