using efcoreApp.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace efcoreApp.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var model = _context.Ogretmenler.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]  
        public  async Task<IActionResult> Create(Ogretmen ogretmen)
        {
            ogretmen.BaslamaTarihi = DateTime.Now.Date;
            _context.Ogretmenler.Add(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _context.Ogretmenler.FirstOrDefault(o => o.OgretmenId == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public  async Task<IActionResult> Edit(Ogretmen ogretmen)
        {
            if (ogretmen == null)
            {
                return NotFound();
            }
            var model = _context.Ogretmenler.FirstOrDefault(o => o.OgretmenId == ogretmen.OgretmenId);
            if (model == null)
            {
                return NotFound();
            }
            model.BaslamaTarihi = ogretmen.BaslamaTarihi;
            model.Ad = ogretmen.Ad;
            model.Soyad = ogretmen.Soyad;
            model.Eposta = ogretmen.Eposta;
            model.Telefon = ogretmen.Telefon;
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _context.Ogretmenler.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Ogretmen ogretmen)
        {
            if (ogretmen==null)
            {
                return NotFound();
            }
            var model = _context.Ogretmenler.FirstOrDefault(o => o.OgretmenId == ogretmen.OgretmenId);
            if (model == null)
            {
                return NotFound();
            }
            _context.Ogretmenler.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
