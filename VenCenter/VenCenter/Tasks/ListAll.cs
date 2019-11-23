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
    [ExportMetadata("name", "listall")]
    [ExportMetadata("help", "Liệt kê các Ven đang chạy. Sử dụng: vencenter listall")]
    public class ListAll : ITaskHandler
    {
        public void OnExecute(string[] args)
        {
            var all = Process.GetProcessesByName("oadr2b-ven");
            if (all.Length >0)
                Console.WriteLine("Các ven đang chay: {0}", all.Count());
            else
                Console.WriteLine("Không có Ven nào đang chạy.");

            foreach (var process in all)
            {
                Console.WriteLine("Ven: {0}", process.MainWindowTitle);
            }
        }
    }
}
