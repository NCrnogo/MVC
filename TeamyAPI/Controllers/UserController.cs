using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TeamyAPI.Models;

namespace TeamyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        SqlConnection Con;
        SqlDataReader dr;
        SqlCommand cmd;
        private List<Users> ListUsers;

        public UserController(List<Users> listUsers, SqlConnection con)
        {
            ListUsers = listUsers;
            Con = con;
        }

        public void onUserAdd(int id)
        {
            cmd = new SqlCommand("getUser", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            Con.Open();
            cmd.Connection = Con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListUsers.Add( new Users
                {
                    Id = (int)dr["IDUser"],
                    Name = dr["LoginName"].ToString(),
                    Roll = dr["Roll"].ToString(),
                    DateCreated = dr["DateCreated"].ToString()
                });
            }
            Con.Close();
        }

        //call http://localhost:5000/api/User
        //Default method that returns all users
        [HttpGet]
        public List<Users> Get()
        {
            return ListUsers;
        }

        //call http://localhost:5000/api/User/id=1
        //get method that returns user with the id if user does not exist it returns null
        [HttpGet("id={id}")]
        public Users GetUser(int id)
        {
            Users a = new Users();
            cmd = new SqlCommand("getUser", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value=id;
            Con.Open();
            cmd.Connection = Con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                a = new Users
                {
                    Id = (int)dr["IDUser"],
                    Name = dr["LoginName"].ToString(),
                    Roll = dr["Roll"].ToString(),
                    DateCreated = dr["DateCreated"].ToString()
                };
            }
            Con.Close();
            return a;
        }

        //call: http://localhost:5000/api/User/uname=Ivo&pwd=12345
        //Get method that returns id of user if pwd and uname are correct and returns -1 if not
        [HttpGet("uname={uname}&pwd={pwd}")]
        public int GetLogin(string uname, string pwd)
        {
            int a=-1;
            cmd = new SqlCommand("CheckLogin", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value=uname.Trim();
            cmd.Parameters.Add("@pwd", System.Data.SqlDbType.VarChar).Value=pwd.Trim();
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                a = (int)dr["IDUser"];
            }
            Con.Close();
            return a;
        }

        //Post method searches for name, pwd and roll as boolean (If the user wants to be a teacher
        //write true and hardcode teacher id if not be ghost and hardcode ghost id other roles come later)
        //Method returns id of created user so we can put it in session and let him on the site
        [HttpPost]
        public int Post(Users user)
        {

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string salt = new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            cmd = new SqlCommand("createUser", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
            if(user.Roll == null || user.Roll=="Student")
            {
                cmd.Parameters.Add("@roll", System.Data.SqlDbType.Int).Value = 1;
            }else
            {
                cmd.Parameters.Add("@roll", System.Data.SqlDbType.Int).Value = 1; // or whatever id of teacher roll is
            }
            cmd.Parameters.Add("@salt", System.Data.SqlDbType.VarChar).Value = salt;
            cmd.Parameters.Add("@date", System.Data.SqlDbType.VarChar).Value = DateTime.Now.ToString();
            cmd.Parameters.Add("@pwd", System.Data.SqlDbType.VarChar).Value = user.Pwd;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Direction= ParameterDirection.Output;
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(cmd.Parameters["@id"].Value);
            Con.Close();
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            onUserAdd(id);
            return id;
        }

        //UPDATE USER ON USER NAME BY CALLING PUT METHOD IN: http://localhost:5000/api/User
        [HttpPut]
        public Users Put(Users user)
        {
            cmd = new SqlCommand("updateUser", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = user.Id;
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(cmd.Parameters["@id"].Value);
            Con.Close();
            onUserUpdate(id,user);
            return user;
        }

        private void onUserUpdate(int id,Users user)
        {
            for(int i=0;i< ListUsers.Count; i++)
            {
                if(ListUsers[i].Id== id)
                {
                    ListUsers[i] = user;
                }
            }
        }

    }
}
