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
        #region Task3 
        // TODO: Make methods which allow us to read from/write to the database 
        #endregion
        // 3 - 1
        //1. Create a new user, using a username given as a parameter and creating a new GUID which is saved as a
        //string to the database as the ApiKey. This must return the ApiKey or the User object so that the server can
        //pass the Key back to the client.
        public static string newUser(string username) 
        {
            using (var dba = new UserContext())
            {
                string role;
                try
                {
                    dba.Users.First();
                    role = "User";
                }
                catch (InvalidOperationException)
                {
                    role = "Admin";
                }

                User user = new User() 
                {
                    ApiKey = Guid.NewGuid().ToString(),
                    UserName = username,

                    Role = role
                };

                dba.Add(user);
                dba.SaveChanges();

                return user.ApiKey;
            }
        }
        // 3 - 2
        //2. Check if a user with a given ApiKey string exists in the database, returning true or false.
        public static bool checkUserApiKey(string apikey) 
        {
            using (var dba = new UserContext())
            {
                User requestedUser = dba.Users.Find(apikey);
                // User not found
                if (requestedUser == null)
                {
                    return false;
                }
                // User found
                return true;
            }
        }
        
        //4. Check if a user with a given ApiKey string exists in the database, returning the User object.
        public static User checkApiKeyReturnUser(string apikey)
        {
            using (var dba = new UserContext())
            {
                User foundUser = dba.Users.Find(apikey);
                return foundUser;
            }
        }

        // 3 - ?
        // Username check
        public static bool checkUsername(string username)
        {
            using (var dba = new UserContext())
            {
                User requestedUsername = dba.Users.SingleOrDefault(User => User.UserName == username);
                if (requestedUsername == null)
                {
                    // User does not exist
                    return false;
                }
                // User exists
                return true;
            }
        }
        // 3 - 3
        //3. Check if a user with a given ApiKey and UserName exists in the database, returning true or false.
        //public static bool checkApiKeyandUsername(string apikey, string username) 
        //{
        //    using (var dba = new UserContext())
        //    {
        //        User foundUser = dba.Find(apikey);
        //        if (foundUser == null) 
        //        {
        //            return false;
        //        }
        //        else if(foundUser.UserName == username)
        //        {
        //            return true;
        //        }
        //        // Apikey found but not username
        //        return false;
        //    }
        //}
        ////3-5
        ////5. Delete a user with a given ApiKey from the database.
        //public static bool removeUser(string apikey)
        //{
        //    using (var dba = new UserContext())
        //    {
        //        var test = apikey.GetType();
        //        var foundUser = dba.Find(test);
        //        dba.Remove(foundUser);
        //        dba.SaveChanges();
        //    }
        //}
    }


}