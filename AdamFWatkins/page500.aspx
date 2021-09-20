<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page500.aspx.cs" Inherits="AdamFWatkins.page500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/2015/bootstrap.min.css" rel="stylesheet" media="screen" />
    <script src="https://code.jquery.com/jquery.js"></script>
    <script src="~/scripts/2015/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="container">
                    <!--Main Container-->
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="/">
                            <div style="font-size: 30px;">
                                <asp:Image runat="server" ImageUrl="~/Images/AFW_topLOGO.png" />
                            </div>
                        </a>
                    </div>

                    <!-- Collect the nav links, forms, and other content for toggling -->
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <ul id="mainmenu" class="nav navbar-nav navbar-right">
                            <li><a href="/">Home</a></li>
                            <li><a href="/#books">Books</a></li>
                            <li><a href="/#events">Events</a></li>
                            <li><a href="/#about">About</a></li>
                            <li><a href="/#art">Art</a></li>
                            <li><a href="/#contact">Contact</a></li>

                            <div id="mainmenubar"></div>
                        </ul>
                    </div>
                    <!-- /.navbar-collapse -->
                </div>
                <!-- Container End-->
            </nav>
        </div>

        <div class="row" style="margin-top:100px;">
            <div class="container">
            <h2>Oops. Something happened. We're working on it.</h2>
            <p>
                Click <a href="/">here</a> to go back to the home page. 
            </p>
            </div>
        </div>
    </form>
</body>
</html>
