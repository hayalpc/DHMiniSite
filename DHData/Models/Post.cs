using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHData.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Active { get; set; }

        public virtual string FullName { get { return UserName + " " + UserSurname; } }
    }
}
