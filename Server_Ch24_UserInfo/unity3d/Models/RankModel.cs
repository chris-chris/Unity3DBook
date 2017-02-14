using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace unity3d.Models
{
    public class RankModel
    {
        // 데이터베이스에서 EXEC USP_RankTotal을 호출하고 결과를 정리해서 콘트롤러에 넘겨줍니다.
        public static RankResult Total(int Start, int Count)
        {
            // 데이터베이스 커넥션을 가져옵니다.
            using (var db = DB.GetDB())
            {
                // 반환할 결과 객체를 생성해 놓습니다.
                RankResult result = new RankResult();

                try
                {
                    // 스토어드 프로시저를 실행시키기 위해 명령을 준비합니다.
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.CommandText = @"USP_RankTotal";
                    command.CommandType = CommandType.StoredProcedure;

                    // 스토어드 프로시저에 전달할 파라미터를 정합니다.
                    command.Parameters.Add("@Start", SqlDbType.Int);
                    command.Parameters["@Start"].Value = Start;
                    command.Parameters.Add("@Count", SqlDbType.Int);
                    command.Parameters["@Count"].Value = Count;
                    
                    List<RankData> list = new List<RankData>();

                    // 스토어드 프로시저를 실행시킵니다.
                    var reader = command.ExecuteReader();

                    //스토어드 프로시저로부터 반환된 결과를 정리합니다.
                    while (reader.Read())
                    {
                        RankData data = new RankData();
                        data.UserID = (Int64)reader["UserID"];
                        data.FacebookID = reader["FacebookID"].ToString(); // check
                        data.FacebookName = reader["FacebookName"].ToString();
                        data.FacebookPhotoURL = reader["FacebookPhotoURL"].ToString();
                        data.Point = (int)reader["Point"];
                        data.Rank = (Int64)reader["Rank"];

                        list.Add(data);
                    }
                    reader.Close();
                    
                    result.Data = list;
                    
                    return result;
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    throw ex;
                }
            }
        }

        public static RankResult Friend(Int64 UserID, List<string> FriendList)
        {
            using (var db = DB.GetDB())
            {
                RankResult result = new RankResult();

                try
                {
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.CommandText = @"USP_FriendRank";
                    command.CommandType = CommandType.StoredProcedure;

                    FriendList.Add(UserID.ToString());

                    for (int i = 0; i < FriendList.Count; i++)
                    {
                        FriendList[i] = "'" + FriendList[i] + "'";
                    }
                    string FriendListStr = string.Join(",", FriendList.ToArray());

                    command.Parameters.Add("@List", SqlDbType.VarChar);
                    command.Parameters["@List"].Value = FriendListStr;
                    

                    List<RankData> list = new List<RankData>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RankData data = new RankData();
                        data.UserID = (Int64)reader["UserID"];
                        data.FacebookID = reader["FacebookID"].ToString();
                        data.FacebookName = reader["FacebookName"].ToString();
                        data.FacebookPhotoURL = reader["FacebookPhotoURL"].ToString();
                        data.Point = (int)reader["Point"];
                        data.Rank = (Int64)reader["Rank"];

                        list.Add(data);
                    }
                    reader.Close();
                    
                    result.Data = list;

                    return result;
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    throw ex;
                }
            }
        }
    }
}