using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    /*
     * YOU CAN USE AddWithValue("@parameter", something)
     * Example: SelectBoatsByStatus(string status)
     * cmd.Parameters.AddWithValue("@StatusID", status);
     * 
     */


    public class UserAccessor : IUserAccessor
    {
        public int AddNewUser(string givenName, string familyName, string phoneNumber, string email, string role)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            //command
            var cmd = new SqlCommand("sp_insert_user_and_role", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50);
            // values
            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@FamilyName"].Value = familyName;
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PhoneNumber"].Value = phoneNumber;
            cmd.Parameters["@Role"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_all_users", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<User> GetAllUsersByActive(bool active)
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int userID)
        {
            User user = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_user_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    return user;
                }
                else
                {
                    throw new ApplicationException("User record not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserVM SelectUserByEmail(string email)
        {
            UserVM user = null;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_user_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new UserVM()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    return user;
                }
                else
                {
                    throw new ArgumentException("User record not found.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_select_roles_by_UserID", conn);
            // command type?
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            // values
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }
        public int SelectUserCountByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_authenticate_user", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                var result = cmd.ExecuteScalar();
                count = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }

        public int UpdatePasswordHashByEmail(int useID, string email, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            //command
            var cmd = new SqlCommand("sp_update_passwordhash_by_email", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int UpdatePasswordHashByEmail(string email, string oldPasswordHash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }

        public int UpdateUserByUserID(int userID, string givenName, string familyName, string phoneNumber, string email, string role)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            //command
            var cmd = new SqlCommand("sp_update_user_by_userid", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50);
            // values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@FamilyName"].Value = familyName;
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PhoneNumber"].Value = phoneNumber;
            cmd.Parameters["@Role"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public User GetUserByFirstAndLastName(string firstName, string lastName)
        {
            UserVM user = null;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_get_user_by_first_and_last_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@GivenName"].Value = firstName;
            cmd.Parameters["@FamilyName"].Value = lastName;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new UserVM()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(6)
                    };
                    return user;
                }
                else
                {
                    throw new ArgumentException("User record not found.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_get_all_roles", conn);
            // command type?
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(new Role
                    {
                        RoleName = reader.GetString(0),
                        Description = reader.GetString(1)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public int UpdateUserRoleByUserID(int userID, string roleID)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            //command
            var cmd = new SqlCommand("sp_update_user_role_by_userid", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 50);
            // values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@RoleID"].Value = roleID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
