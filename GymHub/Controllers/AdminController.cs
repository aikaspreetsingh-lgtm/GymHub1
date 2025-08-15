using GymHub.Data;
using GymHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymHub.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;



namespace GymHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
    


        public AdminController(
            AppDbContext context,
            UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
                     
        }

        #region Dashboard
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalUsers = await _userManager.Users.CountAsync();
            ViewBag.TotalExercises = await _context.Exercises.CountAsync();
            ViewBag.TotalClasses = await _context.GymClasses.CountAsync();
            ViewBag.TotalWorkoutTemplates = await _context.WorkoutPlans.Where(p => p.IsTemplate).CountAsync();
            ViewBag.TotalNutritionTemplates = await _context.NutritionPlans.Where(p => p.IsTemplate).CountAsync();

            return View();
        }


        // EXERCISES 

        public async Task<IActionResult> Exercise(string search)
        {
            var q = _context.Exercises.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(e => e.Name.Contains(search));
            var list = await q.OrderBy(e => e.Name).ToListAsync();
            return View(list);
        }

        //Details of an exercise
        public async Task<IActionResult> ExerciseDetails(int id)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
            if (exercise == null)
                return NotFound();

            return View(exercise);
        }


        public IActionResult ExerciseCreate() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExerciseCreate(Exercises model)
        {
            if (!ModelState.IsValid) return View(model);
            model.Id = 0;
            _context.Exercises.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Exercise));
        }

        public async Task<IActionResult> ExerciseEdit(int id)
        {
            var entity = await _context.Exercises.FindAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExerciseEdit(int id, Exercises model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Exercise));
        }

        public async Task<IActionResult> ExerciseDelete(int id)
        {
            var entity = await _context.Exercises.FindAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost, ActionName("ExerciseDelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ExerciseConfirmedDelete(int id)
        {
            var entity = await _context.Exercises.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Exercises.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Exercise));
        }
        #endregion


        // WORKOUT PLAN 
        #region WorkoutPlans
        public async Task<IActionResult> WorkoutPlan()
        {
            var plans = await _context.WorkoutPlans
                .Where(p => p.IsTemplate)
                .ToListAsync();

            return View(plans);
        }

        //Deatils of a workout plan
        public async Task<IActionResult> WorkoutPlanDetails(int id)
        {
            var plan = await _context.WorkoutPlans.FirstOrDefaultAsync(w => w.Id == id);
            if (plan == null)
                return NotFound();

            return View(plan);
        }


        public IActionResult WorkoutPlanCreate() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutPlanCreate([Bind("Title,DurationWeeks,DifficultyLevel,Goal")] WorkoutPlan model)
        {
            if (!ModelState.IsValid) return View(model);

            model.IsTemplate = true;
            model.UserId = null;

            _context.WorkoutPlans.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkoutPlan));
        }

        public async Task<IActionResult> WorkoutPlanEdit(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();
            return View(plan);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutPlanEdit(int id, [Bind("Id,Title,DurationWeeks,DifficultyLevel,Goal," +
            "IsTemplate,UserId")] WorkoutPlan model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            model.IsTemplate = true;
            model.UserId = null;

            _context.WorkoutPlans.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkoutPlan));
        }

        public async Task<IActionResult> WorkoutPlanDelete(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();
            return View(plan);
        }

        [HttpPost, ActionName("WorkoutPlanDelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> WorkoutPlanConfirmedDelete(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();

            _context.WorkoutPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkoutPlan));
        }
        #endregion


        //Nutrition Plan
      
        #region NutritionPlans
        public async Task<IActionResult> NutritionPlan()
        {
            var plans = await _context.NutritionPlans
                .Where(p => p.IsTemplate)

                .ToListAsync();

            return View(plans);
        }

        //Details of a nutrition plan
        public async Task<IActionResult> NutritionPlanDetails(int id)
        {
            var plan = await _context.NutritionPlans.FirstOrDefaultAsync(n => n.Id == id);
            if (plan == null)
                return NotFound();

            return View(plan);
        }

        public IActionResult NutritionPlanCreate() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> NutritionPlanCreate(NutritionPlan model)
        {
            if (!ModelState.IsValid) return View(model);

            model.IsTemplate = true;
            model.UserId = null;

            _context.NutritionPlans.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(NutritionPlan));
        }

        public async Task<IActionResult> NutritionPlanEdit(int id)
        {
            var plan = await _context.NutritionPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();
            return View(plan);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> NutritionPlanEdit(int id, NutritionPlan model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            model.IsTemplate = true;
            model.UserId = null;

            _context.NutritionPlans.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(NutritionPlan));
        }

        public async Task<IActionResult> NutritionPlanDelete(int id)
        {
            var plan = await _context.NutritionPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();
            return View(plan);
        }

        [HttpPost, ActionName("NutritionPlanDelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> NutritionPlanConfirmedDelete(int id)
        {
            var plan = await _context.NutritionPlans.FindAsync(id);
            if (plan == null || !plan.IsTemplate) return NotFound();

            _context.NutritionPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(NutritionPlan));
        }
        #endregion


        // GYM CLASSES

        #region GymClasses
        public async Task<IActionResult> GymClasses()
        {
            var classes = await _context.GymClasses
                .Include(c => c.Bookings)
                .OrderByDescending(c => c.StartsAt)
                .ToListAsync();
            return View(classes);
        }

        //Details of a gym class
        public async Task<IActionResult> GymClassesDetails(int id)
        {
            var gymClass = await _context.GymClasses
                .Include(g => g.Bookings)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gymClass == null)
                return NotFound();

            return View(gymClass);
        }


        public IActionResult GymClassesCreate() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GymClassesCreate(GymClass model)
        {
            if (!ModelState.IsValid)  return View(new GymClass());
           
            _context.GymClasses.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GymClasses));
        }

        public async Task<IActionResult> GymClassesEdit(int id)
        {
            var entity = await _context.GymClasses.FindAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GymClassesEdit(int id, GymClass model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.GymClasses.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GymClasses));
        }

        public async Task<IActionResult> GymClassesDelete(int id)
        {
            var entity = await _context.GymClasses.FindAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost, ActionName("GymClassesDelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> GymClassConfirmedDelete(int id)
        {
            var entity = await _context.GymClasses.FindAsync(id);
            if (entity == null) return NotFound();

            _context.GymClasses.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GymClasses));
            #endregion
        }

        public async Task<IActionResult> Users()
        {
            var allUsers = await _userManager.Users.ToListAsync();
    var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");


    var normalUsers = allUsers.Where(u => !adminUsers.Contains(u)).ToList();

    return View(normalUsers);
        }

        // GET:UserDetails
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // GET: UserCreate
        public IActionResult UserCreate()
        {
            return View();
        }

        // POST: UserCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(Users model, string password)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userManager.CreateAsync(model, password);
            if (result.Succeeded)
                return RedirectToAction(nameof(Users));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: UserEdit
        public async Task<IActionResult> UserEdit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: UserEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(string id, Users model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.FullName = model.FullName;
         

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Users));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: UserDelete
        public async Task<IActionResult> UserDelete(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: UserDelete
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Users));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsersBulk(string actionType, string[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length == 0)
            {
                TempData["Error"] = "No users selected.";
                return RedirectToAction(nameof(Users));
            }

            foreach (var id in selectedIds)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) continue;

                switch (actionType)
                {
                    case "delete":
                        await _userManager.DeleteAsync(user);
                        break;
                   
                }
            }

            TempData["Message"] = "Bulk action applied successfully.";
            return RedirectToAction(nameof(Users));
        }

        //SummaryReport

        [HttpGet]
        public async Task<IActionResult> GenerateReport()
        {
            var users = await _context.Users
                .Include(u => u.ClassBookings).ThenInclude(cb => cb.GymClass)
                .Include(u => u.ProgressMetric)
                .Include(u => u.WorkoutSession).ThenInclude(ws => ws.Sets)
                .ToListAsync();

            ViewBag.FullName = new SelectList(users.Select(u => u.FullName).Distinct());

            var model = new ReportFilterViewModel
            {
                Users = users
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportFilterViewModel model)
        {
            model.IsSubmitted = true;

            if (!string.IsNullOrEmpty(model.SelectedFullName))
            {
                model.Users = await _context.Users
                    .Where(u => u.FullName == model.SelectedFullName)
                    .Include(u => u.ClassBookings)
                    .Include(u => u.ProgressMetric)
                    .Include(u => u.WorkoutSession)
                    .ThenInclude(ws => ws.Sets)
                    .ToListAsync();
            }
            else
            {
                model.Users = await _context.Users
                    .Include(u => u.ClassBookings)
                    .Include(u => u.ProgressMetric)
                    .Include(u => u.WorkoutSession)
                    .ThenInclude(ws => ws.Sets)
                    .ToListAsync();
            }

            ViewBag.FullName = new SelectList(_context.Users.Select(u => u.FullName).Distinct());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReportPdf(string SelectedFullName)
        {
            var query = _context.Users
                .Include(u => u.ClassBookings).ThenInclude(cb => cb.GymClass)
                .Include(u => u.ProgressMetric)
                .Include(u => u.WorkoutSession).ThenInclude(ws => ws.Sets)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SelectedFullName))
            {
                query = query.Where(u => u.FullName == SelectedFullName);
            }

            var users = await query.ToListAsync();

            using var stream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 12, XFontStyle.Regular);

            int yPoint = 40;

            foreach (var user in users)
            {
                gfx.DrawString($"User: {user.FullName}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 25;

                gfx.DrawString("Class Bookings:", font, XBrushes.Black, new XRect(60, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
                foreach (var cb in user.ClassBookings)
                {
                    gfx.DrawString($"- {cb.GymClass?.Title} on {cb.BookedAt.ToShortDateString()}", font, XBrushes.Black, new XRect(80, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    yPoint += 18;
                }

                gfx.DrawString("Progress Metrics:", font, XBrushes.Black, new XRect(60, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
                foreach (var pm in user.ProgressMetric)
                {
                    gfx.DrawString($"- {pm.LoggedAt.ToShortDateString()} - {pm.WeightKg} kg", font, XBrushes.Black, new XRect(80, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    yPoint += 18;
                }

                gfx.DrawString("Workout Sessions:", font, XBrushes.Black, new XRect(60, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
                foreach (var ws in user.WorkoutSession)
                {
                    gfx.DrawString($"- {ws.PerformedOn.ToShortDateString()} with {ws.Sets?.Count} sets", font, XBrushes.Black, new XRect(80, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    yPoint += 18;
                }

                yPoint += 30;

                if (yPoint > page.Height - 100)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPoint = 40;
                }
            }

            document.Save(stream, false);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "UserReport.pdf");
        }





    }

}




