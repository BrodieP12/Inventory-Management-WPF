using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        int SelectUserCountByEmailAndPasswordHash(string email, string passwordHash);
        UserVM SelectUserByEmail(string email);
        int UpdatePasswordHashByEmail(string email, string oldPasswordHash, string newPasswordHash);
        int UpdateUserByUserID(int userID, string givenName, string familyName, string email, string phoneNumber, string role);
        int AddNewUser(string givenName, string familyName, string phoneNumber, string email, string role);
        List<User> GetAllUsersByActive(bool active);
        List<User> GetAllUsers();
        User GetUserByID(int userID);
        List<string> SelectRolesByUserID(int userID);
        List<Role> GetAllRoles();
        User GetUserByFirstAndLastName(string firstName, string lastName);
        int UpdateUserRoleByUserID(int userID, string roleID);
    }
}
