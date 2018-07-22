using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;

public partial class curs : System.Web.UI.Page
{
    public int firstComment = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        NoCommentsToDisplay.Visible = msg_DownloadedCourse.Visible = msg_CommentEmpty.Visible = msg_CommentCreated.Visible = false;

        if (isQueryFormatCorrect())
        {
            if (isUserLogged())
            {
                int user_id_group = 0;

                Load_NavBar_User(ref user_id_group);

                if (isAllowedToModerateCourse(user_id_group))
                {
                    FormModerateCourse.Visible = true;
                }

                Poll_VOTE.Visible = false;
            }
            else
            {
                Load_NavBar_Guest();
                AddComment.Disabled = true;
                Poll_YES.Visible = false;
                Poll_NO.Visible = false;
            }

            Load_Course_Details(Convert.ToInt32(Request.QueryString["id"]));
            Load_Poll_Results(Convert.ToInt32(Request.QueryString["id"]));
            Load_Course_Author(Convert.ToInt32(Request.QueryString["id"]));
            Add_View(Convert.ToInt32(Request.QueryString["id"]));

            if (!Page.IsPostBack && firstComment == 1)
            {
                Load_Comments(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
        else
        {
            Response.Redirect("eroare.aspx", false);
        }
    }

    protected bool isQueryFormatCorrect()
    {
        if (Request.QueryString["id"] != null)
        {
            int id_current_course = Convert.ToInt32(Request.QueryString["id"]);
            if (isCourseAvailable(id_current_course))
            {
                return true;
            }
        }
        return false;
    }

    protected bool isCourseAvailable(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Courses WHERE (id_course=@id_course)", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

                int isAvailable = (int)cmd.ExecuteScalar();

                if (isAvailable > 0)
                {
                    return true;
                }
            }
        }
        return false;
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
                    {
                        return true;
                    }
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
        FormModerateCourse.Visible = false;
    }

    protected void Load_NavBar_User(ref int user_id_group)
    {
        DesktopFormSignin.Visible = false;
        MobileFormSignin.Visible = false;
        FormModerateCourse.Visible = false;

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
                reader.Close();
                cmd.ExecuteNonQuery();
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

    public bool isAllowedToModerateCourse(int user_id_group)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT moderate_course FROM Permissions WHERE (id_group=@id_group)", con))
            {
                cmd.Parameters.AddWithValue("@id_group", user_id_group);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["moderate_course"].ToString() == "1")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    protected void Load_Course_Details(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    this.Title = reader["title"] + " | Curs";
                    course_title.Text = course_name.Text = reader["title"].ToString();
                    course_description.Text = reader["description"].ToString();
                    course_teory.Text = reader["teory"].ToString();
                    course_explained_exercises.Text = reader["explained_exercises"].ToString();
                    course_solved_exercises.Text = reader["solved_exercises"].ToString();
                    course_datetime.Text = getRelativeDateTime(Convert.ToDateTime(reader["datetime"]));
                    course_downloads.Text = reader["downloads"].ToString();
                    course_comments.Text = reader["comments"].ToString();
                    course_views.Text = reader["views"].ToString();
                    course_category_link.HRef = "cursuri.aspx?category=" + reader["id_category"];
                    course_category.Text = Get_CategoryName(Convert.ToInt32(reader["id_category"]));
                }
                reader.Close();
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT rate FROM Ratings WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

                int rating = 0;
                int nrRate = 0;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nrRate++;
                    rating = rating + Convert.ToInt32(reader["rate"]);
                }
                reader.Close();

                course_rating.Attributes.Add("data-rating", (rating/nrRate).ToString());

