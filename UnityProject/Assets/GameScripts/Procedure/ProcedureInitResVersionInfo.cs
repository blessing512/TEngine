using Launcher;
using System;
using TEngine;
using static TEngine.Constant;
using UnityEngine.Networking;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;
using System.Collections;
using Newtonsoft.Json;
using YooAsset;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Procedure
{

    public class GameResVerInfo
    {
        public string gameDownloadUrl;     // 游戏更新地址
        public string resFloder;         // 资源目录
        public string ServerAppVersion;    // 强更版本
        public string resVersion;          //资源版本号
        public string resUrl;              //资源更新地址
    }

    public class GameUpdateData
    {

        public bool isGM = false;               // 是否白名单
        public GameResVerInfo resInfo;
        public GameResVerInfo gmResInfo;

        public GameResVerInfo GetResVerInfo()
        {
            if (isGM)
            {
                return gmResInfo;
            }
            else
            {
                return resInfo;
            }
        }
    }


    /// <summary>
    /// 流程 => 初始化服务器信息
    /// </summary>
    public class ProcedureInitResVersionInfo : ProcedureBase
    {
        public override bool UseNativeDialog { get; }

        private ProcedureOwner _procedureOwner;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            _procedureOwner = procedureOwner;
            Log.Info("获取服务器信息！");

            LauncherMgr.Show(UIDefine.UILoadUpdate, $"获取服务器信息...");
            InitResVersionInfo().Forget();
        }

        public static string GetHttpUrlWithParams(string strInterFace, Dictionary<string, string> dicParams)
        {
            string url = "";

            string strParams = "";
            int nIndex = 0;

            foreach (var item in dicParams)
            {
                if (nIndex == 0)
                {
                    strParams = item.Key + "=" + item.Value;
                }
                else
                {
                    strParams = strParams + "&" + item.Key + "=" + item.Value;
                }

                nIndex++;
            }


            url = strInterFace + "?" + strParams;

            return ProcedureInitServerInfo.FormatUrl(url);
        }
  

        protected async UniTaskVoid InitResVersionInfo()
        {

            string strInterFace = "gameUpdate";
            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("channelId", UpdateSetting.GetPlatformName());
            dicParams.Add("ver", Settings.UpdateSetting.LocalAppVersion.ToString());
            dicParams.Add("deviceId", Application.identifier);


            var url = string.Format("{0}/{1}", Settings.UpdateSetting.ServerUrl, GetHttpUrlWithParams(strInterFace, dicParams));


            string result = await TEngine.Utility.Http.Get(url);


            if (string.IsNullOrEmpty(result))
            {
                OnHttpError();
            }
            else
            {
                var gameUpdateData = JsonConvert.DeserializeObject<GameUpdateData>(result);

                GameResVerInfo gameResVerInfo;
                if(gameUpdateData.isGM)
                {
                    gameResVerInfo = gameUpdateData.gmResInfo;
        
                }
                else
                {
                    gameResVerInfo = gameUpdateData.resInfo;
                }

                Settings.UpdateSetting.GameDownloadUrl = gameResVerInfo.gameDownloadUrl;
                Settings.UpdateSetting.ResDownLoadPath = gameResVerInfo.resUrl;
                Settings.UpdateSetting.ServerResFloder = gameResVerInfo.resFloder;
                Settings.UpdateSetting.ServerAppVersion = gameResVerInfo.ServerAppVersion;


                if (Convert.ToInt32(gameResVerInfo.ServerAppVersion) > Settings.UpdateSetting.LocalAppVersion)
                {
                    LauncherMgr.ShowMessageBox($"客户端版本过低，请前往应用商店下载最新版本",
                        MessageShowType.TwoButton,
                         LoadStyle.StyleEnum.Style_Default,
                        () => { Application.OpenURL(Settings.UpdateSetting.GameDownloadUrl); },
                        UnityEngine.Application.Quit);

                }
                else
                {
                    ChangeState<ProcedureInitPackage>(_procedureOwner);
                }



            }


        }


        protected void OnHttpError()
        {
            LauncherMgr.ShowMessageBox($"获取资源版本信息失败，请重试",
                 MessageShowType.TwoButton,
                   LoadStyle.StyleEnum.Style_Default,
                () => { ChangeState<ProcedureInitResVersionInfo>(_procedureOwner); }, UnityEngine.Application.Quit);
        }

    }
}