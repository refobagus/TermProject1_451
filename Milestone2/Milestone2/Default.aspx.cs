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
            return "Host = localhost; username = GuestUser; Database = Milestone2; password=abcd123";
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

                lbStates.DataSource = StateList;
                lbStates.DataBind();

                if(StateList.Count == 0)
                {
                    lblError.Text = "No States Found to select from";
                }

                connection.Close();
            }


        }

        protected void GetCities(string State)
        {
            lblError.Text = string.Empty;
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

            lbCities.DataSource = City;
            lbCities.DataBind();

            if(City.Count > 0)
            {
                divCity.Visible = true;
            }
            else
            {
                divCity.Visible = false;
            }

            connection.Close();

        }

        protected void getPostalCodes(string City)
        {
            lblError.Text = string.Empty;
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<string> postalCodes = new List<string>();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT DISTINCT postal_code FROM business WHERE city = " + "'" + City + "'" + "ORDER BY postal_code";
            try
            {
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    postalCodes.Add(reader.GetString(0));
                }
            }
            catch(NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            lbPostalCode.DataSource = postalCodes;
            lbPostalCode.DataBind();
            if(postalCodes.Count > 0)
            {
                divPostalCodes.Visible = true;
            }
            connection.Close();

        }
        protected void getBusinesses(string postalCode)
        {
            lblError.Text = string.Empty;
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<Business> businesses = new List<Business>();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            if(!String.IsNullOrEmpty(postalCode))
            {
                cmd.CommandText = "SELECT * FROM business WHERE postal_code = " + "'" + postalCode + "'" + " AND city = " + "'" + lbCities.SelectedValue + "'" + "ORDER BY name";
            }
            else
            {
                if(!String.IsNullOrEmpty(lbCities.SelectedValue))
                {
                    cmd.CommandText = "SELECT * FROM business WHERE city = " + "'" + lbCities.SelectedValue + "'" + "ORDER BY name";
                }
                else if(!String.IsNullOrEmpty(lbStates.SelectedValue))
                {
                    cmd.CommandText = "SELECT * FROM business WHERE state = " + "'" + lbStates.SelectedValue + "'" + "ORDER BY name";
                }
                else
                {
                    gvBusinesses.DataSource = new List<Business>();
                    return;
                }
            }
            
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
        /*
        protected void getCategories(string postalCode)
        {
            lblError.Text = string.Empty;
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<string> categories = new List<string>();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT DISTINCT "
        }

    */
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


        protected void lbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCities(lbStates.SelectedValue);
            getBusinesses("");
        }

        protected void lbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPostalCodes(lbCities.SelectedValue);
            getBusinesses("");
        }

        protected void lbPostalCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBusinesses(lbPostalCode.SelectedValue);
        }

        protected void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBusinesses(lbCategories.SelectedValue);
        }
    }
}