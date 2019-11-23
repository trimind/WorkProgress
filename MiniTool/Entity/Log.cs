using System;

namespace MiniTool.Entity
{

    public partial class Log
    {
        public int Id { get; set; } // int
         public int VenNum { get; set; } // int
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
         public DateTime? DateCreated { get; set; } // datetime
         //public string Type { get; set; } // varchar(50)

        public int TotalCount { get; set; }
    }

    //public partial class Log
    //{
    //    public int Id { get; set; }
    //    public string Level { get; set; }
    //    public DateTime? Date { get; set; }
    //    public string Thread { get; set; }
    //    public string Location { get; set; }
    //    public string Message { get; set; }
    //    public DateTimeOffset? CreateTime { get; set; }

    //    public int TotalCount { get; set; }
    //}
}
