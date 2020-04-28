using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone2
{
    public partial class UserInfo : System.Web.UI.Page
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
                loggedIn.Visible = true;
                bindThisUser();
                bindFriends();
                bindFriendTips();

            }

        }

        protected void bindThisUser()
        {
            string userID = Session["userID"].ToString();
            // Query for this user's info and bind it to a new User class
            List<userInfo> thisUser = new List<userInfo>();
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT DISTINCT * FROM users WHERE userid = '" + userID + "'";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    thisUser.Add(new userInfo(reader.GetString(5), reader.GetString(0),
                        reader.GetString(9), reader.GetInt32(7), reader.GetInt32(4), reader.GetInt32(2),
                        reader.GetInt32(6), reader.GetInt32(3), reader.GetString(11), reader.GetString(10), reader.GetDouble(1)));
                }
            }
            catch(NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            fvUserForm.DataSource = thisUser;
            fvUserForm.DataBind();
            connection.Close();

        }

        protected void bindFriends()
        {
            string thisUserID = Session["userID"].ToString();

            List<friend> friends = new List<friend>();
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT users.name, users.numlikes, users.avgstar, users.yelping_since" +
                " FROM friends INNER JOIN users ON friends.friendID = users.userID" +
                " WHERE friends.userID = '" + thisUserID + "' ORDER BY users.name";
            try
            {
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    friends.Add(new friend(reader.GetString(0), reader.GetInt32(1),
                         reader.GetDouble(2), Convert.ToDateTime(reader.GetString(3))));
                }
            }
            catch(NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            gvFriends.DataSource = friends;
            gvFriends.DataBind();

            connection.Close();
        }

        protected void bindFriendTips()
        {
            string thisUserID = Session["userID"].ToString();
            List<friendTip> tips = new List<friendTip>();
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT users.name, business.name, business.city, tip.tip, tip.madeOn" +
                               " FROM friends INNER JOIN users ON users.userID = friends.friendID" +
                               " INNER JOIN tip ON tip.userID = friends.friendID" +
                               " INNER JOIN business ON tip.busID = business.busID" +
                               " WHERE friends.userID = '" + thisUserID + "' ORDER BY madeOn";
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tips.Add(new friendTip(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetDateTime(4)));
                }
            }
            catch(NpgsqlException ex)
            {
                lblError.Text = ex.Message;
            }
            gvTipsFriends.DataSource = tips;
            gvTipsFriends.DataBind();
            connection.Close();
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

        protected void editLong()
        {
            TextBox tbLong = (TextBox)fvUserForm.FindControl("tbLong");
            var connection = new NpgsqlConnection(buildConnectionString());
            connection.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "UPDATE users SET longitude = '" + tbLong.Text + "' WHERE users.userID = '" + Session["userID"].ToString() + "'";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }

            bindThisUser();
        }

        public class friendTip
        {
            friendTip()
            {
                this.name = string.Empty;
                this.business = string.Empty;
                this.city = string.Empty;
                this.text = string.Empty;
                this.date = DateTime.Today;
            }
            public friendTip(string name, string business, string city, string text, DateTime date)
            {
                this.name = name;
                this.business = business;
                this.city = city;
                this.text = text;
                this.date = date;
            }
            public string name { get; set; }
            public string business { get; set; }
            public string city { get; set; }
            public string text { get; set; }
            public DateTime date { get; set; }
        }

        public class friend
        {
            public friend()
            {
                this.name = string.Empty;
                this.totalLikes = 0;
                this.avgStars = 0.0;
                this.yelping_since = DateTime.Today;
            }
            public friend(string name, int likes, double stars, DateTime date)
            {
                this.name = name;
                this.totalLikes = likes;
                this.avgStars = stars;
                this.yelping_since = date;
            }

            public string name { get; set; }
            public int totalLikes { get; set; }
            public double avgStars { get; set; }
            public DateTime yelping_since { get; set; }
        }

        public class userInfo
        {
            public userInfo()
            {
                this.name = string.Empty;
                this.userID = string.Empty;
                this.latitude = string.Empty;
                this.longitude = string.Empty;
                this.tipCount = 0;
                this.numLikes = 0;
                this.funny = 0;
                this.cool = 0;
                this.fans = 0;
                this.stars = 0;
               
            }
            
            public userInfo(string n, string uID, string since, int likes, int fun, int cool,
                int tipCount, int fans, string longit, string lat, double stars)
            {
                this.name = n;
                this.userID = uID;
                this.yelping_since = Convert.ToDateTime(since);
                this.numLikes = likes;
                this.funny = fun;
                this.cool = cool;
                this.tipCount = tipCount;
                this.fans = fans;
                this.longitude = longit;
                this.latitude = lat;
                this.stars = stars;
            }

            public string name { get; set; }
            public string userID { get; set; }
            public DateTime yelping_since { get; set; }
            public int numLikes { get; set; }
            public int funny { get; set; }
            public int cool { get; set; }

            public int tipCount { get; set; }
            public int fans { get; set; }

            public string latitude { get; set; }
            
            public string longitude { get; set; }

            public double stars { get; set; }


        }


        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = ddlUser.SelectedItem.Text;
            string userID = ddlUser.SelectedValue;

            userDDLSelect.Visible = false;
            divApplicationLogin.Visible = false;

            Session["userID"] = userID;
            loggedIn.Visible = true;

            bindFriendTips();
            bindThisUser();
            bindFriends();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            bindUsers(tbuser.Text.Trim());
            btnSelect.Visible = false;
            usernameSelect.Visible = false;
            userDDLSelect.Visible = true;
        }

        protected void btnSubmitLat_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmitLong_Click(object sender, EventArgs e)
        {
            editLong();
        }

        protected void btnUpdateLong_Click(object sender, EventArgs e)
        {
            fvUserForm.FindControl("divLongitude").Visible = false;
            fvUserForm.FindControl("divLongUpdate").Visible = true;
        }
    }
}