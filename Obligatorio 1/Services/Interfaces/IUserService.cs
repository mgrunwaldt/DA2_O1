using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace Services
{
    public interface IUserService
    {
        void Register(User u, Address a);
        string Login(string identifier, string password);
        List<User> GetAll();
        User Get(Guid id);
        void ChangeUserRole(Guid id, int role);
        void Delete(Guid id);
        void ChangePassword(Guid id, string oldPassword, string newPassword);
        void Logout(Guid id);
    }
}
