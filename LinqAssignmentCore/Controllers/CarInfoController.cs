using LinqAssignmentCore.Models.Db;
using LinqAssignmentCore.Models;
using Microsoft.AspNetCore.Mvc;
using LinqAssignmentCore.Data;
using LinqAssignmentCore.Models.ViewModels;

namespace LinqAssignmentCore.Controllers
{
    public class CarInfoController : Controller
    {
        private readonly AppDbContext _db;

        public CarInfoController(AppDbContext db)
        {
            _db = db;   
        }

        [Route("linq")]
        public ActionResult GetCarInfo()
        {
           
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            var cars = Car.ProcessCars("Files/fuel.csv");
            _db.Cars.AddRange(cars);
            var manufacturers = Manufacturer.ProcessManufacturers("Files/manufacturers.csv");
            _db.Manufacturers.AddRange(manufacturers);
            _db.SaveChanges();

            return RedirectToAction("Queries");
        }

        public ActionResult Queries()
        {
            return View();
        }

        //// Queries

        // Most fuel efficient cars - (extension) method syntax
        public ActionResult MostEfficient()
        {
            //Take then ToList() more effictient
            

            return View();
        }

        // Most fuel efficient car - query syntax
        public ActionResult MostEfficientQ()
        {
            
            return View();
        }

        // Filtering with Where and FirstOrDefault - (extension) method syntax
        public ActionResult WhereAndFirst()
        {
            return View();
        }

        // Filtering with Where and FirstOrDefault - query syntax
        public ActionResult WhereAndFirstQ()
        {
            
            return View();
        }

        //Use 'Any' when checking if at least one item meet condition (true/false)
        // if(cars.Any(c => c.Name == "BMW")){}

        //Use 'All' when checking if all items meet condition (true/false)
        // if(cars.All(c => c.Name == "BMW")){}

        //Use 'Contains' when comparing items in lists, or checking if items in 
        //a list has a certain value
        //foreach (var item in list1)
        //      {
        //        if(list2.Contains(item))
        //          {Do Something}
        //      }
        //if(list1.Contains("value")) {Do Something}


        // Where condition with projected 'Select' - new objects with fewer properties
        // Method syntax
        public ActionResult ProjectedSelect()
        {
            
            return View();
        }

        // Where condition with projected 'Select' - new objects with fewer properties
        // Query syntax
        public ActionResult ProjectedSelectQ()
        {
            

            
            return View();
        }

        // SelectMany in method syntax
        // Flattening of sequences in a sequence to a single collection
        // E.g. - producing a list of all characters in all car names
        // IEnumerable<char> characters is the same as a string
        public ActionResult SelectMany()
        {
            
            return View();
        }

        // Join tables - query syntax
        public ActionResult JoinTablesQ()
        {

            

            return View();
        }

        // Join tables - method syntax
        public ActionResult JoinTables()
        {
            

            return View();
        }

        // Group by - query syntax
        public ActionResult GroupingQ()
        {
           

            return View();
        }

        // Group by - method syntax
        public ActionResult Grouping()
        {
            

            return View();
        }

        // combined group and join (get 2 properties from manufacturer) - query syntax
        public ActionResult GroupJoinQ()
        {
            

            return View();
        }

        // Combined group and join (get 2 properties from manufacturer) - method syntax
        // Equality of keys
        public ActionResult GroupJoin()
        {
           
            return View();

        }

        // Top 3 fuel efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - query syntax
        public ActionResult JoinAndGroupAndSelectManyQ()
        {
            

            return View();
        }

        // Top 3 fuel least efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - method syntax
        public ActionResult JoinAndGroupAndSelectMany()
        {
            

            return View();
        }

        // Aggregating data (order by most efficient car) - query syntax
        public ActionResult AggregatingDataQ()
        {
            
            return View();
        }

        // Aggregating data (order by most efficient car) - method syntax
        // Using class 'CarStatistics' to avoid iterating through list 3 times
        public ActionResult AggregatingData()
        {
            
            return View();
        }
    }
}
