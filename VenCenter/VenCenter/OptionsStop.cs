using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCenter
{
  public  class OptionsStop
    {
        [Option('a', "all", Required = true, HelpText = "-all yêu cầu phải có.")]

        public bool all { get; set; }
    }
}
