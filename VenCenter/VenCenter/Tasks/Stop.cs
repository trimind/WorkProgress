using CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenCenter.Contracts;

namespace VenCenter.Tasks
{
    [Export(typeof(ITaskHandler))]
    [ExportMetadata("name", "stop")]
    [ExportMetadata("help", "Thoát tất cả các ven đang chạy. Sử dụng: vencenter stop -all")]
    public class Stop : ITaskHandler
    {
        public void OnExecute(string[] args)
        {
            Parser.Default.ParseArguments<OptionsStop>(args)
            .WithParsed<OptionsStop>(o =>
            {
                var all = Process.GetProcessesByName("oadr2b-ven");

                Console.WriteLine("Thực hiện thoát {0} ven", all.Count());
                foreach (var process in all)
                {
                    Console.WriteLine("Đã thoát ven id:{0}", process.Id);
                    process.Kill();
                }
            });

         }

    

    }
}
