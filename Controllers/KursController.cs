using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var model = _context.Kurslar.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Kurs kurs)
        {
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Add(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = _context.Kurslar
                .Include(c => c.KursKayitlari)
                .ThenInclude(c => c.Ogrenci).FirstOrDefault(c => c.KursId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Kurs kurs)
        {
            if (kurs == null)
            {
                return NotFound();
            }
            var model = _context.Kurslar
                .FirstOrDefault(c => c.KursId == kurs.KursId);
            if (model == null)
            {
                return NotFound();
            }
            model.Baslik = kurs.Baslik;
            await _context.SaveChangesAsync();

            return RedirectToAction("List", "Kurs");
        }
        public IActionResult Delete(int? id)
        {
            var silinecek = _context.Kurslar.Find(id);
            return View(silinecek);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Kurs kurs)
        {
            var model = _context.Kurslar.FirstOrDefault(c => c.KursId == kurs.KursId);
            if (model == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
