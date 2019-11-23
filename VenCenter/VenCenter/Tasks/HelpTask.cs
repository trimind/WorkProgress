﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenCenter.Contracts;

namespace VenCenter.Tasks
{
    [Export(typeof(ITaskHandler))]
    [ExportMetadata("name", "help")]
    public class HelpTask : ITaskHandler
    {
        public void OnExecute(string[] args)
        {
            if (args.Length == 0)
            {
                DisplayAllTasks();
            }
            else
            {
                string taskname = args[0];
                DisplayTaskSpecificHelp(taskname);
            }
        }
        /// 
        /// Display a short list of all Task names. 
        ///         
        private void DisplayAllTasks()
        {
            Console.WriteLine("List of all Tasks");
            foreach (var lazy in this.Parent.Tasks)
            {
                string task = ((string)lazy.Metadata["name"]).ToLower();
                if (task == "help") continue;
                Console.WriteLine("-----------------------");
                string help = null;
                if (lazy.Metadata.ContainsKey("help"))
                {
                    help = lazy.Metadata["help"] as string;
                }
                else
                {
                    help = "";
                }
                Console.WriteLine($"{task}      {help}");
            }
        }
        /// 
        /// Display the help description for the specified Task 
        /// 
        private void DisplayTaskSpecificHelp(string taskname)
        {
            Console.WriteLine($"Displaying help on Task:{taskname}");
            var lazy = Parent.Tasks.FirstOrDefault
                  (t => (string)t.Metadata["name"] == taskname.ToLower());
            if (lazy == null)
            {
                throw new ArgumentException($"No task with name={taskname} was found");
            }
            string help = (lazy.Metadata.ContainsKey("help") == false) ?
            "No help documentation found" : (string)lazy.Metadata["help"];
            Console.WriteLine($"Task:{taskname}");
            Console.WriteLine($"{help}");
        }
        ///
        ///MEF will resolve this dependency at the time of instantiation
        ///
        [Import("parent")]
        public Container Parent { get; set; }
    }
}
