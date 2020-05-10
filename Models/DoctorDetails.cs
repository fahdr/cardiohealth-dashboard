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
    public class DoctorDetails
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string DUserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Practice { get; set; }
        public string Bio { get; set; }
        public string PhoneNumber { get; set; }

    }
}
