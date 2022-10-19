namespace LinqAssignmentCore.Models.Db
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Headquarters { get; set; }

        public int Year { get; set; }


        public static List<Manufacturer> ProcessManufacturers(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToManufacturer()
                    .ToList();
        }

        public static List<Manufacturer> ProcessManufacturersOld(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    }).ToList();
        }
    }
}
