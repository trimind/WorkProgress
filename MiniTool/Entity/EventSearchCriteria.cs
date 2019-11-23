using System;

namespace MiniTool.Entity
{

    public class EventSearchCriteria
    {
        public string EventID { get; set; }
        public string MarketContext { get; set; }

        public DateTime? StartTime { get; set; }

        public string Status { get; set; }

        public string CurrentValue { get; set; }

        public string VTNComment { get; set; }

        public string ResponseRequired { get; set; }

    }
}
