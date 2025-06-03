using Launcher;
using System;
using TEngine;
using UnityEngine.Networking;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;


namespace Procedure
{
    public class GamePassportData
    {
        public int code = 0;
        public string authUrl;
        public string iapUrl;
        public int isReview = 0;
    }


    /// <summary>
    /// 流程 => 初始化服务器信息
    /// </summary>
    public class ProcedureInitServerInfo : ProcedureBase
    {
        public override bool UseNativeDialog { get; }

        private ProcedureOwner _procedureOwner;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            _procedureOwner = procedureOwner;
            Log.Info("获取服务器信息！");

            LauncherMgr.Show(UIDefine.UILoadUpdate, $"获取服务器信息...");
            GetServerUrl().Forget();
        }


        public static string FormatUrl(string url)
        {
            return System.Uri.EscapeUriString(url);
        }

        public static bool IsHttpRequestError(UnityWebRequest webRequest)
        {
            if (webRequest == null)
            {
                return true;
            }

            if (webRequest.result == UnityWebRequest.Result.ProtocolError ||
                webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                if (string.IsNullOrEmpty(webRequest.error) == false)
                {
                    Log.Error(webRequest.url + ":" + webRequest.error);
                }


                return true;
            }


            return false;
        }
        private async UniTaskVoid GetServerUrl()
        {
            var url = string.Format("{0}:30005/get_passport?serverType={1}&appVer={2}&platName={3}",
            Settings.UpdateSetting.PassportUrl,
            Settings.UpdateSetting.Servertype,
            Settings.UpdateSetting.LocalAppVersion,
            UpdateSetting.GetPlatformName());

            Log.Info("GetPassport====Send:" + url);

            string result = await TEngine.Utility.Http.Get(url);


            if (string.IsNullOrEmpty(result))
            {
                OnGetServerUrlError();
            }
            else
            {
                var passPortData = JsonConvert.DeserializeObject<GamePassportData>(result);
                OnGetServerUrlSccuess(passPortData);
            }
                

        }

        protected void OnGetServerUrlError()
        {
     
            LauncherMgr.ShowMessageBox($"获取服务器信息失败，请重试",
                MessageShowType.TwoButton,
                LoadStyle.StyleEnum.Style_Default,
                () => { ChangeState<ProcedureInitServerInfo>(_procedureOwner); }, UnityEngine.Application.Quit);

  
        }

        protected void OnGetServerUrlSccuess(GamePassportData urlData)
        {

            Settings.UpdateSetting.IsReview = urlData.isReview == 1;
            Settings.UpdateSetting.ServerUrl = urlData.authUrl;

            ChangeState<ProcedureInitResVersionInfo>(_procedureOwner);

      

        }


        //private void Operation_Completed(YooAsset.AsyncOperationBase obj)
        //{
        //    LauncherMgr.Show(UIDefine.UILoadUpdate, $"清理完成 即将进入游戏...");

        //    ChangeState<ProcedureInitPackage>(_procedureOwner);
        //}
    }
}