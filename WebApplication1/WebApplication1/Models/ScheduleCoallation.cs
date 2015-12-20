using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ScheduleCoallation
    {

        public int schid { get; set; }
        public int offid { get; set; }
        public int slotid { get; set; }
        public string teachername { get; set; }
        public string title { get; set; }
        public string sectionname { get; set; }
        public int dayid { get; set; }
        public int occupied { get; set; }
        public int roomid { get; set; }
        public int slotno { get; set; }
        public string duration { get; set; }
        
    }
}