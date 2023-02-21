using Microsoft.AspNetCore.Mvc; 
using RMS.DataAccess;
using RMS.Models;

namespace RMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Users> userList = _db.Users;
            return View(userList);
        }

    }
}
