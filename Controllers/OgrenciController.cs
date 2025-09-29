using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var model = await _context.Ogrenciler.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                return View(ogrenci);
            }
            await _context.Ogrenciler.AddAsync(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if(id == null || id <= 0)
                {
                return NotFound();
            }

            var model = await
                _context.Ogrenciler
                .Include(o=>o.KursKayitlari)
                .ThenInclude(o=>o.Kurs)
                .FirstOrDefaultAsync(x => x.OgrenciId == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                return View(ogrenci);
            }

            if (ogrenci == null)
            {
                return NotFound();
            }
            var model = _context.Ogrenciler.FirstOrDefault(x=>x.OgrenciId == ogrenci.OgrenciId);
            if(model == null)
            {
                return NotFound();
            }
            model.OgrenciAd = ogrenci.OgrenciAd;
            model.OgrenciSoyad = ogrenci.OgrenciSoyad;
            model.Eposta = ogrenci.Eposta;
            model.Telefon = ogrenci.Telefon;
            _context.SaveChanges();


            return RedirectToAction("List");
        }

        public  async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View(null);
            }
            var model = await _context.Ogrenciler.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }
        [HttpPost]
        public  async Task<IActionResult> Delete(Ogrenci ogrenci)
        {
            if (ogrenci == null)
            {
                return NotFound();
            }
            var model = await _context.Ogrenciler.FindAsync(ogrenci.OgrenciId);
            if (model == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

    }
}
