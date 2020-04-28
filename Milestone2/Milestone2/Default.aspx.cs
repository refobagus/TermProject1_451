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
            if (Session["userID"] != null)
            {
                divApplicationLogin.Visible = false;
                ApplicationHomePage.Visible = true;

            }
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

                if (StateList.Count == 0)
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

            if (City.Count > 0)
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
                while (reader.Read())
                {
                    postalCodes.Add(reader.GetString(0));
                }
            }
            catch (NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            lbPostalCode.DataSource = postalCodes;
            lbPostalCode.DataBind();
            if (postalCodes.Count > 0)
            {
                divPostalCodes.Visible = true;
            }
            connection.Close();

        }
        protected void getBusinesses()
        {
            lblError.Text = string.Empty;
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<Business> businesses = new List<Business>();
            List<string> Categories = new List<string>();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            foreach(ListItem category in cbCategories.Items)
            {
                if (category.Selected == true)
                {
                    Categories.Add(category.Value);
                }
            }
            if (Categories.Count > 0)
            {
                string tail = "') AND postal_code = '" + lbPostalCode.SelectedValue + "' ORDER BY business.name";
                string head = "SELECT DISTINCT business.busID, business.name, business.state, business.city " +
                    "FROM business INNER JOIN Categories ON business.busID = Categories.busID" +
                    " WHERE (category = '";
                string selectCategories = string.Empty;
                foreach(string cat in Categories){
                    selectCategories += (cat + "' OR category = '");
                }

                cmd.CommandText = head + selectCategories + tail;
                lbCategories.DataSource = Categories;
                lbCategories.DataBind();
            }
            else if (!String.IsNullOrEmpty(lbPostalCode.SelectedValue))
            {
                cmd.CommandText = "SELECT * FROM business WHERE postal_code = '" + lbPostalCode.SelectedValue + "' ORDER BY name";
            }
            else if (!String.IsNullOrEmpty(lbCities.SelectedValue))
            {
                cmd.CommandText = "SELECT * FROM business WHERE city = " + "'" + lbCities.SelectedValue + "'" + "ORDER BY name";
            }
            else if (!String.IsNullOrEmpty(lbStates.SelectedValue))
            {
                cmd.CommandText = "SELECT * FROM business WHERE state = " + "'" + lbStates.SelectedValue + "'" + "ORDER BY name";
            }
            else
            {
                gvBusinesses.DataSource = new List<Business>();
                return;
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

        protected void getCategories(string postalCode)
        {
            lblError.Text = string.Empty;
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            List<string> categories = new List<string>();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            if (!string.IsNullOrEmpty(lbCities.SelectedValue) && !String.IsNullOrEmpty(lbStates.SelectedValue) && !String.IsNullOrEmpty(lbPostalCode.SelectedValue))
            {
                cmd.CommandText = "SELECT DISTINCT category FROM Categories INNER JOIN Business on Business.busID = Categories.busID WHERE business.State = '"
                + lbStates.SelectedValue + "' AND Business.city = '" + lbCities.SelectedValue + "' AND business.postal_code = '" + lbPostalCode.SelectedValue + "' ORDER BY category";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "ERROR while getting categories: " + ex.Message;
                    connection.Close();
                    return;
                }
                lbCategories.DataSource = categories;
                lbCategories.DataBind();
                cbCategories.DataSource = categories;
                cbCategories.DataBind();
                divCategories.Visible = true;
                connection.Close();
                return;

            }
            else
            {
                lblError.Text = "Please select a City State and Postal Code before Selecting a cagory";
                connection.Close();
                return;
            }


        }



        protected void bindUsers(string userName)
        {
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            string tempUid = string.Empty, tempUName = string.Empty;

            List<ListItem> users = new List<ListItem>();
            var emptyUser = new ListItem(string.Empty, string.Empty);
            emptyUser.Selected = true;
            users.Add(emptyUser);
            cmd.CommandText = "SELECT DISTINCT userID, name FROM Users WHERE name = '" + userName + "' ORDER BY name";

            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tempUid = reader.GetString(0);
                    tempUName = reader.GetString(1);
                    var li = new ListItem(tempUid);
                    users.Add(li);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error while binding users: " + ex.Message;
                return;
            }

            ddlUser.DataSource = users;
            ddlUser.DataBind();
            connection.Close();
            return;
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


        protected void lbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCities(lbStates.SelectedValue);
            getBusinesses();

            List<string> emptyList = new List<string>();
            lbPostalCode.DataSource = emptyList;
            lbPostalCode.DataBind();
            lbCategories.DataSource = emptyList;
            lbCategories.DataBind();
            
        }

        protected void lbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPostalCodes(lbCities.SelectedValue);
            getBusinesses();
            List<string> emptyList = new List<string>();
            lbCategories.DataSource = emptyList;
            lbCategories.DataBind();
        }

        protected void lbPostalCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCategories(lbPostalCode.SelectedValue);
            getBusinesses();
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = ddlUser.SelectedItem.Text;
            string userID = ddlUser.SelectedValue;

            userDDLSelect.Visible = false;
            divApplicationLogin.Visible = false;

            Session["userID"] = userID;
            ApplicationHomePage.Visible = true;
            
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            bindUsers(tbuser.Text.Trim());
            btnSelect.Visible = false;
            usernameSelect.Visible = false;
            userDDLSelect.Visible = true;
        }

        protected void btnAddCategoryFilter_Click(object sender, EventArgs e)
        {
            getBusinesses();
        }

        protected void btnRemoveCategoryFilter_Click(object sender, EventArgs e)
        {
            foreach(ListItem cat in cbCategories.Items)
            {
                if(cat.Selected == true)
                {
                    cat.Selected = false;
                }
            }
            lbCategories.DataSource = new List<string>();
            lbCategories.DataBind();
            getBusinesses();
        }
    }
}