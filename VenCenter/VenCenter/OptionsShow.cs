using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCenter
{
  public  class ShowOptions
    {
      

        [Option('v', "VENID", Required = true, HelpText = "Nhập VENID để show")]
        public string VENID { get; set; }

    }
}
