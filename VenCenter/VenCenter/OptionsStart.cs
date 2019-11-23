using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCenter
{
  public  class StartOptions
    {
      

        [Option('n', "number", Required = true, HelpText = "Số lượng ven khởi chạy.")]
 
        public int number { get; set; }


       [Option('l', "maxload", Required = true, HelpText = "Số lượng các load trong ven")]

        public int maxload { get; set; }


        [Option('s', "showVen", Required = false, HelpText = "hiển thị hoặc ẩn ven khi tạo")]

        public bool showVen { get; set; }



    }
}
