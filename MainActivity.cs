using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Android.App;
using Android.Widget;
using Android.OS;

namespace ImportDemo
{
    [Activity(Label = "ImportDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private readonly ImportBehavior _import = new ImportBehavior();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //New code
            Button importButton = FindViewById<Button>(Resource.Id.importButton);
            TextView importTextView = FindViewById<TextView>(Resource.Id.ImportTextView);

            importButton.Click += (sender, args) =>
            {
                var result = ImportData();

                //将信息显示到textView中，用某种形式。




                importTextView.Text = "Import successfully";
            };
        }

        public List<PatientProfile> ImportData()
        {
            var patients = new List<PatientProfile>();
            var sqlString = "select PKey, PatientId, FamilyName, MiddleName, FirstName from PatientCache ORDER BY LastDate DESC";
            var parameter = new List<SQLiteParameter>().ToArray();

            using (var reader = _import.ExecuteReader(sqlString, parameter))
            {
                while (reader.Read())
                {
                    var text = CreatePatientProfile(reader);
                    var patient = CreateProfile(text.Pkey, text.PatientId, text.FirstName,
                        text.MiddleName, text.FamilyName);
                    patients.Add(patient);
                }
            }

            return patients;
        }

        private static ProfileText CreatePatientProfile(SQLiteDataReader reader)
        {
            var patient = new ProfileText
            {
                Pkey = reader.GetGuid(0),
                PatientId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                FamilyName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                FirstName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
            };

            return patient;
        }

        public struct ProfileText
        {
            public Guid Pkey { get; set; }
            public string PatientId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string FamilyName { get; set; }
        }

        public static PatientProfile CreateProfile(Guid key, string id, string firstName, string middleName,
            string familyName) 
        {
            return new PatientProfile(key)
            {
                PatientId = id,
                FirstName = firstName,
                MiddleName = middleName,
                FamilyName = familyName
            };
        }
    }
}

