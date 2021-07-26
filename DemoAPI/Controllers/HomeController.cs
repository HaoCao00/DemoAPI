﻿using Lib;
using Lib.Entities;
using Lib.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoAPI.Controllers
{
    public class HomeController : Controller
    {
        StudentService stService = new StudentService();
        ChessService chessService = new ChessService();
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {


            ViewBag.Title = "Home Page";
            /*ApplicationDbContext _dbContext;
            _dbContext = new ApplicationDbContext();
            Lib.Entities.Student st = new Lib.Entities.Student();
            st.Id = Guid.NewGuid();
            st.Name = "test";
            st.IdentifyCode = "test 2";
            _dbContext.Student.Add(st);
            _dbContext.SaveChanges();
            */
            //insertRoom();
            return View(db.Room.ToList());

        }
        public void insertRoom()
        {
            Room r = new Room();
            r.Id = Guid.NewGuid();
            r.Name = "test";
            chessService.insertRoom(r);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            HttpClient client = new HttpClient();
            var request = $"https://api2.scaledrone.com/UIK9xMxnbI3lkd35/observable-" + id + @"/members";
            var member = await client.GetStringAsync(request);
            var membercount = member.Split(',').Count();
            if (membercount >= 2)
            {
                Response.Write("<script>alert('Phòng đã đầy');</script>");
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            Lib.Entities.Global.RoomId = room.Id;
            return View(room);
        }

        public ActionResult Create()
        {
            Random rd = new Random();
            Room room = new Room();
            room.Id = Guid.NewGuid();
            room.Name = "Phòng " + rd.Next(1000, 10000);
            db.Room.Add(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Room room = db.Room.Find(id);
            db.Room.Remove(room);
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
