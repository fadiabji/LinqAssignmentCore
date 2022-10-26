namespace LinqAssignmentCore.Models.Db
{
    public class Engine
    {
        public int Id { get; set; }
        public string MadeOf { get; set; }
        public int FuelEfficiency { get; set; }

        public int WeightKG { get; set; }

        public string Manufacutre { get; set; }

        public virtual ICollection<Car> Cars { get; set; }


        public Engine(string madeOf, int fuelEfficiency, int weightKG, string manufacutre, ICollection<Car> cars)
        {
            MadeOf = madeOf;
            FuelEfficiency = fuelEfficiency;
            WeightKG = weightKG;
            Manufacutre = manufacutre;
            Cars = cars;
        }

        public Engine()
        {

        }
    }
}
