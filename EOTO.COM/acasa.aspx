<%@ Page Language="C#" AutoEventWireup="true" CodeFile="acasa.aspx.cs" Inherits="acasa" %>

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
    <title>Acasa</title>
    
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
            <a href="acasa.aspx" class="active item">Acasa</a>
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
                    <span class="text"><asp:Image id="DesktopAvatar" runat="server" class="ui circular avatar image"/><asp:Literal id="DesktopUsername" runat="server" Text="Avasilcai Alexandru"></asp:Literal></span>
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
                    <a href="acasa.aspx" class="active item">Acasa</a>
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
            <i class="home icon"></i>
            <div class="content">Acasa<div class="sub header" style="color:white">Aceasta platforma poate fi utilizata atat in evaluare cat si in proiectare didactica. Evaluare se face prin intermediul: bibliotecilor virtuale in scopul obtinerii de informatii asupra conceptelor cheie; iar proiectarea didactica se face prin postarea resurselor enumerate anterior.</div></div>
        </h2>
        </div>
        <img class="ui centered image" src="https://i.imgur.com/sLm3iEU.png" style="position: absolute;bottom: 0px;position: absolute;margin-left: auto;margin-right: auto;left: 0;right: 0;"/>
    </div>

    <!-- PAGE CONTENT -->
    <div id="Acasa" class="ui container" style="padding-top:50px;padding-bottom:50px;">
        <div class="ui top attached blue segment" style="background-color:#f9fafb">
            <h2 class="ui centered blue header">Acasa
                <div class="sub header"><p>Pagina introductiva care ofera informatii despre proiect.</p></div>
            </h2>
        </div>
        <div class="ui attached segment" style="padding:30px;">
            <p>Prin intermediul acestui program de formare continuă, cadrele didactice vor avea capacitatea de a utiliza platformele educaționale interactive online în activităţile de predare-învăţare atât cu grupurile cu care interacţionează direct (elevi, părinţi, colegi), cât şi cu mediul profesional (profesori, experţi educaţie). Prin urmare, profesorii vor dobândi competenţele tehnologice necesare pentru a utiliza resursele din mediul online în cadrul activităţii lor profesionale, în  activitatea didactică, în activitatea de suport, cât şi în activitatea de relaţionare cu mediul profesional.</p>
            <div class="ui bulleted list">
                <div class="item">Proiectarea activităţilor de predare cu ajutorul platformelor interactive în cadrul instituţiilor de învăţământ preuniversitar;</div>
                <div class="item">Desfăşurarea activităţilor de învăţare cu ajutorul platformelor educaţionale în cadrul instituţiilor de învăţământ preuniversitar;</div>
                <div class="item">Utilizarea platformelor interactive pentru evaluarea procesului de învăţământ în cadrul instituţiilor de învăţământ preuniversitar;</div>
                <div class="item">Aplicarea celor mai eficiente strategii şi tehnici de management al clasei de elevi în condiţiile integrării platformelor educaţionale interactive.</div>
            </div>
        </div>

        <div id="Cursuri" class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>

        <div class="ui top attached blue segment" style="background-color:#f9fafb">
            <h2 class="ui centered blue header">Cursuri
                <div class="sub header"><p>Aceasta pagina joaca rolul de motor de cautare pentru varietatea noastra de cursuri.</p></div>
            </h2>
        </div>
        <div class="ui attached center aligned segment" style="padding:30px;">
            <div class="ui four small statistics">
                <div class="statistic">
                    <div class="value"><asp:Literal ID="usersNumber" runat="server"></asp:Literal> <i class="users blue icon"></i></div>
                    <div class="label">Utilizatori</div>
                </div>
                <div class="statistic">
                    <div class="value"><asp:Literal ID="coursesNumber" runat="server"></asp:Literal> <i class="book blue icon"></i></div>
                    <div class="label">Cursuri</div>
                </div>
                <div class="statistic">
                    <div class="value"><asp:Literal ID="categoriesNumber" runat="server"></asp:Literal> <i class="tags blue icon"></i></div>
                    <div class="label">Materii</div>
                </div>
                <div class="statistic">
                    <div class="value"><asp:Literal ID="commentsNumber" runat="server"></asp:Literal> <i class="comments blue icon"></i></div>
                    <div class="label">Comentarii</div>
                </div>
            </div>
            <a href="cursuri.aspx" class="ui animated blue button" tabindex="0" style="margin-top:10px;">
                <div class="visible content">Acceseaza chiar acum</div>
                <div class="hidden content"><i class="right arrow icon"></i></div>
            </a>
        </div>

        <div id="Inregistare" class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>

        <div class="ui top attached blue segment" style="background-color:#f9fafb">
            <h2 class="ui centered blue header">Inregistrare
                <div class="sub header"><p>Inregistrati-va pe site pentru a avea acces la toate facilitatile acestuia.</p></div>
            </h2>
        </div>
        <div class="ui attached center aligned segment" style="padding:30px;">
            <h4>Permisiunile utilizatorului in functie de grupul din care face parte:</h4>
            <table class="ui center aligned celled table">
                <thead>
                    <tr>
                        <th>Denumire grup</th>
                        <th>Vizualizare cursuri</th>
                        <th>Postare cursuri</th>
                        <th>Blocare/deblocare cursuri</th>
                        <th>Stergere cursuri</th>
                    </tr>
              </thead>
              <tbody>
                    <tr>
                        <td>Vizitator</td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td class="disabled"><i class="large checkmark icon"></i></td>
                        <td class="disabled"><i class="large checkmark icon"></i></td>
                        <td class="disabled"><i class="large checkmark icon"></i></td>
                    </tr>
                    <tr>
                        <td>Membru</td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td class="disabled"><i class="large checkmark icon"></i></td>
                        <td class="disabled"><i class="large checkmark icon"></i></td>
                    </tr>
                    <tr>
                        <td>Administrator</td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td><i class="large green checkmark icon"></i></td>
                        <td><i class="large green checkmark icon"></i></td>
                    </tr>
              </tbody>
            </table>
            <a href="inregistrare.aspx" class="ui animated blue button" tabindex="0">
                <div class="visible content">Inregistreaza-te chiar acum</div>
                <div class="hidden content"><i class="right arrow icon"></i></div>
            </a>
        </div>

        <div id="DespreNoi" class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>
        <div class="ui hidden divider"></div>

        <div class="ui top attached blue segment" style="background-color:#f9fafb">
            <h2 class="ui centered blue header">Despre noi
                <div class="sub header"><p>Doriti sa aflati cine a contribuit la crearea acestui site?</p></div>
            </h2>
        </div>
        <div class="ui attached center aligned segment" style="padding:30px;">
            <h3 class="ui header">
                <img class="ui circular image" src="images/author.jpg"/>
                <div class="content">Avasilcai Alexandru
                    <div class="sub header">Realizator proiect <a href="#">EOTO.COM</a></div>
                </div>
            </h3>
            <div class="ui divider"></div>
            <i><i class="left quote icon"></i>Proiectul <a href="#">EOTO.COM</a> mi-a oferit ocazia de a vedea cât de bun sunt, m-a ajutat să încerc să fiu mai bun și să caut idei, să explorez, să nu mă mulțumesc cu puțin. Pentru mine, a fost primul pas care mi-a deschis pofta de a dezvolta proiecte proprii, de a le vedea online, de a avea inițiative și mai ales de a le pune în practică <i class="right quote icon"></i></i>
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
    $('.ui.dropdown')
        .dropdown({
            action: 'hide'
        });
</script>

</body>
</html>
