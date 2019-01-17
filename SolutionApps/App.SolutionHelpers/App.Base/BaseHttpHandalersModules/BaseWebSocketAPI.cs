namespace App.Base
{
    namespace BaseWebSocket
    {
        using System;
        using System.Web;
        using System.Net;
        using System.Web.WebSockets;
        using System.Net.WebSockets;
        using System.Text;
        using System.Threading;
        using System.Threading.Tasks;
        using System.Web.Http;
        using System.Net.Http;


        #region BaseWebSocketAPI Block

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL            
        /// Company Name:   RMSI            
        /// Created Date:   Developed on: 07/10/2015      
        /// Summary     :   BaseProxy for ArcGIS Server Security Services.
        ///*************************************************
        /// </summary>

        /// <summary>
        /// Summary description for BaseWebSocketAPI
        /// </summary>
        public class BaseWebSocketAPI : ApiController
        {
            public HttpResponseMessage Get()
            {
                if (HttpContext.Current.IsWebSocketRequest)
                {
                    HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
                }
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            private async Task ProcessWSChat(AspNetWebSocketContext context)
            {
                WebSocket socket = context.WebSocket;
                while (true)
                {
                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                    if (socket.State == WebSocketState.Open)
                    {
                        string userMessage = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        userMessage = "You sent: " + userMessage + " at " + DateTime.Now.ToLongTimeString();
                        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMessage));
                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            /// <summary>
        }

        #endregion BaseWebSocketAPI Block


        #region Integration BaseWebSocketAPI Block

        //        @{
        //    Layout = null;
        //}
        //<!DOCTYPE html>
        //<html>
        //<head>
        //    <meta name="viewport" content="width=device-width" />
        //    <title>Index</title>
        //    <script type="text/javascript" src="Scripts/jquery-2.0.2.js"></script>
        //    <script type="text/javascript">
        //        var ws;
        //        $().ready(function () {
        //            $("#btnConnect").click(function () {
        //                $("#spanStatus").text("connecting");
        //                ws = new WebSocket("ws://" + window.location.hostname + 
        //                    "/Mvc4WSChat/api/WSChat");
        //                ws.onopen = function () {
        //                    $("#spanStatus").text("connected");
        //                };
        //                ws.onmessage = function (evt) {
        //                    $("#spanStatus").text(evt.data);
        //                };
        //                ws.onerror = function (evt) {
        //                    $("#spanStatus").text(evt.message);
        //                };
        //                ws.onclose = function () {
        //                    $("#spanStatus").text("disconnected");
        //                };
        //            });
        //            $("#btnSend").click(function () {
        //                if (ws.readyState == WebSocket.OPEN) {
        //                    ws.send($("#textInput").val());
        //                }
        //                else {
        //                    $("#spanStatus").text("Connection is closed");
        //                }
        //            });
        //            $("#btnDisconnect").click(function () {
        //                ws.close();
        //            });
        //        });
        //    </script>
        //</head>
        //<body>
        //    <input type="button" value="Connect" id="btnConnect" />
        //    <input type="button" value="Disconnect" id="btnDisconnect" /><br />
        //    <input type="text" id="textInput" />
        //    <input type="button" value="Send" id="btnSend" /><br />
        //    <span id="spanStatus">(display)</span>
        //</body>
        //</html>
        #endregion Integration BaseWebSocketAPI Block

    }
}