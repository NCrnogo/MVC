using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Teamy.Models;
using System.Text;


namespace Teamy.Repository
{
    public class Repository
    {
        private static readonly string url = "http://localhost:5000/api/";


        public static int CheckLogin(Users user)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(url + "User/uname=" + user.Name + "&pwd=" + user.Pwd);
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<int>(json);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        internal static int CreateUser(Users user)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "User");
                var newUserJson = JsonConvert.SerializeObject(user);
                var payload = new StringContent(newUserJson,Encoding.UTF8,"application/json");
                var result = client.PostAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<int>(json);
            }
        }

        public static Users GetUsers(string sessionId)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "User/id=" + sessionId);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Users>(json);
            }
        }

        public static Users GetUpdatedUser(Users user)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "User");
                var newUserJson = JsonConvert.SerializeObject(user);
                var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
                var result = client.PutAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Users>(json);
            }
        }

        public static List<Teams> GetTeams(string sessionId)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/id=" + sessionId);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Teams>>(json);
            }
        }

        internal static void JoinTeam(string sessionId, string teamName)
        {
            InviteUser userInvite = new InviteUser
            {
                UserId = sessionId,
                TeamName = teamName
            };

            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/CreateInvite");
                var newUserJson = JsonConvert.SerializeObject(userInvite);
                var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        internal static void CreateTeam(Teams team)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/CreateTeam");
                var newUserJson = JsonConvert.SerializeObject(team);
                var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        internal static List<InviteUser> GetInvites(string sessionId)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/idInvitedUser=" + sessionId);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<InviteUser>>(json);
            }
        }

        internal static void JoinTeamThroughInvite(string teamName,string sessionId)
        {
            InviteUser userInvite = new InviteUser
            {
                UserId = sessionId,
                TeamName = teamName
            };
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/JoinTeamThroughInvite");
                var newUserJson = JsonConvert.SerializeObject(userInvite);
                var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        internal static void DismissJoinTeamThroughInvite(string teamName, string sessionId)
        {
            InviteUser userInvite = new InviteUser
            {
                UserId = sessionId,
                TeamName = teamName
            };
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url + "Teams/DismissJoinTeamThroughInvite");
                var newUserJson = JsonConvert.SerializeObject(userInvite);
                var payload = new StringContent(newUserJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}