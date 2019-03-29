using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ElevenNote.WebMVC.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        [Authorize]
        public ActionResult Index()
        {
            //var model = new NoteListItem[0];
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();


            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);

            //service.CreateNote(model);

            //return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

    }
}