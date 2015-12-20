using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entities;
using System.Web.Script.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class courseController : Controller
    {
        vscheduleEntities db = new vscheduleEntities();
        // GET: course
        public ActionResult Index()
        {
            return View("cou_index");
        }
        public string addCourse(string coursejson)
        {
            coursejson.Trim();
            course_add K = new JavaScriptSerializer().Deserialize<course_add>(coursejson);

            var teach = new teacher { 
                teachername = K.teachername
            };
            if (K.addteacher == "true")
            {
                db.teachers.Add(teach);
                db.SaveChanges();
            }

            var cour = new course
                {
                    code = K.code,
                    abbrev = K.abbr,
                    hours = K.hours,
                    title = K.title
                };
            db.courses.Add(cour);
            db.SaveChanges();

            var oc = new offeredcourse{
                    courseid = cour.courseid,
                    teacherid = teach.teacherid,
                    secid = Int32.Parse( K.programid )
                };
            db.offeredcourses.Add(oc);
            db.SaveChanges();

            return coursejson;
        }
        public string getSearchables() 
        {
            string Coallation =
                "{ \"program\":" + getProgram() + "," +
                "\"teachers\":" + getTeachers() + "}";
            return Coallation;

        }
        private string getTeachers()
        {
            var K = from entity in db.teachers
                    select new
                    {
                        value = "<"+entity.teacherid+"> "+entity.teachername,
                        label = entity.teachername
                    };
            return new JavaScriptSerializer().Serialize(K);
        }
        private string getProgram() 
        {
            var K = from entity in db.sections
                    select new
                    {
                        value = "<"+entity.secid+"> "+entity.sectionname,
                        label = entity.sectionname

                    };
            return new JavaScriptSerializer().Serialize(K);
        }
    }
}