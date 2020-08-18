using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VTS_User_Service.Models;

namespace VTS_User_Service.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<int> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(int UserID, User user);
        Task<bool> UploadProfileImageAsync(int UserID, string fileName);
    }

    public class UserRepository : IUserRepository
    {
        public IConfiguration Configuration { get; }
        readonly string CON_STRING;
        readonly object _lock = new object();

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            CON_STRING = Configuration["ConnectionStrings:VTSDatabase"];
        }


        #region IUserRepository implementation

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> lstData = new List<User>();

            using (SqlConnection con = new SqlConnection(CON_STRING))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PROC_GET_USERS";

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        lstData.Add(new User
                        {
                            UserID = dr.GetSafeInt(0),
                            Name = dr.GetSafeString(1),
                            MobileNumber = dr.GetSafeString(2),
                            Organization = dr.GetSafeString(3),
                            Address = dr.GetSafeString(4),
                            Emailaddress = dr.GetSafeString(5),
                            Location = dr.GetSafeString(6),
                            Photopath = dr.GetSafeString(7),
                        });
                    }
                }
                con.Close();
            }

            return lstData;
        }

        public async Task<int> AddUserAsync(User user)
        {
            int newUserID = -1;
            try
            {

                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_ADD_USER";

                    cmd.Parameters.Add("NAME", SqlDbType.VarChar, 200).Value = user.Name;
                    cmd.Parameters.Add("MOBILE_NUMBER", SqlDbType.VarChar, 15).Value = user.MobileNumber;
                    cmd.Parameters.Add("ORGANIZATION", SqlDbType.VarChar, 400).Value = user.Organization;
                    cmd.Parameters.Add("ADDRESS", SqlDbType.VarChar, 400).Value = user.Address;
                    cmd.Parameters.Add("EMAIL_ADDRESS", SqlDbType.VarChar, 100).Value = user.Emailaddress;
                    cmd.Parameters.Add("LOCATION", SqlDbType.VarChar, 200).Value = user.Location;

                    cmd.Parameters.Add("UPDATED_BY", SqlDbType.Int).Value = user.UpdatedBy;

                    cmd.Parameters.Add("@NEW_USER_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    newUserID = Convert.ToInt32(cmd.Parameters["@NEW_USER_ID"].Value);
                    con.Close();
                }
            }
            catch
            {
                throw ;
            }
            return newUserID;
        }

        public async Task<bool> UpdateUserAsync(int UserID, User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_UPDATE_USER";

                    cmd.Parameters.Add("USER_ID", SqlDbType.Int).Value = UserID;

                    cmd.Parameters.Add("NAME", SqlDbType.VarChar, 200).Value = user.Name;
                    cmd.Parameters.Add("MOBILE_NUMBER", SqlDbType.VarChar, 15).Value = user.MobileNumber;
                    cmd.Parameters.Add("ORGANIZATION", SqlDbType.VarChar, 400).Value = user.Organization;
                    cmd.Parameters.Add("ADDRESS", SqlDbType.VarChar, 400).Value = user.Address;
                    cmd.Parameters.Add("EMAIL_ADDRESS", SqlDbType.VarChar, 100).Value = user.Emailaddress;
                    cmd.Parameters.Add("LOCATION", SqlDbType.VarChar, 200).Value = user.Location;

                    cmd.Parameters.Add("UPDATED_BY", SqlDbType.Int).Value = user.UpdatedBy;


                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
            return true;
        }

        public async Task<bool> UploadProfileImageAsync(int UserID, string fileName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_UPDATE_USER_PROFILE_IMAGE";

                    cmd.Parameters.Add("USER_ID", SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add("PHOTO_PATH", SqlDbType.VarChar, 400).Value = fileName;

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
            return true;
        }

        #endregion
    }
}
