using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using VideogameShop.Library.Models;
using VideogameShopLibrary;

namespace VideogameShop.Library.Services.Authorization
{
    public class ManageRoles
    {
        
        public bool AddRole(Role role)
        {
            var sql = $"INSERT INTO Role(RoleName) SELECT('{role.RoleName}') WHERE NOT EXISTS(SELECT * FROM P_Categories WHERE Category = '{role.RoleName}') ";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
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
        public bool CheckIfRoleExist(Role role)
        {
            var sql = $"SELECT RoleName From Role WHERE RoleName = '{role.RoleName}'";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        public List<Role> GetRoles()
        {
            var listOfRoles = new List<Role>();
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                var sql = "SELECT * FROM Role";
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var role = new Role();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var str = reader.GetName(i);

                                PropertyInfo propertyInfo = role.GetType().GetProperty(str);
                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(role, reader.GetValue(i), null);
                                }
                            }
                            listOfRoles.Add(role);
                        }

                    }
                }

            }
            return listOfRoles;
        }
        public Role GetRoleById(int id)
        {
            var role = new Role();
            var sql = $"SELECT * FROM Role WHERE RoleId = {id}";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var str = reader.GetName(i);

                                PropertyInfo propertyInfo = role.GetType().GetProperty(str);
                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(role, reader.GetValue(i), null);
                                }
                                else
                                {
                                    throw new Exception("Role not found");
                                }
                            }
                        }
                        return role;
                    }

                }
            }
        }
        public bool EditRoleById(Role role)
        {
            var sql = $"UPDATE Role SET RoleName = '{role.RoleName}' WHERE RoleId = {role.RoleId}";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
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
        public List<UserRoleModel> GetUsersInRole(int id)
        {
            var sql = $"SELECT AppUser.UserId, AppUser.UserName FROM AppUser JOIN UserRole ON AppUser.UserId = UserRole.UserId WHERE RoleId = {id}";
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
                                    throw new Exception("User in role not found");
                                }
                            }
                            users.Add(user);
                        }
                        return users;
                    }

                }

            }
        }

        public bool AddUserToRole(UserRoleModel user, Role role)
        {
            var sql = $"UPDATE AppUser SET Role = '{role.RoleName}' WHERE UserId = {user.UserId}";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
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
        public bool RemoveUserFromRole(UserRoleModel user)
        {
            var sql = $"UPDATE AppUser SET Role = null WHERE UserId = {user.UserId}";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
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

    }
}
