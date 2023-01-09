using Microsoft.AspNetCore.Mvc;
using SstTekWebAPI.Linq;

namespace SstTekWebAPI.Controllers
{
    [Route("api/[controller]")]

    public class ExceptionTestController : ControllerBase
    {
        [HttpGet]
        public string FirstEndpoint()
        {
            return "OK";
        }
        [HttpGet]
        [Route("Linq-test")]
        public string LinqTest() 
        { 
            Database db = new Database();
            
            //Bir listenin eleman sayısı
            var studentCount = db.Students.Count();

            //Öğrencilerden soyisimleri Kaya olan kişileri getir
            var kayaSurnamedStudents = db.Students.Where(s => s.Surname == "Kaya").ToList();
            
            //Rankı 80'den yukarı olanlar
            var ranks = db.Students.Where(s => s.Rank > 80);
           
            //Rank'ı 80'den yukarı olanların sayısı
            var ranksCount = db.Students.Count(s => s.Rank > 80);
            
            //Select ile seçim
            var names = db.Students.Where(s => s.Surname == "Kaya").Select(s => s.Name).ToList();
           
            //Birden fazla sütun çağırmak istiyorsak
            var namesColors = db.Students.Where(s => s.Surname == "Kaya").Select(s => new { s.Name, s.Color}).ToList();

            //FirstOrDefault
            var firstStudent = db.Students.Where(f => f.Surname == "Kaya").ToList()[0]; //Bu şekilde uzun yol
            var firstStudents = db.Students.FirstOrDefault(f => f.Surname == "Kaya"); //Tercih edilen yol
            var firstStudents2 = db.Students.First(f => f.Surname == "Kaya");
            //First ve FirstOrDefault arasındaki fark: FirstOrDefault eleman bulamazsa null değer dönerken First metotu veri bulamadığı için hata verir.
            //İstediğimiz koşula uygun veri dönmezse hata fırlat denmesini istersek First kullanırız.

            //İstediğimiz propertye göre sıralamak için
            var orderedStudentList = db.Students.OrderBy(o => o.Height).ToList();
            return "OK";
        }
    }
}
