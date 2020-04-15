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
    public class NotificationsController : Controller
    {
        //db 
        private HospitalContext db = new HospitalContext();

        //user details
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        // GET: Notification
        // This will be for the admin to see all notifications
        public ActionResult List()
        {
            List<Notifications> notification = db.Notifications.SqlQuery("select * from Notifications").ToList();
            return View(notification);
        }
        //this will be for the user to see the one notification in detail
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Notifications notification = db.Notifications.SqlQuery("select * from Notifications where id = @id", new SqlParameter("@id", id)).FirstOrDefault();

            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }
        //show on homepage
        public ActionResult ShowOnHome()
        {
            List<Notifications> notfications = db.Notifications.SqlQuery("select * from Notifications where active = 1").ToList();

            return PartialView("ShowOnHome", notfications);
        }
        //add customer
        public ActionResult Add()
        {
            List<NotificationTypes> types = db.NotificationTypes.SqlQuery("select * from NotificationTypes").ToList();

            return View(types);
        }
        //add customer after post
        [HttpPost]
        public ActionResult Add(string headline, string content, int type, int? active)
        {
            if (active == null)
            {
                string query = "insert into Notifications (headline, content, typeId) values (@headline, @content, @type)";

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter("@headline", headline);
                parameters[1] = new SqlParameter("@content", content);
                parameters[2] = new SqlParameter("@type", type);

                db.Database.ExecuteSqlCommand(query, parameters);

                return RedirectToAction("List");
            }
            else
            {
                string query = "insert into Notifications (headline, content, typeId, active) values (@headline, @content, @type, @active)";

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter("@headline", headline);
                parameters[1] = new SqlParameter("@content", content);
                parameters[2] = new SqlParameter("@type", type);
                parameters[3] = new SqlParameter("@active", active);

                db.Database.ExecuteSqlCommand(query, parameters);

                return RedirectToAction("List");
            }
        }
        //delete notificaiton
        public ActionResult Delete(int id)
        {
            string query = "delete from Notifications where id = @id";

            SqlParameter parameter = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, parameter);

            return RedirectToAction("List");
        }
        //the view of the update notification
        public ActionResult Update(int id)
        {
            Notifications notification = db.Notifications.SqlQuery("select * from Notifications where id = @id", new SqlParameter("@id", id)).FirstOrDefault();

            List<NotificationTypes> types = db.NotificationTypes.SqlQuery("select * from NotificationTypes").ToList();

            UpdateNotification update = new UpdateNotification();

            update.Notifications = notification;
            update.types = types;


            return View(update);
        }
        //update after post

        [HttpPost]
        public ActionResult Update(int id, string headline, string content, int type, int? active)
        {
            Debug.WriteLine("Updating notification with id " + id + " heading of " + headline + " content " + content + " type is " + type + " active is " + active);

            if (active == null)
            {
                active = 0;
            }
            else
            {
                active = 1;
            }
            string query = "update Notifications set headline = @headline, content = @content, typeId = @type, active = @active where id = @id";

            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@headline", headline);
            parameters[1] = new SqlParameter("@content", content);
            parameters[2] = new SqlParameter("@type", type);
            parameters[3] = new SqlParameter("@active", active);
            parameters[4] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, parameters);

            return RedirectToAction("List");

        }
    }
}