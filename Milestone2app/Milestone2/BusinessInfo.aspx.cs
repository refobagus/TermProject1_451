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
            return "Host = localhost; username = postgres; Database = yelpdb; password=wartech25";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userID"] != null)
            {
                divLoggedIn.Visible = true;
            }
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
            cmd.CommandText = "SELECT * FROM business WHERE bus_id = " + "'" + businessID + "'";
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
            int numCheckins = getCheckinCount(businessID);
            List<BusinessStats> businessStats = new List<BusinessStats>();
            int numTips = getTips();
            string categories = string.Empty;
            categories = getCategories(businessID);
            businessStats.Add(new BusinessStats(businessID, BusinessName, State, City, CityCount, StateCount, numTips, numCheckins, categories));
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

        protected int getTips()
        {

            var connection = new NpgsqlConnection(buildConnectionString());
            List<BusinessTips> tips = new List<BusinessTips>();
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT DISTINCT Users.userID, Users.name, tip.likes, tip.tip, tip.madeon " +
                "FROM Tip INNER JOIN Users ON Tip.userID = Users.userID " +
                "WHERE tip.busID = " + "'" + Request.QueryString["BusinessID"].ToString() + "' ORDER BY tip.madeon";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tips.Add(new BusinessTips(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4)));
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return 0;
            }
            gvTips.DataSource = tips;
            gvTips.DataBind();
            return tips.Count;
        }

        protected int getCheckinCount(string busID)
        {
            var connection = new NpgsqlConnection(buildConnectionString());
            int checkins = 0;
            connection.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT date from Checkin WHERE busID = '" + busID + "'";
            try
            {
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    checkins += 1;

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ERROR WHILE counting checkins: " + ex.Message + "<br/> cmd: " + cmd.CommandText;
                return 0;
            }
            return checkins;
        }

        protected string getCategories(string busID)
        {
            var connection = new NpgsqlConnection(buildConnectionString());
            string categories = string.Empty;

            connection.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT category FROM Categories WHERE busID = '" + busID + "'";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(categories))
                    {
                        categories = reader.GetString(0);

                    }
                    else
                    {
                        categories += ", " + reader.GetString(0);
                    }
                }
            }
            catch(Exception ex)
            {
                lblError.Text = "Error while getting Categories: " + ex.Message + "<br/> + CMD: " + cmd.CommandText ;
                return string.Empty;
            }

            return categories;

        }

        protected void btnSubmitNewTip_Click(object sender, EventArgs e)
        {
            string userID = Session["userID"].ToString();
            string busID = Request.QueryString["BusinessID"].ToString();

            var connection = new NpgsqlConnection(buildConnectionString());

            connection.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO tip (busid, userid, likes, tip, madeon) VALUES ('" + busID + "','" + userID + "','" + 0 + "','" + tbTip.Text.Trim() + "','" + DateTime.Now + "');";
            try
            {
                var executer = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message + "<br/>" + "UserID: " + userID + "<br/>" + "busID = " + busID + "<br/>" + "Query: " + cmd.CommandText; 
                return;
            }

            
            getTips();

            divNewTip.Visible = false;
            btnNewTip.Visible = true;
            
        }
        protected void BtnCheckin_Click(object sender, EventArgs e)
        {
            string busID = Request.QueryString["BusinessID"].ToString();

            var connection = new NpgsqlConnection(buildConnectionString());

            connection.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO checkin (busid, date) VALUES ('"+ busID + "','" + DateTime.Now + "');";
            try
            {
                var executer = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
            cmd.CommandText = "UPDATE business SET numcheckins = numcheckins + 1 WHERE bus_id = '" + busID + "'";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            getCheckinCount(busID);
            btnCheckin.Visible = false;

        }
        protected void CancelNewTip_Click(object sender, EventArgs e)
        {
            divNewTip.Visible = false;
            btnNewTip.Visible = true;
        }

        protected void LikeBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string command = btn.CommandName;
            if (command == "Tip")
            {
                string commandArg = string.Empty;
                string timestamp = string.Empty;
                string userID = string.Empty;
                commandArg = btn.CommandArgument.ToString();

                var temp = commandArg.Split(',');
                userID = temp[0];
                timestamp = temp[1];

                string thisUserID = Session["userID"].ToString();
                string busID = Request.QueryString["BusinessID"].ToString();

                var connection = new NpgsqlConnection(buildConnectionString());

                connection.Open();

                var cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "UPDATE Tip SET likes = likes +1 WHERE userID = '" + userID + "' AND busID = '" + busID + "' AND madeOn = '" + timestamp + "'";
                try
                {
                    var update = cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    lblError.Text = ex.Message;
                }
                cmd.CommandText = "UPDATE Users SET numLikes = numLikes + 1 WHERE userID = '" + thisUserID + "'";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    lblError.Text = ex.Message;
                }
                getTips();


            }
        }

        protected class BusinessTips
        {
            public BusinessTips()
            {
                this.userID = string.Empty;
                this.userName = string.Empty;
                this.likes = 0;
                this.tip = string.Empty;
                this.timeMade = DateTime.Today;

            }
            public BusinessTips(string userID, string userName, int likes, string tip, DateTime timeMade)
            {
                this.userID = userID;
                this.userName = userName;
                this.likes = likes;
                this.tip = tip;
                this.timeMade = timeMade;

            }

            public string userID { get; set; }
            public string userName { get; set; }
            public int likes { get; set; }
            public string tip { get; set; }
            public DateTime timeMade { get; set; }

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
                this.numTips = 0;
                this.numCheckins = 0;
                this.categories = string.Empty;
            }
            public BusinessStats(string bID, string name, string state, string city, string cityCount, string stateCount, int numTips, int numCheckins, string categories)
            {

                this.City = city;
                this.BusinessID = bID;
                this.name = name;
                this.State = state;
                this.SameCity = cityCount;
                this.SameState = stateCount;

                this.numCheckins = numCheckins;
                this.numTips = numTips;
                this.categories = categories;
            }

            public string BusinessID { get; set; }
            public string name { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string SameCity { get; set; }
            public string SameState { get; set; }

            public int numCheckins { get; set; }

            public int numTips { get; set; }
            public string categories { get; set; }
        }


    }
}