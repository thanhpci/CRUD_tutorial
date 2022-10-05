using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationConnect.Models;
using GrpcServiceCRUD;
using GrpcService1;

namespace WebApplicationConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5292");
            var client = new EmployeeCRUD.EmployeeCRUDClient(channel);
            
            Employees employees = client.SelectAll(new Empty());

            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Delete(string id)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5292");
            var client = new EmployeeCRUD.EmployeeCRUDClient(channel);

            var empId = new EmployeeFilter();
            empId.EmployeeID = Convert.ToInt32(id);
            var emp = client.Delete(empId);

            return RedirectToAction("Index");
        }
        




        public IActionResult Update(string id)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5292");
            var client = new EmployeeCRUD.EmployeeCRUDClient(channel);


            Employee employee = client.SelectByID(new EmployeeFilter() { EmployeeID = Convert.ToInt32(id) });

            employee.FirstName = "Tom123";
            employee.LastName = "Jerry123";
            
            Empty response2 = client.Update(employee);

            return RedirectToAction("Index");
        }







        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("EmployeeID,FirstName,LastName")] Employee employee)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5292");
            var client = new EmployeeCRUD.EmployeeCRUDClient(channel);

            Empty response1 = client.Insert(new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName
            });
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
