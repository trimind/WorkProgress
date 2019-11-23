using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenCenter.Contracts;

namespace VenCenter
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Container container = new Container();
                string taskname = null;
                if ((args.Length == 0) || (args[0].ToLower() == "help") ||
                                          (args[0].ToLower() == "/?"))
                {
                    taskname = "help";//This is our custom ITaskHandler
                                      //implementation responsible for displaying Help
                }
                else
                {
                    taskname = args[0];
                }
                container.ExecTask(taskname, args.Skip(1).ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
