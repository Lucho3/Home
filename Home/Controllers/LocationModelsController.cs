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

namespace Home.Controllers
{
    public class LocationModelsController : Controller
    {
        private readonly HomeDBContext _context;

        private UserModel user = null;
        public LocationModelsController(HomeDBContext context)
        {
            _context = context;
        }

        // GET: LocationModels
        public async Task<IActionResult> Index()
        {
            if (await extractUser())
            {
                return View(await _context.Locations.Where(l=>l.user==user).ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: LocationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var locationModel = await _context.Locations
                    .FirstOrDefaultAsync(m => m.id == id);
                if (locationModel == null)
                {
                    return NotFound();
                }

                return View(locationModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: LocationModels/Create
        public async Task<IActionResult> Create()
        {
            if (await extractUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: LocationModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,address")] LocationModel locationModel)
        {
            if (await extractUser())
            {
                if (ModelState.IsValid)
                {
                    _context.Add(locationModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(locationModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: LocationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var locationModel = await _context.Locations.FindAsync(id);
                if (locationModel == null)
                {
                    return NotFound();
                }
                return View(locationModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: LocationModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,address")] LocationModel locationModel)
        {
            if (await extractUser())
            {
                if (id != locationModel.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(locationModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LocationModelExists(locationModel.id))
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
                return View(locationModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // GET: LocationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await extractUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var locationModel = await _context.Locations
                    .FirstOrDefaultAsync(m => m.id == id);
                if (locationModel == null)
                {
                    return NotFound();
                }

                return View(locationModel);
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        // POST: LocationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await extractUser())
            {
                var locationModel = await _context.Locations.FindAsync(id);
                _context.Locations.Remove(locationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        private bool LocationModelExists(int id)
        {
            return _context.Locations.Any(e => e.id == id);
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
