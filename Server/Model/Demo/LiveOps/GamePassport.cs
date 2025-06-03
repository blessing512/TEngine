using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

    public partial class GamePassport : Entity, IAwake
    {

        [BsonElement]
        private string authUrl;

        [BsonElement]
        private string iapUrl;

        [BsonElement]
        private string appVer;

        [BsonElement]
        private string platName;

        [BsonElement]
        private int isReview;

        [BsonElement]
        private int serverType;

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

        [BsonIgnore]
        public string AppVer
        {
            get => this.appVer;
            set
            {
                this.appVer = value;
            }
        }

        [BsonIgnore]
        public string PlatName
        {
            get => this.platName;
            set
            {
                this.platName = value;
            }
        }

        [BsonIgnore]
        public int IsReview
        {
            get => this.isReview;
            set
            {
                this.isReview = value;
            }
        }

        [BsonIgnore]
        public int ServerType
        {
            get => this.serverType;
            set
            {
                this.serverType = value;
            }
        }


    }
}