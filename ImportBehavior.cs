using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ImportDemo
{
    public class ImportBehavior
    {
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {

            //var path = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).Path;
            var patientDataPath = @"/sdcard/Download/Patients.db3";
            var connectionString = $"Data Source={patientDataPath}";

            if (!File.Exists(patientDataPath))
            {
                throw new InvalidOperationException($"Database file:{patientDataPath} not exist.");
            }

            var connection = new SQLiteConnection(connectionString);
            using (var command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
    }
}