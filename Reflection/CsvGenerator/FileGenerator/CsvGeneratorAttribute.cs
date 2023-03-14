namespace FileGenerator;

[AttributeUsage(AttributeTargets.Property)]
public class CsvGeneratorAttribute : Attribute
{
    public string Heading { get; set; }

    public string Format { get; set; }
}
