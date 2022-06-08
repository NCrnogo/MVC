﻿using Newtonsoft.Json;
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
            using(var client = new HttpClient())
            {
                var endpoint = new Uri(url + "User/uname=" + user.Name + "&pwd=" + user.Pwd);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<int>(json);
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

        internal static Users GetUpdatedUser(Users user)
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




    }
}