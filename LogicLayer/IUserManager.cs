using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        bool UpdatePassword(string username, string oldPassword, string newPassword);
        List<User> GetAllUsers();
        bool AuthenticateUser(string email, string password);
        User RetrieveUserByEmail(string email);
        User GetUserByID(int id);
        List<Role> GetAllRoles();
        User LoginUser(string email, string password);
        User GetUserByFirstAndLast(string firstName, string lastName);
        bool UpdateUserByUserID(int UserID, User user);
        bool AddUser(UserVM user);
        bool UpdateUserRoleByUserID(int userID, string roleID);
    }
}
