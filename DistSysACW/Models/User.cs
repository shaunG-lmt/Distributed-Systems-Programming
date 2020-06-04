using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public class User
    {
        public User() { }

        [Key]
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        virtual public ICollection<Log> Logs { get; set; }
    }

    public static class UserDatabaseAccess
    {
        public static void AddLog(string request, string apikey)
        {
            using (var dba = new UserContext())
            {
                User foundUser = dba.Users.Find(apikey);
                foundUser.Logs.Add(new Log
                {
                    LogDateTime = DateTime.Now,
                    LogString = request,
                    ApiKey = apikey
                });
                dba.Log_Archive.Add(new Log_Archive
                {
                    LogDateTime = DateTime.Now,
                    LogString = request
                });
                dba.SaveChanges();
            }
        }
        #region Task3 
        // TODO: Make methods which allow us to read from/write to the database 
        #endregion
        // 3 - 1
        //1. Create a new user, using a username given as a parameter and creating a new GUID which is saved as a
        //string to the database as the ApiKey. This must return the ApiKey or the User object so that the server can
        //pass the Key back to the client.
        public static string NewUser(string username) 
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
        public static bool CheckUserApiKey(string apikey) 
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
        public static User ReturnUserFromApiKey(string apikey)
        {
            using (var dba = new UserContext())
            {
                User foundUser = dba.Users.Find(apikey);
                return foundUser;
            }
        }

        // 3 - ?
        // Username check
        public static bool CheckUsername(string username)
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
        public static bool RemoveUser(User user)
        {
            using (var dba = new UserContext())
            {
                // Delete logs assocation with user.
                dba.Logs.RemoveRange(dba.Logs.Where(Log => Log.ApiKey == user.ApiKey));
                
                dba.Remove(user);
                dba.SaveChanges();
                return true;
            }
        }
        public static string ChangeRole(JSONContract jSONContract)
        {
            using (var dba = new UserContext())
            {
                User requestedUser = dba.Users.SingleOrDefault(User => User.UserName == jSONContract.Username);
                if (requestedUser == null)
                {
                    return "NOT DONE: Username does not exist";
                }
                // user exists
                if (jSONContract.Role != "User" && jSONContract.Role != "Admin")
                {
                    return "NOT DONE: Role does not exist";
                }
                else
                {
                    try
                    {
                        requestedUser.Role = jSONContract.Role;
                        dba.SaveChanges();
                        return "DONE";
                    }
                    catch
                    {
                        return "NOT DONE: An error occured";
                    }
                }
            }
        }
    }
}