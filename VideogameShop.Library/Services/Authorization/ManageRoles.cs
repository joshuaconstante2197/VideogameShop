﻿using System;
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
        public bool GetRoleById(string id)
        {
            var sql = $"SELECT 1 FROM Role WHERE RoleId = {id}";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql,sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.FieldCount > 0)
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
        public bool EditRoleById(Role role)
        {
            var sql = $"UPDATE Role SET (RoleName = '{role.RoleName}') WHERE RoleId = {role.RoleId}";
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
