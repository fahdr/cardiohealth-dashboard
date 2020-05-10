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
    public class DoctorLogin
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string DUserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public DateTime PasswordChangedDate { get; set; }
        public string PasswordVerificationToken { get; set; }
        public DateTime PasswordVerificationTokenExpirationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
