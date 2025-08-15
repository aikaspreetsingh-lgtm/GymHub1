using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GymHub.Data;  
using GymHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymHub.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public UserController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.TotalProgressMetrics = _context.ProgressMetric
                .Count(pm => pm.UserId == userId);

            ViewBag.TotalWorkoutSession = _context.WorkoutSession
                .Count(ws => ws.UserId == userId);

            ViewBag.TotalWorkoutSet = _context.WorkoutSet
                .Where(ws => ws.WorkoutSession.UserId == userId)
                .Count();

            ViewBag.UpcomingClassBooking = _context.ClassBooking
             .Count(cb => cb.UserId == userId);

            return View();
        }

        //WorkoutSession
        public async Task<IActionResult> WorkoutSession()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessions = await _context.WorkoutSession
                   .Include(ws => ws.User)
        .Where(ws => ws.UserId == userId)
        .ToListAsync();

               

            return View("~/Views/User/WorkoutSession.cshtml", sessions);
        }

        // GET: WorkoutSessionsDetails
        public async Task<IActionResult> WorkoutSessionDetails(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _context.WorkoutSession
                .Include(ws => ws.Sets)
                .ThenInclude(s => s.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (session == null) return NotFound();

            return View("~/Views/User/WorkoutSessionDetails.cshtml", session);
        }

        // GET:WorkoutSessionCreate
        public IActionResult WorkoutSessionCreate()
        {
           
            var ws = new WorkoutSession
            {
                PerformedOn = DateTime.Today
            };
            return View("~/Views/User/WorkoutSessionCreate.cshtml", ws);
        }

        // POST: WorkoutSessionCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutSessionCreate([Bind("Id,PerformedOn,Notes")] WorkoutSession 
            workoutSession)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            workoutSession.UserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(workoutSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(WorkoutSession));
            }

            return View("~/Views/User/WorkoutSessionCreate.cshtml", workoutSession);
        }

        // GET: WorkoutSessionEdit
        public async Task<IActionResult> WorkoutSessionEdit(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutSession = await _context.WorkoutSession
                .FirstOrDefaultAsync(ws => ws.Id == id && ws.UserId == userId);
            if (workoutSession == null) return NotFound();
            return View("~/Views/User/WorkoutSessionEdit.cshtml", workoutSession);
        }
        // POST: WorkoutSessionsEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutSessionEdit(int id, [Bind("Id,PerformedOn,Notes")] WorkoutSession workoutSession)
        {
            if (id != workoutSession.Id) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existing = await _context.WorkoutSession
                .AsNoTracking()
                .FirstOrDefaultAsync(ws => ws.Id == id && ws.UserId == userId);
            if (existing == null) return NotFound();
            workoutSession.UserId = userId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutSessionExists(workoutSession.Id, userId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(WorkoutSession));
            }
            return View("~/Views/User/WorkoutSessionEdit.cshtml", workoutSession);
        }

        // GET: WorkoutSessionDelete
        public async Task<IActionResult> WorkoutSessionDelete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutSession = await _context.WorkoutSession
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (workoutSession == null) return NotFound();

            return View("~/Views/User/WorkoutSessionDelete.cshtml", workoutSession);
        }

        // POST: WorkoutSessionsDelete
        [HttpPost, ActionName("WorkoutSessionDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutSessionDeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workoutSession = await _context.WorkoutSession
                .FirstOrDefaultAsync(ws => ws.Id == id && ws.UserId == userId);

            if (workoutSession == null) return NotFound();

            _context.WorkoutSession.Remove(workoutSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkoutSession));
        }

        private bool WorkoutSessionExists(int id, string userId)
        {
            return _context.WorkoutSession.Any(e => e.Id == id && e.UserId == userId);
        }

        //ProgessMetric
        public async Task<IActionResult> ProgressMetric()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var metrics = await _context.ProgressMetric
                .Where(pm => pm.UserId == userId)
                .OrderByDescending(pm => pm.LoggedAt)
                .ToListAsync();

            return View("~/Views/User/ProgressMetric.cshtml", metrics);
        }

        // DETAILS
        public async Task<IActionResult> ProgressMetricDetails(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var metric = await _context.ProgressMetric
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.UserId == userId);

            if (metric == null) return NotFound();

            return View("~/Views/User/ProgressMetricDetails.cshtml", metric);
        }

        // CREATE (GET)
        public IActionResult ProgressMetricCreate()
        {
            var model = new ProgressMetric
            {
                LoggedAt = DateTime.Now
            };

            return View("~/Views/User/ProgressMetricCreate.cshtml", model);
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProgressMetricCreate([Bind("Id,LoggedAt,WeightKg,BodyFatPercent")] ProgressMetric metric)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            metric.UserId = userId;

            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
            {
                return View("~/Views/User/ProgressMetricCreate.cshtml", metric);
            }

            _context.ProgressMetric.Add(metric);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProgressMetric));
        }

        // EDIT (GET)
        public async Task<IActionResult> ProgressMetricEdit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var metric = await _context.ProgressMetric
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.UserId == userId);

            if (metric == null) return NotFound();

            return View("~/Views/User/ProgressMetricEdit.cshtml", metric);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProgressMetricEdit(int id, [Bind("Id,LoggedAt,WeightKg,BodyFatPercent")] ProgressMetric metric)
        {
            if (id != metric.Id) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure it belongs to the current user
            var existing = await _context.ProgressMetric
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.UserId == userId);

            if (existing == null) return NotFound();

            metric.UserId = userId; 

            if (!ModelState.IsValid)
            {
                return View("~/Views/User/ProgressMetricEdit.cshtml", metric);
            }

            try
            {
                _context.Update(metric);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressMetricExists(metric.Id, userId))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(ProgressMetric));
        }

        // DELETE (GET)
        public async Task<IActionResult> ProgressMetricDelete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var metric = await _context.ProgressMetric
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.UserId == userId);

            if (metric == null) return NotFound();

            return View("~/Views/User/ProgressMetricDelete.cshtml", metric);
        }

        // DELETE (POST)
        [HttpPost, ActionName("ProgressMetricDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProgressMetricDeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var metric = await _context.ProgressMetric
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.UserId == userId);

            if (metric == null) return NotFound();

            _context.ProgressMetric.Remove(metric);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProgressMetric));
        }

        private bool ProgressMetricExists(int id, string userId)
        {
            return _context.ProgressMetric.Any(e => e.Id == id && e.UserId == userId);
        }

        //WorkoutSet
        public async Task<IActionResult> WorkoutSet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workoutSets = await _context.WorkoutSet
                .Include(ws => ws.Exercise)
                .Include(ws => ws.WorkoutSession)
                .Where(ws => ws.WorkoutSession.UserId == userId)
                .ToListAsync();

            return View("~/Views/User/WorkoutSet.cshtml", workoutSets);
        }

        // DETAILS
        public async Task<IActionResult> WorkoutSetDetails(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workoutSet = await _context.WorkoutSet
                .Include(ws => ws.Exercise)
                 
                .Include(ws => ws.WorkoutSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.WorkoutSession.UserId == userId);

            if (workoutSet == null) return NotFound();

            return View("~/Views/User/WorkoutSetDetails.cshtml", workoutSet);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult WorkoutSetCreate(int workoutSessionId)  
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new WorkoutSet
            {
                WorkoutSessionId = workoutSessionId
            };
            ViewBag.ExerciseId = new SelectList(_context.Exercises, "Id", "Name");
            ViewBag.WorkoutSessionId = new SelectList(
                _context.WorkoutSession.Where(ws => ws.UserId == userId),
                "Id", "PerformedOn", workoutSessionId
            );
            PopulateDropdowns(model, userId);
            return View("~/Views/User/WorkoutSetCreate.cshtml", model);
        }
        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WorkoutSetCreate(WorkoutSet model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var kvp in ModelState.Where(m => m.Value.Errors.Count > 0))
            {
                var errors = string.Join(" | ", kvp.Value.Errors.Select(e => e.ErrorMessage));
                System.Diagnostics.Debug.WriteLine($"{kvp.Key} -> {errors}");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ExerciseId = new SelectList(_context.Exercises, "Id", "Name", model.ExerciseId);
                ViewBag.WorkoutSessionId = new SelectList(
                    _context.WorkoutSession.Where(ws => ws.UserId == userId),
                    "Id", "PerformedOn", model.WorkoutSessionId
                );
                PopulateDropdowns(model, userId); 
                return View("~/Views/User/WorkoutSetCreate.cshtml", model);
            }
            _context.WorkoutSet.Add(model);
            _context.SaveChanges();
            return RedirectToAction("WorkoutSet", "User",
                new { workoutSessionId = model.WorkoutSessionId });
        }
        private void PopulateDropdowns(WorkoutSet model, string userId)
        {
            ViewBag.ExerciseId = new SelectList(
                _context.Exercises.ToList(),
                "Id",
                "Name",
                model.ExerciseId
            );

            ViewBag.WorkoutSessionId = new SelectList(
                _context.WorkoutSession.Where(ws => ws.UserId == userId).ToList(),
                "Id",
                "PerformedOn",
                model.WorkoutSessionId
            );
        }

        // EDIT (GET)
        public async Task<IActionResult> WorkoutSetEdit(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workoutSet = await _context.WorkoutSet
                .Include(ws => ws.WorkoutSession)
                .FirstOrDefaultAsync(ws => ws.Id == id && ws.WorkoutSession.UserId == userId);

            if (workoutSet == null) return NotFound();

            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name", workoutSet.ExerciseId);
            ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSession.Where(ws => ws.UserId == userId), "Id", "PerformedOn", workoutSet.WorkoutSessionId);

            return View("~/Views/User/WorkoutSetEdit.cshtml", workoutSet);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutSetEdit(int id, [Bind("Id,Reps,Weight,ExerciseId,WorkoutSessionId")] WorkoutSet workoutSet)
        {
            if (id != workoutSet.Id) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _context.WorkoutSession.FirstOrDefaultAsync(ws => ws.Id == workoutSet.WorkoutSessionId && ws.UserId == userId);
            if (session == null)
            {
                ModelState.AddModelError("", "Invalid workout session selected.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name", workoutSet.ExerciseId);
                ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSession.Where(ws => ws.UserId == userId), "Id", "PerformedOn", workoutSet.WorkoutSessionId);
                return View("~/Views/WorkoutSet/Edit.cshtml", workoutSet);
            }

            try
            {
                _context.Update(workoutSet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.WorkoutSet.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(WorkoutSet));
        }

        // DELETE (GET)
        public async Task<IActionResult> WorkoutSetDelete(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workoutSet = await _context.WorkoutSet
                .Include(ws => ws.Exercise)
                .Include(ws => ws.WorkoutSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.WorkoutSession.UserId == userId);

            if (workoutSet == null) return NotFound();

            return View("~/Views/User/WorkoutSetDelete.cshtml", workoutSet);
        }

        // DELETE (POST)
        [HttpPost, ActionName("WorkoutSetDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutSetDeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workoutSet = await _context.WorkoutSet
                .Include(ws => ws.WorkoutSession)
                .FirstOrDefaultAsync(ws => ws.Id == id && ws.WorkoutSession.UserId == userId);

            if (workoutSet == null) return NotFound();

            _context.WorkoutSet.Remove(workoutSet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkoutSet));
        }
        private bool WorkoutsetExists(int id, string userId)
        {
            return _context.ProgressMetric.Any(e => e.Id == id && e.UserId == userId);
        }





        //Class Booking
        public async Task<IActionResult> ClassBooking()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookings = await _context.ClassBooking
                .Include(cb => cb.GymClass)
                .Include(cb => cb.User)
                .Where(cb => cb.UserId == userId)
                .ToListAsync();

            return View("~/Views/User/ClassBooking.cshtml", bookings);
        }

        // DETAILS
        public async Task<IActionResult> ClassBookingDetails(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = await _context.ClassBooking
                .Include(cb => cb.GymClass)
                .Include(cb => cb.User)
                .FirstOrDefaultAsync(cb => cb.Id == id && cb.UserId == userId);

            if (booking == null) return NotFound();

            return View("~/Views/User/ClassBookingDetails.cshtml", booking);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult ClassBookingCreate()
        {
            var gymClasses = _context.GymClasses
                .Where(gc => gc.StartsAt > DateTime.Now)
                .OrderBy(gc => gc.StartsAt)
                .ToList();

            ViewBag.GymClassId = new SelectList(gymClasses, "Id", "Title");
            return View("~/Views/User/ClassBookingCreate.cshtml", new ClassBooking());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassBookingCreate(ClassBooking model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ModelState.Remove(nameof(model.UserId));
            ModelState.Remove(nameof(model.User));
            ModelState.Remove(nameof(model.GymClass));
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.BookedAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                var gymClasses = _context.GymClasses
                    .Where(gc => gc.StartsAt > DateTime.Now)
                    .OrderBy(gc => gc.StartsAt)
                    .ToList();

                ViewBag.GymClassId = new SelectList(gymClasses, "Id", "Title");
                return View("~/Views/User/ClassBookingCreate.cshtml", model);
            }

            _context.ClassBooking.Add(model);
            var affected = await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(ClassBooking));
        }

        // EDIT (GET)
        public async Task<IActionResult> ClassBookingEdit(int? id)
        {
            if (id == null) return NotFound();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new NullReferenceException("User is not logged in.");
            var classBooking = await _context.ClassBooking
                .FirstOrDefaultAsync(cb => cb.Id == id && cb.UserId == userId);
            if (classBooking == null)
                throw new NullReferenceException($"No booking found with Id={id} for UserId={userId}.");

            var gymClasses = await _context.GymClasses.ToListAsync();
            if (!gymClasses.Any())
                throw new NullReferenceException("No GymClasses found.");
            ViewData["GymClassId"] = new SelectList(_context.GymClasses, "Id", "Ttile", classBooking.GymClassId);
            return View("~/Views/User/ClassBookingEdit.cshtml", classBooking);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassBookingEdit(int id, [Bind("Id,GymClassId")] ClassBooking classBooking)
        {
            if (id != classBooking.Id) return NotFound();
           
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new NullReferenceException("User is not logged in.");
            var existingBooking = await _context.ClassBooking.FirstOrDefaultAsync(cb => cb.Id == id && cb.UserId == userId);
            if (existingBooking == null)
                throw new NullReferenceException($"No booking found with Id={id} for UserId={userId}.");
            existingBooking.GymClassId = classBooking.GymClassId;
            existingBooking.BookedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                ViewData["GymClassId"] = new SelectList(_context.GymClasses, "Id", "Title", existingBooking.GymClassId);
                return View("~/Views/User/ClassBookingEdit.cshtml", existingBooking);
            } 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ClassBooking));
           
        }

        // DELETE (GET)
        public async Task<IActionResult> ClassBookingDelete(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var classBooking = await _context.ClassBooking
                .Include(cb => cb.GymClass)
                .FirstOrDefaultAsync(cb => cb.Id == id && cb.UserId == userId);

            if (classBooking == null) return NotFound();

            return View("~/Views/User/ClassBookingDelete.cshtml", classBooking);
        }

        // DELETE (POST)
        [HttpPost, ActionName("ClassBookingDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassBookingDeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var classBooking = await _context.ClassBooking.FirstOrDefaultAsync(cb => cb.Id == id && cb.UserId == userId);
            if (classBooking == null) return NotFound();

            _context.ClassBooking.Remove(classBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ClassBooking));
        }
        // Cancel Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelBooking(int id)
        {
            var booking = _context.ClassBooking.Find(id);
            if (booking != null)
            {
                _context.ClassBooking.Remove(booking);
                _context.SaveChanges();
            }
            return RedirectToAction("ClassBooking");
        }

    }
}



