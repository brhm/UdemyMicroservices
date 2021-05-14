using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Models
{
    public class Course
    {

        [BsonId]// mongo db tarafındaki key
        [BsonRepresentation(BsonType.ObjectId)] // mongo db tarafında type belirliyoruz.
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }

        public Feature Feature { get; set; } // feater 1-1 ilişki

        [BsonRepresentation(BsonType.ObjectId)] // category ile 1-n ilişki
        public string CategoryId { get; set; }

        [BsonIgnore]// bu caregory alanı mongo db ya yansımayacak. sadece uygulama içinde kullanacağız.
        public Category Category { get; set; }
    }
}