                cmd.ExecuteNonQuery();
            }
        }        
    }

    protected void Rate_Click(object sender, EventArgs e)
    {
        if (!userRated())
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Ratings(id_course, id_user, rate) VALUES(@id_course, @id_user, @rate)", con))
                {

                    cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@rate", Convert.ToInt32(course_rating.Attributes["data-rating"]));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    protected int Get_UserId()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_user FROM Users WHERE username=@username", con))
            {
                HttpCookie usernameCookie = Request.Cookies["username"];
                string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");
                cmd.Parameters.AddWithValue("@username", username_decrypted);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["id_user"]);
                }

                cmd.ExecuteNonQuery();
            }
        }
        return 0;
    }

    protected bool userRated()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ratings WHERE id_user=@id_user", con))
            {
                cmd.Parameters.AddWithValue("@id_user", Get_UserId());

                int userRated = (int)cmd.ExecuteScalar();
                if (userRated > 0)
                {
                    return true;
                }

                cmd.ExecuteNonQuery();
            }
        }
        return false;   
    }

    protected void Poll_YES_Click(object sender, EventArgs e)
    {
        if (!userPolled())
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Poll(id_course, id_user, vote) VALUES(@id_course, @id_user, @vote)", con))
                {

                    cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@vote", 1);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    protected void Poll_NO_Click(object sender, EventArgs e)
    {
        if (!userPolled())
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Poll(id_course, id_user, vote) VALUES(@id_course, @id_user, @vote)", con))
                {

                    cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@vote", 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    protected bool userPolled()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Poll WHERE id_user=@id_user AND id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));

                int userPolled = (int)cmd.ExecuteScalar();
                if (userPolled > 0)
                {
                    return true;
                }

                cmd.ExecuteNonQuery();
            }
        }
        return false;
    }

    protected void Load_Poll_Results(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT vote FROM Poll WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

                int vote = 0;
                int nrVote = 0;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nrVote++;
                    if(reader["vote"].ToString() == "1")
                        vote = vote + Convert.ToInt32(reader["vote"]);
                }
                reader.Close();

                PollResults.Attributes.Add("data-percent", ((vote / nrVote) * 100).ToString());

                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Download_Click(object sender, EventArgs e)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT title, teory, explained_exercises, solved_exercises  FROM Courses WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string filePath = desktopPath + "/" + reader["title"].ToString() + ".html";

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine("<h3>" + reader["title"] + "</h3><hr/>");
                            w.WriteLine("<p>" + reader["teory"] + "</p>");
                            w.WriteLine("<p>" + reader["explained_exercises"] + "</p>");
                            w.WriteLine("<p>" + reader["solved_exercises"] + "</p>");
                            w.WriteLine("<hr/>Copyright <a href=\"WWW.EOTO.COM\">EOTO.COM</a>");
                        }
                    } 
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }

        Add_Download(Convert.ToInt32(Request.QueryString["id"]));
        msg_DownloadedCourse.Visible = true;
    }

    protected void Load_Course_Author(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_user, id_group, username, courses, avatar FROM Users WHERE id_user=@id_user", con))
            {
                cmd.Parameters.AddWithValue("@id_user", Get_AuthorId(id_course));

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Author_Avatar.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                    Author_Link.HRef = "profil.aspx?id=" + reader["id_user"].ToString();
                    Author_Username.Text = reader["username"].ToString();
                    Author_Courses.Text = reader["courses"].ToString();
                    Author_Group.Text = Get_GroupName(Convert.ToInt32(reader["id_group"]));
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
    }

    protected int Get_AuthorId(int id_course)
    { 
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_user FROM Courses WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

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

    protected string Get_CategoryName(int id_category)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM Categories WHERE id_category=@id_category", con))
            {
                cmd.Parameters.AddWithValue("@id_category", id_category);

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

    protected void Unlock_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET opened=1 WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Lock_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET opened=0 WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Courses WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                cmd.ExecuteNonQuery();
            }
        }
    }

    public string getRelativeDateTime(DateTime date)
    {
        TimeSpan ts = DateTime.Now - date;
        if (ts.TotalMinutes < 1)//seconds ago
            return "Chiar acum";
        if (ts.TotalHours < 1)//min ago
            return (int)ts.TotalMinutes == 1 ? "Acum un minut" : "Acum " + (int)ts.TotalMinutes + " minute";
        if (ts.TotalDays < 1)//hours ago
            return (int)ts.TotalHours == 1 ? "Acum o ora" : "Acum " + (int)ts.TotalHours + " ore";
        if (ts.TotalDays < 7)//days ago
            return (int)ts.TotalDays == 1 ? "Acum o zi" : "Acum " + (int)ts.TotalDays + " zile";
        if (ts.TotalDays < 30.4368)//weeks ago
            return (int)(ts.TotalDays / 7) == 1 ? "Acum o saptamana" : "Acum " + (int)(ts.TotalDays / 7) + " saptamani";
        if (ts.TotalDays < 365.242)//months ago
            return (int)(ts.TotalDays / 30.4368) == 1 ? "Acum o luna" : "Acum " + (int)(ts.TotalDays / 30.4368) + " luni";
        //years ago
        return (int)(ts.TotalDays / 365.242) == 1 ? "Acum un an" : "Acum " + (int)(ts.TotalDays / 365.242) + " ani";
    }

    public void Add_View(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET views = views + 1 WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Add_Download(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET downloads = downloads + 1 WHERE id_course=@id_course", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Load_Comments(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Comments WHERE id_course=@id_course ORDER BY datetime DESC", con))
            {
                cmd.Parameters.AddWithValue("@id_course", id_course);

                int commentsNumber = 0;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    commentsNumber++;
                    if (commentsNumber < firstComment + 3)
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divContent.InnerHtml = "<a class=\"avatar\"><img src=\"" + Get_UserAvatarSrc(Convert.ToInt32(reader["id_user"])) + "\"/></a><div class=\"content\"><a class=\"author\" href=\"profil.aspx?id=" + reader["id_user"] + "\">" + Get_UserName(Convert.ToInt32(reader["id_user"])) + "</a><div class=\"metadata\"><span class=\"date\">" + getRelativeDateTime(Convert.ToDateTime(reader["datetime"])) + "</span></div><div class=\"text\">" + reader["comment"] + "</div></div>";
                        divContent.Attributes.Add("class", "comment");
                        divContainer.Controls.Add(divContent);
                    }
                }
                reader.Close();

                if (firstComment >= commentsNumber)
                {
                    Load_Comments_Button.Visible = false;
                }

                if (commentsNumber == 0)
                {
                    NoCommentsToDisplay.Visible = true;
                }

                cmd.ExecuteNonQuery();
            }
        }  
    }

    public string Get_UserAvatarSrc(int id_user)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT avatar FROM Users WHERE id_user=@id_user", con))
            {
                cmd.Parameters.AddWithValue("@id_user", id_user);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    return "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                }

                cmd.ExecuteNonQuery();
            }
        }
        return null;
    }

    public string Get_UserName(int id_user)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT username FROM Users WHERE id_user=@id_user", con))
            {
                cmd.Parameters.AddWithValue("@id_user", id_user);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return reader["username"].ToString();
                }

                cmd.ExecuteNonQuery();
            }
        }
        return null;
    }

    protected void Load_Comments_Click(object sender, EventArgs e)
    {
        firstComment = firstComment + 3;
        Load_Comments(Convert.ToInt32(Request.QueryString["id"]));
    }

    protected void AddComment_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrWhiteSpace(AddComment.Value))
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Comments(id_course, id_user, comment, datetime) VALUES(@id_course, @id_user, @comment, @datetime)", con))
                {
                    HttpCookie usernameCookie = Request.Cookies["username"];
                    string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");

                    cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@comment", AddComment.Value.ToString());
                    cmd.Parameters.AddWithValue("@datetime", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET comments=comments + 1 WHERE id_course=@id_course", con))
                {
                    HttpCookie usernameCookie = Request.Cookies["username"];
                    string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");

                    cmd.Parameters.AddWithValue("@id_course", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.ExecuteNonQuery();
                }
            }
            msg_CommentCreated.Visible = true;
        }
        else
        {
            msg_CommentEmpty.Visible = true;
        }
    }
}