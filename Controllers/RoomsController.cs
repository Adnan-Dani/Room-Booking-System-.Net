using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess;
using RMS.DataAccess.Repository.IRepository;
using RMS.DTOS;
using RMS.Models;

namespace RMS.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public RoomsController(AppDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Rooms> roomsList = _unitOfWork.Rooms.getRooms(); 

            return View(roomsList);
        }
        public IActionResult AddRoom()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoom( Rooms obj)
        { 
            if (ModelState.IsValid)
            {
                //_unitOfWork.Rooms
                _db.Rooms.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Successfylly Added";
                return RedirectToAction("Index");
            }
            return View(); 
        }
        public IActionResult editRoom(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            } 
            var room = _db.Rooms.Find(id);
            // if not found
            if(room == null) {  return NotFound(); }
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult editRoom(Rooms room)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Successfylly Updated";
                _db.Rooms.Update(room);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult deleteRoom(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var room = _db.Rooms.Find(id);
            if(room == null) { return NotFound(); }
            TempData["success"] = "Successfylly Deleted.";

            _db.Rooms.Remove(room); 
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult bookRoom(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var room = _db.Rooms.Find(id); 
            // if not found
            if (room == null) { return NotFound(); }
            BookRoomDTO bookRoomDTO = new BookRoomDTO();

            bookRoomDTO.room_no = room.room_no;
            return View(bookRoomDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult bookRoom(DTOS.BookRoomDTO obj)
        {
            int userId = 1;
            string res = _unitOfWork.Rooms.BookRooms(2, obj.room_no, obj.booking_date);
            TempData["success"] = res;
            return RedirectToAction("Index");
        }
           
    }
}
