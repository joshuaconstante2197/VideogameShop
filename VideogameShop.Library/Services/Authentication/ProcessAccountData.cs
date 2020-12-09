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
    public class ProcessAccountData
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
        public bool Login(AppUser user)
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
        
        
    }
}
