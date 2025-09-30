namespace efcoreApp.Data
{
    public class Ogretmen
    {
        public int OgretmenId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public DateTime? BaslamaTarihi { get; set; }
        public ICollection<Kurs>? Kurslar { get; set; }
    }
}
//1Öğretmen birden fazla kurs verebilir.
//1 kursta birden fazla öğretmen olamaz
//1 = öğretmen , n = kurs
//o halde Kurs.cs dosyasına gidip öğretmen propertysini ekleyelim.
//public Ogretmen Ogretmen { get; set; }
//Ogretmen.cs dosyasına gidip kurslar propertysini ekleyelim.