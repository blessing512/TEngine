using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

    public  class GamePassport : Entity, IAwake
    {


        [BsonElement]
        public string AuthUrl
        {
            get;
            set;
        }

        [BsonElement]
        public string IapUrl
        {
            get;
            set;
        }

        [BsonElement]
        public string AppVer
        {
            get;
            set;
        }

        [BsonElement]
        public string PlatName
        {
            get;
            set;
        }

        [BsonElement]
        public int IsReview
        {
            get;
            set;
        }

        [BsonElement]
        public int ServerType
        {
            get;
            set;
        }


    }
}