<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cursuri.aspx.cs" Inherits="cursuri" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <!-- STANDARD META -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0"/>

    <!-- PAGE ICON -->
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />

    <!-- PAGE TITLE -->
    <title>Cursuri</title>
    
    <!-- SEMANTIC-UI -->
    <link href="semantic/semantic.min.css" rel="stylesheet" type="text/css"/>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="semantic/semantic.min.js"></script>

    <!-- CKEDITOR -->
    <script src="ckeditor/ckeditor.js"></script>

    <!-- RESPONSIVE NAVIGATION BAR -->
    <link rel="stylesheet" href="stylesheet/nav.css"/>

    <!-- OTHERS CSS -->
    <link href="stylesheet/others.css" rel="stylesheet"/>
    
    <!-- ANIMATED BACKGROUND ON HEADER -->
    <link href="stylesheet/background.css" rel="stylesheet" type="text/css"/>
    <link href="stylesheet/secondary-image.css" rel="stylesheet" type="text/css"/>
</head>

<body style="background-color:ghostwhite">
<form runat="server">
    <!-- DESKTOP NAVIGATION BAR -->
    <div class="desktop-only">
        <div class="ui top fixed blue inverted secondary menu" style="border-bottom: 2px solid #1e77bb">
            <div class="item header">
                <h5 class="ui header">
                    <i class="university icon" style="color:gold"></i>
                    <div class="content">
                        <span class="ui tiny header" style="color:gold"><b>EOTO.COM</b></span>
                        <div class="sub header"><i><small style="color:#b9d8f0">Platforma educationala</small></i></div>
                    </div>
                </h5>
            </div>
            <a href="acasa.aspx" class="item">Acasa</a>
            <a href="cursuri.aspx" class="active item">Cursuri</a>
            <div class="right menu">
                <!-- LOGN IN AND SIGN UP NAVIGATION BAR -->
                <div class="item" id="DesktopFormSignin" runat="server">
                    <div class="ui buttons">
                        <a href="logare.aspx" class="ui teal button"><i class="signing icon"></i>Logare</a>
                        <div class="or" data-text="sau"></div>
                        <a href="inregistrare.aspx" class="ui green button"><i class="signup icon"></i>Inregistrare</a>
                    </div>
                </div>
                <!-- PROFILE NAVIGATION BAR -->
                <div id="DesktopFormProfile" runat="server" class="ui pointing dropdown button link item">
                    <span class="text"><asp:Image id="DesktopAvatar" runat="server" class="ui circular avatar image"/><asp:Literal id="DesktopUsername" runat="server" Text="Avasilcai Alexandru"></asp:Literal></span>
                    <i class="dropdown icon"></i>
                    <div class="menu">
                        <a id="ProfileLink" runat="server" class="item"><i class="user blue icon"></i>Profil</a>
                        <div class="divider"></div>
                        <a runat="server" onserverclick="Logout_Click" class="item"><i class="sign out alternate blue icon"></i>Deconectare</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- MOBILE NAVIGATION BAR -->
    <div class="mobile-only">
        <div class="ui top fixed blue inverted secondary menu" style="border-bottom: 2px solid #1e77bb">
            <div class="item header">
                <h5 class="ui header">
                    <i class="university icon" style="color:gold"></i>
                    <div class="content">
                        <span class="ui tiny header" style="color:gold">EOTO.COM</span>
                        <div class="sub header"><i><small style="color:#b9d8f0">Platforma educationala</small></i></div>
                    </div>
                </h5>
            </div>
            <div class="right menu">
                <a class="item" onclick="show_hide()"><i class="bars icon"></i></a>
            </div>
        </div>
        <div id="toggle" style="display:none">
            <div class="ui top fixed fluid blue inverted vertical menu" style="margin-top: 55px;border-bottom: 2px solid #1e77bb">
                <div class="ui center aligned container">
                    <a href="acasa.aspx" class="item">Acasa</a>
                    <a href="cursuri.aspx" class="active item">Cursuri</a>
                    <!-- PROFILE NAVIGATION BAR -->
                    <a id="MobileFormProfile" runat="server" class="item"><asp:Image id="MobileAvatar" runat="server" class="ui circular avatar image"/><asp:Literal id="MobileUsername" runat="server" Text="Avasilcai Alexandru"></asp:Literal></a>
                    <!-- LOGN IN AND SIGN UP NAVIGATION BAR -->
                    <div class="item" id="MobileFormSignin" runat="server">
                        <div class="ui buttons">
                            <a href="logare.aspx" class="ui teal button"><i class="signing icon"></i>Logare</a>
                            <div class="or" data-text="sau"></div>
                            <a href="inregistrare.aspx" class="ui green button"><i class="signup icon"></i>Inregistrare</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- ANIMATED BACKGROUND ON HEADER -->
    <div id="animate-area" style="position: relative">
        <div class="ui container center aligned" style="padding-top:75px">
        <h2 class="ui icon header" style="color:white">
            <i class="book icon"></i>
            <div class="content">Cursuri cu caracter educational<div class="sub header" style="color:white">Aceasta pagina joaca rolul de motor de cautare pentru varietatea noastra de cursuri, care la randul lor sunt impartite pe o multime de categorii. Filtrarea cursurilor se poate realiza prin completarea barii de cautare. De asemenea, utilizatorii cu un statut mai mare de acces pot adauga cursuri publice.</div></div>
        </h2>
        </div>
        <img id="SecondayImage" runat="server" class="ui secondary-image centered image" src="https://i.imgur.com/kjD25c2.png"/>
    </div>

    <!-- PAGE CONTENT -->
    <div class="ui container" style="padding-top:30px;padding-bottom:30px">
        <div class="ui segment" style="margin-bottom:20px">
            <div class="ui fluid action input">
                <input id="keywords" runat="server" type="text" placeholder="Cauta dupa cuvinte cheie: titlul, descriere..."/>
                <a runat="server" onserverclick="Search_Click" class="ui blue button"><i class="search icon"></i>Search</a>
            </div>
        </div>
        <!-- COURSES CATEGORIES -->
        <div class="ui stackable grid">
            <div class="five wide column">
                <div id="categoriesContainer" runat="server" class="ui vertical fluid menu">
                </div>
            </div>
            <!-- RECENT COMMENTS LIST -->
            <div class="eleven wide column column-margin">
                <div id="msg_CourseEmpty" runat="server" class="ui negative icon message" style="margin-bottom:10px;">
                        <i class="close icon"></i>
                        <i class="delete icon"></i>
                        <div class="content">
                            <div class="header">Datele introduse sunt incorecte!</div>
                            <p>Va rugam sa completati toate campurile inainte sa dati click pe buton. Pentru a vedea erorilor, selectati un camp si apasati tasta "enter".</p>
                        </div>
                </div>

                <div id="msg_CourseCreated" runat="server" class="ui success icon message" style="margin-bottom:10px;">
                        <i class="close icon"></i>
                        <i class="check icon"></i>
                        <div class="content">
                            <div class="header">Cursul a fost adaugat cu succes!</div>
                            <p>Va multumim pentru adaugare! Cursul poate fi acum accesat.</p>
                        </div>
                </div>
                <!-- ADDING A NEW COURSE -->
                <div id="FormAddCourse" runat="server" class="ui center aligned inverted green segment">
                    <div class="ui inverted accordion">
                        <div class="title"><b>Doriti sa adaugati un curs nou<i class="question icon"></i></b></div>
                        <div class="content">
                            <div class="ui inverted divider"></div>
                            <div class="ui top attached message">
                                <b>Completati toate campurile de mai jos<i class="warning icon"></i></b>
                                <div class="ui divider"></div>
                                <div class="ui form">
                                    <div class="field">
                                        <div class="ui left icon input">
                                            <input id="title" runat="server" type="text" placeholder="Titlul cursului"/>
                                            <i class="slack blue icon"></i>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <div class="ui left icon input">
                                            <input id="description" runat="server" type="text" placeholder="Descrierea cursului"/>
                                            <i class="info blue icon"></i>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <div id="categoriesDropdown" runat="server" class="ui selection dropdown">
                                            <input id="id_category" runat="server" type="hidden" name="id_category">
                                            <i class="dropdown icon"></i>
                                            <div class="default text"><i class="tags blue icon"></i>Numele categoriei</div>
                                            <div id="categoriesDropdownContainer" runat="server" class="menu">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <div class="ui top attached left aligned segment"><i class="sticky note outline blue icon"></i>Teoria cursului</div>
                                        <div class="ui attached segment">
                                            <CKEditor:CKEditorControl ID="teory" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <div class="ui top attached left aligned segment"><i class="pencil alternate blue icon"></i>Exercitii explicate</div>
                                        <div class="ui attached segment">
                                            <CKEditor:CKEditorControl ID="explained_exercises" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <div class="ui top attached left aligned segment"><i class="edit outline blue icon"></i>Exercitii de rezolvat</div>
                                        <div class="ui attached segment">
                                            <CKEditor:CKEditorControl ID="solved_exercises" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>
                                    <div class="ui error message"></div>
                                </div>
                            </div>
                            <a runat="server" onserverclick="AddCourse_Click" class="ui fluid blue attached button"><i class="plus green icon"></i>Adauga curs</a>
                        </div>
                    </div>
                </div>

                <!-- COURSES LIST -->
                <div id="NoCoursesToDisplay" runat="server" class="ui center aligned container" style="margin-bottom:25px"><p>Nu exista cursuri de afisat.</p></div>
                <div id="coursesContainer" runat="server" class="ui one column grid">
                </div>

                <!-- PAGINATION LIST -->
                <div class="ui center aligned container" style="margin-top:15px">
                    <div class="ui pagination menu">
                        <a id="BackButton" runat="server" class="item"><i class="angle double left blue icon"></i>Anterioara</a>
                        <a class="disabled item">Pagina curenta: <asp:Literal id="CurrentPage" runat="server"></asp:Literal></a>
                        <a id="NextButton" runat="server" class="item">Urmatoarea<i class="angle double right blue icon"></i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- FOOTER -->
    <div class="ui inverted vertical footer center aligned segment" style="background-color:#2185d0">
        <div class="ui container" style="padding-top:10px;padding-bottom:10px">
            <div class="ui inverted divider"></div>
            <div class="ui stackable inverted grid">
                <!-- FIRST COLUMN -->
                <div class="seven wide column">
                    <div class="ui inverted list">
                        <h5 class="ui center aligned inverted header">Despre noi</h5>
                        <div class="item" style="color:#90c2e7"><b>EACH ONE TEACH ONE COMMUNITY</b> este o platforma educationala, iar aceasta poate fi utilizata atat in evaluare cat si in proiectare didactica. Evaluare se face prin intermediul: bibliotecilor virtuale in scopul obtinerii de informatii asupra conceptelor cheie, a testarii online, a aprofundarii si sistematizarii cunostintelor utilizand cursurile online; iar proiectarea didactica se face prin postarea resurselor enumerate anterior.</div>
                    </div>
                </div>
                <!-- SECOND COLUMN -->
                <div class="three wide center aligned column">
                    <h5 class="ui inverted header">Comunitate</h5>
                    <div class="ui inverted link list">
                        <a href="acasa.aspx" class="item">Acasa</a>
                        <a href="cursuri.aspx" class="item">Cursuri</a>
                        <a href="logare.aspx" class="item">Logare</a>
                        <a href="inregistrare.aspx" class="item">Inregistrare</a>
                    </div>
                </div>
                <!-- THIRD COLUMN -->
                <div class="three wide center aligned column">
                    <h5 class="ui inverted header">Informatii</h5>
                    <div class="ui inverted link list">
                        <a href="acasa.aspx#Acasa" class="item">Acasa</a>
                        <a href="acasa.aspx#Cursuri" class="item">Cursuri</a>
                        <a href="acasa.aspx#DespreNoi" class="item">Despre noi</a>
                        <a href="acasa.aspx#Inregistrare" class="item">Inregistrare</a>
                    </div>
                </div>
                <!-- FOURTH COLUMN -->
                <div class="three wide center aligned column">
                    <h5 class="ui inverted header">Social</h5>
                    <div class="ui inverted link list">
                        <a href="#" class="item"><div class="ui twitter circular mini button"><i class="twitter icon"></i>Twitter</div></a>
                        <a href="#" class="item"><div class="ui red mini circular button"><i class="youtube icon"></i>Youtube</div></a>
                        <a href="#" class="item"><div class="ui facebook circular mini button"><i class="facebook icon"></i>Facebook</div></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="ui inverted divider"></div>
        <div class="ui horizontal list">
            <div class="item">Copyright © <b><a href="acasa.aspx" style="color:gold">EOTO.COM</a></b> 2018. Toate drepturile rezervate.</div>
        </div>
    </div>
</form>

<!-- JAVASCRIPTS -->
<script src="javascripts/javascript.js" type="text/javascript"></script>
<script>
    $('.ui.rating')
        .rating('disable');
    $('.ui.dropdown')
        .dropdown({
            action: 'hide'
        });
    $('#categoriesDropdown')
        .dropdown();
    $('.ui.accordion')
        .accordion();
    $('.ui .form')
        .form({
            fields: {
                title: 'empty',
                description: 'empty',
                teory: 'empty',
                explained_exercises: 'empty',
                solved_exercises: 'empty',
                id_category: 'empty'
            }});
    $('.card')
        .transition({
            animation: 'pulse',
            duration: 800,
            interval: 200
        });
</script>

</body>
</html>
