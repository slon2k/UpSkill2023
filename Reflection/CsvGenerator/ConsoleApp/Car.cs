namespace ConsoleApp;

internal class Car
{
    public int Year { get; set; }

    public string Manufacturer { get; set; } = "";

    public string Name { get; set; } = "";

    public double Displacement { get; set; }

    public int Cylinders { get; set; }

    public int City { get; set; }

    public int Highway { get; set; }

    public int Combined { get; set; }

    public Car(int year, string manufacturer, string name, double displacement, int cylinders, int city, int highway, int combined)
    {
        Year = year;
        Manufacturer = manufacturer;
        Name = name;
        Displacement = displacement;
        Cylinders = cylinders;
        City = city;
        Highway = highway;
        Combined = combined;
    }

    public static IEnumerable<Car> GenerateSampleData() => new List<Car>
    {
        new Car(2016, "HYUNDAI MOTOR COMPANY", "Elantra", 2.0, 4, 24, 35, 28),
        new Car(2016, "Mini", "Mini Cooper Clubman", 1.5, 4, 25, 35, 28),
        new Car(2016, "Volvo", "V60 AWD", 3.0, 6, 18, 27, 21)
    };
}
