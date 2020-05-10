using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace cardiohealthv001.Models
{
    public class PatientLogin
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string PUserName { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
    }
}