using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Entities;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        vscheduleEntities db = new vscheduleEntities();

        public ActionResult Index() 
        {
            return View("sch_Index");
        }
        public string courses()
        {
            var K = from entity in db.sections
                    select new
                    {
                        secid = entity.secid,
                        programid = entity.programid,
                        sectionname = entity.sectionname
                    };
            return new JavaScriptSerializer().Serialize(K);
        }
        public string fetchDataCoallation(string campus = "0", string program = "ALL")
        {
            string Coallation;
                    Coallation = 
                        "{ \"schedule\":" + scheduleDataJson(campus,program)+","+
                        "\"rooms\":" + roomsDataJson(campus)+","+
                        "\"slotstype\":" + slotsDataJson() + "," +
                        "\"days\":" + daysDataJson()+"}";
                    return Coallation;
        }
        private string daysDataJson()
        {
            var K = from entity in db.days
                    select new
                    {
                        dayid = entity.dayid,
                        dayname = entity.dayname,
                    };
            return new JavaScriptSerializer().Serialize(K);

        }

        private string roomsDataJson(string campus)
        {
            if (campus == "0")
                campus = "90";
            var K = from entity in db.rooms
                    where entity.campus == campus
                    select new
                    {
                        roomtype = entity.roomtype,
                        roomno = entity.roomno,
                        roomid = entity.roomid,
                        campus = entity.campus
                    };
            return new JavaScriptSerializer().Serialize(K);

        }
        private string slotsDataJson() 
        {
            var K = from entity in db.slottypes
                    where entity.slottype1 == 1
                    select new
                    {
                        subslot = entity.subslot,
                        slottypeid = entity.slottypeid,
                        slottype1 = entity.slottype1,
                        slotno = entity.slotno,
                        duration = entity.duration
                    };
            return new JavaScriptSerializer().Serialize(K);
        }


        private String scheduleDataJson(String campus, String program)
        {
            if (campus == "0")
                campus = "90";

            string query = "SELECT * FROM [schedule2].[dbo].[vschedule] WHERE campus=" + campus;
            if(program!="ALL") query += " AND sectionname LIKE '%" + program + "%'";
            query += " ORDER BY dayid,roomid,slotno";
            
            Console.WriteLine(query);

            var sche = db.vschedules.SqlQuery(query).ToList();
            
            Dictionary<String, List<ScheduleCoallation>> hSchedule = new Dictionary<string,List<ScheduleCoallation>>();
            List<ScheduleCoallation> alSchedule = null;
            String key, pkey = null;

            foreach (var i in sche)
            {

                ScheduleCoallation sc = new ScheduleCoallation();
                sc.occupied = (int)i.occupied;
                sc.offid = (int)i.offid;
                sc.roomid = (int)i.roomid;
                sc.schid = (int)i.schid;
                sc.sectionname = (string)i.sectionname;
                sc.slotid = (int)i.slotid;
                sc.slotno = (int)i.slotno;
                sc.teachername = (string)i.teachername;
                sc.title = (string)i.title;
                sc.dayid = (int)i.dayid;
                sc.duration = (string)i.duration;

                key = i.dayid + "-" + i.roomid + "-" + i.slotno;
                
				if(key.Equals(pkey)){
                    alSchedule.Add(sc);
				} else {
					alSchedule = new List<ScheduleCoallation>();
					alSchedule.Add(sc);
				}

                try
                {
                    hSchedule.Add(key, alSchedule);
                }
                catch (Exception ex)
                {
                    hSchedule[key] = alSchedule;
                    Console.WriteLine(ex.ToString()+" Forcefully Added Value");
                }
				pkey = key;
            }

            string jsonstring = MyDictionaryToJson(hSchedule);

            return jsonstring;
        }
        private string convertToJson (Object s){
            
            return "";
        }
        private string MyDictionaryToJson(Dictionary<string, List<ScheduleCoallation>> dict)
        {
            var entries = dict.Select(d => string.Format("\"{0}\": [{1}]", d.Key, ScheduleParser(d.Value)));
            return "{" + string.Join(",", entries) + "}";
        }

        private string ScheduleParser(List<ScheduleCoallation> scl) {
            string innerObject = "";
            int c = 0;

            foreach (var i in scl)
            {
                c++;
                ScheduleCoallation sc = i;

                innerObject += "{" +
                    "\"occupied\" :\"" + sc.occupied + "\"," +
                    "\"oddis\" :\"" + sc.offid + "\"," +
                    "\"roomid\" :\"" + sc.roomid + "\"," +
                    "\"schid\" :\"" + sc.schid + "\"," +
                    "\"sectionname\" :\"" + sc.sectionname + "\"," +
                    "\"slotid\" :\"" + sc.slotid + "\"," +
                    "\"slotno\" :\"" + sc.slotno + "\"," +
                    "\"teachername\" :\"" + sc.teachername + "\"," +
                    "\"title\" :\"" + sc.title + "\"," +
                    "\"dayid\" :\"" + sc.dayid + "\"," +
                    "\"duration\" :\"" + sc.duration + "\"" +
                "}";
                if(!(c.Equals(scl.Count)))
                    innerObject += ",";
                else
                    c = 0;

            }
            return innerObject;
        }

    }
}