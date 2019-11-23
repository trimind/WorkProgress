using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCenter.Contracts
{
    
        ///
        ///Every Task handler must implement this interface
        ///
        public interface ITaskHandler
        {
        void OnExecute(string[] args);
        }
    
}
