using System.Data.SqlClient;

namespace unity3d.Models
{
    public class DB
    {
        public static SqlConnection GetDB()
        {
            var db =
                new SqlConnection(Properties.Settings.Default.ConnectionString);
            db.Open();
            return db;
        }
    }
}