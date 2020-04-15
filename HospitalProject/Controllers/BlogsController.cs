using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.IO;
using HospitalProject.Data;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class BlogsController : Controller
    {
        //create the db context to connect to the database
        private HospitalContext db = new HospitalContext();

        // GET: Blogs
        //get a list of all blogs in the index page
        public ActionResult Index()
        {
            //redirecting to the List method
            return RedirectToAction("List");
        }
        //If the user searches a keyword
        public ActionResult ListAdmin(string searchkey)
        {
            //check that it has a value (not null and not empty)
            if (!String.IsNullOrEmpty(searchkey))
            {
                //check the keyword in various columns (Title, Body, Date)
                List<Blog> blogs = db.Blogs.Where(blog =>
                        blog.Title.Contains(searchkey) ||
                        blog.Body.Contains(searchkey)).ToList();
                return View(blogs);
            }//if not show the list of all blogs
            else
            {
                List<Blog> blogs = db.Blogs.ToList();
                return View(blogs);
            }
        }        //If the user searches a keyword
        public ActionResult List(string searchkey)
        {
            //check that it has a value (not null and not empty)
            if (!String.IsNullOrEmpty(searchkey))
            {
                //check the keyword in various columns (Title, Body, Date -> first convert to string :https://stackoverflow.com/questions/901332/how-do-i-filter-linq-query-by-date)
                List<Blog> blogs = db.Blogs.Where(blog =>
                    blog.Title.Contains(searchkey) ||
                    blog.Body.Contains(searchkey)).ToList();
                return View(blogs);
            }//if not show the list of all blogs
            else
            {
                List<Blog> blogs = db.Blogs.ToList();
                return View(blogs);
            }
        }

        //Details of a blog post (News)
        public ActionResult Show(int id)
        {
            Blog selectedBlog = db.Blogs.Find(id);
            return View(selectedBlog);
        }

        //Add action for the GET request to show the page
        //GET: Blogs/Add
        public ActionResult Add()
        {
            return View();
        }

        //add function: A post request to submit the form
        [HttpPost]
        //model binding
        public ActionResult Add(Blog blog)
        {
            Debug.WriteLine(blog);
            //add the blog to the dbcontext :not the database but to the memory 
            db.Blogs.Add(blog);
            //add to the database
            db.SaveChanges();
            //go back to the list of Blogs in Admin view
            return RedirectToAction("ListAdmin");
        }

        //Get request for the confirmation page
        public ActionResult ConfirmDelete(int id)
        {
            Blog selectedBlog = db.Blogs.Find(id);
            return View(selectedBlog);
        }
        //Post request to delete the blog
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Blog selectedBlog = db.Blogs.Find(id);

            //remove from the dbcontext
            db.Blogs.Remove(selectedBlog);
            //delete from the actual database
            db.SaveChanges();
            return RedirectToAction("ListAdmin");
        }
        //get update page of a specific blog
        //GET: Update/id
        public ActionResult Update(int id)
        {
            //find the blog in the db
            Blog blog = db.Blogs.Find(id);
            //display the update blog page
            return View(blog);
        }
        //Post request to make changes to in the update page
        [HttpPost]
        public ActionResult Update(Blog blog)
        {
            var selectedBlog = db.Blogs.Single(b => b.Id == blog.Id);
            //binding params
            selectedBlog.Title = blog.Title;
            selectedBlog.Body = blog.Body;
            //change the db
            db.SaveChanges();
            //back to the List
            return RedirectToAction("ListAdmin");
        }


    }
}