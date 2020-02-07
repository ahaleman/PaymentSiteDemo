<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="PaymentSite.Payment" %>

<!DOCTYPE html>
<html>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0">
<head>
    <title>HostedPayment Test Page</title>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#add_payment").show();
            $("#send_token").attr({ "action": "https://secure.authorize.net/payment/payment", "target": "add_payment" }).submit();
            $(window).scrollTop($('#add_payment').offset().top - 50);
        });
    </script>
</head>
<body>
    <h1>Parent Page</h1>
    <div id="iframe_holder" class="center-block" style="width: 90%; max-width: 1000px; border-color:red; border-width:thick;">
        <iframe id="add_payment" class="embed-responsive-item panel" name="add_payment" width="100%" frameborder="0" scrolling="no" hidden="true"></iframe>
    </div>
    <form id="send_token" action="" method="post" target="add_payment">
        <input type="hidden" name="token" value="<%=Token%>" />
    </form>

    <script type="text/javascript">
        (function () {
            if (!window.AuthorizeNetIFrame) window.AuthorizeNetIFrame = {};
            AuthorizeNetIFrame.onReceiveCommunication = function (querystr) {
                var params = parseQueryString(querystr);
                switch (params["action"]) {
                    case "cancel":
                        console.log("cancel");
                        console.log(querystr);
                        break;
                    case "resizeWindow":
                        console.log("resizeWindow");
                        console.log(querystr);
                        var w = parseInt(params["width"]);
                        var h = parseInt(params["height"]);
                        var ifrm = document.getElementById("add_payment");
                        ifrm.style.width = w.toString() + "px";
                        ifrm.style.height = h.toString() + "px";
                        break;
                    case "transactResponse":
                        console.log("transactResponse");
                        console.log(querystr);
                        var ifrm = document.getElementById("add_payment");
                        ifrm.style.display = 'none';
                }
            };

            function parseQueryString(str) {
                var vars = [];
                var arr = str.split('&');
                var pair;
                for (var i = 0; i < arr.length; i++) {
                    pair = arr[i].split('=');
                    vars.push(pair[0]);
                    vars[pair[0]] = unescape(pair[1]);
                }
                return vars;
            }
        }());
    </script>
</body>
</html>
