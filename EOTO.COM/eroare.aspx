<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eroare.aspx.cs" Inherits="eroare" %>

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
    <title>Eroare</title>
    
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
            <i class="low vision icon"></i>
            <div class="content">Eroare<div class="sub header" style="color:white">Când un utilizator solicită o pagină care nu există, un server va returna o eroare 404 (pagina nu a fost găsită). Acest cod de răspuns HTTP transmite în mod clar atât browserelor, cât şi motoarelor de căutare faptul că pagina nu există. Ca urmare, conţinutul paginii (dacă există) nu poate fi accesat.</div></div>
        </h2>
        </div>
        <img class="ui centered image" src="https://i.imgur.com/sLm3iEU.png" style="position: absolute;bottom: 0px;position: absolute;margin-left: auto;margin-right: auto;left: 0;right: 0;"/>
    </div>

    <!-- PAGE CONTENT -->
    <div style="padding-top:5%;padding-bottom:5%;padding-left:40%;padding-right:40%">
        <h1 class="ui icon header">
            <i class="ban red icon"></i>
            <div class="content">
                 Eroare 404<div class="sub header">Ne cerem scuze! Pagina pe care o cautati a fost mutata sau nu exista. Daca doriti, puteti sa reveniti la <a href="acasa.aspx">prima pagina</a> a website-ului.</div>
            </div>
        </h1>
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
