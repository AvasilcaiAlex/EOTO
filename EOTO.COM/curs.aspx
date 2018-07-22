<%@ Page Language="C#" AutoEventWireup="true" CodeFile="curs.aspx.cs" Inherits="curs" %>

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
    <title>Curs</title>
    
    <!-- SEMANTIC-UI -->
    <link href="semantic/semantic.min.css" rel="stylesheet" type="text/css"/>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="semantic/semantic.min.js"></script>

    <!-- RESPONSIVE NAVIGATION BAR -->
    <link rel="stylesheet" href="stylesheet/nav.css"/>

    <!-- OTHERS CSS -->
    <link href="stylesheet/others.css" rel="stylesheet"/>
    
    <!-- ANIMATED BACKGROUND ON HEADER -->
    <link href="stylesheet/background.css" rel="stylesheet" type="text/css"/>
    <link href="stylesheet/secondary-image.css" rel="stylesheet" type="text/css"/>
</head>

<body style="background-color:ghostwhite">
<form id="Form1" runat="server">
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
            <a href="cursuri.aspx" class="item">Cursuri</a>
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
                    <span class="text"><asp:Image id="DesktopAvatar" runat="server" class="ui circular image"/><asp:Literal id="DesktopUsername" runat="server" Text="Avasilcai Alexandru"></asp:Literal></span>
                    <i class="dropdown icon"></i>
                    <div class="menu">
                        <a id="ProfileLink" runat="server" class="item"><i class="user blue icon"></i>Profil</a>
                        <div class="divider"></div>
                        <a id="A1" runat="server" onserverclick="Logout_Click" class="item"><i class="sign out alternate blue icon"></i>Deconectare</a>
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
                    <a href="cursuri.aspx" class="item">Cursuri</a>
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
            <div class="content">Curs specific<div class="sub header" style="color:white">Accesand aceasta pagina aveti acces la un anumit curs, cu informatii precum: titlul, descrierea, autorul, data, descarcarile, vizualizari, comentariile, dar si evaluarile cursului. Acesta este independent de celelalte cursuri.</div></div>
        </h2>
        </div>
        <img class="ui centered image" src="https://i.imgur.com/IOHueNn.png" style="position: absolute;bottom: 0px;position: absolute;margin-left: auto;margin-right: auto;left: 0;right: 0;"/>
    </div>

    <!-- CONTENT -->
