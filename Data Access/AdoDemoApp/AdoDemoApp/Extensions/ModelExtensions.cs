using AdoDemoApp.Models;
namespace AdoDemoApp.Extensions;

internal static class ModelExtensions
{
    internal static void Print(this IEnumerable<HouseModel> houses)
    {
        foreach (var house in houses)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine($"{house.Name}");
            Console.WriteLine("-----------------");
            foreach (var student in house.Students)
            {
                Console.WriteLine(student.Name);
            }
            Console.WriteLine("-----------------");
        }
    }
}
