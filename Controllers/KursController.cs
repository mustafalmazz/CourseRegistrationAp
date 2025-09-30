using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        // Kurs listesi
        public async Task<IActionResult> List()
        {
            var model = await _context.Kurslar
                .Include(o => o.Ogretmen)
                .Include(o => o.KursKayitlari)
                    .ThenInclude(k => k.Ogrenci)
                .ToListAsync();

            return View(model);
        }

        // Kurs ekleme GET
        public async Task<IActionResult> Create()
        {
            ViewBag.OgretmenListesi = new SelectList(
                await _context.Ogretmenler.ToListAsync(),
                "OgretmenId",
                "Ad"
            );

            return View();
        }

        // Kurs ekleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs kurs)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OgretmenListesi = new SelectList(
                    await _context.Ogretmenler.ToListAsync(),
                    "OgretmenId",
                    "Ad",
                    kurs.OgretmenId
                );
                return View(kurs);
            }

            _context.Kurslar.Add(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        // Kurs düzenleme GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Kurslar
                .Include(c => c.KursKayitlari)
                    .ThenInclude(c => c.Ogrenci)
                .FirstOrDefaultAsync(c => c.KursId == id);

            if (course == null) return NotFound();

            ViewBag.OgretmenListesi = new SelectList(
                await _context.Ogretmenler.ToListAsync(),
                "OgretmenId",
                "Ad",
                course.OgretmenId
            );

            return View(course);
        }

        // Kurs düzenleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kurs kurs)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OgretmenListesi = new SelectList(
                    await _context.Ogretmenler.ToListAsync(),
                    "OgretmenId",
                    "Ad",
                    kurs.OgretmenId
                );
                return View(kurs);
            }

            var model = await _context.Kurslar.FirstOrDefaultAsync(c => c.KursId == kurs.KursId);
            if (model == null) return NotFound();

            model.Baslik = kurs.Baslik;
            model.OgretmenId = kurs.OgretmenId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        // Kurs silme GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var silinecek = await _context.Kurslar
                .Include(c => c.Ogretmen)
                .FirstOrDefaultAsync(c => c.KursId == id);

            if (silinecek == null) return NotFound();

            return View(silinecek);
        }

        // Kurs silme POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Kurslar.FirstOrDefaultAsync(c => c.KursId == id);
            if (model == null) return NotFound();

            _context.Kurslar.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
