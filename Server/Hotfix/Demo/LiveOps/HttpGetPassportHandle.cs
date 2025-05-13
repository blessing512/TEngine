using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ET.Server
{
    [HttpHandler(SceneType.LiveOps, "/get_passport")]
    public class HttpGetPassportHandler : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            HttpGetPassportResponse response = HttpGetPassportResponse.Create();

            response.authUrl = "http://127.0.0.1:30005";
            response.isReview = 0;
            response.iapUrl = "http://127.0.0.1:30005";
            HttpHelper.Response(context, response);
            await ETTask.CompletedTask;
        }
    }
}