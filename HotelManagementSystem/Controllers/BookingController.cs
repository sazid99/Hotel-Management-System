using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        // In-memory "room inventory" for this module. The Room Management
        // module (RoomController) is currently an empty stub with no data of
        // its own, so this seed lives here for now — swap it out for the
        // shared Room data source once that module is built.
        internal static List<RoomType> rooms = new List<RoomType>
        {
            new RoomType { Id = 1, RoomNumber = "101", Category = "Single", PricePerNight = 2500, Status = "Available", Amenities = "WiFi, AC, TV", Capacity = 1, ImageUrl = "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=600&h=400&fit=crop" },
            new RoomType { Id = 2, RoomNumber = "102", Category = "Single", PricePerNight = 2500, Status = "Available", Amenities = "WiFi, AC, TV", Capacity = 1, ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=600&h=400&fit=crop" },
            new RoomType { Id = 3, RoomNumber = "201", Category = "Double", PricePerNight = 4000, Status = "Available", Amenities = "WiFi, AC, TV, Mini Bar", Capacity = 2, ImageUrl = "https://images.unsplash.com/photo-1566665797739-1674de7a421a?w=600&h=400&fit=crop" },
            new RoomType { Id = 4, RoomNumber = "202", Category = "Double", PricePerNight = 4200, Status = "Available", Amenities = "WiFi, AC, TV, Mini Bar", Capacity = 2, ImageUrl = "https://images.unsplash.com/photo-1584132967334-10e028bd69f7?w=600&h=400&fit=crop" },
            new RoomType { Id = 5, RoomNumber = "301", Category = "Suite",  PricePerNight = 8000, Status = "Available", Amenities = "WiFi, AC, TV, Mini Bar, Jacuzzi", Capacity = 4, ImageUrl = "https://images.unsplash.com/photo-1591088398332-8a7791972843?w=600&h=400&fit=crop" },
            new RoomType { Id = 6, RoomNumber = "302", Category = "Suite",  PricePerNight = 8500, Status = "Under Maintenance", Amenities = "WiFi, AC, TV, Mini Bar, Jacuzzi", Capacity = 4, ImageUrl = "https://images.unsplash.com/photo-1611048267451-e6ed903d4a38?w=600&h=400&fit=crop" },
        };

        private static List<Booking> bookings = new List<Booking>
        {
            new Booking
            {
                Id = 1,
                GuestId = 1,
                Guest = GuestController.guests.FirstOrDefault(g => g.Id == 1),
                RoomId = 3,
                Room = rooms.FirstOrDefault(r => r.Id == 3),
                CheckInDate = DateTime.Today.AddDays(2),
                CheckOutDate = DateTime.Today.AddDays(5),
                TotalPrice = 12000,
                AdvancePayment = 2400,
                Status = "Confirmed",
                CreatedAt = DateTime.Now
            }
        };

        // GET: Booking  -> reservation / booking history
        public IActionResult Index(string statusFilter, string guestSearch)
        {
            var result = bookings.AsEnumerable();

            if (!string.IsNullOrEmpty(statusFilter))
                result = result.Where(b => b.Status == statusFilter);

            if (!string.IsNullOrEmpty(guestSearch))
                result = result.Where(b => b.Guest != null &&
                    b.Guest.Name.Contains(guestSearch, StringComparison.OrdinalIgnoreCase));

            ViewBag.StatusFilter = statusFilter;
            ViewBag.GuestSearch = guestSearch;

            return View(result.OrderByDescending(b => b.CreatedAt).ToList());
        }

        // GET: Booking/Search -> room search by date, type, price range
        public IActionResult Search(DateTime? checkIn, DateTime? checkOut, string? category, decimal? minPrice, decimal? maxPrice)
        {
            ViewBag.CheckIn = checkIn?.ToString("yyyy-MM-dd");
            ViewBag.CheckOut = checkOut?.ToString("yyyy-MM-dd");
            ViewBag.Category = category;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Searched = checkIn.HasValue && checkOut.HasValue;

            if (checkIn == null || checkOut == null)
                return View(new List<RoomType>());

            if (checkOut <= checkIn)
            {
                ModelState.AddModelError(string.Empty, "Check-out date must be after check-in date.");
                return View(new List<RoomType>());
            }

            var available = rooms.Where(r => IsRoomAvailable(r.Id, checkIn.Value, checkOut.Value));

            if (!string.IsNullOrEmpty(category))
                available = available.Where(r => r.Category == category);

            if (minPrice.HasValue)
                available = available.Where(r => r.PricePerNight >= minPrice.Value);

            if (maxPrice.HasValue)
                available = available.Where(r => r.PricePerNight <= maxPrice.Value);

            return View(available.OrderBy(r => r.PricePerNight).ToList());
        }

        private static bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null || room.Status == "Under Maintenance") return false;

            bool hasOverlap = bookings.Any(b =>
                b.RoomId == roomId &&
                b.Status != "Cancelled" &&
                checkIn < b.CheckOutDate && checkOut > b.CheckInDate);

            return !hasOverlap;
        }

        // GET: Booking/Create
        public IActionResult Create(int? roomId, DateTime? checkIn, DateTime? checkOut)
        {
            ViewBag.Guests = GuestController.guests;
            ViewBag.Rooms = rooms.Where(r => r.Status != "Under Maintenance").ToList();

            var booking = new Booking
            {
                RoomId = roomId ?? 0,
                CheckInDate = checkIn ?? DateTime.Today,
                CheckOutDate = checkOut ?? DateTime.Today.AddDays(1)
            };
            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            ModelState.Remove(nameof(Booking.Guest));
            ModelState.Remove(nameof(Booking.Room));

            var room = rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            var guest = GuestController.guests.FirstOrDefault(g => g.Id == booking.GuestId);

            if (room == null)
                ModelState.AddModelError(nameof(booking.RoomId), "Please select a valid room.");

            if (guest == null)
                ModelState.AddModelError(nameof(booking.GuestId), "Please select a valid guest.");

            if (booking.CheckOutDate <= booking.CheckInDate)
                ModelState.AddModelError(nameof(booking.CheckOutDate), "Check-out date must be after check-in date.");
            else if (room != null && !IsRoomAvailable(booking.RoomId, booking.CheckInDate, booking.CheckOutDate))
                ModelState.AddModelError(string.Empty, "This room is no longer available for the selected dates.");

            if (!ModelState.IsValid || room == null || guest == null)
            {
                ViewBag.Guests = GuestController.guests;
                ViewBag.Rooms = rooms.Where(r => r.Status != "Under Maintenance").ToList();
                return View(booking);
            }

            int nights = (booking.CheckOutDate.Date - booking.CheckInDate.Date).Days;
            booking.TotalPrice = nights * room.PricePerNight;

            // Advance payment / deposit: default to 20% of the total unless a
            // larger amount was entered on the form.
            if (booking.AdvancePayment <= 0)
                booking.AdvancePayment = Math.Round(booking.TotalPrice * 0.2m, 2);
            booking.AdvancePayment = Math.Min(booking.AdvancePayment, booking.TotalPrice);

            booking.Id = bookings.Count == 0 ? 1 : bookings.Max(b => b.Id) + 1;
            booking.Guest = guest;
            booking.Room = room;
            booking.Status = "Pending"; // awaiting staff confirmation
            booking.CreatedAt = DateTime.Now;

            bookings.Add(booking);

            TempData["Success"] = "Booking created. It is awaiting confirmation.";
            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        // GET: Booking/Details/5
        public IActionResult Details(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        // GET: Booking/Edit/5 -> modify a reservation
        public IActionResult Edit(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            if (booking.Status == "Cancelled")
            {
                TempData["Error"] = "A cancelled booking cannot be modified.";
                return RedirectToAction(nameof(Details), new { id });
            }

            ViewBag.Guests = GuestController.guests;
            ViewBag.Rooms = rooms;
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Booking updated)
        {
            ModelState.Remove(nameof(Booking.Guest));
            ModelState.Remove(nameof(Booking.Room));

            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            var room = rooms.FirstOrDefault(r => r.Id == updated.RoomId);
            if (room == null)
                ModelState.AddModelError(nameof(updated.RoomId), "Please select a valid room.");

            if (updated.CheckOutDate <= updated.CheckInDate)
                ModelState.AddModelError(nameof(updated.CheckOutDate), "Check-out date must be after check-in date.");
            else if (room != null)
            {
                bool overlapsOther = bookings.Any(b =>
                    b.Id != id &&
                    b.RoomId == updated.RoomId &&
                    b.Status != "Cancelled" &&
                    updated.CheckInDate < b.CheckOutDate && updated.CheckOutDate > b.CheckInDate);

                if (overlapsOther)
                    ModelState.AddModelError(string.Empty, "This room is already booked for the selected dates.");
            }

            if (!ModelState.IsValid || room == null)
            {
                ViewBag.Guests = GuestController.guests;
                ViewBag.Rooms = rooms;
                updated.Id = id;
                return View(updated);
            }

            int nights = (updated.CheckOutDate.Date - updated.CheckInDate.Date).Days;

            booking.RoomId = updated.RoomId;
            booking.Room = room;
            booking.CheckInDate = updated.CheckInDate;
            booking.CheckOutDate = updated.CheckOutDate;
            booking.TotalPrice = nights * room.PricePerNight;
            booking.AdvancePayment = Math.Min(booking.AdvancePayment, booking.TotalPrice);
            booking.Status = "Modified";

            TempData["Success"] = "Booking updated.";
            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        // POST: Booking/Confirm/5
        [HttpPost]
        public IActionResult Confirm(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null && booking.Status != "Cancelled")
            {
                booking.Status = "Confirmed";
                TempData["Success"] = "Reservation confirmed.";
            }
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Booking/Cancel/5
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                booking.Status = "Cancelled";
                TempData["Success"] = "Reservation cancelled.";
            }
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
