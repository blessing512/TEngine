using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TEngine
{
    /// <summary>
    /// 强制更新类型。
    /// </summary>
    public enum UpdateStyle
    {
        /// <summary>
        /// 强制更新(不更新无法进入游戏。)
        /// </summary>
        Force = 1,

        /// <summary>
        /// 非强制(不更新可以进入游戏。)
        /// </summary>
        Optional = 2,
    }

    /// <summary>
    /// 是否提示更新。
    /// </summary>
    public enum UpdateNotice
    {
        /// <summary>
        /// 更新存在提示。
        /// </summary>
        Notice = 1,

        /// <summary>
        /// 更新非提示。
        /// </summary>
        NoNotice = 2,
    }


    public enum ServerType
    {
        Online = 1,
        Test = 2,
        Review = 3,
    }


    [CreateAssetMenu(menuName = "TEngine/UpdateSetting", fileName = "UpdateSetting")]
    public class UpdateSetting : ScriptableObject
    {
        /// <summary>
        /// 项目名称。
        /// </summary>
        [SerializeField]
        private string projectName = "Demo";

        public bool Enable
        {
            get
            {
#if ENABLE_HYBRIDCLR
                return true;
#else
                return false;
#endif
            }
        }

        [Header("Auto sync with [HybridCLRGlobalSettings]")]
        public List<string> HotUpdateAssemblies = new List<string>() {"GameProto.dll", "GameLogic.dll" };

        [Header("Need manual setting!")]
        public List<string> AOTMetaAssemblies = new List<string>() { "mscorlib.dll", "System.dll", "System.Core.dll", "TEngine.Runtime.dll" ,"UniTask.dll", "YooAsset.dll"};

        /// <summary>
        /// Dll of main business logic assembly
        /// </summary>
        public string LogicMainDllName = "GameLogic.dll";

        /// <summary>
        /// 程序集文本资产打包Asset后缀名
        /// </summary>
        public string AssemblyTextAssetExtension = ".bytes";

        /// <summary>
        /// 程序集文本资产资源目录
        /// </summary>
        public string AssemblyTextAssetPath = "AssetRaw/DLL";

        [Header("更新设置")]
        public UpdateStyle UpdateStyle = UpdateStyle.Force;

        public UpdateNotice UpdateNotice = UpdateNotice.Notice;

        [SerializeField]
        /// <summary>
        /// 服务器类型，正式服，先行服，审核服
        /// </summary>
        public ServerType Servertype = ServerType.Online;

        /// <summary>
        /// 服务器的基础地址
        /// 会根据Servertype,LocalAppVersion,还有PlatformName，deviceId,等参数
        /// 获取真正的服务器地址方便同一个客户端切换连接不同的服务器
        /// </summary>
        [SerializeField]
        public string PassportUrl = "1";


        /// <summary>
        /// 连接服务器的真正地址
        /// </summary>
        public string ServerUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 游戏强更下载地址
        /// </summary>
        public string GameDownloadUrl
        {
            get;
            set;
        }

        
        /// <summary>
        /// 本地客户端版本号。
        /// </summary>
        [SerializeField]
        public int LocalAppVersion = 1;

        /// <summary>
        /// 服务器客户端版本号。
        /// </summary>
        public string ServerAppVersion
        {
            get;
            set;
        }


        /// <summary>
        /// 服务器资源版本号
        /// </summary>
        public string ServerResVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器更新地址
        /// </summary>
        public string ResDownLoadPath
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器资源目录
        /// </summary>
        public string ServerResFloder
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是白名单
        /// </summary>
        public bool IsWhitelist
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是白名单
        /// </summary>
        public bool IsReview
        {
            get;
            set;
        }



        /// <summary>
        /// 获取资源下载路径。
        /// </summary>
        public string GetResDownLoadPath()
        {
            return Path.Combine(ResDownLoadPath, projectName, GetPlatformName(),ServerResFloder).Replace("\\", "/");
        }

        public string GetFallbackResDownLoadPath()
        {
            return Path.Combine(ResDownLoadPath, projectName, GetPlatformName(), ServerResFloder).Replace("\\", "/");
        }


  


        /// <summary>
        /// 获取当前的平台名称。
        /// </summary>
        /// <returns>平台名称。</returns>
        public static string GetPlatformName()
        {
#if UNITY_ANDROID
        return "Android";
#elif UNITY_IOS
        return "IOS";
#elif UNITY_WEBGL
        return "WebGL";
#else
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    return "Windows64";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows64";

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "MacOS";

                case RuntimePlatform.IPhonePlayer:
                    return "IOS";

                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";

                case RuntimePlatform.PS5:
                    return "PS5";
                default:
                    throw new NotSupportedException($"Platform '{Application.platform.ToString()}' is not supported.");
            }
#endif
        }
    }
}