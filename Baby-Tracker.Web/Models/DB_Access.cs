
namespace Baby_Tracker.Web.Models
{
    using System;

    using Microsoft.Data.Sqlite;

    public class DB_Access
    {
        private static void TestConnection()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            //Use DB in project directory.  If it does not exist, create it:
            connectionStringBuilder.DataSource = "./SqliteDB.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
            }
        }
        public static void DBUpgrade(Type model) 
        {
            TestConnection();
        }

    }
}
