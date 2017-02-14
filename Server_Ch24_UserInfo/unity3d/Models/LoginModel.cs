using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace unity3d.Models
{
    public class LoginModel
    {
        // 데이터베이스에서 EXEC USP_User_Login을 호출하고 결과를 정리해서 콘트롤러에 넘겨줍니다.
        public static LoginResult Login(UserData data)
        {
            // 데이터베이스 커넥션을 가져옵니다.
            using (var db = DB.GetDB())
            {
                // 트랜즈액션 시작합니다.
                var transaction = db.BeginTransaction();

                // 반환할 결과 객체를 생성해 놓습니다.
                LoginResult result = new LoginResult();

                try
                {
                    // 스토어드 프로시저를 실행시키기 위해 명령을 준비합니다.
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.Transaction = transaction;
                    command.CommandText = @"USP_User_Login";
                    command.CommandType = CommandType.StoredProcedure;

                    // 스토어드 프로시저에 전달할 파라미터를 정합니다.
                    command.Parameters.Add("@FacebookID", data.FacebookID);
                    command.Parameters.Add("@FacebookName", data.FacebookName);
                    command.Parameters.Add("@FacebookPhotoURL", data.FacebookPhotoURL);

                    command.Parameters.Add("@AccessToken", SqlDbType.VarChar, 500);
                    command.Parameters["@AccessToken"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@UserID", SqlDbType.BigInt);
                    command.Parameters["@UserID"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@ResultCode", SqlDbType.Int);
                    command.Parameters["@ResultCode"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Message", SqlDbType.VarChar, 300);
                    command.Parameters["@Message"].Direction = ParameterDirection.Output;

                    // 스토어드 프로시저를 실행시킵니다.
                    command.ExecuteNonQuery();

                    // 스토어드 프로시저로부터 반환된 결과를 정리합니다.
                    data.AccessToken = command.Parameters["@AccessToken"].Value.ToString();
                    data.UserID = (Int64)command.Parameters["@UserID"].Value;

                    // 반환할 데이터에 AccessToken과 UserID를 넣기 위해 UserData에 입력합니다.
                    result.Data = data;

                    // 스토어드 프로시저가 잘 실행됐는지 확인할 수 있는 ResultCode와 Message를 반환 데이터에 명시합니다.
                    result.ResultCode = (int)command.Parameters["@ResultCode"].Value;
                    result.Message = command.Parameters["@Message"].Value.ToString();

                    // 트랜즈액션을 커밋합니다.
                    transaction.Commit();

                    return result;
                }
                catch (System.Exception ex)
                {
                    // 에러가 발생하면 트랜즈액션을 롤백합니다.
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

    }
}