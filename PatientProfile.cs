using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImportDemo
{
    public class PatientProfile
    {
        private string _patientFolderName;
        private string _patientId;
        private string _familyName;
        private string _firstName;
        private string _middleName;
        private bool _isDefault;

        protected string PatientFolderFullPath { get; set; }
        protected long FolderSize { get; set; }

        internal bool IsDirty { get; set; }

        public Guid Pkey { get; private set; }

        public string PatientId
        {
            get { return _patientId; }
            set
            {
                string input = TrimInput(value);
                if (_patientId != input)
                {
                    IsDirty = true;
                    _patientId = input;
                   
                }
            }
        }

        public bool IsDefault
        {
            get { return _isDefault; }
            internal set
            {
                if (_isDefault != value)
                {
                    _isDefault = value;
                    
                }
            }
        }

        public string FamilyName
        {
            get { return _familyName; }
            set
            {
                string input = TrimInput(value);
                if (_familyName != input)
                {
                    IsDirty = true;
                    _familyName = input;
                    
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                string input = TrimInput(value);
                if (_firstName != input)
                {
                    IsDirty = true;
                    _firstName = input;
                    
                }
            }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                string input = TrimInput(value);
                if (_middleName != input)
                {
                    IsDirty = true;
                    _middleName = input;
                }
            }
        }

        public bool IsDemoPatient { get; private set; }

        public PatientProfile(Guid key, string fullPath = null)
        {
            Pkey = key;
            _patientId = string.Empty;
            _familyName = string.Empty;
            _firstName = string.Empty;
            _middleName = string.Empty;
            IsDirty = true;
            PatientFolderFullPath = fullPath;
        }

        public static string TrimInput(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return input.Trim();
        }
    }
}