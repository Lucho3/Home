using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Home.Models;
using Home.Models.Entity;

namespace Home.Controllers
{
    public class TaskModelsController : Controller
    {
        private readonly HomeDBContext _context;

        public TaskModelsController(HomeDBContext context)
        {
            _context = context;
        }

        // GET: TaskModels
        public async Task<IActionResult> Index()
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
        public IActionResult Create()
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
            {
                return View();
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("id", out buffer))
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
    }
}
