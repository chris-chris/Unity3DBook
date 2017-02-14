using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace unity3d.Models
{
    public class AuthModel
    {
        public static Dictionary<string, string> Login(UserData data)
        {
            using (var db = DB.GetDB())
            {
                var transaction = db.BeginTransaction();
                string returnvalue = null;
                try
                {
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.Transaction = transaction;
                    command.CommandText = @"USP_AuthLogin";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FacebookID", data.FacebookID);
                    command.Parameters.Add("@FacebookName", data.FacebookName);
                    command.Parameters.Add("@FacebookPhotoURL", data.FacebookPhotoURL);

                    command.Parameters.Add("@AccessToken", SqlDbType.VarChar, 500);
                    command.Parameters["@AccessToken"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    returnvalue = command.Parameters["@AccessToken"].Value.ToString();

                    transaction.Commit();

                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("AccessToekn", returnvalue);

                    return dict;
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static Dictionary<string, string> Signup(UserData data)
        {
            using (var db = DB.GetDB())
            {
                var transaction = db.BeginTransaction();
                string returnvalue = null;
                try
                {
                    var command = new SqlCommand();
                    command.Connection = db;
                    command.Transaction = transaction;
                    command.CommandText = @"USP_AuthSignUp";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FacebookID", data.FacebookID);
                    command.Parameters.Add("@FacebookName", data.FacebookName);
                    command.Parameters.Add("@FacebookPhotoURL", data.FacebookPhotoURL);

                    command.Parameters.Add("@AccessToken", SqlDbType.VarChar, 500);
                    command.Parameters["@AccessToken"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    returnvalue = command.Parameters["@AccessToken"].Value.ToString();

                    transaction.Commit();

                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("AccessToekn", returnvalue);

                    return dict;
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

    }
}