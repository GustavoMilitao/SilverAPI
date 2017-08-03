using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class UserBLL
    {
        private UserDAL userDAL;

        public UserDAL UserDAL
        {
            get
            {
                if (userDAL == null)
                    userDAL = new UserDAL();
                return userDAL;
            }
        }

        public int InsertUser(User user)
        {
            return UserDAL.InsertUser(user);
        }

        public bool UpdateUser(User user)
        {
            return UserDAL.UpdateUser(user);
        }

        public User getUserByID(int id)
        {
            return UserDAL.getUserByID(id);
        }

        public bool DeleteUserByID(int id)
        {
            return UserDAL.DeleteUserByID(id);
        }

        public User getUserByPartialName(string partialName)
        {
            return UserDAL.getUserByPartialName(partialName);
        }

        public List<User> ListUsersByPartialName(string partialName)
        {
            return UserDAL.ListUsersByPartialName(partialName);
        }

        public User getUserByPartialNickname(string partialNickname)
        {
            return UserDAL.getUserByPartialNickname(partialNickname);
        }

        public List<User> ListUsersByPartialNickname(string partialNickname)
        {
            return UserDAL.ListUsersByPartialNickname(partialNickname);
        }

        public List<User> ListUsers()
        {
            return UserDAL.ListUsers();
        }
    }
}
