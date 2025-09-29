using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            ViewBag.Ogrenciler = new SelectList(_context.Ogrenciler.ToList(), "OgrenciId","OgrenciAd");
            ViewBag.Kurslar = new SelectList(_context.Kurslar.ToList(),"KursId","Baslik");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KursKayit kayit)
        {
           _context.KursKayitlari.Add(kayit);
           await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            var model = _context.KursKayitlari.Include(k=>k.Ogrenci).Include(k=>k.Kurs).ToList();
            return View(model);
        }
    }
}
