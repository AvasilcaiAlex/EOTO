using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

public partial class profil : System.Web.UI.Page
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

        if (IsPostBack && FileUpload1.PostedFile != null)
        {
            if (FileUpload1.PostedFile.FileName.Length > 0)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                Image image = (Image)bmp;

                using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET avatar=@avatar WHERE id_user=@id_user", con))
                    {
                        cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                        cmd.Parameters.AddWithValue("@avatar", ImageTobyteArray(image));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        FileUpload1.Visible = false;
        Incarca_Detalii();
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
                    DesktopAvatar.Src = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                    MobileAvatar.Src = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
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

    public byte[] ImageTobyteArray(Image img)
    {
        using (var stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }

    protected int Get_UserId()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_user FROM Users WHERE username=@username", con))
            {
                string username_decrypted = "";
                HttpCookie usernameCookie = Request.Cookies["username"];
                if(usernameCookie!=null)
                {
                    username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");
                }
                cmd.Parameters.AddWithValue("@username", username_decrypted);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["id_user"]);
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
        return 0;
    }

    protected void Incarca_Detalii()
    {
        int curent_id_utilizator = Convert.ToInt32(Request.QueryString["id"]);
        if (Utilizator_Existent(curent_id_utilizator))
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE id_user=@id_user", con))
                {
                    cmd.Parameters.AddWithValue("@id_user", curent_id_utilizator);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Title = reader["first_name"] + " " + reader["second_name"] + " | Profil";

                        curent_avatar.Src = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                        curent_utilizator.Text = reader["username"].ToString();
                        curent_nume_complet.Text = reader["first_name"] + " " + reader["second_name"];
                        curent_email.Text = reader["email"].ToString();
                        curent_id_grup.Text = Get_GroupName(Convert.ToInt32(reader["id_group"]));
                        curent_cursuri.Text = reader["courses"].ToString();

                        if (curent_id_utilizator == Get_UserId())
                        {
                            FileUpload1.Visible = true;
                        }
                    }
                    reader.Close();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        else
        {
            Response.Redirect("eroare.aspx", false);
        }
    }

    public bool Utilizator_Existent(int id_utilizator)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE (id_user=@id_user)", con))
            {
                cmd.Parameters.AddWithValue("@id_user", id_utilizator);

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

    protected string Get_GroupName(int id_group)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM Groups WHERE id_group=@id_group", con))
            {
                cmd.Parameters.AddWithValue("@id_group", id_group);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return reader["name"].ToString();
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
        return null;
    }
}