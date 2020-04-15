using HospitalProject.Data;
using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    public class FrequentlyAskedQuestionController : Controller
    {
        // GET: FrequentlyAskedQuestion
        public ActionResult Index()
        {
            return View();
        }

        //Creating a db object of the Hospita Context file. 
        private HospitalContext db = new HospitalContext();

        //This method is used to get the list of FAQs for the admin
        public ActionResult ListFAQAdmin(string searchKey, int pageNum = 0)
        {
            Debug.WriteLine("Entered ListFAQs..");
            Debug.WriteLine("Entered Search key is" + searchKey);

            //Query to get the list of FAQs
            string query = "select * from FrequentlyAskedQuestions";

            if (searchKey != null)
            {
                query = query + " where Category like '%" + searchKey + "%'";

            }
            List<FrequentlyAskedQuestion> FAQ = db.FrequentlyAskedQuestions.SqlQuery(query).ToList();

            /****************************************************************************************************************************************************
             Pagination code cited from below source: 
             Author: Christine Bittle
             Site: https://github.com/christinebittle/PetGroomingMVC/blob/master/PetGrooming/Controllers/PetController.cs
             Purpose: Pagination Algorithm for List. 
             ***************************************************************************************************************************************************--*/
            int recordsPerPage = 5;
            int faqCount = FAQ.Count();

            //Below line calculates the number of pages needed in pagination
            int maxPage = (int)Math.Ceiling((decimal)faqCount / recordsPerPage) - 1;
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
                string currentQuery = query + " order by id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(currentQuery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + searchKey);
                FAQ = db.FrequentlyAskedQuestions.SqlQuery(currentQuery, newparams.ToArray()).ToList();
            }
            /************************************************************************************************************************************************
             * End of Pagination Algorithm 
             ***********************************************************************************************************************************************/
            
            return View(FAQ);
        }

        // GET: Function is used to call the view that will display the Add FAQ form
        public ActionResult AddFAQAdmin()
        {
            Debug.WriteLine("Adding new FAQ...");
            return View();
        }

        //POST: Method to get the values and add it to the FAQ table for the admin
        [HttpPost]
        public ActionResult AddFAQAdmin(string questions, string answers, string category)
        {
            Debug.WriteLine("Adding a new FAQ");
            Debug.WriteLine(questions + answers + category);

            //Query to create a new FAQ
            string query = "insert into FrequentlyAskedQuestions (questions,answers,category) values (@questions,@answers,@category)";
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@questions", questions);
            sqlParams[1] = new SqlParameter("@answers", answers);
            sqlParams[2] = new SqlParameter("@category", category);

            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to FAQ list admin view. 
            return RedirectToAction("ListFAQAdmin");
        }

        //GET: This function is used to fetch the data that will be displayed in the update FAQ view. 
        public ActionResult UpdateFAQAdmin(int id)
        {
            Debug.WriteLine("Entering Update FAQ");
            Debug.WriteLine("Fetching data to be displayed in the update FAQ page");

            //Query to get the FAQ Details with specific FAQ id. 
            string query = "select * from FrequentlyAskedQuestions where id = @id";
            FrequentlyAskedQuestion FAQ = db.FrequentlyAskedQuestions.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault();

            //Calling the FAQ view with the Specific FAQ details
            return View(FAQ);
        }

        //POST: This function is used to Update the FAQ table with the data entered in the update FAQ page form. 
        [HttpPost]
        public ActionResult UpdateFAQAdmin(int? id, string questions, string answers, string category)
        {
            Debug.WriteLine("Updating FAQ " + id);
            Debug.WriteLine("Updating the data entered in the form to the FAQ table ");

            //Query to update the FAQ details into the FAQ table
            string query = "update FrequentlyAskedQuestions set questions = @questions,answers = @answers,  category = @category where id = @id";
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@questions", questions);
            sqlParams[1] = new SqlParameter("@answers", answers);
            sqlParams[2] = new SqlParameter("@category", category);
            sqlParams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to the List FAQ Admin view. 
            return RedirectToAction("ListFAQAdmin");
        }

        //GET: Function to delete a FAQ in the FAQ table. 
        public ActionResult DeleteFAQAdmin(int id)
        {
            Debug.WriteLine("Deleting FAQ with the ID: ");
            Debug.WriteLine(id);

            //Query to delete the FAQ with the specific FAQ id
            string query = "delete from FrequentlyAskedQuestions where id = @id";
            Debug.WriteLine(query);
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlParams);

            //Redirecting the control back to List Jobs view. 
            return RedirectToAction("ListFAQAdmin");
        }

        //GET: Function to display the list of FAQs for the user
        public ActionResult ListFAQ(string searchKey, int pageNum = 0)
        {
            Debug.WriteLine("Entered ListFAQs..");
            Debug.WriteLine("Entered Search key is" + searchKey);

            //Query to select the list of FAQs
            string query = "select * from FrequentlyAskedQuestions";

            if (searchKey != null)
            {
                query = query + " where Category like '%" + searchKey + "%'";
            }
            List<FrequentlyAskedQuestion> FAQ = db.FrequentlyAskedQuestions.SqlQuery(query).ToList();

            /****************************************************************************************************************************************************
             Pagination code cited from below source: 
             Author: Christine Bittle
             Site: https://github.com/christinebittle/PetGroomingMVC/blob/master/PetGrooming/Controllers/PetController.cs
             Purpose: Pagination Algorithm for List. 
             ***************************************************************************************************************************************************--*/
            int recordsPerPage = 5;
            int faqCount = FAQ.Count();

            //Below line calculates the number of pages needed in pagination
            int maxPage = (int)Math.Ceiling((decimal)faqCount / recordsPerPage) - 1;
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
                string currentQuery = query + " order by id offset @start rows fetch first @perpage rows only ";
                FAQ = db.FrequentlyAskedQuestions.SqlQuery(currentQuery, newparams.ToArray()).ToList();
            }
            /************************************************************************************************************************************************
             * End of Pagination Algorithm 
             ***********************************************************************************************************************************************/

            return View(FAQ);
        }
    }
}
 