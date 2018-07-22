using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class acasa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isUserLogged())
        {
            int user_id_group = 0;
            Load_NavBar_User(ref user_id_group);
        }
        else
        {
            Load_NavBar_Guest();
        }

        Project_Details();
    }

    protected bool isUserLogged()
    {
        if (isUserAuthenticated())
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username=@username AND password=@password", con))
                {
                    HttpCookie usernameCookie = Request.Cookies["username"];
                    HttpCookie passwordCookie = Request.Cookies["password"];
                    string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");
                    string password_decrypted = Encrypt.DecryptString(passwordCookie.Value, "EncryptPassword");

                    cmd.Parameters.AddWithValue("@username", username_decrypted);
                    cmd.Parameters.AddWithValue("@password", password_decrypted);

                    int userExist = Convert.ToInt32(cmd.ExecuteScalar());
                    if (userExist > 0)
                        return true;
                }
            }
        }

        return false;
    }

    protected bool isUserAuthenticated()
    {
        HttpCookie usernameCookie = Request.Cookies["username"];
        HttpCookie passwordCookie = Request.Cookies["password"];

        if (usernameCookie != null && passwordCookie != null)
        {
            return true;
        }

        return false;
    }

    protected void Load_NavBar_Guest()
    {
        DesktopFormProfile.Visible = false;
        MobileFormProfile.Visible = false;
    }

    protected void Load_NavBar_User(ref int user_id_group)
    {
        DesktopFormSignin.Visible = false;
        MobileFormSignin.Visible = false;

        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_user, id_group, username, avatar FROM Users WHERE username=@username AND password=@password", con))
            {
                HttpCookie usernameCookie = Request.Cookies["username"];
                HttpCookie passwordCookie = Request.Cookies["password"];
                string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");
                string password_decrypted = Encrypt.DecryptString(passwordCookie.Value, "EncryptPassword");

                cmd.Parameters.AddWithValue("@username", username_decrypted);
                cmd.Parameters.AddWithValue("@password", password_decrypted);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DesktopAvatar.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                    MobileAvatar.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                    ProfileLink.HRef = MobileFormProfile.HRef = "profil.aspx?id=" + reader["id_user"];
                    DesktopUsername.Text = MobileUsername.Text = reader["username"].ToString();
                    user_id_group = Convert.ToInt32(reader["id_group"]);
                }
            }
        }
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        HttpCookie usernameCookie = new HttpCookie("username");
        HttpCookie passwordCookie = new HttpCookie("password");

        usernameCookie.Expires = DateTime.Now.AddYears(-1);
        passwordCookie.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(usernameCookie);
        Response.Cookies.Add(passwordCookie);

        Response.Redirect("logare.aspx", false);
    }

    public void Project_Details()
    {
        int users = 0;
        int courses = 0;
        int categories = 0;
        int comments = 0;

        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users = users + 1;
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courses = courses + 1;
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories = categories + 1;
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Comments", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comments = comments + 1;
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }

        usersNumber.Text = users.ToString();
        coursesNumber.Text = courses.ToString();
        categoriesNumber.Text = categories.ToString();
        commentsNumber.Text = comments.ToString();
    }
}