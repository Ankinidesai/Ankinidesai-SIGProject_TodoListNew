using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TodoDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TodoDetails
        public ActionResult Index()
        {
            //this will get us the current user
            //string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);//looking into db
            return View();// db.todoDetails.ToList().Where(x=>x.User==currentUser));
        }

        private IEnumerable<TodoDetail> getMytodos()
        {
             string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);//looking into db
            IEnumerable<TodoDetail> mytodo = db.todoDetails.ToList().Where(x => x.User == currentUser && x.IsParent);
            //for complete percentage
            //int count = 0;
            //foreach(TodoDetail todoDetail in mytodo)
            //{
            //    count++;
            //}
            return mytodo;
        }
        private List<TodoList.Models.TodoDetail> GetNewMyTodos()
        {

            var child = (List<TodoList.Models.TodoDetail>)getMytodosChild().ToList();
            var parent = (List<TodoList.Models.TodoDetail>)getMytodos().ToList();
            List<TodoList.Models.TodoDetail> todo = new List<TodoDetail>();
            foreach (var item in parent)
            {
                todo.Add(item);
                var result = child.Where(ch => ch.ParentID == item.ID).ToList();
                foreach (var ch in result)
                {
                    todo.Add(ch);
                }
            }
            return todo;
        }
        private IEnumerable<TodoDetail> getMytodosChild()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);//looking into db
            IEnumerable<TodoDetail> mytodo = db.todoDetails.ToList().Where(x => x.User == currentUser && !x.IsParent);

            return mytodo;
        }

        public ActionResult tabView()
        {
            //this will get us the current user

            //string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);//looking into db
            //getting the list of todos for the user that is currently logged in the application

            //return PartialView("_tabView", getMytodos());//,db.todoDetails.ToList().Where(x => x.User == currentUser));
            return PartialView("_tabView", GetNewMyTodos());//,db.todoDetails.ToList().Where(x => x.User == currentUser));
        }


        public ActionResult allTodos()
        {
            return View(db.todoDetails.ToList());
        }

        // GET: TodoDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoDetail todoDetail = db.todoDetails.Find(id);
            if (todoDetail == null)
            {
                return HttpNotFound();
            }
            return View(todoDetail);
        }

        // GET: TodoDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateChild(int id)
        {
            ViewBag.ParentId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChild([Bind(Include = "ID,Description,DeadlineDate,Isdone")] TodoDetail todoDetail)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                todoDetail.User = currentUser;
                todoDetail.IsParent = false;
                todoDetail.ParentID = todoDetail.ID;
                todoDetail.ID = 0;

                db.todoDetails.Add(todoDetail);
                db.SaveChanges();
              return RedirectToAction("Index");
            }

            return View(todoDetail);
        }
        //public ActionResult CreateChild(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TodoDetail todoDetail = db.todoDetails.Find(id);
        //    if (todoDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View();
        //}

        // POST: TodoDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,DeadlineDate")] TodoDetail todoDetail)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x=>x.Id==currentUserId);
                todoDetail.User = currentUser;
                todoDetail.IsParent = true;
                todoDetail.ParentID = 0;

                db.todoDetails.Add(todoDetail);
                db.SaveChanges();
               return RedirectToAction("Index");
            }

            //return View(todoDetail);
            return PartialView("_tabView", GetNewMyTodos());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "ID,Description,DeadlineDate")] TodoDetail todoDetail)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                todoDetail.User = currentUser;
                todoDetail.Isdone = false;//because we have just created

                db.todoDetails.Add(todoDetail);
                db.SaveChanges();
                //return RedirectToAction("Index"); //no need to redirect but we will return the partial view
                
            }

            //return PartialView("_tabView", getMytodos());
            return PartialView("_tabView", GetNewMyTodos());
        }

        // GET: TodoDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoDetail todoDetail = db.todoDetails.Find(id);
            if (todoDetail == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);//looking into db
            
            if(todoDetail.User!=currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(todoDetail);
        }

        // POST: TodoDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for runs when we click save
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,DeadlineDate,Isdone")] TodoDetail todoDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("_tabView", GetNewMyTodos());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AjaxEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoDetail todoDetail = db.todoDetails.Find(id);
            if (todoDetail == null)
            {
                return HttpNotFound();
            }
            else
            {
                todoDetail.Isdone = value;
                db.Entry(todoDetail).State = EntityState.Modified; //looks the todo and modifies the changes
                db.SaveChanges();
                //return PartialView("_tabView", getMytodos());
                return PartialView("_tabView", GetNewMyTodos());
            }

            //if (ModelState.IsValid)
            //{
            //    db.Entry(todoDetail).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(todoDetail);
        }

        // GET: TodoDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoDetail todoDetail = db.todoDetails.Find(id);
            if (todoDetail == null)
            {
                return HttpNotFound();
            }
            return View(todoDetail);
        }

        // POST: TodoDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            TodoDetail todoDetail = db.todoDetails.Find(id);
            db.todoDetails.Remove(todoDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
