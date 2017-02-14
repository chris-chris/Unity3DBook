using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace unity3d.Models
{

    namespace unity3d.Models
    {
        public class UserModel
        {
            // 데이터베이스에서 EXEC USP_User_Login 을 호출하고 결과를 정리해서 콘트롤러에 넘겨줌
            public static UserResult Select(Int64 UserID)
            {
                // 데이터베이스 커넥션을 가져옴
                using (var db = DB.GetDB())
                {

                    // 반환할 결과 객체를 생성해 놓습니다.
                    UserResult result = new UserResult();

                    try
                    {
                        // 스토어드 프로시져를 실행시키기 위해 명령을 준비합니다.
                        var command = new SqlCommand();
                        command.Connection = db;
                        command.CommandText = @"USP_User_Select";
                        command.CommandType = CommandType.StoredProcedure;

                        // 스토어드 프로시져에 전달할 파라미터를 정합니다.
                        command.Parameters.Add("@UserID", UserID);

                        command.Parameters.Add("@ResultCode", SqlDbType.Int);
                        command.Parameters["@ResultCode"].Direction = ParameterDirection.Output;
                        command.Parameters.Add("@Message", SqlDbType.VarChar, 300);
                        command.Parameters["@Message"].Direction = ParameterDirection.Output;

                        // 스토어드 프로시져를 실행시킵니다.
                        var reader = command.ExecuteReader();

                        UserData data = new UserData();

                        // 스토어드 프로시져로부터 반환된 결과를 정리합니다.
                        if (reader.Read())
                        {

                            data.UserID = (Int64)reader["UserID"];
                            data.FacebookID = reader["FacebookID"].ToString();
                            data.FacebookName = reader["FacebookName"].ToString();
                            data.FacebookPhotoURL = reader["FacebookPhotoURL"].ToString();
                            data.Point = (int)reader["Point"];
                            data.Diamond = (int)reader["Diamond"];

                            data.Level = (int)reader["Level"];
                            data.Experience = (int)reader["Experience"];
                            data.ExpAfterLastLevel = (int)reader["ExpAfterLastLevel"];
                            data.ExpForNextLevel = (int)reader["ExpForNextLevel"];

                            data.Damage = (int)reader["Damage"];
                            data.Health = (int)reader["Health"];
                            data.Speed = (int)reader["Speed"];
                            data.Defense = (int)reader["Defense"];

                            data.DamageLevel = (int)reader["DamageLevel"];
                            data.HealthLevel = (int)reader["HealthLevel"];
                            data.SpeedLevel = (int)reader["SpeedLevel"];
                            data.DefenseLevel = (int)reader["DefenseLevel"];

                        }

                        result.Data = data;

                        reader.Close();
                        
                        // 스토어드 프로시저가 잘 실행됐는지 확인할 수 있는 ResultCode와 Message를 반환 데이터에 명시합니다.
                        result.ResultCode = (int)command.Parameters["@ResultCode"].Value;
                        result.Message = command.Parameters["@Message"].Value.ToString();


                        return result;
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }
    }
}