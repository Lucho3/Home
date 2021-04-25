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
        public async Task<IActionResult> Create([Bind("id,description,deadline,budget,picture")] TaskModel taskModel)
        {
            if (await extractUser())
            {
                if (ModelState.IsValid)
                {
                    _context.Add(taskModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(taskModel);
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
                return View(taskModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,description,deadline,budget,picture")] TaskModel taskModel)
        {
            if (await extractUser())
            {
                if (id != taskModel.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(taskModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TaskModelExists(taskModel.id))
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
                return View(taskModel);
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
