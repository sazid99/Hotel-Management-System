using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {
        // ডামি ফিডব্যাক লিস্ট
        private static List<Feedback> _feedbackList = new List<Feedback>
        {
            new Feedback { Id = 1, GuestName = "Tanvir Ahmed", Email = "tanvir@gmail.com", Rating = 5, Comments = "Great service and wonderful hospitality!" },
            new Feedback { Id = 2, GuestName = "Nusrat Jahan", Email = "nusrat@gmail.com", Rating = 4, Comments = "Clean rooms, but the food delivery was a bit slow." }
        };

        // GET: Feedback
        public IActionResult Index()
        {
            return View(_feedbackList);
        }

        // GET: Feedback/Details/5
        public IActionResult Details(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // GET: Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.Id = _feedbackList.Any() ? _feedbackList.Max(f => f.Id) + 1 : 1;
                feedback.DateSubmitted = System.DateTime.Now;
                _feedbackList.Add(feedback);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // GET: Feedback/Edit/5
        public IActionResult Edit(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            var existingFeedback = _feedbackList.FirstOrDefault(f => f.Id == id);
            if (existingFeedback != null)
            {
                existingFeedback.GuestName = feedback.GuestName;
                existingFeedback.Email = feedback.Email;
                existingFeedback.Rating = feedback.Rating;
                existingFeedback.Comments = feedback.Comments;

                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // GET: Feedback/Delete/5
        public IActionResult Delete(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);
            if (feedback != null)
            {
                _feedbackList.Remove(feedback);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}