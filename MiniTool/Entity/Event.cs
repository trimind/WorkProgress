using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTool.Entity
{
    public partial class Event
    {
        public int Id { get; set; } // int
        public string VenID { get; set; } // varchar(50)
        public string VenName { get; set; } // varchar(50)
        public int VenNum { get; set; } // int
         public string IDEvent { get; set; } // varchar(50)
         public DateTime? StartTime { get; set; } // datetime
         public string Duration { get; set; } // varchar(50)
         public string Status { get; set; } // varchar(50)
         public string OptState { get; set; } // varchar(20)
         public string MarketContext { get; set; } // varchar(1000)
         public string SignalType { get; set; } // varchar(50)
         public string CurrentValue { get; set; } // varchar(1000)
         public string VTNComment { get; set; } // varchar(1000)
         public string TestEvent { get; set; } // varchar(1000)
         public string ResponseRequired { get; set; } // varchar(1000)
         public DateTime? DateCreated { get; set; } // datetime

        public int TotalCount { get; set; }
    }
}
