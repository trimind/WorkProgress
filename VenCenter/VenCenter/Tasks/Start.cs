using CommandLine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using VenCenter.Contracts;

namespace VenCenter.Tasks
{
    [Export(typeof(ITaskHandler))]
    [ExportMetadata("name", "start")]
    [ExportMetadata("help", "Khởi chạy số lượng ứng dụng ven. Sử dụng:vencenter start -n Number")]
    public class Start : ITaskHandler
    {
        private readonly BlockingCollection<string> _collection = new BlockingCollection<string>();
        private int totalitem=0;
        public void OnExecute(string[] args)
        {
            
            Parser.Default.ParseArguments<StartOptions>(args)
                     .WithParsed<StartOptions>(o =>
                     {

                         Console.WriteLine("Đang bắt đầu chạy {0} ven cùng lúc", o.number);
                         //lấy các thiết đặt tư file txt
                         var settings = new IniFile("Settings.ini");
                         var IDstext = settings.Read("VenIDs", "Ven Settings") ?? "";
                         var arrIDs = IDstext.Split('|').Where(a => !string.IsNullOrWhiteSpace(a)).ToList();

                       


                         int maxLoads = o.maxload;
                         int maxProcess = o.number;
                         bool isShowVen = o.showVen;

                         if (arrIDs.Count < maxProcess)
                         {
                             var tt = arrIDs.OrderBy(x => Guid.NewGuid()).Take(maxProcess- arrIDs.Count).ToArray();

                             arrIDs.AddRange(tt);


                         }
                             // Console.Write(isShowVen.ToString());
                             totalitem = maxProcess;
                         if (maxLoads > 100)
                         {
                             Console.WriteLine("Số lượng các Load không lớn hơn 100");
                             return;

                         }

                         var tasks = new List<Task> {
                            // startup publisher task...
                            Task.Factory.StartNew(() => {
                                for(var i = 1; i <= maxProcess; i++)
                                {
                                    _collection.Add(i +  "|" + maxLoads.ToString() + "|" + arrIDs[i-1]);
                                }
                                Console.WriteLine("Chuẩn bị thiết đặt cho {0} ven đã hoàn thành.",maxProcess);
                                Console.WriteLine("Nhấn Enter để kết thúc quá trình khởi chạy ven.", o.number);
                                _collection.CompleteAdding();
                                }),
                            };
                         

                         for (var i = 0; i < maxProcess; i++)
                         {
                             tasks.Add(Task.Factory.StartNew(ConsumerTask(i, isShowVen)));
                         }


                         // var taskfinal= Task.Factory.StartNew(() => { Console.WriteLine("{0}", tasks.Last().Status);  while (tasks.Last().Status != TaskStatus.Running) { System.Threading.Thread.Sleep(100);  } });
                           Task.WaitAll(tasks.ToArray()); // wait for completion
                         //  Task.WaitAny(taskfinal);
                         Console.WriteLine("Ket thuc qua trinh load ven.");
                         Console.ReadLine();
                         Console.WriteLine("Ket thuc qua trinh load ven.");
                       
                     });
           
        }
        private  SecureString GetSecureString(string str)
        {
            SecureString secureString = new SecureString();
            foreach (char ch in str)
            {
                secureString.AppendChar(ch);
            }
            secureString.MakeReadOnly();
            return secureString;
        }
        private Action ConsumerTask(int id,bool isShowVen)
        {
            // return a closure just so the id can get passed
            return () =>
            {


                string item;
                //while (true)
                //{
                if (_collection.TryTake(out item, -1))
                {
                    var arr = item.Split('|');
                    var venacc = arr[2];
                  //  Console.WriteLine(venacc);

                   //  Console.WriteLine("task id: {0}", C:\Users\hieu\Desktop\MiniTool\IniFile.cs[0]);

                   //System.Diagnostics.Process p = new System.Diagnostics.Process();

                   //// Domain and User Name:
                   //p.StartInfo.Domain = "optional_domain";
                   //p.StartInfo.UserName = "user_to_run_as";

                   //// Command to execute and arguments:
                   //p.StartInfo.FileName = "c:\\path\\to\\executable.exe";
                   //p.StartInfo.Arguments = "your argument string";

                   //// Build the SecureString password...
                   //System.String rawPassword = "your_password";
                   //System.Security.SecureString encPassword = new System.Security.SecureString();
                   //foreach (System.Char c in rawPassword)
                   //{
                   //    encPassword.AppendChar(c);
                   //}

                   //p.StartInfo.Password = encPassword;

                   //// The UseShellExecute flag must be turned off in order to supply a password:
                   //p.StartInfo.UseShellExecute = false;

                   //p.Start();


                   var p = new ProcessStartInfo()
                    {
                        WindowStyle= isShowVen?  ProcessWindowStyle.Minimized: ProcessWindowStyle.Hidden,
                       // UseShellExecute = false,
                        FileName = AppDomain.CurrentDomain.BaseDirectory + "/oadr2b-ven.exe", // "C:\\Users\\hieu\\Desktop\\OpenADR-Virtual-End-Node-master\\oadr2b-ven\\bin\\Release\\oadr2b-ven.exe ",
                        Arguments= arr[0] + " " + arr[1] + " " + venacc

                   };

                    using (var process = Process.Start(p))
                    {
                        Console.WriteLine("Bắt đầu chạy ven: {0}", venacc);
                        process.WaitForExit();
                    }
                }
            };
        }
    }

}
