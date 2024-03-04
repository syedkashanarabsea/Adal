using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adal.DbContexts;
using Core.CoreClass;
using Adal.Models;
using Adal.Utilities;

namespace Adal.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var getUsers = await _context.Users.ToListAsync();

            //_context.Users != null ? 
            //          View(await _context.Users.ToListAsync()) :
            //          Problem("Entity set 'DatabaseContext.Users'  is null.");

            var usersDTOList = new List<UsersDTO>();
            if (getUsers != null)
            {
                var getCity = _context.City.ToList();
                var utilities = new UtilitiesClass<Users, UsersDTO>();
                foreach (var item in getUsers)
                {
                    utilities.CreateMap(); 
                    var usersDTO = utilities.Map(item);

                    if (item.UserRoleId == 1)
                    {
                        usersDTO.UserRoleName = "Admin";
                    }
                    if (getCity.FirstOrDefault(z => z.Id == item.CityId) != null)
                    {
                        usersDTO.CityName = getCity.FirstOrDefault(z => z.Id == item.CityId).Name;
                    }
                    usersDTOList.Add(usersDTO);
                }
                return View(usersDTOList);
            }

            return View(new List<UsersDTO>()) ;
        }


        #region Clints Working
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            UsersDTO model = new UsersDTO();

            var getCity = _context.City.ToList();

            model.CityList.AddRange(getCity);

			model.LawyerTypeList.Add(new UsersDTO.LawyerTypesClass { UserType = 1, UserTypeName = "Standard" });
			model.LawyerTypeList.Add(new UsersDTO.LawyerTypesClass { UserType = 2, UserTypeName = "Premium" });
			return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var getCity = _context.City.ToList();
            users.CityList.AddRange(getCity);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DatabaseContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion


        #region Lawyer 


        public IActionResult Register()
        {
            UsersDTO model = new UsersDTO();

            var getCity = _context.City.ToList();

            model.CityList.AddRange(getCity);
            model.UserRoleId = 2;

            return View(model);
        }

        #endregion
    }
}
