using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace task_api.Entities
{
    public class TaskDetail
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NomeTask")]
        public string NomeTask { get; set; }

        [BsonElement("DescricaoTask")]
        public string DescricaoTask { get; set; }

        [BsonElement("StatusTask")]
        public string StatusTask { get; set; }

    }
}
