using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace unity3d.Models
{
    public class UpgradeModel
    {

        // 데이터베이스에서 EXEC USP_RankTotal을 호출하고 결과를 정리해서 콘트롤러에 넘겨줍니다.
        public static UpgradeResult Info()
        {
            // 데이터베이스 커넥션을 가져옵니다.
            using (var db = DB.GetDB())
            {

                // 반환할 결과 객체를 생성해 놓습니다.
                UpgradeResult result = new UpgradeResult();

                try
                {
                    // 스토어드 프로시저를 실행시키기 위해 명령을 준비합니다.
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.CommandText = @"USP_UpgradeInfo_Select";
                    command.CommandType = CommandType.StoredProcedure;

                    List<UpgradeData> list = new List<UpgradeData>();

                    // 스토어드 프로시저를 실행시킵니다.
                    var reader = command.ExecuteReader();

                    //스토어드 프로시저로부터 반환된 결과를 정리합니다.
                    while (reader.Read())
                    {
                        UpgradeData data = new UpgradeData();
                        data.UpgradeType = reader["UpgradeType"].ToString();
                        data.UpgradeLevel = (int)reader["UpgradeLevel"];
                        data.UpgradeAmount = (int)reader["UpgradeAmount"];
                        data.UpgradeCost = (int)reader["UpgradeCost"];

                        list.Add(data);
                    }
                    reader.Close();

                    // 스토어드 프로시저가 잘 실행됐는지 확인할 수 있는 ResultCode와 Message를 반환 데이터에 명시합니다.
                    result.ResultCode = 0;
                    result.Message = "OK";

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
        public static ResultBase Execute(Int64 UserID, string UpgradeType)
        {

            ResultBase result = new ResultBase(); using (var db = DB.GetDB())
            {

                try
                {
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.CommandText = @"USP_User_Upgrade";
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.Add("@UserID", SqlDbType.BigInt);
                    command.Parameters["@UserID"].Value = UserID;
                    command.Parameters.Add("@UpgradeType", SqlDbType.VarChar,100);
                    command.Parameters["@UpgradeType"].Value = UpgradeType;


                    command.Parameters.Add("@ResultCode", SqlDbType.Int);
                    command.Parameters["@ResultCode"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Message", SqlDbType.VarChar, 300);
                    command.Parameters["@Message"].Direction = ParameterDirection.Output;

                    // 스토어드 프로시저를 실행시킵니다.
                    command.ExecuteNonQuery();
                    

                    // 스토어드 프로시저가 잘 실행됐는지 확인할 수 있는 ResultCode와 Message를 반환 데이터에 명시합니다.
                    result.ResultCode = (int)command.Parameters["@ResultCode"].Value;
                    result.Message = command.Parameters["@Message"].Value.ToString();

                    return result;
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    throw ex;
                }
            }
            return result;
        }
    }
}