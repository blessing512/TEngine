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
            var data = context.Request.QueryString;
            if (data["platName"] == null || data["appVer"] == null || data["serverType"] == null)
            {
                ResponseError(context,response);
                await ETTask.CompletedTask;
                return;
            }


            var passPorts = await scene.GetComponent<DBManagerComponent>().GetZoneDB(ZoneType.LiveOps).Query<GamePassport>(e => e.PlatName == data["platName"] && e.ServerType == Convert.ToInt32(data["serverType"]));
          
            if(passPorts != null && passPorts.Count > 0)
            {
                foreach (var passport in passPorts)
                {
                    if( passport.AppVer == data["appVer"])
                    {
                        response.code = 1;
                        response.authUrl = passport.AuthUrl;
                        response.isReview = passport.IsReview;
                        response.iapUrl = passport.IapUrl;
                        //response.authUrl = "http://127.0.0.1:30005";
                        //response.isReview = 0;
                        //response.iapUrl = "http://127.0.0.1:30005";
                        break;
                    }
                }


            }
   

            HttpHelper.Response(context, response);
            await ETTask.CompletedTask;
        }

        private void ResponseError(HttpListenerContext context, HttpGetPassportResponse response)
        {
            response.code = 0;
            response.Message = "参数错误";
            HttpHelper.Response(context, response);
        }
    }
}