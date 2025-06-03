using System.Diagnostics;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{

    public partial class GamePassport : Entity, IAwake<int>
    {

        [BsonElement]
        private string authUrl;

        [BsonElement]
        private string iapUrl;

        [BsonElement]
        private string appVer;

        [BsonElement]
        private string platName;

        [BsonIgnore]
        public string AuthUrl
        {
            get => this.authUrl;
            set
            {
                this.authUrl = value;
            }
        }

        [BsonIgnore]
        public string IapUrl
        {
            get => this.iapUrl;
            set
            {
                this.iapUrl = value;
            }
        }




    }
}