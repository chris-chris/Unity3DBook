using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace unity3d.Models
{
    public class StageModel
    {
        public static StageResult UpdateRecord(StageData data)
        {
            StageResult result = new StageResult();
            using (var db = DB.GetDB())
            {

                try
                {
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.CommandText = @"USP_StageUpdateRecord";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserID", data.UserID);
                    command.Parameters.Add("@Point", data.Point);


                    List<RankData> list = new List<RankData>();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result.IsLevelUp = (int)reader["IsLevelUp"];
                        result.LevelBeforeGame = (int)reader["LevelBeforeGame"];
                        result.LevelAfterGame = (int)reader["LevelAfterGame"];
                        result.ExpBeforeGame = (int)reader["ExpBeforeGame"];
                        result.ExpAfterGame = (int)reader["ExpAfterGame"];
                        
                    }
                    reader.Close();

                    result.Message = "OK";
                    result.ResultCode = 1;

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