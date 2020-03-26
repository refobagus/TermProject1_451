using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone2
{
    public partial class BusinessInfo : System.Web.UI.Page
    {
    
            protected string buildConnectionString()
            {
                return "Host = localhost; username = GuestUser; Database = dbmilestonev1; password=abcd123";
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                if (Request.QueryString["BusinessID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                string businessID = Request.QueryString["BusinessID"].ToString();
                getBusinessData(businessID);

            }

            protected void donebtn_Click(object sender, EventArgs e)
            {
                Response.Redirect("Default.aspx");
            }

            protected void getBusinessData(string businessID)
            {
                var connection = new NpgsqlConnection(buildConnectionString());
                connection.Open();
                var cmd = new NpgsqlCommand();
                string BusinessName = string.Empty;
                string City = string.Empty;
                string State = string.Empty;
                string StateCount = string.Empty;
                string CityCount = string.Empty;
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM business WHERE busid = " + "'" + businessID + "'";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BusinessName = reader.GetString(1);
                        State = reader.GetString(2);
                        City = reader.GetString(3);
                    }

                }
                catch (NpgsqlException ex)
                {
                    lblError.Text = ex.Message;
                }


                StateCount = GetStateCount(State);
                CityCount = GetCityCount(City);
                List<BusinessStats> businessStats = new List<BusinessStats>();
                businessStats.Add(new BusinessStats(businessID, BusinessName, State, City, CityCount, StateCount));
                businessForm.DataSource = businessStats;
                businessForm.DataBind();

                BusinessSelectedAlert.Visible = true;

                connection.Close();
            }

            protected string GetStateCount(string state)
            {
                int stateCount = 0;
                var connection = new NpgsqlConnection(buildConnectionString());
                connection.Open();
                var cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT state FROM business WHERE state = " + "'" + state + "'";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        stateCount += 1;
                    }

                }
                catch (NpgsqlException ex)
                {
                    lblError.Text = ex.Message;
                }

                connection.Close();
                return stateCount.ToString();
            }

            protected string GetCityCount(string City)
            {
                int cityCount = 0;
                var connection = new NpgsqlConnection(buildConnectionString());
                connection.Open();
                var cmd = new NpgsqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "SELECT city FROM business WHERE city = " + "'" + City + "'";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cityCount += 1;
                    }
                }
                catch (NpgsqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                connection.Close();
                return cityCount.ToString();
            }

            public class BusinessStats
            {
                public BusinessStats()
                {
                    this.BusinessID = string.Empty;
                    this.name = string.Empty;
                    this.State = string.Empty;
                    this.City = string.Empty;
                    this.SameCity = string.Empty;
                    this.SameState = string.Empty;
                }
                public BusinessStats(string bID, string name, string state, string city, string cityCount, string stateCount)
                {
                    this.City = city;
                    this.BusinessID = bID;
                    this.name = name;
                    this.State = state;
                    this.SameCity = cityCount;
                    this.SameState = stateCount;
                }

                public string BusinessID { get; set; }
                public string name { get; set; }
                public string State { get; set; }
                public string City { get; set; }
                public string SameCity { get; set; }
                public string SameState { get; set; }
            }


        }
    }