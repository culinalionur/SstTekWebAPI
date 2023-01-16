using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SstTekWebAPI.Filters;
using SstTekWebAPI.Linq;
using System.Text.Json.Serialization;

namespace SstTekWebAPI.Controllers
{
    [Route("api/[controller]")]

    public class ExceptionTestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ExceptionTestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("config-test")]

        public string ConfigTest()
        {
            var secretKey = _configuration.GetValue<string>("Secrets:SecretKey");
            var accessKey = _configuration.GetValue<int>("Secrets:AccessKey");
            var count = _configuration.GetValue<int>("Secrets:Props:Count");

            var copyrightOwner = _configuration.GetValue<string>("Copyright:Owner");
            return "OK";
        }
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

            var groupedByColor = db.Students.GroupBy(g => g.Color);
            var response = new List<GroupedStudent>();

            foreach(var group in groupedByColor)
            {
                var groupName = group.Key;
                var sameGroupedStudents = group.ToList();
                response.Add(new GroupedStudent
                {
                    GroupName = groupName,
                    Students = sameGroupedStudents
                });
            }

            var groupedByTwoProps = db.Students.GroupBy(g => new { g.Color, g.Surname });
            foreach (var groupedBy in groupedByTwoProps)
            {
                var colorName = groupedBy.Key.Color;
                var surname = groupedBy.Key.Surname;
                var students2 = groupedBy.ToList();
            }


            return "OK";

            
        }
        [HttpGet]
        [Route("filter-test")]
        [TestActionFilter]
        public IActionResult FilterTest()
        {
            try
            {
                var response = new Response
                {
                    Message = "Onur"
                };
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(response)
                };
            }
            catch (Exception e )
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
