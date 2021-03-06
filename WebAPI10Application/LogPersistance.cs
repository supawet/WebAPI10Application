﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
//using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;

using System.Collections;
using System.Globalization;
using WebAPI10Application.Models;
using System.Configuration;

namespace WebAPI10Application
{
    public class LogPersistance
    {
        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public LogCMSResponse AddCMS(LogCMSRequest LogcmsRequest)
        {
            /*
            OdbcConnection conn = null;
            OdbcCommand command = null;
            OdbcDataReader mySQLReader = null;
            */

            SqlConnection conn = null;
            SqlCommand command = null;
            SqlDataReader mySQLReader = null;

            LogCMSResponse LogcmsResponse = new LogCMSResponse();
            LogcmsResponse.Status = "Fail";
            LogcmsResponse.Message = "Error";

            if(!IsValidEmail(LogcmsRequest.User_Email))
            {
                LogcmsResponse.Message = "Invalid email format.";
                return LogcmsResponse;
            }
            else if (!ValidateIPv4(LogcmsRequest.Source_IP_Address))
            {
                LogcmsResponse.Message = "Invalid ip address format.";
                return LogcmsResponse;
            }
            else if (!ValidateIPv4(LogcmsRequest.Target_IP_Address))
            {
                LogcmsResponse.Message = "Invalid ip address format.";
                return LogcmsResponse;
            }
            else if (String.IsNullOrWhiteSpace(LogcmsRequest.Login_Result))
            {
                LogcmsResponse.Message = "Result can't blank.";
                return LogcmsResponse;
            }
            else
            {

                try
                {
                    string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString; ;
                    //conn = new OleDbConnection(myConnectionString);
                    conn = new SqlConnection(myConnectionString);

                    conn.Open();

                    //command = new OleDbCommand();
                    command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandTimeout = 0;

                    SqlParameter param = null;

                    //--------------------------------  Insert CMS Log   ----------------
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into Log_CMS_Cloud(Source_IP_Address,Target_IP_Address,User_Email,Login_Result,Dt_Gen) values(@Source_IP_Address,@Target_IP_Address,@User_Email,@Login_Result,DATEADD(HH,7,GETUTCDATE()))";
                    command.Parameters.Clear();
                    param = null;

                    param = new SqlParameter("@Source_IP_Address", System.Data.SqlDbType.NVarChar, 15);   //nvarchar(max)
                    param.Value = LogcmsRequest.Source_IP_Address == null ? "" : LogcmsRequest.Source_IP_Address.Trim();
                    param.Direction = ParameterDirection.Input;
                    command.Parameters.Add(param);

                    param = new SqlParameter("@Target_IP_Address", System.Data.SqlDbType.NVarChar, 15);   //nvarchar(max)
                    param.Value = LogcmsRequest.Target_IP_Address == null ? "" : LogcmsRequest.Target_IP_Address.Trim();
                    param.Direction = ParameterDirection.Input;
                    command.Parameters.Add(param);

                    param = new SqlParameter("@User_Email", System.Data.SqlDbType.NVarChar, 50);   //nvarchar(max)
                    param.Value = LogcmsRequest.User_Email == null ? "" : LogcmsRequest.User_Email.Trim();
                    param.Direction = ParameterDirection.Input;
                    command.Parameters.Add(param);

                    param = new SqlParameter("@Login_Result", System.Data.SqlDbType.NVarChar, 15);   //nvarchar(max)
                    param.Value = LogcmsRequest.Login_Result == null ? "" : LogcmsRequest.Login_Result.Trim();
                    param.Direction = ParameterDirection.Input;
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
                    //-------------------------------  /Insert CMS Log   ----------------

                    LogcmsResponse.Message = "";
                    LogcmsResponse.Status = "Success";
                    return LogcmsResponse;
                }

                catch (Exception ex)
                {
                    LogcmsResponse.Message = ex.ToString();
                    LogcmsResponse.Status = "Fail";
                    return LogcmsResponse;
                }
                finally
                {
                    if (mySQLReader != null)
                    {
                        mySQLReader.Close();
                    }
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }   //-------------  /AddCMS
    }
}