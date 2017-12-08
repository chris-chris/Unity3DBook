using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IRankDao{
        List<RankUser> TotalRank(int Start, int Limit);
    }

    public class RankDao : IRankDao
    {
        public IDB db {get;}

        public RankDao(IDB db){
            this.db = db;
        }
        
        public List<RankUser> TotalRank(int Start, int Limit){
            
            List<RankUser> list = new List<RankUser>();

            using (MySqlConnection conn = db.GetConnection())
            {   
                conn.Open();
                string query = String.Format(
                    @"
SELECT * FROM (
	SELECT 
        UserID,
        FacebookID,
        FacebookName,
        FacebookPhotoURL,

        Point,
        CreatedAt,
        AccessToken,

        Diamond, 
        Health,
        Defense,
        Damage,
        Speed,

        HealthLevel,
        DefenseLevel,
        DamageLevel,
        SpeedLevel,

        Level,
        Experience,

        Rank() over (order by Point desc) as Rank 
    FROM tb_user
) a WHERE a.Rank >= {0} AND a.Rank <= {0} + {1} - 1;",
                     Start, Limit);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            RankUser user = new RankUser();
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.AccessToken = reader.GetString(6);

                            user.Diamond = reader.GetInt32(7);
                            user.Health = reader.GetInt32(8);
                            user.Defense = reader.GetInt32(9);
                            user.Damage = reader.GetInt32(10);
                            user.Speed = reader.GetInt32(11);
                            user.HealthLevel = reader.GetInt32(12);
                            user.DefenseLevel = reader.GetInt32(13);
                            user.DamageLevel = reader.GetInt32(14);
                            user.SpeedLevel = reader.GetInt32(15);
                            user.Level = reader.GetInt32(16);
                            user.Experience = reader.GetInt32(17);
                            user.Rank = reader.GetInt32(18);

                            list.Add(user);
                            
                        }
                    }
                }
                
            }
            return list;
        }


        public List<RankUser> FriendRank(List<string> facebookIDList){
            
            List<string> newFacebookIDList = new List<string>();

            foreach(string FacebookID in facebookIDList){
                newFacebookIDList.Add("'" + FacebookID + "'");
            }

            string facebookIDStr = string.Join(",", newFacebookIDList);

            List<RankUser> list = new List<RankUser>();

            using (MySqlConnection conn = db.GetConnection())
            {   
                conn.Open();
                string query = String.Format(
                    @"
	SELECT 
        UserID,
        FacebookID,
        FacebookName,
        FacebookPhotoURL,

        Point,
        CreatedAt,
        AccessToken,

        Diamond, 
        Health,
        Defense,
        Damage,
        Speed,

        HealthLevel,
        DefenseLevel,
        DamageLevel,
        SpeedLevel,

        Level,
        Experience,

        Rank() over (order by Point desc) as Rank 
    FROM tb_user
    WHERE FacebookID IN (
        {0}
        )
    ",
                     facebookIDStr);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            RankUser user = new RankUser();
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.AccessToken = reader.GetString(6);

                            user.Diamond = reader.GetInt32(7);
                            user.Health = reader.GetInt32(8);
                            user.Defense = reader.GetInt32(9);
                            user.Damage = reader.GetInt32(10);
                            user.Speed = reader.GetInt32(11);
                            user.HealthLevel = reader.GetInt32(12);
                            user.DefenseLevel = reader.GetInt32(13);
                            user.DamageLevel = reader.GetInt32(14);
                            user.SpeedLevel = reader.GetInt32(15);
                            user.Level = reader.GetInt32(16);
                            user.Experience = reader.GetInt32(17);
                            user.Rank = reader.GetInt32(18);

                            list.Add(user);
                            
                        }
                    }
                }
                
            }
            return list;
        }


    }
}