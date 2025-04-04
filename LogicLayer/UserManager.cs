using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager
    {
        // pay attention to dependency inversion
        private IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
        public bool AddUser(UserVM user)
        {
            bool result = false;
            result = (1 == _userAccessor.AddNewUser(user.GivenName, user.FamilyName, user.PhoneNumber, user.Email, user.Roles[0]));

            return result;
        }
        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;

            password = HashSHA256(password);
            result = (1 == _userAccessor.SelectUserCountByEmailAndPasswordHash(email, password));

            return result;
        }
        public bool UpdateUserByUserID(int UserID, UserVM user)
        {
            bool result = false;
            result = (1 == _userAccessor.UpdateUserByUserID(UserID, user.GivenName, user.FamilyName, user.PhoneNumber, user.Email, user.Roles[0]));
            return result;
        }
        public bool UpdateUserRoleByUserID(int userID, string roleID)
        {
            bool result = false;
            result = (1 == _userAccessor.UpdateUserRoleByUserID(userID, roleID));
            return result;
        }

        public List<string> GetRolesForUser(int UserId)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectRolesByUserID(UserId);

                if (roles.Count == 0)
                {
                    throw new Exception("No roles were retrieved from the database.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No roles were found.", ex);
            }

            return roles;
        }


        public List<string> GetAllUserFirstAndLastName(List<User> users)
        {
            List<string> firstlastnames = new List<string>();
            foreach (User user in users)
            {
                firstlastnames.Add(user.GivenName + " " + user.FamilyName);
            }
            return firstlastnames;
        }
        public User GetUserByFirstAndLast(string firstName, string lastName)
        {
            User user;
            try
            {
                user = _userAccessor.GetUserByFirstAndLastName(firstName, lastName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            return user;
        }
        public string GetUserFirstNameLastName(int userId)
        {
            string firstnamelastname = "";
            try
            {
                User user = GetUserByID(userId);
                firstnamelastname += user.GivenName;
                firstnamelastname += " ";
                firstnamelastname += user.FamilyName;
            } catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            return firstnamelastname;
        }
        

        public string HashSHA256(string password)
        {
            if (password == "" || password == null)
            {
                throw new ArgumentException("Missing Input.");
            }
            // begin with what you need to return
            string hashValue = null;

            // cryptographic algorithms run on bits and bytes, so make a byte array
            byte[] data;

            // create a hash provider object
            using (SHA256 sha256provider = SHA256.Create())
            {
                // hash the input
                data = sha256provider.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            // build a string from the result
            var s = new StringBuilder();

            // loop through the bytes to make a string of hex characters
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // convert the result
            hashValue = s.ToString();

            // return once on the last line of the method
            return hashValue;
        }

        public UserVM LoginUser(string email, string password)
        {
            UserVM user = null;
            try
            {
                if (AuthenticateUser(email, password))
                {
                    user = (UserVM)RetrieveUserByEmail(email);
                    if (user != null)
                    {
                        user.Roles = GetRolesForUser(user.UserID);
                    }
                }
                else
                {
                    throw new ArgumentException("Bad password or email.");
                }
            }
            catch (Exception ex)
            {
                throw; // exception would already be wrapped by logic functions
            }
            return user;
        }

        public User RetrieveUserByEmail(string email)
        {
            User user = null;
            try
            {
                user = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed", ex);
            }
            return user;
        }

        public bool UpdatePassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;
            oldPassword = HashSHA256(oldPassword);
            newPassword = HashSHA256(newPassword);
            try
            {
                result = (1 == _userAccessor.UpdatePasswordHashByEmail(email, oldPassword, newPassword));
                if (!result)
                {
                    throw new ApplicationException("Duplicate User records found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                users = _userAccessor.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed getting all users", ex);
            }
            return users;
        }
        public User GetUserByID(int id)
        {
            User user = null;
            try
            {
                user = _userAccessor.GetUserByID(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User record does not exist", ex);
            }
            return user;
        }
        public List<Role> GetAllRoles()
        {
            return _userAccessor.GetAllRoles();
        }
    }
}
