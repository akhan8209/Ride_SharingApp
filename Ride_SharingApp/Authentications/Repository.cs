using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ride_SharingApp.Authentications
{
    public class Repository
    {
        static List<UserRoles> users = new List<UserRoles>() {

        new UserRoles() {Email="admin@gmail.com",Roles="AdminView",Password="admin123" },
        new UserRoles() {Email="user@gmail.com",Roles="UserView",Password="user123" }
    };

        public static UserRoles GetUserDetails(UserRoles user)
        {
            return users.Where(u => u.Email.ToLower() == user.Email.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
        }

    }
}