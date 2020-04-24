using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public class User
    {
        #region Task2
        // TODO: Create a User Class for use with Entity Framework
        // Note that you can use the [key] attribute to set your ApiKey Guid as the primary key 
        #endregion

        public User() { }

        [Key]
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }

    #region Task13?
    // TODO: You may find it useful to add code here for Logging
    #endregion

    public static class UserDatabaseAccess
    {
        public static int newUser(string username) 
        {
            using (var dba = new UserContext())
            {
                int guid;
                User user = new User() 
                {
                    UserName = username,
                    Role = "user"
                };
                dba.Add(user);
                dba.SaveChanges();

                return guid = user.ApiKey;
            }
        }
        

        #region Task3 
        // TODO: Make methods which allow us to read from/write to the database 
        #endregion
    }


}