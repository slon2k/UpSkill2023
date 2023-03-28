using System.Collections.Concurrent;
using System.Diagnostics;


//var task = Task.Run(() => Parallel.Invoke(
//    () => Console.WriteLine("1"),
//    () => Console.WriteLine("2"),
//    () => Console.WriteLine("3")
//));


//await task;


// ForEach Loop
var primesList = new List<long>();

Stopwatch stopwatch = Stopwatch.StartNew();

foreach (var num in Range(1, 10_000_000))
{
	if (IsPrime(num))
	{
		primesList.Add(num);
	}
}

stopwatch.Stop();

Console.WriteLine($"Sequencial: {stopwatch.ElapsedMilliseconds}");


// Parallel ForEach Loop
var primesBag = new ConcurrentBag<long>();

stopwatch.Restart();

Parallel.ForEach(Range(1, 10_000_000), i =>
{
    if (IsPrime(i))
	{
        primesBag.Add(i);
    }
});

stopwatch.Stop();

Console.WriteLine($"Parallel ForEach: {stopwatch.ElapsedMilliseconds}");

// Parallel For Loop
primesBag.Clear();

stopwatch.Restart();

Parallel.For(1, 10_000_000, i => 
{
    if (IsPrime(i))
	{
        primesBag.Add(i);
    }
});

stopwatch.Stop();
Console.WriteLine($"Parallel For: {stopwatch.ElapsedMilliseconds}");


Console.WriteLine("Press a key to quit");
Console.ReadKey();

static IEnumerable<long> Range(long from, long to)
{
	while (from++ < to)
	{
		yield return from;
	}
}

static bool IsPrime(long number)
{
	if (number == 1)
	{
		return false;
	}

	long i = 2;

	while (i * i <= number) 
	{
		if (number % i == 0)
		{
			return false;
		}

		i++;
	}

	return true;
}