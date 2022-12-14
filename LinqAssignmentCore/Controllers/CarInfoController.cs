using LinqAssignmentCore.Models.Db;
using LinqAssignmentCore.Models;
using Microsoft.AspNetCore.Mvc;
using LinqAssignmentCore.Data;
using LinqAssignmentCore.Models.ViewModels;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.X86;
using System.Linq;


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
            var Top10EfficientCars = _db.Cars.OrderByDescending(c => c.Combined).ThenByDescending(c => c.Name).Take(10).ToList();
            return View(Top10EfficientCars);
        }

        // Most fuel efficient car - query syntax
        public ActionResult MostEfficientQ()
        {
            var QTop10EfficientCars = (from c in _db.Cars
                              select c).OrderByDescending(c => c.Combined).ThenByDescending(c =>c.Name).Take(10).ToList();
            return View(QTop10EfficientCars);
        }

        // Filtering with Where and FirstOrDefault - (extension) method syntax
        public ActionResult WhereAndFirst()
        {
            if (_db.Cars.Any(c => c.Name == "BMW")) { 
            var TheMostFuleEfficientOfOneManufacture = _db.Cars.Where(c => c.Year == 2016 && c.Manufacturer == "BMW").OrderByDescending(c => c.Combined).FirstOrDefault();
                return View(TheMostFuleEfficientOfOneManufacture);
            }else
                return View(null);

        }

        // Filtering with Where and FirstOrDefault - query syntax
        public ActionResult WhereAndFirstQ()
        {
            if (_db.Cars.Any(c => c.Name == "BMW"))
            {
                var QTheMostFuleEfficientOfOneManufactur = (from c in _db.Cars
                                                            where c.Year == 2016 && c.Manufacturer == "BMW"
                                                            select c).OrderByDescending(c => c.Combined).FirstOrDefault();
                return View(QTheMostFuleEfficientOfOneManufactur);
            }
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
            var Top10EfficientCars = _db.Cars.OrderByDescending(c => c.Combined).ThenByDescending(c => c.Name).Take(10).ToList();
            var selectionTop10EfficientCars = Top10EfficientCars.Select( c => new ProjectedCarsVM { 
                                                                                        Combined = c.Combined,
                                                                                        Manufacturer = c.Manufacturer, 
                                                                                        Name = c.Name});
            return View(selectionTop10EfficientCars);

        }

        // Where condition with projected 'Select' - new objects with fewer properties
        // Query syntax
        public ActionResult ProjectedSelectQ()
        {
            var QTop10EfficientCars = (from c in _db.Cars
                                       select c).OrderByDescending(c => c.Combined).ThenByDescending(c => c.Name).Take(10).ToList();
            var QselectionTop10EfficientCars = from c in QTop10EfficientCars
                                               select new ProjectedCarsVM()
                                               {
                                                   Name = c.Name,
                                                   Combined = c.Combined,
                                                   Manufacturer = c.Manufacturer
                                               };

            return View(QselectionTop10EfficientCars);
        }

        // SelectMany in method syntax
        // Flattening of sequences in a sequence to a single collection
        // E.g. - producing a list of all characters in all car names
        // IEnumerable<char> characters is the same as a string
        public ActionResult SelectMany()
        {
            List<char> CarsNamsChars = _db.Cars.ToList().SelectMany(c => c.Name).Distinct().OrderBy(c => c).ToList();
            return View(CarsNamsChars);
        }

        // Join tables - query syntax
        public ActionResult JoinTablesQ()
        {
            
            var carsNamesAndManfucturers = (from c in _db.Cars
                        join m in _db.Manufacturers
                             on c.Manufacturer equals m.Name
                        select new JointTablesVM()
                        {
                            ModelName = c.Name,
                            Combined = c.Combined,
                            Manufacturer = c.Manufacturer,
                            HeadQuarters = m.Headquarters,
                        }).ToList();

            var top10OfpreviousList = carsNamesAndManfucturers.OrderByDescending(c => c.Combined).ThenByDescending(c => c.ModelName).Take(10).ToList();


            return View(top10OfpreviousList);
        }

        // Join tables - method syntax
        public ActionResult JoinTables()
        {
            var carsNamesAndManfucturers = _db.Cars.ToList().Join(_db.Manufacturers.ToList(),
                                                                                c => c.Manufacturer,
                                                                                m => m.Name, 
                                                                                 (c,m) => new JointTablesVM
                                                                                {
                                                                                    Combined = c.Combined,
                                                                                    Manufacturer = c.Manufacturer,
                                                                                    ModelName = c.Name,
                                                                                    HeadQuarters = m.Headquarters
                                                                                });
            var top10OfpreviousList = carsNamesAndManfucturers.OrderByDescending(c => c.Combined).ThenByDescending(c => c.ModelName).Take(10).ToList();

            return View(top10OfpreviousList);
        }

        // Group by - query syntax
        public ActionResult GroupingQ()
        {
            List<Car> initialCarlList = new List<Car>();
            List<Car> finalCarsList = new List<Car>();
            List<string> keysList = new List<string>();
            var carSGroupedBy = (from c in _db.Cars
                                 select c)
                                 .ToList()
                                 .GroupBy(c => c.Manufacturer);
            foreach (var oneGroup in carSGroupedBy)
            {
                keysList.Add(oneGroup.Key);
                foreach (Car c in oneGroup)
                {
                    initialCarlList.Add(c);
                }

                finalCarsList = (from car in initialCarlList
                                 select car)
                                 .OrderBy(c => c.Manufacturer)
                                .ThenByDescending(c => c.Combined)
                                .Take(2)
                                .ToList();
            }

            var result = from c in finalCarsList
                         from key in keysList
                         select new GroupingVM()
                            {
                            Manufacturer = key,
                            Cars = finalCarsList,
                            };
            return View(result);
        }

        // Group by - method syntax
        public ActionResult Grouping()
        {
            List<Car> initiaCarlList = new List<Car>();
            List<Car> finalCarsList = new List<Car>();
            List<string> keysList = new List<string>();
            var carSGroupedBy = _db.Cars.ToList().GroupBy(c => c.Manufacturer);
            foreach (var oneGroup in carSGroupedBy)
            {
                keysList.Add(oneGroup.Key);
                foreach (Car c in oneGroup)
                {
                    initiaCarlList.Add(c);
                }

                finalCarsList = initiaCarlList
                                .OrderBy(c => c.Manufacturer)
                                .ThenByDescending(c => c.Combined)
                                .Take(2)
                                .ToList();
            }

            var finalResult = keysList
                .Select(k => new GroupingVM 
                {
                    Manufacturer = k, 
                    Cars = finalCarsList 
                });
            
            return View(finalResult);
        }

        // combined group and join (get 2 properties from manufacturer) - query syntax
        public ActionResult GroupJoinQ()
        {
            var GroupJoinQS = (from m in _db.Manufacturers.ToList()
                               join c in _db.Cars.ToList()
                               on m.Name equals c.Manufacturer
                               into carsGruop
                               select new GroupJoinVM
                               { CarManufacturer = m,
                                   CarBrands = (from c in carsGruop.ToList()
                                                select c)
                                                .OrderByDescending(c => c.Combined)
                                                .Take(5)
                                                .ToList()

                               }).ToList();


            return View(GroupJoinQS);
        }

        // Combined group and join (get 2 properties from manufacturer) - method syntax
        // Equality of keys
        public ActionResult GroupJoin()
        {
            var result = _db.Manufacturers.ToList().GroupJoin(_db.Cars.ToList(),
                         m => m.Name,
                         c => c.Manufacturer,
                         (m, c) => new GroupJoinVM
                             {
                                 CarManufacturer = m,
                                 CarBrands = _db.Cars.ToList().OrderByDescending(c => c.Combined).Take(5).ToList()
                             }).ToList();
            return View(result);
        }

        // Top 3 fuel efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - query syntax
        public ActionResult JoinAndGroupAndSelectManyQ()
        {
            var groupJoinList = (from m in _db.Manufacturers.ToList()
                               join c in _db.Cars.ToList()
                               on m.Name equals c.Manufacturer
                               into carsGruop
                               select new GroupJoinSelectManyVM 
                               {
                                 Country = m.Headquarters,
                                 CarBrands = carsGruop.ToList()
                               }).ToList();

            var top3EfficientCarsByCountries = (from m in _db.Manufacturers.ToList()
                                               join c in groupJoinList.ToList()
                                               on m.Headquarters equals c.Country
                                               into TopCountryEfflist
                                               select new GroupJoinSelectManyVM
                                               {
                                                   Country = m.Headquarters,
                                                   CarBrands = TopCountryEfflist.SelectMany(c => c.CarBrands)
                                                   .OrderByDescending(c => c.Combined).Distinct().Take(3).ToList()
                                               }).Distinct().ToList();

            return View(top3EfficientCarsByCountries);
        }

        // Top 3 fuel least efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - method syntax
        public ActionResult JoinAndGroupAndSelectMany()
        {
            var groupJoinList = _db.Manufacturers.ToList().GroupJoin(_db.Cars.ToList(),
                                 m => m.Name,
                                 c => c.Manufacturer,
                                 (m,c) => new GroupJoinSelectManyVM
                                 {
                                     Country = m.Headquarters,
                                     CarBrands = c.ToList()
                                 }).ToList();

           

            var top3EfficientCarsByCountries = _db.Manufacturers.ToList().GroupJoin(groupJoinList.ToList(),
                                                   m => m.Headquarters,
                                                   g => g.Country,
                                                   (m,g) => new  GroupJoinSelectManyVM
                                                {
                                                    Country = m.Headquarters,
                                                    CarBrands = g.SelectMany(c => c.CarBrands)
                                                    .OrderByDescending(c => c.Combined).Take(3).ToList()
                                                }).Distinct().ToList();

            return View(top3EfficientCarsByCountries.Distinct().ToList());
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
            List<Car> carsList = _db.Cars.ToList();
            List<Manufacturer> manufacturers = _db.Manufacturers.ToList();

            IEnumerable<AggregatingVM> aggregateData = carsList.GroupBy(c => c.Manufacturer)
                .Select(g =>
                {
                    var result = g.Aggregate(new CarStatistics(),
                        (acc, c) => acc.Accumulate(c),
                        acc => acc.Compute());
                    return new AggregatingVM()
                    {
                        Name = g.Key,
                        Average = result.Average,
                        Max = result.Max,
                        Min = result.Min,
                    };
                })
                .OrderByDescending(r => r.Max);
            return View(aggregateData);
        }



        public ActionResult GetEngineInfo()
        {

            List<Engine> EnginesList = new List<Engine>
            {
                new Engine{MadeOf = "Aleminum",  FuelEfficiency = 33,  WeightKG = 350, Manufacutre  = "Audi", Cars = null},
                new Engine{MadeOf = "Iron",  FuelEfficiency = 44,  WeightKG = 550, Manufacutre  = "Toyota", Cars = null},
                new Engine{MadeOf = "AleminumIron",  FuelEfficiency = 35,  WeightKG = 450, Manufacutre  = "Mercides", Cars = null},
                new Engine{MadeOf = "MixedMetal",  FuelEfficiency = 23,  WeightKG = 450, Manufacutre  = "BMW", Cars = null},
                new Engine{MadeOf = "Aleminum",  FuelEfficiency = 23,  WeightKG = 350, Manufacutre  = "Vlovo", Cars = null},
            };

            _db.Engines.AddRange(EnginesList);
            _db.SaveChanges();

            return RedirectToAction("Queries");
        }
    }
}

