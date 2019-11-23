using CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VenCenter.Contracts;

namespace VenCenter.Tasks
{
    [Export(typeof(ITaskHandler))]
    [ExportMetadata("name", "show")]
    [ExportMetadata("help", "Hiển thị cửa sổ ven. Sử dụng: vencenter show -v VENID")]
    public class Show : ITaskHandler
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
 
    
        public void OnExecute(string[] args)
        {
            Parser.Default.ParseArguments<ShowOptions>(args)
            .WithParsed<ShowOptions>(o =>
            {

                var venid = o.VENID;
                var all = Process.GetProcessesByName("oadr2b-ven");
                foreach (var process in all)
                {
                    if (process.MainWindowTitle.Contains(venid))
                    {
                        Console.WriteLine("Hiển thị ven:{0}", venid);
                        ShowWindow(process.MainWindowHandle, 5);
                      
                    }
                   
                }
            });

         }

    

    }
}
