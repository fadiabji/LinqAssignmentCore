namespace LinqAssignmentCore.Models.Db
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Displacement { get; set; }
        public int Cylinders { get; set; }

        //Fuel efficency for city driving
        public int City { get; set; }

        //Fuel efficency for highway driving
        public int Highway { get; set; }

        //Fuel efficency combined city - highway
        public int Combined { get; set; }

        public static List<Car> ProcessCars(string path)
        {
            List<Car> carList =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar()
                    .ToList();
            return carList;
        }
    }
}
