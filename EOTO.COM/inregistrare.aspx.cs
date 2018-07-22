using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Drawing;

public partial class inregistrare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mesaj_cont_creat.Visible = mesaj_cont_existent.Visible = mesaj_continut_gol.Visible = false;

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

    protected void buton_inregistrare_click(object sender, EventArgs e)
    {
        string nume = text_nume.Value;
        string prenume = text_prenume.Value;
        string utilizator = text_utilizator.Value;
        string parola = text_parola.Value;
        string email = text_email.Value;
        bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        if (!String.IsNullOrEmpty(nume) && !String.IsNullOrEmpty(prenume) && !String.IsNullOrEmpty(utilizator) && !String.IsNullOrEmpty(parola) && !String.IsNullOrEmpty(email) && terms.Checked)
        {
            if (isEmail)
            {
                if (!Verificare_Utilizator(utilizator, email))
                {
                    Add_User();
                    mesaj_cont_creat.Visible = true;
                }
                else
                {
                    mesaj_cont_existent.Visible = true;
                }
            }
            else
            {
                mesaj_continut_gol.Visible = true;
            }
        }
        else
        {
            mesaj_continut_gol.Visible = true;
        }
    }

    public void Add_User()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Users(id_group, username, password, avatar, first_name, second_name, email, courses) VALUES(@id_group, @username, @password, @avatar, @first_name, @second_name, @email, @courses)", con))
            {
                cmd.Parameters.AddWithValue("@id_group", 2);
                cmd.Parameters.AddWithValue("@username", text_utilizator.Value);
                cmd.Parameters.AddWithValue("@password", text_parola.Value);
                cmd.Parameters.AddWithValue("@avatar", ImageTobyteArray());
                cmd.Parameters.AddWithValue("@first_name", text_nume.Value);
                cmd.Parameters.AddWithValue("@second_name", text_prenume.Value);
                cmd.Parameters.AddWithValue("@email", text_email.Value);
                cmd.Parameters.AddWithValue("@courses", 0);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public bool Verificare_Utilizator(string utilizator, string email)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE (username=@utilizator) OR (email=@email)", con))
            {
                cmd.Parameters.AddWithValue("@utilizator", utilizator);
                cmd.Parameters.AddWithValue("@email", email);

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

    public byte[] ImageTobyteArray()
    {
        /*string url = Server.MapPath("~/images/no_avatar.png");

        FileStream imgStream = File.OpenRead(url);
        byte[] blob = new byte[imgStream.Length];
        imgStream.Read(blob, 0, (int)imgStream.Length);

        imgStream.Dispose();
        return blob;*/

        string url = Server.MapPath("~/images/no_avatar.png");

        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(url);
        Image image = (Image)bmp;

        using (var stream = new MemoryStream())
        {
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}