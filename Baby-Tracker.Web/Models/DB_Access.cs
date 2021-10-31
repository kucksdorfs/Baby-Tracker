
namespace Baby_Tracker.Web.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.Sqlite;
    using System.Linq;
    using System.Text;

    public class DB_Access
    {
        private const int DEFAULTSTRINGLENGTH = 50;
        protected static SqliteConnectionStringBuilder connBuilder = null;
        static DB_Access()
        {
            connBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = "./BabyTracker.db"

            };
        }

        public static String ConnectionString
        {
            get
            {
                return connBuilder.ConnectionString;
            }
        }
        private static void TestConnection()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private static Dictionary<String, String> DataTypeMapper(Type model)
        {
            var retVal = new Dictionary<String, String>();
            foreach (var field in model.GetFields())
            {
            }
            foreach (var property in model.GetProperties())
            {
                var propType = property.PropertyType;
                if (property.CanRead && property.CanWrite)
                {
                    if (propType == typeof(bool) ||
                        propType == typeof(byte) ||
                        propType == typeof(int) ||
                        propType == typeof(uint) ||
                        propType == typeof(sbyte))
                    {
                        retVal.Add(propType.Name, "INTEGER");
                    }
                    else if (propType == typeof(Double) ||
                             propType == typeof(Single))
                    {
                        retVal.Add(propType.Name, "REAL");
                    }
                    else if (propType == typeof(char) ||
                             propType == typeof(DateTime) ||
                             propType == typeof(string))
                    {
                        retVal.Add(propType.Name, "TEXT");
                    }
                    else
                    {
                        retVal.Add(propType.Name, "BLOB");
                    }
                }
            }
            return retVal;
        }

        private static string CreateBaseTableStatement(Type model, Dictionary<string, string> dataTypes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"create table {model.Name} (");

            sb.Append(string.Join(",", dataTypes.Select(x => $"{x.Key} {x.Value}")));
            sb.Append(");");
            return sb.ToString();
        }


        public static void DBUpgrade(Type model)
        {
            if (model == null)
            {
                return;
            }
            // Sets up the database if not there.
            TestConnection();

            using (var conn = new SqliteConnection(ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName";
                        cmd.Parameters.AddWithValue("@tableName", model.Name);
                        Dictionary<String, String> dataMapper = DataTypeMapper(model);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                using (var createcommand = conn.CreateCommand())
                                {
                                    createcommand.CommandText = CreateBaseTableStatement(model, dataMapper);

                                    createcommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

        }

    }
}
