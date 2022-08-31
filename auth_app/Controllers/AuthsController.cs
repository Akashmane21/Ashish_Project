using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using auth_app.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace auth_app.Controllers
{
    public class AuthsController : Controller
    {
        private readonly DBContext _context;

        public AuthsController(DBContext context)
        {
            _context = context;
        }

        // GET: Auths
        public async Task<IActionResult> Index()
        {
              return _context.all_users != null ? 
                          View(await _context.all_users.ToListAsync()) :
                          Problem("Entity set 'DBContext.all_users'  is null.");
        }

        // GET: Auths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.all_users == null)
            {
                return NotFound();
            }

            var auth = await _context.all_users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // GET: Auths/Create
        public IActionResult Create()
        {
            return View();
        }

       public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,UserName,Password,Email,location,language,Role,userpin")] Auth auth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auth);
                await _context.SaveChangesAsync();
                return Redirect("/Auths/Login");
            }
            return View(auth);
        }

       

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login data)
        {

            Debug.WriteLine(data.UserName );
            Debug.WriteLine(data.Password);



            List<Login> daata = new List<Login>();
            using (SqlConnection con = new SqlConnection("Data Source=PSL-J957XM3\\SQLEXPRESS;Initial Catalog=FEBBATCH;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False"))
            {
               
                SqlCommand cmd = new SqlCommand("select * from all_users", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var details = new Login();
                    details.UserName = rdr["UserName"].ToString();
                    details.Password = rdr["Password"].ToString();
                    daata.Add(details);
                }
                Debug.WriteLine(daata[0].UserName);

                var DetailsData = daata.Find(x => x.UserName == data.UserName);
               

                if (DetailsData != null)
                {

                   
                        if (DetailsData.Password.Equals(data.Password))
                        {
                            ViewBag.msg = "User Found";
                            return Redirect("/Auths/Index");

                        }
                        else
                        {

                            ViewBag.msg = "User Found but Password Does not match";
                        }
                    
                   
                }
                else
                {
                    ViewBag.msg = "User Not Found";
                }  
                return View();
            }

        }



        // POST: Auths/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email,location,language,Role,userpin")] Auth auth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auth);
                await _context.SaveChangesAsync();
                return Redirect("/Auths/Login");
            }
            return View(auth);
        }

        // GET: Auths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.all_users == null)
            {
                return NotFound();
            }

            var auth = await _context.all_users.FindAsync(id);
            if (auth == null)
            {
                return NotFound();
            }
            return View(auth);
        }

        // POST: Auths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,location,language,Role,userpin")] Auth auth)
        {
            if (id != auth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthExists(auth.Id))
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
            return View(auth);
        }

        // GET: Auths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.all_users == null)
            {
                return NotFound();
            }

            var auth = await _context.all_users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // POST: Auths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.all_users == null)
            {
                return Problem("Entity set 'DBContext.all_users'  is null.");
            }
            var auth = await _context.all_users.FindAsync(id);
            if (auth != null)
            {
                _context.all_users.Remove(auth);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthExists(int id)
        {
          return (_context.all_users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
