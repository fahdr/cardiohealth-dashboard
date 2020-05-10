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
        public class PatientWorkout
        {
            public int WorkoutNumber { get; set; }
            public DateTime WorkoutDate { get; set; }
            public string WorkoutType { get; set; }
            public int HeartRate { get; set; }
            public int MaxHeartRate { get; set; }
            public string Comment { get; set; }
            public int SquatNum { get; set; }
            public int JumpNum { get; set; }
            public int WorkoutLength { get; set; } // seconds

        }


        public class PatientDiagnosis
        {
            public int DiagnosisNum { get; set; }
            public string Diagnosis { get; set; }
            public DateTime DiagnosisDate { get; set; }

        }

        public class PatientMessages
        {
            public int MessageNum { get; set; }
            public string Message { get; set; }
            public DateTime MessageDate { get; set; }

        }

        public class PatientDetails
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string PUserName { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string PhoneNumber { get; set; }
            public int FitnessLevel { get; set; }
            public string Condition { get; set; }
            public int WeightKG { get; set; }
            public bool IsMale { get; set; }
            public IList<PatientWorkout> Workouts { get; set; }
            public IList<PatientDiagnosis> Diagnosis { get; set; }
            public IList<PatientMessages> Messages { get; set; }
        }
}


