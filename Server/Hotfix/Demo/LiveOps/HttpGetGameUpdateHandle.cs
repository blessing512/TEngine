using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ET.Server
{
    [HttpHandler(SceneType.LiveOps, "/get_game_update")]
    public class HttpGetGameUpdateHandle : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            HttpGetGameUpdateResponse response = HttpGetGameUpdateResponse.Create();

            response.resUrl = "http://127.0.0.1:30005";
            response.resVersion = "1";
            response.isGM = 0;
            response.ServerAppVersion = "1";
            response.resFloder = "App_V_1";
            HttpHelper.Response(context, response);
            await ETTask.CompletedTask;
        }
    }
}