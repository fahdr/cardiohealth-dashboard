using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace cardiohealthv001.Models
{
    public class DToPAllocation
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string PUserName { get; set; }
        public string DUserName { get; set; }
        public DateTime DateAllocated { get; set; }


    }
}
