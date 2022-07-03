using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using TeamyAPI.Models;

namespace TeamyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        SqlConnection Con;
        SqlDataReader dr;
        SqlCommand cmd;
        private List<Teams> ListTeams;

        public TeamsController(List<Teams> listTeams, SqlConnection con)
        {
            ListTeams = listTeams;
            Con = con;
        }

        //call http://localhost:5000/api/Teams
        [HttpGet]
        public List<Teams> Get()
        {
            return ListTeams;
        }

        //call http://localhost:5000/api/Teams/idTeam=1
        [HttpGet("idTeam={id}")]
        public Teams GetTeam(int id)
        {
            Teams a = new Teams();
            cmd = new SqlCommand("getTeam", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                a= new Teams
                {
                    Id = (int)dr["IDTeam"],
                    Name = dr["Team"].ToString(),
                    DateCreated = dr["Created"].ToString()
                };
            }
            Con.Close();
            return a;
        }

        //call http://localhost:5000/api/Teams/id=1
        [HttpGet("id={id}")]
        public List<Teams> GetTeams(int id)
        {
            List<Teams> a = new List<Teams>();
            cmd = new SqlCommand("getTeamsByUser", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                a.Add(new Teams
                {
                    Id = (int)dr["IDTeam"],
                    Name = dr["Team"].ToString(),
                    DateCreated = dr["Created"].ToString(),
                    TeacherID = dr["TeacherID"] == null ? (int)dr["TeacherID"] : -1,
                    OwnerID = (int)dr["OwnerID"]
                });
            }
            Con.Close();
            return a;
        }

        [HttpGet("idInvitedUser={id}")]
        public List<InviteUser> GetInvites(int id)
        {
            List<InviteUser> a = new List<InviteUser>();
            cmd = new SqlCommand("getTeamInvites", Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = id;
            Con.Open();
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                a.Add(new InviteUser
                {
                    UserId = dr["UserID"].ToString(),
                    TeamName = dr["Team"].ToString()
                });
            }
            Con.Close();
            return a;
        }

        [HttpPost]
        [Route("CreateInvite")]
        public void CreateInvite(InviteUser userInvite)
        {
            try
            {
                cmd = new SqlCommand("joinRequestUser", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@teamName", System.Data.SqlDbType.VarChar).Value = userInvite.TeamName;
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = int.Parse(userInvite.UserId);
                Con.Open();
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                Con.Close();
                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            }
            catch
            {
                Con.Close();
            }
        }

        [HttpPost]
        [Route("JoinTeamThroughInvite")]
        public void JoinTeamThroughInvite(InviteUser userInvite)
        {
            try
            {
                cmd = new SqlCommand("joinTeam", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = int.Parse(userInvite.UserId);
                cmd.Parameters.Add("@teamName", System.Data.SqlDbType.VarChar).Value = userInvite.TeamName;
                Con.Open();
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                Con.Close();
                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            }
            catch
            {
                Con.Close();
            }
        }

        [HttpPost]
        [Route("DismissJoinTeamThroughInvite")]
        public void DismissJoinTeamThroughInvite(InviteUser userInvite)
        {
            try
            {
                cmd = new SqlCommand("dismissJoinTeam", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = int.Parse(userInvite.UserId);
                cmd.Parameters.Add("@teamName", System.Data.SqlDbType.VarChar).Value = userInvite.TeamName;
                Con.Open();
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                Con.Close();
                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            }
            catch
            {
                Con.Close();
            }
        }

        [HttpPost]
        [Route("CreateTeam")]
        public void CreateTeam(Teams team)
        {
            try
            {
                cmd = new SqlCommand("createTeam", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = team.OwnerID;
                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = team.Name;
                cmd.Parameters.Add("@created", System.Data.SqlDbType.VarChar).Value = team.DateCreated;
                Con.Open();
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                Con.Close();
                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            }
            catch
            {
                Con.Close();
            }
        }
    }
}
