using crud_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace crud_3.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext studentDb;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(StudentDbContext studentDb)
        {
            this.studentDb = studentDb;
        }

        public IActionResult Index()
        {
            var data=studentDb.Students.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                studentDb.Students.Add(std);
                studentDb.SaveChanges();
                TempData["Insert"] = "data successfully Inserted";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id== null || studentDb.Students == null)
            {
                return NotFound();
            }
           var data= await studentDb.Students.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> edit(int? id,Student std)
        {
            if (ModelState.IsValid)
            {
                studentDb.Update(std);
                await studentDb.SaveChangesAsync();
                TempData["Edit"] = "Data Update  SuccessFully";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
            
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null || studentDb.Students == null)
            {
                return NotFound();
            }
            var data = await studentDb.Students.FirstOrDefaultAsync(x => x.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null || studentDb.Students == null)
            {
                return NotFound();
            }
            var data = await studentDb.Students.FirstOrDefaultAsync(x=>x.id==id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost,ActionName("Deleted")]
        public async Task<IActionResult> Deleted(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await studentDb.Students.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            studentDb.Students.Remove(data);
            await studentDb.SaveChangesAsync();
            TempData["delete"] = "Data deleted successfully";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
