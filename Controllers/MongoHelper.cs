using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace cardiohealthv001
{
    public class MongoHelper<T> where T : class
    {
        public MongoCollection<T> Collection { get; private set; }

        private MongoClient client;
        private MongoServer server;
        private MongoDatabase provider;
        private MongoUrl mongoUrl;
        // private MongoCredential.CreateCredential credential;
        //private MongoClientSettings settings;

        //string _connectionString;


        public MongoHelper()

        {
            /*
            credential = new MongoCredential.CreateCredential
                            ("PatientDatabaseDemo",
                             "demouser",
                             "123");
            settings = new MongoClientSettings
           {
                Credentials = new[] { credential },
                Server = new MongoServerAddress("localhost", Convert.ToInt32("27017"))
            };
            */
            var client = new MongoClient("mongodb+srv://ch:cardio123@cardiohealth-h6cio.azure.mongodb.net/test?retryWrites=true");

            //MongoClient client = new MongoClient(new MongoUrl("mongodb://localhost:27017"));



            // client = new MongoClient(mongoUrl);           
            server = client.GetServer();
            server.Connect();


            //provider = server.GetDatabase("PatientDatabaseDemo");
            provider = server.GetDatabase("cardiohealth");
            //MongoDatabase db = server.GetDatabase("MongoLab-f");

            // provider = server.GetDatabase(mongoUrl.DatabaseName, WriteConcern.Acknowledged);

            Collection = provider.GetCollection<T>(typeof(T).Name.ToLower());
          
        }


        public MongoHelper(string connectionString)
        {

            mongoUrl = new MongoUrl(connectionString);
            client = new MongoClient(mongoUrl);
            server = client.GetServer();
            server.Connect();
            MongoDatabase db = server.GetDatabase("PatientDatabaseDemo");

            //provider = server.GetDatabase(mongoUrl.DatabaseName, WriteConcern.Acknowledged);
            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }

    }


}
