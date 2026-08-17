using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class BillingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: Billing/Details/5
        public IActionResult Details(int id)
        {
            // ডামি ডাটা তৈরি করে পাস করা হলো যাতে নাল এরর না আসে
            var sampleInvoice = new Invoice
            {
                Id = id > 0 ? id : 101,
                BookingId = 15,
                IssueDate = DateTime.Now,
                Status = "Paid",
                TotalAmount = 350.00m,
                PaidAmount = 350.00m,
                DueAmount = 0.00m
            };

            return View(sampleInvoice);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        public IActionResult ProcessPayment(int invoiceId)
        {
            var payment = new Payment { InvoiceId = invoiceId };
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
    }
}