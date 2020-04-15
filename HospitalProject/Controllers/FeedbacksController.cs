using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    public class FeedbacksController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Feedbacks
        public ActionResult List()
        {
            List<Feedbacks> feedbacks = db.Feedbacks.SqlQuery("select * from Feedbacks").ToList();
            return View(feedbacks);
        }
        public ActionResult ShowType(int id)
        {
            FeedbackTypes type = db.FeedbackTypes.SqlQuery("Select * from FeedbackTypes where typeId = @id", new SqlParameter("@id", id)).FirstOrDefault();

            List<Departments> departments = db.Departments.SqlQuery("Select * from Departments join FeedbackTypesDepartments on id = Departments_id where FeedbackTypes_typeId = @id", new SqlParameter("@id", id)).ToList();

            List<Feedbacks> feedbacks = db.Feedbacks.SqlQuery("Select * from Feedbacks where typeId = @id", new SqlParameter("@id", id)).ToList();
            ShowFeedbackType show = new ShowFeedbackType();
            show.type = type;
            show.departments = departments;
            show.feedbacks = feedbacks;
            return View(show);
        }
        public ActionResult Add()
        {
            List<FeedbackTypes> feedbackTypes = db.FeedbackTypes.SqlQuery("Select * from FeedbackTypes").ToList();

            return View(feedbackTypes);
        }
        //add a feedback 
        //user view
        [HttpPost]
        public ActionResult Add(string fname, string lname, string email, string feedback, int type)
        {
            Debug.WriteLine("I am trying to add users feedback with first name: " + fname + " last name: " + lname + " email: " + email +
                " feedback of " + feedback + " type of id " + type);

            string query = "insert into Feedbacks (FirstName, LastName, Email, Feedback, typeId) values (@fname, @lname, @email, @feedback, @type)";

            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@fname", fname);
            sqlParameters[1] = new SqlParameter("@lname", lname);
            sqlParameters[2] = new SqlParameter("@email", email);
            sqlParameters[3] = new SqlParameter("@feedback", feedback);
            sqlParameters[4] = new SqlParameter("@type", type);

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("List");
        }

        //update a feedback 
        //admin view
        public ActionResult Update(int id)
        {
            Feedbacks feedbacks = db.Feedbacks.SqlQuery("select * from feedbacks where id = @id", new SqlParameter("@id", id)).FirstOrDefault();

            List<FeedbackTypes> feedbackTypes = db.FeedbackTypes.SqlQuery("Select * from FeedbackTypes").ToList();

            UpdateFeedback update = new UpdateFeedback();
            update.Feedbacks = feedbacks;
            update.feedbackTypes = feedbackTypes;
            return View(update);
        }
        [HttpPost]
        public ActionResult Update(int id, string fname, string lname, string email, string feedback, int type)
        {
            Debug.WriteLine("I am trying to update feedback with the id of " + id + " with the first name: " + fname + " last name: " + lname + " email: " + email +
                " feedback of " + feedback + " type of id " + type);

            string query = "update Feedbacks set FirstName = @fname, LastName = @lname, Email = @email, Feedback = @feedback, typeId = @type where id = @id";

            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@fname", fname);
            sqlParameters[1] = new SqlParameter("@lname", lname);
            sqlParameters[2] = new SqlParameter("@email", email);
            sqlParameters[3] = new SqlParameter("@feedback", feedback);
            sqlParameters[4] = new SqlParameter("@type", type);
            sqlParameters[5] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("List");
        }

        //delete feedback
        //admin view
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("I am trying to delete the feedbake with the id of "+id);

            string query = "Delete from Feedbacks where id = @id";
            SqlParameter sqlParameter = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParameter);

            return View("List");
        }
        public ActionResult AddDepartment()
        {
            List<Departments> departments = db.Departments.SqlQuery("Select * from departments").ToList();

            List<FeedbackTypes> feedbackTypes = db.FeedbackTypes.SqlQuery("Select * from FeedbackTypes").ToList();

            AddDepartment add = new AddDepartment();
            add.Departments = departments;
            add.Feedbacks = feedbackTypes;

            return View(add);
        }
        //attach feedback to department
        //admin view
        [HttpPost]
        public ActionResult AddDepartment(int feedbacktype, int departmentid)
        {
            Debug.WriteLine("Attach department with the id of :" + departmentid + " to the feedback with the id of :" + feedbacktype);

            //check if they are already attached
            string check = "select * from Departments join FeedbackTypesDepartments on id = Departments_id where Departments_id = @did and FeedbackTypes_typeId = @fid";
            SqlParameter[] sql = new SqlParameter[2];
            sql[0] = new SqlParameter("@did", departmentid);
            sql[1] = new SqlParameter("@fid", feedbacktype);

            
            List<Departments> departments = db.Departments.SqlQuery(check, sql).ToList();
            if (departments.Count <= 0)
            {
                string query = "insert into FeedbackTypesDepartments (Departments_id,FeedbackTypes_typeId) values (@did, @fid)";
                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter("@did", departmentid);
                sqlParameter[1] = new SqlParameter("@fid", feedbacktype);

                db.Database.ExecuteSqlCommand(query, sqlParameter);
            }
            return RedirectToAction("ShowType/"+feedbacktype);
        }
        //detach feedback to department 
        //admin view
        [HttpGet]
        public ActionResult DetachDeparment(int typeId, int did)
        {
            Debug.WriteLine("Deattach department with the id of :" + did + " to the feedback with the id of :" + typeId);

            string query = "delete from FeedbackTypesDepartments where Departments_id = @did and FeedbackTypes_typeId = @fid";
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@did", did);
            sqlParameter[1] = new SqlParameter("@fid", typeId);

            db.Database.ExecuteSqlCommand(query, sqlParameter);

            return RedirectToAction("ShowType/"+typeId);
        }
    }
}