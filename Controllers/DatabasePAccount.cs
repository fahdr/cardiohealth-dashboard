using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardiohealthv001.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
namespace cardiohealthv001
{
    public class DatabasePAccount
    {
        private readonly MongoHelper<PatientLogin> patientlogin;
        private string ConnectString;
        private List<string> usernames;

        public DatabasePAccount()
        {
            patientlogin = new MongoHelper<PatientLogin>();

        }


        public DatabasePAccount(string connectionString)
        {
            ConnectString = connectionString;
            patientlogin = new MongoHelper<PatientLogin>();

        }

        public void CreateLogin(PatientLogin logins)
        {
            if (!CheckUserName(logins.PUserName))
            {
                patientlogin.Collection.Save(logins);
            }

        }

        public void CreateFullAccount(PatientLogin logins, PatientDetails details)
        {

            if ((!CheckUserName(logins.PUserName)) && (logins.PUserName.Equals(details.PUserName)))
            {

                patientlogin.Collection.Save(logins);
                DatabasePDetails savedetails = new DatabasePDetails();
                savedetails.Create(details);

            }
            else
            {
                //Error
            }
        }

        private List<string> GetAllUsernames()
        {
            IList<PatientLogin> templist = patientlogin.Collection.FindAll().ToList();

            List<string> usernames = new List<string>();

            foreach (PatientLogin temp in templist)
            {
                usernames.Add(temp.PUserName);

            }
            return usernames;
        }

        public int PatientCount()
        {
            usernames = GetAllUsernames();
            return usernames.Count;
        }


        public bool CheckUserName(string username)
        {
            List<string> userlist = GetAllUsernames();

            if (userlist.Contains(username))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Delete(string username)
        {
            if (CheckUserName(username))
            {
                patientlogin.Collection.Remove(Query.EQ("PUserName", username));
            }
        }
    }
}

