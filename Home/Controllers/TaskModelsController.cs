using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Home.Models;
using Home.Models.Entity;
using System.Text;
using Home.Models.ViewModels;

namespace Home.Controllers
{
    public class TaskModelsController : Controller
    {
        private readonly HomeDBContext _context;

        private UserModel user = null;

        public TaskModelsController(HomeDBContext context)
        {
            _context = context;
        }

        // GET: TaskModels
        public async Task<IActionResult> Index()
        {
            if (await extractUser())
            {
                return View(await _context.Tasks.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

        }

        // GET: TaskModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var taskModel = await _context.Tasks
                    .FirstOrDefaultAsync(m => m.id == id);
                if (taskModel == null)
                {
                    return NotFound();
                }

                return View(taskModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        public async Task<IActionResult> MyTasks()
        {
            if (await extractUser())
            {
                List<TaskModel> tm = await _context.Tasks.Include(u => u.user).Include(u=>u.status).Where(u => u.user == this.user).ToListAsync(); ;

                return View(tm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }
        public async Task<IActionResult> Decline(int? id)
        {
            if (await extractUser())
            {
                TaskModel t = await _context.Tasks.Where(t => t.id == id).FirstOrDefaultAsync();
                t.status = await _context.Statuses.Where(s => s.status == "Refused").FirstOrDefaultAsync();
                List<TaskModel> tm = await _context.Tasks.Include(u => u.user).Include(u => u.status).Where(u => u.user == this.user).ToListAsync(); ;
                await _context.SaveChangesAsync();
                return View("MyTasks",tm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        public async Task<IActionResult> Done(int? id)
        {
            if (await extractUser())
            {
                TaskModel t = await _context.Tasks.Where(t => t.id == id).FirstOrDefaultAsync();
                t.status = await _context.Statuses.Where(s => s.status == "Fulfilled").FirstOrDefaultAsync();
                List<TaskModel> tm = await _context.Tasks.Include(u => u.user).Include(u => u.status).Where(u => u.user == this.user).ToListAsync(); ;
                await _context.SaveChangesAsync();
                return View("MyTasks", tm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }
        // GET: TaskModels/Create
        public async Task<IActionResult> Create()
        {
            if (await extractUser())
            {
                TaskCreateViewModel tcvm = new TaskCreateViewModel();
                tcvm.categories = await _context.Categories.ToListAsync();
                tcvm.locations = user.locations;

                return View(tcvm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: TaskModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string desc, DateTime deadline, decimal budget)
        {
            if (await extractUser())
            {
                TaskModel tm = new TaskModel();
                tm.name = name;
                tm.description = desc;
                tm.deadline = deadline;
                tm.budget = budget;

                string location = Request.Form["Locations"];
                string category = Request.Form["Categories"];
                tm.location = await _context.Locations.Where(l => l.name == location).FirstOrDefaultAsync();
                tm.category = await _context.Categories.Where(c => c.type == category).FirstOrDefaultAsync();
                tm.status = await _context.Statuses.Where(s => s.id == 1).FirstOrDefaultAsync();
                tm.user = this.user;

                if (ModelState.IsValid)
                {
                    _context.Add(tm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                TaskCreateViewModel tcvm = new TaskCreateViewModel();
                tcvm.categories = await _context.Categories.ToListAsync();
                tcvm.locations = user.locations;
                return View(tcvm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: TaskModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var taskModel = await _context.Tasks.FindAsync(id);
                if (taskModel == null)
                {
                    return NotFound();
                }
                 TaskCreateViewModel tcvm = new TaskCreateViewModel();
                tcvm.categories = await _context.Categories.ToListAsync();
                tcvm.locations = user.locations;
                tcvm.task = await _context.Tasks.Where(t => t.id == id).FirstOrDefaultAsync();
                return View(tcvm);
 
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: TaskModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string desc, DateTime deadline, decimal budget)
        {
            if (await extractUser())
            {
                TaskModel tm =await _context.Tasks.Where(t=>t.id==id).FirstOrDefaultAsync();
                
                if (id != tm.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        tm.name = name;
                        tm.description = desc;
                        tm.deadline = deadline;
                        tm.budget = budget;

                        string location = Request.Form["Locations"];
                        string category = Request.Form["Categories"];
                        tm.location = await _context.Locations.Where(l => l.name == location).FirstOrDefaultAsync();
                        tm.category = await _context.Categories.Where(c => c.type == category).FirstOrDefaultAsync();
                        tm.status = await _context.Statuses.Where(s => s.id == 1).FirstOrDefaultAsync();
                        tm.user = this.user;
                        _context.Update(tm);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TaskModelExists(tm.id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                TaskCreateViewModel tcvm = new TaskCreateViewModel();
                tcvm.categories = await _context.Categories.ToListAsync();
                tcvm.locations = user.locations;
                tcvm.task = await _context.Tasks.Where(t => t.id == id).FirstOrDefaultAsync();
                return View(tcvm);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: TaskModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var taskModel = await _context.Tasks
                    .FirstOrDefaultAsync(m => m.id == id);
                if (taskModel == null)
                {
                    return NotFound();
                }

                return View(taskModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: TaskModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await extractUser())
            {
                var taskModel = await _context.Tasks.FindAsync(id);
                _context.Tasks.Remove(taskModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.id == id);
        }

        [NonAction]
        private async Task<bool> extractUser()
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
            {
                int userId = int.Parse(Encoding.UTF8.GetString(buffer));
                user = await _context.Users.Where(u => u.id == userId).Include(u => u.tasks).Include(u => u.locations).Include(u => u.type).FirstOrDefaultAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
