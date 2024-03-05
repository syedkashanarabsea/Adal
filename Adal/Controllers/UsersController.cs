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

            var usersDTOList = new List<UsersDTO>();

            if (getUsers != null)
            {
                var getCity = _context.City.ToList();
                var utilities = new UtilitiesClass<Users, UsersDTO>();

                // Call CreateMapWithAutoProperties to automatically include all properties in the mapping
                //utilities.CreateMapWithAutoProperties();

                foreach (var item in getUsers)
                {
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

            return View(new List<UsersDTO>());
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

			foreach (var item in getCity)
			{
				model.CityList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
			}

			model.UserRoleId = 3;

			return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersDTO users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var getCity = _context.City.ToList();
        
			foreach (var item in getCity)
			{
				users.CityList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
			}

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

			if (users != null)
            {
				var getCity = _context.City.ToList();

				var utilities = new UtilitiesClass<Users, UsersDTO>();
				var usersDTO = utilities.Map(users);

                foreach (var item in getCity)
                {
                    usersDTO.CityList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Selected = item.Id == users.CityId });
				}
				usersDTO.LawyerTypeList.Add(new SelectListItem { Value = "1", Text = "Standard" });
				usersDTO.LawyerTypeList.Add(new SelectListItem { Value = "2", Text = "Premium" });


				return View(usersDTO);

            }
			return NotFound();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsersDTO users)
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

			foreach (var item in getCity)
			{
				model.CityList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name});
			}
			model.LawyerTypeList.Add(new SelectListItem { Value = "1", Text = "Standard" });
			model.LawyerTypeList.Add(new SelectListItem { Value = "2", Text = "Premium" });

			return View(model);
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UsersDTO users)
		{
			if (ModelState.IsValid)
			{
				_context.Add(users);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			var getCity = _context.City.ToList();

			foreach (var item in getCity)
			{
				users.CityList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
			}
			return View(users);
		}


		#endregion
	}
}
