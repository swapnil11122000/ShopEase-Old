﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

using ECommWeb.Models;

namespace ECommWeb.Models
{
    public class UsersDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public UsersDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection"));
        }


        public List<Users> GetAllUsers()
        {
            List<Users> list = new List<Users>();
            string qry = "select * from Users";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dr["Id"]);
                    user.UserName = dr["UserName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.Email = dr["Email"].ToString();

                    user.Mobile = Convert.ToInt32(dr["Mobile"]);
                    user.Gender = dr["Gender"].ToString();

                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        // add record
        public int AddUser(Users user)
        {
            int result = 0;
            string qry = "insert into Users values(@UserName,@Password,@Email,@Mobile,@Gender)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //update record
        public int UpdateUser(Users user)
        {
            int result = 0;
            string qry = "update Users set UserName=@UserName,Password=@Password,Email=@Email,Mobile=@Mobile,Gender=@Gender where Id=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        // delete record
        public int DeleteUser(int Id)
        {
            int result = 0;
            string qry = "delete from Users where Id=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}