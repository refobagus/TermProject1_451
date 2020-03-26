using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone2
{
    public partial class _Default : Page
    {

        protected string buildConnectionString()
        {
            return "Host = localhost; username = GuestUser; Database = dbmilestonev1; password=abcd123";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblError.Text = string.Empty;
                var connection = new NpgsqlConnection(buildConnectionString());
                connection.Open();
                var cmd = new NpgsqlCommand();
                List<string> StateList = new List<string>();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT DISTINCT state FROM business ORDER BY state";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                        StateList.Add(reader.GetString(0));

                }
                catch (NpgsqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                ddlState.DataSource = StateList;
                ddlState.DataBind();

                connection.Close();
            }


        }

        protected void GetCities(string State)
        {
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<string> City = new List<string>();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT DISTINCT city FROM business WHERE state = " + "'" + State.ToUpper() + "'" + "  ORDER BY city";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    City.Add(reader.GetString(0));

            }
            catch (NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            ddlCity.DataSource = City;
            ddlCity.DataBind();
            connection.Close();

        }
        protected void getBusinesses(string City)
        {
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<Business> businesses = new List<Business>();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM business WHERE city = " + "'" + City + "'" + "ORDER BY businessname";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    businesses.Add(new Business(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));

            }
            catch (NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }

            gvBusinesses.DataSource = businesses;
            gvBusinesses.DataBind();
            connection.Close();
        }


        public class Business
        {
            public Business()
            {
                this.BusinessID = string.Empty;
                this.name = string.Empty;
                this.State = string.Empty;
                this.City = string.Empty;
            }
            public Business(string bID, string name, string state, string city)
            {
                this.City = city;
                this.BusinessID = bID;
                this.name = name;
                this.State = state;
            }

            public string BusinessID { get; set; }
            public string name { get; set; }
            public string State { get; set; }
            public string City { get; set; }

        }


        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCities(ddlState.SelectedItem.ToString());

        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBusinesses(ddlCity.SelectedItem.ToString());
        }
    }
}