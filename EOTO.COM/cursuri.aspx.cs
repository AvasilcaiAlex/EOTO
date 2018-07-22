using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;

public partial class cursuri : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NoCoursesToDisplay.Visible = msg_CourseCreated.Visible = msg_CourseEmpty.Visible = false;

        if (isUserLogged())
        {
            int user_id_group = 0;
            Load_NavBar_User(ref user_id_group);

            if(isAllowedToAddCourse(user_id_group))
            {
                FormAddCourse.Visible = true;
            }
        }
        else
        {
            Load_NavBar_Guest();
        }

        Load_Categories();
        Load_Dropdown_Categories();
        Load_Courses();
    }

    protected bool isUserLogged()
    {
        if(isUserAuthenticated())
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
        FormAddCourse.Visible = false;
    }

    protected void Load_NavBar_User(ref int user_id_group)
    {
        DesktopFormSignin.Visible = false;
        MobileFormSignin.Visible = false;
        FormAddCourse.Visible = false;

        using(SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("SELECT id_user, id_group, username, avatar FROM Users WHERE username=@username AND password=@password", con))
            {
                HttpCookie usernameCookie = Request.Cookies["username"];
                HttpCookie passwordCookie = Request.Cookies["password"];
                string username_decrypted = Encrypt.DecryptString(usernameCookie.Value, "EncryptPassword");
                string password_decrypted = Encrypt.DecryptString(passwordCookie.Value, "EncryptPassword");

                cmd.Parameters.AddWithValue("@username", username_decrypted);
                cmd.Parameters.AddWithValue("@password", password_decrypted);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
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

    public bool isAllowedToAddCourse(int user_id_group)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT add_course FROM Permissions WHERE (id_group=@id_group)", con))
            {
                cmd.Parameters.AddWithValue("@id_group", user_id_group);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["add_course"].ToString() == "1")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    protected void Load_Categories()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string category = null;

                    if (Request.QueryString["category"] != null)
                    {
                        category = Request.QueryString["category"].ToString();
                    }

                    if (category != null)
                    {
                        if (category == reader["id_category"].ToString())
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                            divContent.InnerHtml = "<h5 class=\"ui blue header\">"+ reader["name"] +" <div class=\"ui right floated icon circular tiny blue label\">" + Get_CategoriesNumber(Convert.ToInt32(reader["id_category"])) + "</div></h5><small>"+ reader["description"] +"</small>";
                            divContent.Attributes.Add("class", "active item");
                            divContent.Attributes.Add("href", "cursuri.aspx?category=" + reader["id_category"]);
                            categoriesContainer.Controls.Add(divContent);
                        }
                        else
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                            divContent.InnerHtml = "<h5 class=\"ui header\">" + reader["name"] + " <div class=\"ui right floated icon circular tiny grey label\">" + Get_CategoriesNumber(Convert.ToInt32(reader["id_category"])) + "</div></h5><small>" + reader["description"] + "</small>";
                            divContent.Attributes.Add("class", "item");
                            divContent.Attributes.Add("href", "cursuri.aspx?category=" + reader["id_category"]);
                            categoriesContainer.Controls.Add(divContent);
                        }
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                        divContent.InnerHtml = "<h5 class=\"ui header\">" + reader["name"] + " <div class=\"ui right floated icon circular tiny grey label\">" + Get_CategoriesNumber(Convert.ToInt32(reader["id_category"])) + "</div></h5><small>" + reader["description"] + "</small>";
                        divContent.Attributes.Add("class", "item");
                        divContent.Attributes.Add("href", "cursuri.aspx?category=" + reader["id_category"]);
                        categoriesContainer.Controls.Add(divContent);
                    }
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Load_Dropdown_Categories()
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM Categories", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divContent.InnerHtml = "<i class=\"tags icon\"></i>" + reader["name"].ToString();
                    divContent.Attributes.Add("class", "item");
                    divContent.Attributes.Add("data-value", reader["name"].ToString());
                    categoriesDropdownContainer.Controls.Add(divContent);

                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
    }

    protected int Get_CategoriesNumber(int id_category)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Courses WHERE id_category=@id_category", con))
            {
                cmd.Parameters.AddWithValue("@id_category", id_category);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    protected void Load_Courses()
    {
        int page = 1;
        int coursesNumber = 0;

        if (Request.QueryString["page"] != null)
        {
            page = Convert.ToInt32(Request.QueryString["page"]);
        }

        int firstCourse = (page*5)-4;
        int lastCourse = page*5;

        CurrentPage.Text = page.ToString();
        if (Request.QueryString["category"] != null)
        {
            BackButton.HRef = "cursuri.aspx?category=" + Request.QueryString["category"] + "&page=" + (page - 1).ToString();
            NextButton.HRef = "cursuri.aspx?category=" + Request.QueryString["category"] + "&page=" + (page + 1).ToString();
        }
        else
        {
            BackButton.HRef = "cursuri.aspx?page=" + (page - 1).ToString();
            NextButton.HRef = "cursuri.aspx?page=" + (page + 1).ToString();
        }

        bool url_empty = true;
        string sql;
        if (Request.QueryString["search"] != null)
        {
            url_empty = false;
            string words = Request.QueryString["search"];
            GlobalVariables.keywords = words.Split('-');
        }

        if (url_empty == true)
        {
            sql = "SELECT * FROM Courses ORDER BY datetime DESC";
            if (Request.QueryString["category"] != null)
            {
                sql = "SELECT * FROM Courses WHERE id_category='" + Request.QueryString["category"].ToString() + "' ORDER BY datetime DESC";
            }
        }
        else
        {
            sql = "SELECT * FROM Courses WHERE (";
            for (int i = 0; i < GlobalVariables.keywords.Length; i++)
            {
                if (i == 0)
                {
                    sql = sql + "title LIKE '%" + GlobalVariables.keywords[i] + "%'";
                    sql = sql + " OR description LIKE '%" + GlobalVariables.keywords[i] + "%'";
                }
                else
                {
                    sql = sql + " OR title LIKE '%" + GlobalVariables.keywords[i] + "%'";
                    sql = sql + " OR description LIKE '%" + GlobalVariables.keywords[i] + "%'";
                }
            }
            sql = sql + " OR title='" + Request.QueryString["search"].Replace("-", " ") + "'";
            sql = sql + " OR description='" + Request.QueryString["search"].Replace("-", " ") + "')";
            if (Request.QueryString["category"] != null)
            {
                sql = sql + " AND id_category='" + Request.QueryString["category"].ToString() + "'";
            }
            sql = sql + " ORDER BY datetime DESC";
        }

        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    coursesNumber++;

                    if (coursesNumber >= firstCourse && coursesNumber <= lastCourse)
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divContent.InnerHtml = "<div class=\"ui fluid blue card\"><div class=\"content\"> <div class=\"meta right floated\"> <div class=\"ui star rating\" data-rating=\"" + Get_Rating(Convert.ToInt32(reader["id_course"])) + "\" data-max-rating=\"5\"></div> </div> <div class=\"header\"><a href=\"curs.aspx?id=" + reader["id_course"] + "\"><i class=\"share square blue icon\"></i>" + reader["title"] + "</a><div class=\"sub header\"><a href=\"cursuri.aspx?category=" + reader["id_category"] + "\" class=\"ui tag label\">" + Get_CategoryName(Convert.ToInt32(reader["id_category"])) + "</a></div></div> </div> <div class=\"content\"> <div class=\"description\"> <p>" + reader["description"] + "</p> </div> </div> <div class=\"extra content\"> <div class=\"ui center aligned container\"> <div class=\"ui mini horizontal list\"> <div class=\"item\"> <img class=\"ui mini circular image\" src=\"" + Get_UserAvatarSrc(Convert.ToInt32(reader["id_user"])) + "\"> <div class=\"content\"> <a href=\"profil.aspx?id=" + reader["id_user"] + "\" class=\"ui sub header\">" + Get_UserName(Convert.ToInt32(reader["id_user"])) + "</a> Autorul </div> </div> <div class=\"item\"> <img class=\"ui mini circular image\" src=\"images/date.png\"> <div class=\"content\"> <div class=\"ui sub header\">" + getRelativeDateTime(Convert.ToDateTime(reader["datetime"])) + "</div> Data postarii </div> </div> <div class=\"item\"> <img class=\"ui mini circular image\" src=\"images/download.png\"> <div class=\"content\"> <div class=\"ui sub header\">" + reader["downloads"] + "</div> Descarcari </div> </div> <div class=\"item\"> <img class=\"ui mini circular image\" src=\"images/comments.png\"> <div class=\"content\"> <div class=\"ui sub header\">" + reader["comments"] + "</div> Comentarii </div> </div> <div class=\"item\"> <img class=\"ui mini circular image\" src=\"images/views.png\"> <div class=\"content\"> <div class=\"ui sub header\">" + reader["views"] + "</div> Vizualizari </div> </div> </div> </div> </div> </div>";
                        divContent.Attributes.Add("class", "column");
                        coursesContainer.Controls.Add(divContent);
                    }
                }
                reader.Close();

                if (lastCourse > coursesNumber)
                {
                    NextButton.Attributes.Add("class", "disabled item");
                    NextButton.HRef = "";
                }

                if(page == 1)
                {
                    BackButton.Attributes.Add("class", "disabled item");
                    BackButton.HRef = "";
                }

                if (coursesNumber == 0)
                {
                    NoCoursesToDisplay.Visible = true;
                }

                cmd.ExecuteNonQuery();
            }
        }
    }

    protected string Get_Rating(int id_course)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
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

                return (rating/nrRate).ToString();

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

    protected int Get_CategoryId(string name)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT id_category FROM Categories WHERE name=@name", con))
            {
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["id_category"]);
                }
                reader.Close();

                cmd.ExecuteNonQuery();
            }
        }
        return 0;
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

    public string Get_UserAvatarSrc(int id_user)
    {
        using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT avatar FROM Users WHERE id_user=@id_user", con))
            {
                cmd.Parameters.AddWithValue("@id_user", id_user);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return "data:image;base64," + Convert.ToBase64String((byte[])reader["avatar"]);
                }

                cmd.ExecuteNonQuery();
            }
        }
        return null;
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        string words = keywords.Value;
        if (!String.IsNullOrEmpty(words))
        {
            string keyword = words.Replace(" ", "-");
            Response.Redirect("cursuri.aspx?search=" + keyword, false);
        }
    }

    protected void AddCourse_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(title.Value) && !String.IsNullOrEmpty(description.Value) && !String.IsNullOrEmpty(teory.Text) && !String.IsNullOrEmpty(explained_exercises.Text) && !String.IsNullOrEmpty(solved_exercises.Text) && !String.IsNullOrEmpty(id_category.Value))
        {
            using (SqlConnection con = new SqlConnection(GlobalVariables.ConnectionString))
            {
                int id_course = 0;

                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Courses (id_user, id_category, title, description, teory, explained_exercises, solved_exercises, datetime, downloads, comments, views, opened) VALUES (@id_user, @id_category, @title, @description, @teory, @explained_exercises, @solved_exercises, @datetime, @downloads, @comments, @views, @opened);SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@id_category", Get_CategoryId(Request.Form["id_category"].ToString()));
                    cmd.Parameters.AddWithValue("@title", title.Value);
                    cmd.Parameters.AddWithValue("@description", description.Value);
                    cmd.Parameters.AddWithValue("@teory", teory.Text);
                    cmd.Parameters.AddWithValue("@explained_exercises", explained_exercises.Text);
                    cmd.Parameters.AddWithValue("@solved_exercises", solved_exercises.Text);
                    cmd.Parameters.AddWithValue("@datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@downloads", 0);
                    cmd.Parameters.AddWithValue("@comments", 0);
                    cmd.Parameters.AddWithValue("@views", 0);
                    cmd.Parameters.AddWithValue("@opened", 1);

                    id_course = Convert.ToInt32(cmd.ExecuteScalar());

                    //cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET courses = courses + 1 WHERE id_user=@id_user", con))
                {
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Ratings(id_course, id_user, rate) VALUES(@id_course, @id_user, @rate)", con))
                {

                    cmd.Parameters.AddWithValue("@id_course", id_course);
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@rate", 5);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Poll(id_course, id_user, vote) VALUES(@id_course, @id_user, @vote)", con))
                {

                    cmd.Parameters.AddWithValue("@id_course", id_course);
                    cmd.Parameters.AddWithValue("@id_user", Get_UserId());
                    cmd.Parameters.AddWithValue("@vote", 1);
                    cmd.ExecuteNonQuery();
                }
            }

            msg_CourseCreated.Visible = true;
        }
        else
        {
            msg_CourseEmpty.Visible = true;
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
}