using System.Reflection;

namespace FileGenerator;

public class CsvGenerator
{
    private readonly char separator;

    public CsvGenerator(char separator = ',')
    {
        this.separator = separator;
    }

    public void GenerateFile<TSource>(IEnumerable<TSource> source, string fileName)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        ArgumentNullException.ThrowIfNull(source);

        using var outputStream = new FileStream(fileName, FileMode.Create);
        using var streamWriter = new StreamWriter(outputStream);

        var header = CreateHeader(typeof(TSource));
        Console.WriteLine(header);
        streamWriter.WriteLine(header);


        foreach (var item in source)
        {
            var row = CreateRow(item);
            Console.WriteLine(row);
            streamWriter.WriteLine(row);
        }
        
    }

    private string CreateRow<TSource>(TSource item)
    {
        var type = typeof(TSource);

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return string.Join(separator, props.Select(p => CreateItem(p, item)));
    }

    private string CreateHeader(Type type)
    {
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return string.Join(separator, props.Select(CreateHeaderItem));
    }

    private string CreateHeaderItem(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<CsvGeneratorAttribute>();

        return attribute?.Heading ?? propertyInfo.Name;
    }

    private static string CreateItem<TSource>(PropertyInfo propertyInfo, TSource item)
    {
        var attribute = propertyInfo.GetCustomAttribute<CsvGeneratorAttribute>();
        
        var format = attribute?.Format ?? string.Empty;

        string template = "{0:" + format + "}";

        return string.Format(template, propertyInfo.GetValue(item));

    }
}