<div class="ui container" style="padding-top:40px;padding-bottom:40px">
    <div class="ui stackable grid">
            <div id="msg_DownloadedCourse" runat="server" class="ui success icon message" style="margin-bottom:10px;">
                <i class="close icon"></i>
                <i class="download icon"></i>
                <div class="content">
                    <div class="header">Descarcarea a avut succes!</div>
                    <p>Cursul a fost salvata pe desktop cu denumirea <b><asp:Literal id="course_name" runat="server"></asp:Literal>.html</b></p>
                </div>
            </div>

            <div id="msg_CommentEmpty" runat="server" class="ui negative icon message" style="margin-bottom:10px;">
                    <i class="close icon"></i>
                    <i class="delete icon"></i>
                    <div class="content">
                        <div class="header">Datele introduse sunt incorecte!</div>
                        <p>Va rugam sa completati campul anterior inainte sa dati click pe butonul "Posteaza".</p>
                    </div>
            </div>

            <div id="msg_CommentCreated" runat="server" class="ui success icon message" style="margin-bottom:10px;">
                    <i class="close icon"></i>
                    <i class="check icon"></i>
                    <div class="content">
                        <div class="header">Comentariul a fost adaugat cu succes!</div>
                        <p>Va multumim pentru adaugare! Cursul poate fi acum accesat.</p>
                    </div>
            </div>

        <div class="eleven wide column">
            <div class="ui piled blue segment">
                <div class="ui container center aligned">
                    <h3 class="ui header">
                        <div class="content">
                            <i class="quote left icon"></i><asp:Literal id="course_title" runat="server"></asp:Literal><i class="quote right icon"></i>
                            <div class="ui divider"></div>
                            <div class="sub header">
                                <div class="ui mini horizontal list">
                                    <div class="item">
                                        <img class="ui mini circular image" src="images/date.png">
                                        <div class="content"><div class="ui sub header"><asp:Literal id="course_datetime" runat="server"></asp:Literal></div>Data postarii</div>
                                    </div>
                                    <div class="item">
                                        <img class="ui mini circular image" src="images/download.png">
                                        <div class="content"><div class="ui sub header"><asp:Literal id="course_downloads" runat="server"></asp:Literal></div>Descarcari</div>
                                    </div>
                                    <div class="item">
                                        <img class="ui mini circular image" src="images/comments.png">
                                        <div class="content"><div class="ui sub header"><asp:Literal id="course_comments" runat="server"></asp:Literal></div>Comentarii</div>
                                    </div>
                                    <div class="item">
                                        <img class="ui mini circular image" src="images/views.png">
                                        <div class="content"><div class="ui sub header"><asp:Literal id="course_views" runat="server"></asp:Literal></div>Vizualizari</div>
                                        </div>
                                    <div class="item">
                                        <img class="ui mini circular image" src="images/rating.png">
                                        <div class="content"><div class="ui sub header"><div id="course_rating" runat="server" class="ui star rating" data-rating="1" data-max-rating="5"></div></div><a runat="server" onserverclick="Rate_Click">Evalueaza</a></div>
                                    </div>
                                </div>
                            </div>
                            <div class="ui divider"></div>
                        </div>
                    </h3>
                </div>

                <div class="ui horizontal divider header">
                    <h4 class="ui blue header">
                        <i class="sticky note outline icon"></i>
                        <div class="content">Primul pas<div class="sub header">Invata teoria</div></div>
                    </h4>
                </div>
                <asp:Literal id="course_teory" runat="server"></asp:Literal>

                <div class="ui horizontal divider header">
                    <h4 class="ui blue header">
                        <i class="pencil alternate icon"></i>
                        <div class="content">Al doilea pas<div class="sub header">Intelege exercitiile rezolvate</div></div>
                    </h4>
                </div>
                <asp:Literal id="course_explained_exercises" runat="server"></asp:Literal>

                <div class="ui horizontal divider header">
                    <h4 class="ui blue header">
                        <i class="edit outline icon"></i>
                        <div class="content">Al treilea pas<div class="sub header">Verifica-ti cunostintele acumulate</div></div>
                    </h4>
                </div>
                <asp:Literal id="course_solved_exercises" runat="server"></asp:Literal>

                <div class="ui divider"></div>
                <div class="ui container center aligned">
                    <div class="ui mini horizontal list">
                        <div class="item">
                            <img class="ui mini circular image" src="images/download.png">
                            <div class="content"><a runat="server" onserverclick="Download_Click" class="ui sub header">Descarca</a>Fisier PDF</div>
                      </div>
                      <div class="item">
                            <img class="ui mini circular image" src="images/facebook.png">
                            <div class="content"><a href="http://www.facebook.com/sharer.php" class="ui sub header">Distribuie</a>Facebook</div>
                      </div>
                    </div>
                </div>
            </div>

            <div class="ui stacked blue segment" style="margin-top:-20px">
                <h5 class="ui dividing header"><i class="comments blue icon"></i>Comentariile cursului</h5>
                <div id="NoCommentsToDisplay" runat="server"><p>Nu exista comentarii de afisat.</p></div>
                <div id="divContainer" runat="server" class="ui comments">
                </div>
                <div id="Load_Comments_Button" runat="server" class="ui center aligned container" style="padding-bottom:20px">
                    <a runat="server" onserverclick="Load_Comments_Click" class="ui button"><i class="spinner icon"></i>Incarca comentarii...</a>
                </div>
                <div class="ui fluid action left icon input">
                    <input id="AddComment" runat="server" type="text" cols="40" rows="5" placeholder="Adauga un comentariu..."/>
                    <a runat="server" onserverclick="AddComment_Click" class="ui blue labeled submit icon button"><i class="icon edit"></i> Posteaza</a>
                    <i class="comment alternate outline icon"></i>
                </div>
            </div>
        </div>

        <div class="five wide column column-margin">
        <div class="ui raised blue segment">
            <h5 class="ui dividing header"><i class="user blue icon"></i>Autorul cursului</h5>
            <div class="ui container">
                <div class="ui centered card">
                    <div class="image"><asp:Image id="Author_Avatar" runat="server"/></div>
                    <div class="center aligned content">
                        <a id="Author_Link" runat="server" class="header" href="#"><asp:Literal id="Author_Username" runat="server"></asp:Literal></a>
                        <div class="meta"><span class="date"><asp:Literal id="Author_Group" runat="server"></asp:Literal></span></div>
                    </div>
                    <div class="extra center aligned content">
                        <a><i class="book icon"></i><asp:Literal id="Author_Courses" runat="server"></asp:Literal> Cursuri</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="ui raised blue segment">
            <h5 class="ui dividing header"><i class="info circle blue icon"></i>Descrierea cursului</h5>
            <div class="ui container"><p><asp:Literal id="course_description" runat="server"></asp:Literal></p></div>
            <div class="ui divider"></div>  
            <div class="ui center aligned container"><a id="course_category_link" runat="server" class="ui tag label"><asp:Literal id="course_category" runat="server"></asp:Literal></a></div>
        </div>

        <div class="ui raised blue segment">
            <h5 class="ui dividing header"><i class="question circle blue icon"></i>Formularul cursului</h5>
            <div class="ui container">
                Te-a <b>ajutat</b> acest curs? <a id="Poll_YES" runat="server" onserverclick="Poll_YES_Click" class="ui mini focus button">DA</a><a id="Poll_NO" runat="server" onserverclick="Poll_NO_Click" class="ui mini button">NU</a> <a id="Poll_VOTE" runat="server" href="logare.aspx" class="ui yellow label"><i class="warning icon"></i>Trebuie sa fii logat pentru a vota</a>
                <h5 class="ui horizontal divider header">Rezultate</h5>
                <div id="PollResults" runat="server" class="ui indicating small success progress" data-percent="80">
                  <div class="bar"><div class="progress"></div></div>
                </div>
            </div>
        </div>

        <div id="FormModerateCourse" runat="server" class="ui raised blue segment">
            <h5 class="ui dividing header"><i class="cogs blue icon"></i>Actiuni de moderator</h5>
            <div class="ui container center aligned">
                <a class="ui small basic icon button" data-variation="inverted" data-content="Editeaza curs // inca in lucru"><i class="edit icon"></i></button>
                <a runat="server" onserverclick="Unlock_Click" class="ui small basic icon button" data-variation="inverted" data-content="Deblocheaza curs"><i class="unlock icon"></i></a>
                <a runat="server" onserverclick="Lock_Click" class="ui small basic icon button" data-variation="inverted" data-content="Blocheaza curs"><i class="lock icon"></i></a>
                <a runat="server" onserverclick="Delete_Click" class="ui small basic icon button" data-variation="inverted" data-content="Sterge curs"><i class="trash alternate outline icon"></i></a>
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
<script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
<script src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php"type="text/javascript"></script>
<script>
    $('.ui.rating')
        .rating();
    $('.ui.progress').progress();
    $('.ui.dropdown')
        .dropdown({
            action: 'hide'
        });
    $('.button')
    .popup({
        inline: true
    });
</script>

</body>
</html>