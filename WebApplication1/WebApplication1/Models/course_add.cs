using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class course_add
    {
            //code: $("#courseCode")[0].value,
            //title: $("#courseTitle")[0].value,
            //hours: $("#courseHours")[0].value,
            //abbr: $("#courseAbbr")[0].value,
            //teacherid: selectedTeacher.substring(1, selectedTeacher.indexOf(">")),
            //teachername: selectedTeacher.substring(selectedTeacher.indexOf(">") + 1),
            //addteacher: $('input[type=radio][value=1]').is(':checked').toString(),
            //programid: selectedProgram

        public string code { get; set; }
        public string title { get; set; }
        public string hours { get; set; }
        public string abbr { get; set; }
        public string teacherid { get; set; }
        public string teachername { get; set; }
        public string addteacher { get; set; }
        public string programid { get; set; }

    }
}