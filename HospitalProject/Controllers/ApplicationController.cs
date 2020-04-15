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
    public class ApplicationController : Controller
    {
        // GET: Application Index page
        public ActionResult Index()
        {
            return RedirectToAction("ListApplicationsAdmin");
        }

        //Creating a db object of the HospitalContext file.
        private HospitalContext db = new HospitalContext();


        //This method is used to get the list of appications for the admin
        public ActionResult ListApplicationsAdmin(string searchKey, int pageNum=0)
        {
            Debug.WriteLine("Entered ApplicationList..");
            Debug.WriteLine("Entered Search key is" + searchKey);

            //Query to get the list of applications from the Applications table
            string query = "select * from Applications";

            //Code to implement Search Box feature
            if (searchKey != null)
            {
                query = query + " where firstName like '%" + searchKey + "%' OR lastName like '%" + searchKey + "%' ";

            }
            //executing the above query
            List<Application> applications = db.Applications.SqlQuery(query).ToList();



            /****************************************************************************************************************************************************
             Pagination code cited from below source: 
             Author: Christine Bittle
             Site: https://github.com/christinebittle/PetGroomingMVC/blob/master/PetGrooming/Controllers/PetController.cs
             Purpose: Pagination Algorithm for List. 
            ***************************************************************************************************************************************************--*/
            int recordsPerPage = 5;
            int applicationCount = applications.Count();

            //Below line calculates the number of pages needed in pagination
            int maxPage = (int)Math.Ceiling((decimal)applicationCount / recordsPerPage) - 1;
            if (maxPage < 0) maxPage = 0;
            if (pageNum < 0) pageNum = 0;
            if (pageNum > maxPage) pageNum = maxPage;
            int start = (int)(recordsPerPage * pageNum);
            ViewData["pageNum"] = pageNum;
            ViewData["pageSummary"] = "";

            //If number of pages greater than 0 the below code will execute
            if (maxPage > 0)
            {
                //Below line calculates the current page number in pagination
                ViewData["pageSummary"] = (pageNum + 1) + " of " + (maxPage + 1);
                List<SqlParameter> newparams = new List<SqlParameter>();

                if (searchKey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + searchKey + "%"));
                    ViewData["searchKey"] = searchKey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", recordsPerPage));
                string currentQuery = query + " order by applicationId offset @start rows fetch first @perpage rows only ";
                applications = db.Applications.SqlQuery(currentQuery, newparams.ToArray()).ToList();
            }
            /************************************************************************************************************************************************
             * End of Pagination Algorithm 
             ***********************************************************************************************************************************************/
            
            //Query to fetch the list of jobs. This is needed to show the job name of each application in the view.
            query = "select * from Jobs";
            List<Job> jobs = db.Jobs.SqlQuery(query).ToList();

            //Creating an object for the JobsApplications ViewModel and adding the result of the above 2 queries. 
            JobsApplications jobsApplications = new JobsApplications();
            jobsApplications.applicationList = applications;
            jobsApplications.jobList = jobs;

            return View(jobsApplications);
        }

        // GET: Function is used to fetch the list of jobs for the admin to choose while creating a new application.
        public ActionResult AddApplicationAdmin()
        {
            Debug.WriteLine("Adding new Appcliation...");
            Debug.WriteLine("Getting list of jobs for the admin to choose while creating a new application...");

            //Query to get the list of jobs
            string query = "select * from jobs";
            List<Job> jobs = db.Jobs.SqlQuery(query).ToList();


            return View(jobs);
        }

        //POST: Method to get the values from the Add Application form and insert it into the database for the Admin
        [HttpPost]
        public ActionResult AddApplicationAdmin(string firstName, string lastName, string emailId, string coverLetter, int jobId)
        {
            Debug.WriteLine("Adding a new Application");

            //Query to insert an Application into the database. 
            string query = "insert into Applications (firstName,lastName,emailId,coverLetter,jobId) values (@firstName,@lastName,@emailId,@coverLetter,@jobId)";
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@firstName", firstName);
            sqlParams[1] = new SqlParameter("@lastName", lastName);
            sqlParams[2] = new SqlParameter("@emailId", emailId);
            sqlParams[3] = new SqlParameter("@coverLetter", coverLetter);
            sqlParams[4] = new SqlParameter("@jobId", jobId);
            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to Applications List. 
            return RedirectToAction("ListApplicationsAdmin");
        }

        //GET: Function to get the details of a specific Application for the Admin
        public ActionResult ShowApplicationAdmin(int id)
        {
            Debug.WriteLine("Entering Show Application");
            Debug.WriteLine("Entered Application ID is:" + id);

            //This query gets the Application with the specified applicationId
            string query = "select * from Applications where ApplicationId = @ApplicationId";
            Application application = db.Applications.SqlQuery(query, new SqlParameter("@ApplicationId", id)).FirstOrDefault();

            return View(application);
        }

        //GET: This function is used to fetch the data that will be displayed in the Update Application form for the Admin. 
        public ActionResult UpdateApplicationAdmin(int id)
        {
            Debug.WriteLine("Entering Update Application");
            Debug.WriteLine("Fetching data to be displayed in the update application page");

            //Query to get the job Details with specific jobId id. 
            string query = "select * from Applications where ApplicationId = @id";
            Application application = db.Applications.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault();

            //Calling the view with the current Application details which the Admin can update
            return View(application);
        }

        //POST: This function is used to Update the Application table with the data entered in the Update Application page form for the Admin
        [HttpPost]
        public ActionResult UpdateApplicationAdmin(int? id, string firstName, string lastName, string emailId, string coverLetter,int jobId)
        {
            Debug.WriteLine("Updating Application with job ID" + id);
            Debug.WriteLine("Updating the data entered in the form to the Job table ");

            //Query to update the Application details in the Application table
            string query = "update Applications set firstName = @firstName, lastName = @lastName,emailId = @emailId,coverLetter = @coverLetter, JobId = @jobId where applicationId = @applicationId";
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@firstName", firstName);
            sqlParams[1] = new SqlParameter("@lastName", lastName);
            sqlParams[2] = new SqlParameter("@emailId", emailId);
            sqlParams[3] = new SqlParameter("@coverLetter", coverLetter);
            sqlParams[4] = new SqlParameter("@jobId", jobId);
            sqlParams[5] = new SqlParameter("@applicationId", id);
            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to the Application List viesw. 
            return RedirectToAction("ListApplicationsAdmin");
        }

        //GET: Function to delete an Application in the Application table. 
        public ActionResult DeleteApplicationAdmin(int id)
        {
            Debug.WriteLine("Deleting Applications with the ID: ");
            Debug.WriteLine(id);

            //Query to delete an Application with the specific Application id
            string query = "delete from Applications where applicationId = @ApplicationId";
            Debug.WriteLine(query);
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ApplicationId", id);
            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to Application List view. 
            return RedirectToAction("ListApplicationsAdmin");
        }
    }
}