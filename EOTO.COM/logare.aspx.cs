using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

public partial class logare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mesaj_cont_gresit.Visible = mesaj_continut_gol.Visible = false;

        if (isUserLogged())
        {
            int user_id_group = 0;
            Load_NavBar_User(ref user_id_group);
        }
        else
        {
            Load_NavBar_Guest();
        }
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

    protected void CreateCookies(string username, string password)
    {
        HttpCookie usernameCookie = new HttpCookie("username");
        usernameCookie.Value = Encrypt.EncryptString(username, "EncryptPassword");
        usernameCookie.Expires = DateTime.Now.AddYears(50);
        Response.Cookies.Add(usernameCookie);

        HttpCookie passwordCookie = new HttpCookie("password");
        passwordCookie.Value = Encrypt.EncryptString(password, "EncryptPassword");
        passwordCookie.Expires = DateTime.Now.AddYears(50);
        Response.Cookies.Add(passwordCookie);
    }

    public byte[] ImageTobyteArray()
    {
        FileStream imgStream = File.OpenRead(@"C:\Users\Alex-Deidei\Desktop\car.png");
        byte[] blob = new byte[imgStream.Length];
        imgStream.Read(blob, 0, (int)imgStream.Length);

        imgStream.Dispose();
        return blob;
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string Utilizator = utilizator.Value;
        string Parola = parola.Value;

        if (!String.IsNullOrEmpty(Utilizator) && !String.IsNullOrEmpty(Parola))
        {
            if (Check_User(Utilizator, Parola))
            {
                CreateCookies(Utilizator, Parola);
                Response.Redirect("cursuri.aspx", false);
            }
            else
            {
                mesaj_cont_gresit.Visible = true;
            }
        }
        else
        {
            mesaj_continut_gol.Visible = true;
        }
    }

    public bool Check_User(string utilizator, string parola)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE (username=@utilizator) AND (password=@parola)", con))
            {
                cmd.Parameters.AddWithValue("@utilizator", utilizator);
                cmd.Parameters.AddWithValue("@parola", parola);

                int UtilizatorExistent = (int)cmd.ExecuteScalar();

                if (UtilizatorExistent > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}