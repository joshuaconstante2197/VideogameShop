using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using VideogameShop.Library.Models;
using VideogameShopLibrary;

namespace VideogameShop.Library.Services.Authentication
{
    public class UserManager
    {
        

        public bool Register(RegisterModel appUser)
        {
            var hashPassword = EncryptPassword(appUser.Password); 
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                var sql = "INSERT INTO AppUser(UserName, Password, Role)" +
                          $"VALUES('{appUser.UserName}','{hashPassword}', '{appUser.Role}' )";
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                try
                {
                    cmd.ExecuteNonQuery();
                    InsertUserRole(appUser);
                    return true;
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                    return false;
                    throw;
                }
            }
        }
        private bool InsertUserRole(RegisterModel appUser)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                var sql = $"INSERT INTO UserRole(UserId, RoleId) SELECT(SELECT UserId FROM AppUser WHERE UserName = '{appUser.UserName}'), (SELECT RoleId FROM Role WHERE RoleName = '{appUser.Role}')";
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                    return false;
                    throw;
                }
            }
        }
        public bool Login(LoginModel user)
        {
            
            var sql = $"SELECT * FROM AppUser WHERE UserName = '{user.UserName}'";
            DataTable dtbl = new DataTable();
            byte[] savedPaswordHashBytes = new byte[36];

            SqlConnection sqlCon = new SqlConnection(Config.ConnString);
            
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlCon);
                sda.Fill(dtbl);

            if(dtbl.Rows.Count == 1)
            {
                //get saved string
                string savedPasswordHash = dtbl.Rows[0][2].ToString();
                user.Role = dtbl.Rows[0][3].ToString();
                //turn into bytes
                savedPaswordHashBytes = Convert.FromBase64String(savedPasswordHash); 
            }
            else
            {
                return false;
            }

            byte[] salt = new byte[16];
            //copy saved salt into byte array
            Array.Copy(savedPaswordHashBytes, 0, salt, 0, 16);
            //hash entered password with the saved salt
            var hashEnteredPassword = new Rfc2898DeriveBytes(user.Password, salt, 1000);
            //save hashPassword into byte array to compare with DB password
            byte[] enteredPasswordHashBytes = hashEnteredPassword.GetBytes(20);

            int ok = 1;
            for (int i = 0; i < 20; i++)
            {
                //loop trough every byte to check if they match
                if (savedPaswordHashBytes[i + 16] != enteredPasswordHashBytes[i])
                    ok = 0;
            }

            if(ok == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public string EncryptPassword(string password)
        {
            byte[] salt;
            //generate salt
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //hash the password using the created salt 
            var encryptedPassword = new Rfc2898DeriveBytes(password, salt, 1000);
            //place string in byte array
            byte[] hash = encryptedPassword.GetBytes(20);
            //create new array with the hash and salt
            byte[] hashBytes = new byte[36];
            //concatenate hash and salt into single array
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            //convert byte array to string
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
        //returns bool property true if the user has the same role name passed to the method
        public List<UserRoleModel> GetUsersByRole(Role role)
        {
            var sql = "SELECT UserId, UserName, Role FROM AppUser";
            var users = new List<UserRoleModel>();

            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserRoleModel();
                            
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var str = reader.GetName(i);

                                PropertyInfo propertyInfo = user.GetType().GetProperty(str);
                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(user, reader.GetValue(i), null);
                                }
                                else
                                {
                                    var Err = new CreateLogFiles();
                                    Err.ErrorLog(Config.PathToData + "err.log", $"User property not found {propertyInfo}");
                                    throw new Exception($"Error occurred while getting users, please refer to error log");
                                }
                            }
                            if (user.Role == role.RoleName)
                            {
                                user.IsSelected = true;
                            }
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public UserRoleModel GetUserById(string id)
        {
            var sql = $"SELECT * FROM AppUser WHERE UserId = {id}";
            var user = new UserRoleModel();
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var str = reader.GetName(i);

                                PropertyInfo propertyInfo = user.GetType().GetProperty(str);
                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(user, reader.GetValue(i), null);
                                }
                                else
                                {
                                    var Err = new CreateLogFiles();
                                    Err.ErrorLog(Config.PathToData + "err.log", $"User property not found {propertyInfo}");
                                    throw new Exception($"Error occurred while getting users, please refer to error log");
                                }
                            }
                        }
                    }
                }
            }
            return user;
        }


    }
}
