<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inregistrare.aspx.cs" Inherits="inregistrare" %>

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
    <title>Inregistrare</title>
    
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
                        <a href="inregistrare.aspx" class="ui active green button"><i class="signup icon"></i>Inregistrare</a>
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
                            <a href="inregistrare.aspx" class="ui active green button"><i class="signup icon"></i>Inregistrare</a>
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
            <i class="user icon"></i>
            <div class="content">Inregistrare<div class="sub header" style="color:white">Proces de identificare personală a unui utilizator sau navigator care intenţionează să se inregistreze la un serviciu ordine de pe Internet. Aceasta consta, mai exact, în introducerea cu ajutorul unei tastaturi a unor detalii specifice fiecarui utilizator in cauza.</div></div>
        </h2>
        </div>
        <img class="ui centered image" src="https://i.imgur.com/ZVzbrRL.png" style="position: absolute;bottom: 0px;position: absolute;margin-left: auto;margin-right: auto;left: 0;right: 0;"/>
    </div>

    <!-- PAGE CONTENT -->
    <div class="ui container" style="padding-top:50px;padding-bottom:50px">
        <div id="mesaj_continut_gol" runat="server" class="ui negative icon message" style="margin-bottom:20px;">
            <i class="close icon"></i>
            <i class="delete icon"></i>
            <div class="content">
                <div class="header">Datele introduse sunt incorecte!</div>
                <p>Va rugam sa completati toate campurile inainte sa dati click pe buton. Pentru a vedea erorilor, selectati un camp si apasati tasta "enter".</p>
            </div>
        </div>

        <div id="mesaj_cont_existent" runat="server" class="ui negative icon message" style="margin-bottom:20px;">
            <i class="close icon"></i>
            <i class="delete icon"></i>
            <div class="content">
                <div class="header">Numele de utilizator sau email-ul sunt deja inregistrate!</div>
                <p>Datele introduse exista deja in baza de date. Va rugam sa incercati alte date!</p>
            </div>
        </div>

        <div id="mesaj_cont_creat" runat="server" class="ui success icon message" style="margin-bottom:20px;">
            <i class="close icon"></i>
            <i class="check icon"></i>
            <div class="content">
                <div class="header">Contul dvs. a fost creat cu succes!</div>
                <p>Va multumim pentru inregistrare! Contul dvs. poate fi utilizat la <a href="logare.aspx">logare</a>.</p>
            </div>
        </div>

        <div class="ui two column centered stackable grid">
            <div class="column">
                <div class="ui attached icon message">
                    <i class="user icon"></i>
                    <div class="content">
                        <div class="header">Nu aveti un cont inregistrat?</div>
                        <p>Creati-va unul pentru a avea acces la toate facilitatile site-ului.</p>
                    </div>
                </div>
                <div class="ui form attached fluid segment">
                    <div class="two fields">
                        <div class="field">
                            <label>Nume</label>
                            <div class="ui left icon input">
                                <input id="text_nume" runat="server" type="text" placeholder="Nume"/>
                                <i class="id card icon"></i>
                            </div>
                        </div>
                        <div class="field">
                            <label>Prenume</label>
                            <div class="ui left icon input">
                                <input id="text_prenume" runat="server" type="text" placeholder="Prenume"/>
                                <i class="address card icon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <label>Utilizator</label>
                        <div class="ui left icon input">
                            <input id="text_utilizator" runat="server" type="text" placeholder="Utilizator"/>
                            <i class="user icon"></i>
                        </div>
                    </div>
                    <div class="field">
                        <label>Parola</label>
                        <div class="ui left icon input">
                            <input id="text_parola" runat="server" type="password" placeholder="Parola"/>
                            <i class="lock icon"></i>
                        </div>
                    </div>
                    <div class="field">
                        <label>Email</label>
                        <div class="ui left icon input">
                            <input id="text_email" runat="server" type="text" placeholder="Email"/>
                            <i class="mail icon"></i>
                        </div>
                    </div>
                    <div class="inline field">
                        <div class="ui checkbox">
                            <input id="terms" runat="server" type="checkbox"/>
                            <label for="terms">Sunt de acord cu termenii si conditiile</label>
                        </div>
                    </div>
                    <a id="A2" runat="server" onserverclick="buton_inregistrare_click" class="ui fluid green button">Inregistrare</a>
                    <div class="ui error message"></div>
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
    $('.ui.dropdown')
        .dropdown({
            action: 'hide'
        });
        $('.ui .form')
    .form({
        fields: {
            text_utilizator: 'empty',
            text_parola: ['minLength[6]', 'empty'],
            text_email: 'empty',
            text_nume: 'empty',
            text_prenume: 'empty',
            terms: 'checked'
        }
    });
        $('.message .close')
    .on('click', function () {
        $(this)
            .closest('.message')
            .transition('fade');
    });
</script>

</body>
</html>
