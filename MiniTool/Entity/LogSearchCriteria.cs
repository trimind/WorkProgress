using System;

namespace MiniTool.Entity
{

    public class LogSearchCriteria
    {
        public string VenNum { get; set; }
        public string VenID { get; set; } // varchar(50)
        public string VenName { get; set; } // varchar(50)
        public DateTime? Date { get; set; } // datetime
        public decimal? ResponseTime { get; set; } // decimal(18, 0)
        public string RequestType { get; set; } // varchar(50)
        public string ResponseType { get; set; } // varchar(50)
        public int? ResponseCode { get; set; } // int
        public string ResponseDescription { get; set; } // varchar(1000)
        public string RequestXML { get; set; } // varchar(1000)
        public string ResponseXML { get; set; } // varchar(1000)

        public decimal? fromResponseTime { get; set; }

        public decimal? toResponseTime { get; set; }

        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        //public string Type { get; set; }
    }
}
