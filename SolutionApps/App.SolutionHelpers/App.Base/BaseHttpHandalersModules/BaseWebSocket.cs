namespace App.Base
{
    namespace BaseWebSocket
    {
        using System;
        using System.Web;
        using System.Web.WebSockets;
        using System.Net.WebSockets;
        using System.Text;
        using System.Threading;
        using System.Threading.Tasks;


        #region BaseWebSocket Block

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL            
        /// Company Name:   RMSI            
        /// Created Date:   Developed on: 07/10/2015      
        /// Summary     :   BaseProxy for ArcGIS Server Security Services.
        ///*************************************************
        /// </summary>

        /// <summary>
        /// Summary description for BaseWebSocket
        /// </summary>
        public class BaseWebSocket : IHttpHandler, IDisposable
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void ProcessRequest(HttpContext context)
            {
                try
                {
                    HttpResponse response = context.Response;
                    if (context.IsWebSocketRequest)
                    {
                        context.AcceptWebSocketRequest(ProcessWebSocketCommunication);
                    }

                }
                catch (Exception ex)
                {
                    context.Response.Write("Hello and Welcome Web BaseWebSocket. This is Not Vailid Request Action.");
                    context.Response.Write("<p>Your Browser:</p>");
                    context.Response.Write("Type: " + context.Request.Browser.Type + "<br>");
                    context.Response.Write("Version: " + context.Request.Browser.Version);
                }
            }
            private async Task ProcessWebSocketCommunication(AspNetWebSocketContext context)
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
            /// 
            /// </summary>
            public bool IsReusable
            {
                get
                {
                    return false;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion BaseWebSocket Block


        #region Integration BaseWebSocket Block

        //   Web.Config
        //        <system.webServer>
        //    <handlers>
        //      <add path="/WSChat/WSHandler.ashx" verb="*" name="WSHandler"
        //           type="WSChat.WSHandler"/>
        //    </handlers>
        //</system.webServer>

        //        HTML

        //        <!DOCTYPE html>
        //<html xmlns="http://www.w3.org/1999/xhtml">
        //<head>
        //    <title>WebSocket Chat</title>
        //    <script type="text/javascript" src="Scripts/jquery-2.0.2.js"></script>
        //    <script type="text/javascript">
        //        var ws;
        //        $().ready(function () {
        //            $("#btnConnect").click(function () {
        //                $("#spanStatus").text("connecting");
        //                ws = new WebSocket("ws://" + window.location.hostname +
        //                    "/WSChat/WSHandler.ashx");
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

        #endregion Integration BaseWebSocket Block

    }
}