using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class Program
    {
        static void Main(string[] args)
        {
            int floors = 10;
            int elevators = 2;
            Building building = new Building(floors, elevators);

        }
        public async Task CallWebAPIAsync()
        {
            var student = "{'Id':'1','Name':'Steve'}";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58847/");
            var response = await client.PostAsync("/", new StringContent(student, Encoding.UTF8, "application/json"));
            if (response != null)
            {
                Console.WriteLine(response.ToString());
            }
        }
    }
}
