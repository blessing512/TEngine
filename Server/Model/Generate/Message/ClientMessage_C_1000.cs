using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(ClientMessage.Main2NetClient_Login)]
    [ResponseType(nameof(NetClient2Main_Login))]
    public partial class Main2NetClient_Login : MessageObject, IRequest
    {
        public static Main2NetClient_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Main2NetClient_Login), isFromPool) as Main2NetClient_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int OwnerFiberId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [MemoryPackOrder(2)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MemoryPackOrder(3)]
        public string Password { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.OwnerFiberId = default;
            this.Account = default;
            this.Password = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.NetClient2Main_Login)]
    public partial class NetClient2Main_Login : MessageObject, IResponse
    {
        public static NetClient2Main_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(NetClient2Main_Login), isFromPool) as NetClient2Main_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long PlayerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.PlayerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.HttpGetPassportResponse)]
    public partial class HttpGetPassportResponse : MessageObject, IResponse
    {
        public static HttpGetPassportResponse Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(HttpGetPassportResponse), isFromPool) as HttpGetPassportResponse;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public int code { get; set; }

        [MemoryPackOrder(4)]
        public string authUrl { get; set; }

        [MemoryPackOrder(5)]
        public string iapUrl { get; set; }

        [MemoryPackOrder(6)]
        public int isReview { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.code = default;
            this.authUrl = default;
            this.iapUrl = default;
            this.isReview = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.HttpGetGameUpdateResponse)]
    public partial class HttpGetGameUpdateResponse : MessageObject, IResponse
    {
        public static HttpGetGameUpdateResponse Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(HttpGetGameUpdateResponse), isFromPool) as HttpGetGameUpdateResponse;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public int isGM { get; set; }

        /// <summary>
        /// 游戏更新地址
        /// </summary>
        [MemoryPackOrder(4)]
        public string gameDownloadUrl { get; set; }

        /// <summary>
        /// 资源目录
        /// </summary>
        [MemoryPackOrder(5)]
        public string resFloder { get; set; }

        /// <summary>
        /// 强更版本
        /// </summary>
        [MemoryPackOrder(6)]
        public string ServerAppVersion { get; set; }

        /// <summary>
        /// 资源版本号
        /// </summary>
        [MemoryPackOrder(7)]
        public string resVersion { get; set; }

        /// <summary>
        /// 资源更新地址
        /// </summary>
        [MemoryPackOrder(8)]
        public string resUrl { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.isGM = default;
            this.gameDownloadUrl = default;
            this.resFloder = default;
            this.ServerAppVersion = default;
            this.resVersion = default;
            this.resUrl = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    public static class ClientMessage
    {
        public const ushort Main2NetClient_Login = 1001;
        public const ushort NetClient2Main_Login = 1002;
        public const ushort HttpGetPassportResponse = 1003;
        public const ushort HttpGetGameUpdateResponse = 1004;
    }
}