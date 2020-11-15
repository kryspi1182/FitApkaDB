using System;
using System.Collections.Generic;
using System.Text;
using FitApka.DAL;
using FitApka.Model;
using Microsoft.Extensions.Configuration;

namespace FitApka.Controller
{
    class UserController
    {
        UserDAL userDAL;

        public UserController(IConfiguration _configuration)
        {
            userDAL = new UserDAL(_configuration);
        }

        public List<User> GetUsers()
        {
            List<User> users = userDAL.GetList();
            return users;
        }

        public void AddUser(User newUser)
        {
            userDAL.AddUser(newUser);
        }

        public void EditUser(int IDUser, User modifiedUser)
        {
            userDAL.EditUser(IDUser, modifiedUser);
        }
    }
}
