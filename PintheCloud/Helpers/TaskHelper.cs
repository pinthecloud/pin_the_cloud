﻿using PintheCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintheCloud.Helpers
{
    public class TaskHelper
    {
        // Tasks
        private IDictionary<string, Task<bool>> Tasks = new Dictionary<string, Task<bool>>();
        private Dictionary<string ,Task<bool>> SignInTasks = new Dictionary<string, Task<bool>>();
        private Dictionary<string, Task> SignOutTasks = new Dictionary<string, Task>();


        public void AddTask(string name, Task<bool> task)
        {
            Task<bool> existedTask = null;
            if (!this.Tasks.TryGetValue(name, out existedTask))
                this.Tasks.Add(name, task);
        }


        public async Task<bool> WaitTask(string name)
        {
            Task<bool> task = null;
            if (this.Tasks.TryGetValue(name, out task))
            {
                await task;
                this.Tasks.Remove(name);
                return task.Result;
            }
            throw new Exception();
        }

        public void AddSignInTask(string key, Task<bool> task)
        {
            if (!this.SignInTasks.ContainsKey(key))
                this.SignInTasks.Add(key, task);
        }


        public async Task<bool> WaitSignInTask(string key)
        {
            if (this.SignInTasks.ContainsKey(key))
            {
                bool resut = await this.SignInTasks[key];
                this.SignInTasks.Remove(key);
                return resut;
            }
            else
            {
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                tcs.SetResult(true);
                return tcs.Task.Result;
            }
        }


        public void AddSignOutTask(string key, Task task)
        {
            if (!this.SignOutTasks.ContainsKey(key))
                this.SignOutTasks.Add(key, task);
        }


        public Task WaitSignOutTask(string key)
        {
            if (this.SignOutTasks.ContainsKey(key))
            {
                Task task = this.SignOutTasks[key];
                this.SignOutTasks.Remove(key);
                return task;
            }
            else
            {
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                tcs.SetResult(true);
                return tcs.Task;
            }
        }
    }
}
