﻿using DroneScanning.Interface;
using DroneScanning.Models;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneScanning.Services
{
    public class LoginService: ILoginRepository
    {
        async Task<User> ILoginRepository.Login(string username, string password)
        {
            User userTask = new User();
            try
            {
                //Task<User> user = (Task<User>)await CrossCloudFirestore.Current
                var user = await CrossCloudFirestore.Current
                                .Instance
                                .Collection("Users")
                                .Document(username)
                                .GetAsync();
                string tmp1 = JsonConvert.SerializeObject(user.Data);

                User us = JsonConvert.DeserializeObject<User>(tmp1);
                if (!String.IsNullOrEmpty(us.UserId))
                {
                    userTask = us;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Se ha presentado un error: {e.Message.ToString()}");
            }
            return userTask;
        }
    }
}
